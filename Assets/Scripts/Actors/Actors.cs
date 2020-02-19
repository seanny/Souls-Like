using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class Actors : MonoBehaviour
    {
        public static Actors instance { get; private set; }

        public List<GameObject> actorPrefabs = new List<GameObject>();
        public RuntimeAnimatorController runtimeAnimatorController;
        public GameObject actorBasePrefab;

        private SortedDictionary<string, int> deathCount;
        private List<Actor> actors = new List<Actor>();

        const float maxProcessingRange = 1000;
        const float minProcessingRange = maxProcessingRange / 2;

        [Range(minProcessingRange, maxProcessingRange)]
        public float ActorProcessingRange;

        private void Start()
        {
            instance = this;

            ActorStats actorStats = new ActorStats();
            actorStats.name = "Test Actor";
            Vector3 pos = new Vector3(0, 1, 0);

            AddActor(pos, actorStats);
            UpdateProcessingRange();
            StartCoroutine(RemoveActorTimer());
        }

        /// <summary>
        /// Set the actor stats for a number of actors (based on array)
        /// </summary>
        /// <param name="actors"></param>
        /// <param name="actorStats"></param>
        public void SetActorStats(Actor[] actors, ActorStats[] actorStats)
        {
            foreach(var _actor in actors)
            {
                SetActorStat(actorStats, _actor);
            }
        }

        private void SetActorStat(ActorStats[] actorStats, Actor _actor)
        {
            foreach (var _actorStat in actorStats)
            {
                if (_actor.actorStats.actorID == _actorStat.actorID)
                {
                    _actor.actorStats = _actorStat;
                    break;
                }
            }
        }

        public void UpdateProcessingRange()
        {
            // TODO: Get processing range from settings file.
            float actorProcessingRange = 1000;
            actorProcessingRange = Mathf.Clamp(actorProcessingRange, minProcessingRange, maxProcessingRange);
            ActorProcessingRange = actorProcessingRange;
        }

        public bool IsActorDetected(Actor actor, Actor observer)
        {
            return observer.CanDetect(actor);
        }

        public Actor AddActor(Vector3 position, ActorStats actorStats)
        {
            if(actorStats.name.Length < 1)
            {
                actorStats.name = "New Actor";
            }

            GameObject actorObject;
            switch (actorStats.actorSpecies)
            {
                case ActorSpeciesType.HumanFemale:
                    actorObject = Instantiate(actorPrefabs[0]);
                    break;
            }
            return null;
        }

        public List<Actor> GetAllNearbyActors()
        {
            List<Actor> _actors = new List<Actor>();
            foreach (Actor actor in actors)
            {
                if (IsInProcessingRange(actor))
                {
                    _actors.Add(actor);
                }
            }
            return _actors;
        }

        public void RemoveActor(Actor actor)
        {
            actors.Remove(actor);
            Destroy(actor.gameObject);
        }

        public bool IsInFaction(Actor actor, Faction faction)
        {
            if(actor.actorStats.actorFaction == faction.name)
            {
                return true;
            }
            return false;
        }

        public bool IsInFaction(Actor actor, string name)
        {
            if (actor.actorStats.actorFaction == name)
            {
                return true;
            }
            return false;
        }

        public bool IsInEnemyFaction(Actor actor, Actor target)
        {
            Faction faction = Faction.GetFactionByName(actor.actorStats.actorFaction);
            if(faction == null)
            {
                Debug.LogError($"Error in IsInEnemyFaction({actor}, {target}): faction is null.");
                return false;
            }
            foreach (var enemyFac in faction.enemies)
            {
                if (target.actorStats.actorFaction == enemyFac.name)
                {
                    return true;
                }
            }
            return false;
        }

        public Actor FindTarget(NonPlayerActor actor, float range = 25f)
        {
            if (actor.aggressionLevel >= NonPlayerActor.AggressionLevel.Agressive)
            {
                Actor[] actors = FindObjectsOfType<Actor>();
                Actor _actor = actor.FindNearestActor(actors, range);
                if (_actor != null)
                {
                    if (actor.CanDetect(_actor) == true && !_actor.actorStats.isDead)
                    {
                        if (actor.aggressionLevel >= NonPlayerActor.AggressionLevel.HatesEveryone)
                        {
                            return _actor;
                        }

                        // Check if actor is in an enemy faction
                        if(IsInEnemyFaction(actor, _actor) == true)
                        {
                            return _actor;
                        }
                    }
                }
            }
            return null;
        }

        public MovementPoint FindRandomMovementTarget(NonPlayerActor actor, float range = 25f)
        {
            MovementPoint[] movementPoints = FindObjectsOfType<MovementPoint>();
            List<MovementPoint> validMovementPoints = new List<MovementPoint>();
            foreach(var movementPoint in movementPoints)
            {
                if (Vector3.Distance(movementPoint.transform.position, actor.transform.position) <= range)
                {
                    validMovementPoints.Add(movementPoint);
                }
            }
            if(validMovementPoints.Count < 1)
            {
                return null;
            }
            int rand = Random.Range(0, validMovementPoints.Count);
            return validMovementPoints[rand];
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
            foreach(Actor actor in actors)
            {
                if(actor.actorStats.isDead)
                {
                    RemoveActor(actor);
                }
            }
        }

        private IEnumerator RemoveActorTimer()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);
                KillDeadActors();
            }
        }

        public bool IsInProcessingRange(Actor actor)
        {
            if(actor == PlayerActor.instance)
            {
                return true;
            }
            float distance = Vector3.Distance(actor.transform.position, PlayerActor.instance.transform.position);
            if (distance <= ActorProcessingRange)
            {
                return true;
            }
            return false;
        }
    }
}
