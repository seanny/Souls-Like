using UnityEngine;

namespace SoulsLike
{
    class AiPursue : AiState
    {
        public Actor chasingActor;
        private int lookSpeed = 5;

        public AiPursue(NonPlayerActor actor, Actor chasingActor)
        {
            stateType = StateTypeID.Pursue;
            this.chasingActor = chasingActor;
            this.actor = actor;
        }

        bool IsLookingAtObject(Transform looker, Vector3 targetPos, float FOVAngle)
        {

            Vector3 direction = targetPos - looker.position;
            float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float lookerAngle = looker.eulerAngles.z;
            float checkAngle = 0f;

            if (ang >= 0f)
                checkAngle = ang - lookerAngle - 90f;
            else if (ang < 0f)
                checkAngle = ang - lookerAngle + 270f;

            if (checkAngle < -180f)
                checkAngle = checkAngle + 360f;

            if (checkAngle <= FOVAngle * .5f && checkAngle >= -FOVAngle * .5f)
                return true;
            else
                return false;
        }

        public override bool Execute(float delta)
        {
            if (actor.actorStats.isDead)
            {
                return false;
            }

            if (chasingActor.actorStats.isDead)
            {
                return false;
            }

            Vector3 actorPos = actor.transform.position;
            Vector3 targetPos = chasingActor.transform.position;
            if(Vector3.Distance(targetPos, actorPos) > MIN_DISTANCE)
            {
                lookSpeed = Random.Range(2, 3);

                Vector3 targetDir = targetPos - actorPos;
                targetPosition = targetPos;

                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                actor.transform.rotation = Quaternion.Slerp(actor.transform.rotation, targetRotation, lookSpeed * delta);

                // If actor's distance is more than FOLLOW_DISTANCE then move towards the target
                actor.MoveTowardsActor(chasingActor);

                // Run towards target
                actor.runningTowards = true;
            }
            else
            {
                AiCombat combatState = new AiCombat(actor, chasingActor);
                actor.aiSequence.AddState(combatState);
            }
            return true;
        }
    }
}
