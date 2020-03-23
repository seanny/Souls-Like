using System.Collections;
using UnityEngine;

namespace SoulsLike
{
    public class ActorToxins : MonoBehaviour
    {
        private const float TOXIN_TIMER = 5f;
        private const float TOXIN_DECREASE = 0.5f;

        [SerializeField] protected Actor m_Actor;

        private void Start()
        {
            m_Actor = GetComponent<Actor>();
            StartCoroutine(ToxinTimer());
        }

        private IEnumerator ToxinTimer()
        {
            while(true)
            {
                HandleToxins();
                yield return new WaitForSeconds(TOXIN_TIMER);
            }
        }

        private void HandleToxins()
        {
            float percent = m_Actor.actorStats.currentToxins / m_Actor.actorStats.maxToxins;
            if(percent >= .5f)
            {
                Debug.Log($"[{m_Actor.name}.ActorToxins] Applying magic debuff if not already applied");
                // Apply magic debuff if not already applied.
            }
            if (percent >= .9f)
            {
                Debug.Log($"[{m_Actor.name}.ActorToxins] Applying weaker attacks debuff if not already applied");
                // Apply weaker attacks debuff
            }
            if (percent >= 1f)
            {
                Debug.Log($"[{m_Actor.name}.ActorToxins] Reducing health by 1pt");
                // Reduce health by 1pt every 5 seconds.
            }
            if(m_Actor.actorStats.currentToxins > 0)
            {
                m_Actor.actorStats.currentToxins -= TOXIN_DECREASE;
                if(m_Actor.actorStats.currentToxins < 0)
                {
                    m_Actor.actorStats.currentToxins = 0;
                }
            }
        }

        public void ApplyToxins(float amount)
        {
            m_Actor.actorStats.currentToxins += amount;
            if(m_Actor.actorStats.currentToxins > m_Actor.actorStats.maxToxins)
            {
                m_Actor.actorStats.currentToxins = m_Actor.actorStats.maxToxins;
            }
        }
    }
}
