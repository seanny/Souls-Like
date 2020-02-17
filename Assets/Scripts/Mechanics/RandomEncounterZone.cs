using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "Random Encounter", menuName = "Random Encounter")]
    public class RandomEncounter : ScriptableObject
    {
        public List<Actor> actors = new List<Actor>();
    }

    [Serializable]
    public struct RandomEncounterZoneData
    {
        public int zoneID;
        public float encounterSpawnTime;
    }

    public class RandomEncounterZone : MonoBehaviour
    {
        public RandomEncounter[] randomEncounters;
        public RandomEncounterZoneData randomEncounterZoneData;

        [Range(1f, 100f)]
        public float range = 1f;

        private void Start()
        {
            randomEncounterZoneData.zoneID = gameObject.GetInstanceID();
            // TODO: Find random encounter zone data from save file based on zoneID.
        }

        private void Update()
        {
            PickRandomEncounter();
        }

        void PickRandomEncounter()
        {
            if(randomEncounterZoneData.encounterSpawnTime >= 0)
            {
                return;
            }

            if (Vector3.Distance(transform.position, PlayerActor.instance.transform.position) <= range + 100f)
            {
                int rand = Random.Range(0, randomEncounters.Length);
                RandomEncounter randomEncounter = randomEncounters[rand];

                RaycastHit hit;

                foreach(var actor in randomEncounter.actors)
                {
                    Vector3 position = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
                    if (Physics.Raycast(position + new Vector3(0, range, 0), Vector3.down, out hit, 200.0f))
                    {
                        Instantiate(actor, hit.point, Quaternion.identity);
                    }
                    else
                    {
                        Debug.Log("there seems to be no ground at this position");
                    }
                }
                randomEncounterZoneData.encounterSpawnTime = 1f;
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 1, 1, 0.5f);
            Gizmos.DrawSphere(transform.position, range);
        }
    }
}