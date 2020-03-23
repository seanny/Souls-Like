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
            float value = PlayerActor.instance.actorStats.currentHealth / PlayerActor.instance.actorStats.maxHealth;
            if (float.IsNaN(value))
            {
                value = 0f;
            }
            m_Scrollbar.size = value;
        }

        // Update is called once per frame
        protected void Update()
        {
            GetHealth();
        }
    }
}