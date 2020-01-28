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

        public override bool Execute(float delta)
        {
            base.Execute(delta);
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

                actor.SmoothLook(targetDir, lookSpeed);

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
