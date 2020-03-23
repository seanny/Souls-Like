using UnityEngine;

namespace SoulsLike
{
    public class PlayerToxins : ActorToxins
    {
        [SerializeField] private ToxinHUD m_ToxinHUD;

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            //m_ToxinHUD.SetHUD(m_Actor.actorStats.currentToxins, m_Actor.actorStats.maxToxins);
        }
    }
}
