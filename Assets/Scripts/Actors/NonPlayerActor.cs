using UnityEngine;
using UnityEngine.AI;

namespace SoulsLike
{
    [RequireComponent(typeof(StateManager))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AiCombat))]
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
        public const float COMBAT_WAIT_TIME = 1.5f;

        public AggressionLevel aggressionLevel;
        NavMeshAgent navMeshAgent;
        public Actor movingTowards;
        public Actor fightingActor;
        StateManager stateManager;
        float fightWait;
        float navMeshEnabled = 0;
        float delta;
        AiCombat aiCombat;

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            stateManager = GetComponent<StateManager>();
            stateManager.Init();
            navMeshAgent = GetComponent<NavMeshAgent>();
            actorStats.maxHealth = 80f + (actorStats.level / 2) + Random.Range(0, 10);
            actorStats.currentHealth = actorStats.maxHealth;
            aiCombat = GetComponent<AiCombat>();
        }

        public void InitiateCombat(Actor actor)
        {
            fightingActor = actor;
        }

        public void Kill()
        {
            actorStats.currentHealth = 0f;
            actorStats.isDead = true;
        }

        /// <summary>
        /// Move NPC towards an area.
        /// </summary>
        /// <param name="destination"></param>
        public void MoveTowardsActor(Actor actor)
        {
            movingTowards = actor;
            navMeshAgent.destination = actor.transform.position;
            navMeshAgent.isStopped = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(actorStats.isDead == true)
            {
                return;
            }

            float delta = Time.deltaTime;
            stateManager.Tick(delta);
            /*if(navMeshEnabled < 1.0f)
            {
                navMeshEnabled += delta;
                if(navMeshEnabled >= 1.0f)
                {
                    navMeshAgent = GetComponent<NavMeshAgent>();
                }
            }*/

            if (movingTowards != null)
            {
                if (navMeshAgent.remainingDistance > MIN_FIGHT_DISTANCE)
                {
                    if (movingTowards == fightingActor)
                    {
                        stateManager.moveAmount = 1f;
                    }
                    else
                    {
                        stateManager.moveAmount = 0.5f;
                    }
                    MoveTowardsActor(movingTowards);
                    return;
                }
                else stateManager.moveAmount = 0;
            }

            aiCombat.Execute(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            stateManager.FixedTick(delta);
        }
    }
}