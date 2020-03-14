namespace SoulsLike
{
    public class HealthHUD : BaseHUD
    {
        private void GetHealth()
        {
            if(PlayerActor.instance == null)
            {
                return;
            }
            m_Scrollbar.size = PlayerActor.instance.actorStats.currentHealth / PlayerActor.instance.actorStats.maxHealth;
        }

        // Update is called once per frame
        protected void Update()
        {
            GetHealth();
        }
    }
}