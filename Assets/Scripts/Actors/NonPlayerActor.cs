using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace SoulsLike
{
    [RequireComponent(typeof(StateManager))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NonPlayerActor : Actor
    {
        public enum AggressionLevel
        {
            // Does not attack, unless attacked.
            Neutral,

            // Attacks everyone, except allies.
            Agressive,

            // Attacks everyone, including allies.
            HatesEveryone
        };

        public const float MIN_FIGHT_DISTANCE = 1.5f;

        public AggressionLevel aggressionLevel;
        NavMeshAgent navMeshAgent;
        public Actor followingActor;
        public Actor movingTowards;
        public bool runningTowards { get; set; }
        public Actor fightingActor;
        public StateManager stateManager;
        float delta;
        bool canRun;
        public AiSequence aiSequence;
        public AiState[] aiStates;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            stateManager = GetComponent<StateManager>();
            if (!stateManager)
            {
                stateManager = gameObject.AddComponent<StateManager>();
            }
            stateManager.Init();
            navMeshAgent = GetComponent<NavMeshAgent>();
            actorStats.maxHealth = 80f + (actorStats.level / 2) + Random.Range(0, 10);
            actorStats.currentHealth = actorStats.maxHealth;
            aiSequence = new AiSequence(this);
            canRun = true;
        }

        public void InitiateCombat(Actor actor)
        {
            fightingActor = actor;
        }

        public override void Kill()
        {
            base.Kill();
            navMeshAgent.enabled = false;
        }

        private float MovementSpeed()
        {
            float movementSpeed = 0f;
            if(!navMeshAgent.isStopped || navMeshAgent.destination != Vector3.zero)
            {
                if(runningTowards)
                {
                    movementSpeed = 1.0f;
                }
                else
                {
                    movementSpeed = 0.5f;
                }
            }
            return movementSpeed;
        }

        /// <summary>
        /// Move NPC towards an area.
        /// </summary>
        /// <param name="destination"></param>
        public void MoveTowardsActor(Actor actor)
        {
            movingTowards = actor;
            MoveTowardsPoint(actor.transform.position);
        }

        public void MoveTowardsPoint(Vector3 point)
        {
            stateManager.moveAmount = MovementSpeed();
            navMeshAgent.destination = point;
            navMeshAgent.isStopped = false;
        }

        public void StopMovingTowardsActor(Actor actor)
        {
            movingTowards = null;
            navMeshAgent.isStopped = true;
        }

        public bool FollowActor(Actor actor)
        {
            if(actor.actorStats.isDead)
            {
                return false;
            }
            followingActor = actor;
            return true;
        }

        private AiState GetAiState()
        {
            AiWander aiWander = new AiWander(this);
            aiWander.targetMovementPoint = Actors.instance.FindRandomMovementTarget(this);

            Actor chaseActor = Actors.instance.FindTarget(this);

            if(chaseActor != null)
            {
                if(Vector3.Distance(transform.position, chaseActor.transform.position) > AiState.MIN_DISTANCE)
                {
                    AiPursue aiPursue = new AiPursue(this, chaseActor);
                    return aiPursue;
                }
                AiCombat aiCombat = new AiCombat(this, chaseActor);
                return aiCombat;
            }

            return aiWander;
        }

        // Update is called once per frame
        void Update()
        {
            if(actorStats.isDead == true || !canRun)
            {
                return;
            }

            float delta = Time.deltaTime;
            stateManager.Tick(delta);
            aiStates = aiSequence.aiStates.ToArray();

            if (aiSequence.StatesEmpty || aiSequence.lastAiState == StateTypeID.None)
            {
                aiSequence.AddState(GetAiState());
            }

            aiSequence.Execute(Time.deltaTime);

            /*if (aiPursue.chasingActor == null)
            {
                aiPursue.chasingActor = Actors.instance.FindTarget(this);
            }
            else
            {
                if (Vector3.Distance(aiPursue.chasingActor.transform.position, transform.position) <= AiState.MIN_DISTANCE)
                {
                    aiCombat.Execute(Time.deltaTime);
                }
                else
                {
                    aiPursue.Execute(Time.deltaTime);
                }
            }*/
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            stateManager.FixedTick(delta);
        }
    }
}