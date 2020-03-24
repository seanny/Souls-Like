using UnityEngine;
using UnityEngine.AI;

namespace SoulsLike
{
    public class NpcMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent m_NavMeshAgent;
        [SerializeField] private NonPlayerActor m_Actor;
        [SerializeField] private bool m_Running;
        [SerializeField] private bool m_IsEnabled;

        /// <summary>
        /// Unity3D Start Callback
        /// </summary>
        private void Start()
        {
            AssignActor();
            AssignNavMeshAgent();
            Enable();
        }

        /// <summary>
        /// Assign unity NavMeshAgent for NPC Movement
        /// </summary>
        private void AssignNavMeshAgent()
        {
            if (m_Actor != null)
            {
                m_NavMeshAgent = m_Actor.GetComponent<NavMeshAgent>();
            }
        }

        /// <summary>
        /// Assign Actor for NPC Movement
        /// </summary>
        private void AssignActor()
        {
            m_Actor = GetComponent<NonPlayerActor>();
        }

        public void SetRunning(bool running)
        {
            m_Running = running;
        }

        /// <summary>
        /// Enable NPC movement
        /// </summary>
        public void Enable()
        {
            m_IsEnabled = true;
            m_NavMeshAgent.enabled = true;
        }

        /// <summary>
        /// Disable NPC movement
        /// </summary>
        public void Disable()
        {
            m_IsEnabled = false;
            m_NavMeshAgent.enabled = false;
        }

        /// <summary>
        /// Get NPC movement speed
        /// </summary>
        /// <returns></returns>
        public float MovementSpeed()
        {
            if(!m_IsEnabled)
            {
                return 0f;
            }
            float movementSpeed = 0f;
            if (!m_NavMeshAgent.isStopped || m_NavMeshAgent.destination != Vector3.zero)
            {
                if (m_Running)
                {
                    movementSpeed = 1.0f;
                }
                else
                {
                    movementSpeed = 0.5f;
                }
            }
            return movementSpeed;
        }

        /// <summary>
        /// Move NPC towards an area.
        /// </summary>
        /// <param name="destination"></param>
        public void MoveTowardsActor(Actor actor)
        {
            if (!m_IsEnabled)
            {
                return;
            }
            MoveTowardsPoint(actor.transform.position);
        }

        /// <summary>
        /// Move towards vector 3
        /// </summary>
        /// <param name="point"></param>
        public void MoveTowardsPoint(Vector3 point)
        {
            if (!m_IsEnabled)
            {
                return;
            }
            m_Actor.stateManager.moveAmount = MovementSpeed();
            m_NavMeshAgent.destination = point;
            m_NavMeshAgent.isStopped = false;
        }

        /// <summary>
        /// Stop moving towards actor
        /// </summary>
        /// <param name="actor"></param>
        public void StopMovingTowardsActor(Actor actor)
        {
            if (!m_IsEnabled)
            {
                return;
            }
            m_NavMeshAgent.isStopped = true;
        }

        /// <summary>
        /// Follow actor
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool FollowActor(Actor actor)
        {
            throw new System.NotImplementedException();
        }
    }
}
