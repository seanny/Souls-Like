using UnityEngine;

namespace SoulsLike
{
    /// <summary>
    /// AI Follow Class.
    /// AI should follow the specified
    /// </summary>
    public class AiFollow : AiState
    {
        public Actor followingActor;
        private const float FOLLOW_DISTANCE = 2.5f;
        private int lookSpeed = 5;
        public float MinDistance { get; set; }

        public AiFollow(NonPlayerActor actor, Actor followingActor)
        {
            MinDistance = FOLLOW_DISTANCE;
            stateType = StateTypeID.Follow;
            this.followingActor = followingActor;
            this.actor = actor;
        }

        public AiFollow(NonPlayerActor actor, Actor followingActor, float minDistance) : this(actor, followingActor)
        {
            MinDistance = minDistance;
        }

        public override bool Execute(float delta)
        {
            base.Execute(delta);
            if (actor.actorStats.isDead)
            {
                return false;
            }

            if(followingActor.actorStats.isDead)
            {
                return false;
            }

            lookSpeed = Random.Range(4, 6);

            Vector3 actorPos = actor.transform.position;
            Vector3 targetPos = followingActor.transform.position;
            Vector3 targetDir = targetPos - actorPos;
            targetPosition = targetPos;

            float distance = Vector3.Distance(targetPos, actorPos);
            if (distance > MinDistance)
            {
                actor.SmoothLook(targetDir, lookSpeed);

                // If actor's distance is more than FOLLOW_DISTANCE then move towards the target
                Debug.Log($"[AiFollow|{actor}] Following {followingActor}");
                actor.MoveTowardsActor(followingActor);


                if (distance > 14.5f)
                {
                    // Run towards target
                    actor.runningTowards = true;
                }
                else
                {
                    // Walk towards target
                    actor.runningTowards = false;
                }
            }
            else
            {
                // Ensure that the actor does not clip (get too close) with the target.
                actor.StopMovingTowardsActor(followingActor);
            }
            return true;
        }
    }
}
