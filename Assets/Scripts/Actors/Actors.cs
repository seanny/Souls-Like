using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class Actors : MonoBehaviour
    {
        public GameObject actorPrefab;
        private SortedDictionary<string, int> deathCount;
        private List<Actor> actors;
        public float ActorProcessingRange { get; private set; }

        private void Start()
        {
            UpdateProcessingRange();
        }

        public void UpdateProcessingRange()
        {
            const float maxProcessingRange = 1000;
            const float minProcessingRange = maxProcessingRange / 2;

            float actorProcessingRange = 1000;
            actorProcessingRange = Mathf.Clamp(actorProcessingRange, minProcessingRange, maxProcessingRange);
            ActorProcessingRange = actorProcessingRange;
        }

        public bool IsActorDetected(Actor actor, Actor observer)
        {
            return observer.CanDetect(actor);
        }

        public void AddActor(Actor actor)
        {
            Actor newActor = actor;
            if(newActor.actorStats.name.Length < 1)
            {
                newActor.actorStats.name = "New Actor";
            }
            GameObject gameObject = new GameObject(newActor.actorStats.name);
            gameObject.AddComponent<Actor>();
            
        }

        public void RemoveActor(Actor actor)
        {

        }

        public void Resurrect(Actor actor)
        {

        }

        public bool IsAttacking(Actor actor)
        {
            return false;
        }

        public bool IsRunning(Actor actor)
        {
            return false;
        }

        public bool IsSneaking(Actor actor)
        {
            return false;
        }

        public void Clear()
        {

        }

        private void KillDeadActors()
        {

        }
    }
}
