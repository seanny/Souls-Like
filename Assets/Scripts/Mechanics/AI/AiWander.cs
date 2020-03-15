using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulsLike
{
    public class AiWander : AiState
    {
        [SerializeField] private List<MovementPoint> m_MovementPoints = new List<MovementPoint>();
        public List<MovementPoint> MovementPoints => m_MovementPoints;
        [SerializeField] private MovementPoint targetMovementPoint = null;

        protected override void Start()
        {
            base.Start();
            InitMovementPoints();
        }

        /// <summary>
        /// Initialise Movement Points
        /// </summary>
        void InitMovementPoints()
        {
            MovementPoint[] movementPoints;
            movementPoints = GameObject.FindObjectsOfType<MovementPoint>();
            m_MovementPoints.Clear();
            foreach(var item in movementPoints)
            {
                if(Vector3.Distance(item.transform.position, actor.transform.position) <= 500)
                {
                    Debug.Log($"Added {item} to {m_MovementPoints}");
                    m_MovementPoints.Add(item);
                }
            }
            AssignMovementPoint();
        }

        /// <summary>
        /// Execute AiWander
        /// </summary>
        public override void Execute()
        {
            if(actor == null)
            {
                Debug.LogError($"[AiWander] Actor is null");
            }
            Vector3 actorPos = actor.transform.position;
            Vector3 targetPos = targetMovementPoint.transform.position;

            float distance = Vector3.Distance(targetPos, actorPos);
            if (distance > MIN_DISTANCE)
            {
                Debug.Log($"[AiWander] Moving towards {targetPos}");
                actor.transform.LookAt(targetPos);

                // If actor's distance is more than MIN_DISTANCE then walk towards the target
                actor.NpcMovement.MoveTowardsPoint(targetPos);
                actor.NpcMovement.SetRunning(false);
            }
            else
            {
                AssignMovementPoint();
            }
        }

        private void AssignMovementPoint()
        {
            var random = UnityEngine.Random.Range(0, m_MovementPoints.Count);
            Debug.Log($"[AiWander] AssignMovementPoints Count: {m_MovementPoints.Count}");
            if(m_MovementPoints[random] != null)
            {
                Debug.Log($"[AiWander] Assigned movement point {random} (Count: {m_MovementPoints.Count})");
                targetMovementPoint = m_MovementPoints[random];
            }
            else
            {
                Debug.LogError($"[AiWander] Movement Point {random} is null.");
            }
        }
    }
}
