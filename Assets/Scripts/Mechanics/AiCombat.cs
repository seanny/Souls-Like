using System;
using UnityEngine;

namespace SoulsLike
{
    [Serializable]
    public struct AiCombatStorage
    {
        public float attackCooldown;
        public float timerReact;
        public float actionCooldown;
    }

    public class AiCombat : MonoBehaviour
    {
        private const float MIN_FIGHT_DISTANCE = 2.0f;
        private const float COMBAT_WAIT_TIME = 1.5f;

        public NonPlayerActor actor { get; private set; }

        public float actionCooldown;

        private void Start()
        {
            actor = GetComponent<NonPlayerActor>();
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public bool Execute(float duration)
        {
            if(actor.actorStats.isDead)
            {
                return true;
            }

            Actor target = FindTarget();
            if(target == null)
            {
                return false;
            }

            if(target == actor)
            {
                return true;
            }

            if(target.actorStats.isDead)
            {
                return true;
            }

            // TODO: Check if the target is fleeing.

            if(Attack(target, duration))
            {
                return true;
            }
            return false;
        }

        private Actor FindTarget()
        {
            if (actor.aggressionLevel >= NonPlayerActor.AggressionLevel.Agressive)
            {
                Actor[] actors = FindObjectsOfType<Actor>();
                Actor _actor = actor.FindNearestActor(actors, 25f);
                if (_actor != null)
                {
                    if (actor.CanDetect(_actor) == true)
                    {
                        if (actor.aggressionLevel >= NonPlayerActor.AggressionLevel.HatesEveryone)
                        {
                            return _actor;
                        }
                        // Check if actor is in an enemy faction
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="storage"></param>
        /// <returns>True if combat should end, else false.</returns>
        private bool Attack(Actor target, float duration)
        {
            if (target.actorStats.isDead)
            {
                return true;
            }
            float distance = Vector3.Distance(target.transform.position, actor.transform.position);
            if (distance < MIN_FIGHT_DISTANCE)
            {
                actionCooldown += duration;
                if (actionCooldown > COMBAT_WAIT_TIME)
                {
                    actionCooldown = 0f;
                    transform.LookAt(target.transform.position);
                    // Start the attack animation, and also enable the weapon attack scripts.
                    actor.FightingAnimation();  
                    actor.OnCombat(target);
                }
                return false;
            }
            else
            {
                // Go towards actor, until we are within MIN_FIGHT_DISTANCE.
                actor.MoveTowardsActor(target);
            }
            return false;
        }
    }
}
