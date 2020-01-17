using UnityEngine;

namespace SoulsLike
{
    public class AiWander : AiState
    {
        public MovementPoint targetMovementPoint;
        private int lookSpeed;

        public AiWander(NonPlayerActor actor)
        {
            stateType = StateTypeID.Wander;
            this.actor = actor;
        }

        public override bool Execute(float delta)
        {
            if (actor.actorStats.isDead)
            {
                return false;
            }

            lookSpeed = Random.Range(2, 4);

            Vector3 actorPos = actor.transform.position;
            Vector3 targetPos = targetMovementPoint.transform.position;
            Vector3 targetDir = targetPos - actorPos;
            targetPosition = targetPos;

            if (Vector3.Distance(targetPos, actorPos) > MIN_DISTANCE)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                actor.transform.rotation = Quaternion.Slerp(actor.transform.rotation, targetRotation, lookSpeed * delta);

                // If actor's distance is more than MIN_DISTANCE then walk towards the target
                actor.MoveTowardsPoint(targetPos);
                actor.runningTowards = false;
                return false;
            }
            return true;
        }
    }
}
