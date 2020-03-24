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

        /// <summary>
        /// Set the actor stats for a single actor.
        /// </summary>
        /// <param name="actorStats"></param>
        /// <param name="_actor"></param>
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

        /// <summary>
        /// Update processing range from the settings file.
        /// Note: Getting processing range from file is not yet implemented.
        /// </summary>
        public void UpdateProcessingRange()
        {
            // TODO: Get processing range from settings file.
            float actorProcessingRange = 1000;
            actorProcessingRange = Mathf.Clamp(actorProcessingRange, minProcessingRange, maxProcessingRange);
            ActorProcessingRange = actorProcessingRange;
        }

        /// <summary>
        /// Is Actor Detected?
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="observer"></param>
        /// <returns></returns>
        public bool IsActorDetected(Actor actor, Actor observer)
        {
            return observer.CanDetect(actor);
        }

        /// <summary>
        /// Add Actor to Scene
        /// </summary>
        /// <param name="position"></param>
        /// <param name="actorStats"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all nearby actors.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Remove Actor from scene
        /// </summary>
        /// <param name="actor"></param>
        public void RemoveActor(Actor actor)
        {
            actors.Remove(actor);
            Destroy(actor.gameObject);
        }

        /// <summary>
        /// Is Actor in faction?
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="faction"></param>
        /// <returns></returns>
        public bool IsInFaction(Actor actor, Faction faction)
        {
            if(actor.actorStats.actorFaction == faction.name)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is Actor in faction?
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsInFaction(Actor actor, string name)
        {
            if (actor.actorStats.actorFaction == name)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Is target in actor enemy faction?
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="target"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find an enemy within the specified range.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public Actor FindTarget(NonPlayerActor actor, float range = 25f)
        {
            if (actor.NpcCombat.AggressionLevel >= AggressionLevel.Agressive)
            {
                Actor[] actors = FindObjectsOfType<Actor>();
                Actor _actor = actor.FindNearestActor(actors, range);
                if (_actor != null)
                {
                    if (actor.CanDetect(_actor) == true && !_actor.actorStats.isDead)
                    {
                        if (actor.NpcCombat.AggressionLevel >= AggressionLevel.HatesEveryone)
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

        /// <summary>
        /// Find random movement point
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="range"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Resurrect Actor
        /// Note: Not implemented.
        /// </summary>
        /// <param name="actor"></param>
        public void Resurrect(Actor actor)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Is Attacking?
        /// Note: Not implemented.
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool IsAttacking(Actor actor)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Is Running?
        /// Note: Not implemented.
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool IsRunning(Actor actor)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Is Sneaking?
        /// Note: Not implemented.
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool IsSneaking(Actor actor)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Clear all actors.
        /// Note: Not implemented.
        /// </summary>
        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Clear all dead actors.
        /// </summary>
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

        /// <summary>
        /// Is Actor inside processing range
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
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
