using System;
using UnityEngine;

namespace SoulsLike
{
    public class AiCombat : AiState
    {
        private const float MIN_FIGHT_DISTANCE = 2.0f;
        private const float COMBAT_WAIT_TIME = 1.5f;

        public float actionCooldown;
        Actor target;
        float combatDistance;

        public AiCombat(NonPlayerActor actor, Actor target)
        {
            stateType = StateTypeID.Combat;
            canRepeat = true;
            this.actor = actor;
            this.target = target;
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public override bool Execute(float delta)
        {
            base.Execute(delta);
            if (actor.actorStats.isDead)
            {
                canRepeat = false;
                return true;
            }

            combatDistance = Vector3.Distance(target.transform.position, actor.transform.position);
            if(target == null || combatDistance > MIN_DISTANCE)
            {
                canRepeat = false;
                return false;
            }

            if(target == actor)
            {
                canRepeat = false;
                return true;
            }

            if(target.actorStats.isDead)
            {
                canRepeat = false;
                return true;
            }

            if (Attack(delta))
            {
                canRepeat = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="storage"></param>
        /// <returns>True if combat should end, else false.</returns>
        private bool Attack(float duration)
        {
            if (target.actorStats.isDead)
            {
                return true;
            }

            Vector3 targetPos = target.transform.position;
            float distance = Vector3.Distance(targetPos, actor.transform.position);
            targetPosition = targetPos;
            if (distance < MIN_FIGHT_DISTANCE)
            {
                actor.actorStats.actorFightWait += duration;
                if (actor.actorStats.actorFightWait > COMBAT_WAIT_TIME)
                {
                    Debug.Log($"{actor} is attacking {target}");
                    actor.actorStats.actorFightWait = 0f;
                    actor.transform.LookAt(target.transform.position);
                    // Start the attack animation, and also enable the weapon attack scripts.
                    actor.Attack();
                    //actor.OnCombat(target);
                    //target.attackedBy = actor;
                }
                return false;
            }
            else
            {
                // Go towards actor, until we are within MIN_FIGHT_DISTANCE.
                Debug.Log($"[AiCombat|{actor}] Moving towards {target}");
                actor.MoveTowardsActor(target);
            }
            return false;
        }
    }
}
