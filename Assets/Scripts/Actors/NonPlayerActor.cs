using UnityEngine;
using UnityEngine.AI;

namespace SoulsLike
{
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

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            stateManager = GetComponent<StateManager>();
            stateManager.Init();
            actorStats.maxHealth = 80f + (actorStats.level / 2) + Random.Range(0, 10);
            actorStats.currentHealth = actorStats.maxHealth;
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
            if(navMeshEnabled < 1.0f)
            {
                navMeshEnabled += delta;
                if(navMeshEnabled >= 1.0f)
                {
                    navMeshAgent = GetComponent<NavMeshAgent>();
                }
            }

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

            // Engage in combat if we are fighting an actor.
            if(fightingActor != null)
            {
                float distance = Vector3.Distance(fightingActor.transform.position, transform.position);
                // do something
                if (distance < MIN_FIGHT_DISTANCE)
                {
                    fightWait += delta;
                    if (fightWait > COMBAT_WAIT_TIME)
                    {
                        fightWait = 0f;
                        transform.LookAt(fightingActor.transform.position);
                        // Start the attack animation, and also enable the weapon attack scripts.
                        animationHelper.PlayWeaponAnim(Helper.WeaponType.OneHanded);
                        navMeshAgent.isStopped = true;
                        fightingActor.OnActorAttacked(this, 1.5f);

                        if(fightingActor.actorStats.isDead == true)
                        {
                            fightingActor = null;
                            movingTowards = null;
                        }
                    }
                    return;
                }
                else
                {
                    if (fightingActor.actorStats.isDead == true)
                    {
                        fightingActor = null;
                        movingTowards = null;
                        return;
                    }

                    // Go towards actor, until we are within MIN_FIGHT_DISTANCE.
                    MoveTowardsActor(fightingActor);
                }
            }

            // Move Actor to the current target, being either the enemy they are attacking, their movement target or to a random nearby movment point.
            if(aggressionLevel >= AggressionLevel.Agressive)
            {
                if (aggressionLevel >= AggressionLevel.HatesEveryone)
                {
                    // Find nearest Actor.
                    Actor[] actors = FindObjectsOfType<Actor>();
                    Actor actor = FindNearestActor(actors, 25f);
                    if(actor != null)
                    {
                        if (CanDetect(actor) == true && movingTowards != actor)
                        {
                            InitiateCombat(actor);
                        }
                    }
                }
                else
                {
                    
                }
            }
        }

        private bool CanDetect(Actor actor)
        {
            ActorStats actorStats = actor.actorStats;
            if (actorStats.isDead == true)
            {
                return false;
            }

            if (actorStats.isSneaking == true && attackedBy != actor)
            {
                // sneakChance: (((0.5 + Distance)/500) + (Fatigue/2) + (Luck/10)) * (Sneak/5)
                // spotChance: (((0.5 + Distance)/500) + (Fatigue/2) + (Luck*1.1))
                // detectionChance = sneakChance > spotChance
                float sneakChance = ((0.5f + (Vector3.Distance(transform.position, actor.transform.position) / 500) + (actorStats.fatigue / 2) + ((float)actorStats.luck / 10)) * ((float)actorStats.sneak / 5));
                float spotChance = (((0.5f + Vector3.Distance(transform.position, actor.transform.position)) / 500) + ((float)this.actorStats.fatigue / 2) + ((float)this.actorStats.luck * 1.1f));
                if(sneakChance > spotChance)
                {
                    return false;
                }
            }
            return true;
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            stateManager.FixedTick(delta);
        }
    }
}