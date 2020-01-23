using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class AiSight : MonoBehaviour
    {
        public float fieldOfView = 110f;
        List<Actor> nearbyActors = new List<Actor>();
        public Actor sightedActor { get; private set; }

        public bool ActorInSight(Actor actor)
        {
            Vector3 targetDir = actor.transform.position - transform.position;
            float angleToActor = (Vector3.Angle(targetDir, transform.forward));

            if (angleToActor >= -90 && angleToActor <= 90) // 180° FOV
            {
                return true;
            }
            return false;
        }

        private void Start()
        {
            StartCoroutine(ActorNearby());
        }

        IEnumerator ActorNearby()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);
                Actor[] actors = FindObjectsOfType<Actor>();
                foreach(var actor in actors)
                {
                    if(Vector3.Distance(actor.transform.position, transform.position) < 25f)
                    {
                        nearbyActors.Add(actor);
                    }
                }
            }
        }

        private void Update()
        {
            foreach(var actor in nearbyActors)
            {
                if(ActorInSight(actor))
                {
                    sightedActor = actor;
                    break;
                }
            }
        }
    }
}
