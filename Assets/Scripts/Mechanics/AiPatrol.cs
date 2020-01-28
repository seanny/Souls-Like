using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    class AiPatrol : AiState
    {
        List<MovementPoint> patrolRoute;
        Queue<MovementPoint> patrolPath;
        MovementPoint targetMovementPoint;
        private int lookSpeed;

        public AiPatrol(NonPlayerActor actor, List<MovementPoint> patrolPoints)
        {
            canRepeat = true;
            patrolRoute = patrolPoints;
            CreatePatrolPath();
        }

        private Queue<MovementPoint> CreatePatrolPath()
        {
            return patrolPath = new Queue<MovementPoint>(patrolRoute);
        }

        public override bool Execute(float delta)
        {
            base.Execute(delta);
            if (actor.actorStats.isDead)
            {
                return false;
            }

            lookSpeed = Random.Range(2, 4);

            if(patrolPath.Count < 1)
            {
                patrolPath = CreatePatrolPath();
            }
            if(targetMovementPoint == null)
            {
                targetMovementPoint = patrolPath.Dequeue();
            }

            Vector3 actorPos = actor.transform.position;
            Vector3 targetPos = targetMovementPoint.transform.position;
            Vector3 targetDir = targetPos - actorPos;
            targetPosition = targetPos;

            if (Vector3.Distance(targetPos, actorPos) <= MIN_DISTANCE)
            {
                targetMovementPoint = patrolPath.Dequeue();
            }
            else
            {
                actor.SmoothLook(targetDir, lookSpeed);

                // If actor's distance is more than MIN_DISTANCE then walk towards the target
                actor.MoveTowardsPoint(targetPos);
                actor.runningTowards = false;
                return false;
            }
            return true;
        }
    }
}
