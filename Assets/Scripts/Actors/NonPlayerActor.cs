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
        public AiState[] aiStates;
        public Actor enemyActor;
        public bool isEnemy;

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
            canRun = true;
        }

        /// <summary>
        /// Smooth Look towards direction
        /// </summary>
        /// <param name="targetDir"></param>
        /// <param name="lookSpeed"></param>
        public void SmoothLook(Vector3 targetDir, int lookSpeed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * delta);
        }

        /// <summary>
        /// Initiate Combat with actor
        /// </summary>
        /// <param name="actor"></param>
        public void InitiateCombat(Actor actor)
        {
            fightingActor = actor;
        }

        /// <summary>
        /// Kill Actor
        /// </summary>
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

        /// <summary>
        /// Move towards vector 3
        /// </summary>
        /// <param name="point"></param>
        public void MoveTowardsPoint(Vector3 point)
        {
            stateManager.moveAmount = MovementSpeed();
            navMeshAgent.destination = point;
            navMeshAgent.isStopped = false;
        }

        /// <summary>
        /// Stop moving towards actor
        /// </summary>
        /// <param name="actor"></param>
        public void StopMovingTowardsActor(Actor actor)
        {
            movingTowards = null;
            navMeshAgent.isStopped = true;
        }

        /// <summary>
        /// Follow actor
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool FollowActor(Actor actor)
        {
            if(actor.actorStats.isDead)
            {
                return false;
            }
            followingActor = actor;
            return true;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if(actorStats.isDead == true || !canRun)
            {
                return;
            }

            float delta = Time.deltaTime;
            stateManager.Tick(delta);
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            stateManager.FixedTick(delta);
        }
    }
}