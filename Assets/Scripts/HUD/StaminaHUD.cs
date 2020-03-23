namespace SoulsLike
{
    public class StaminaHUD : BaseHUD
    {
        /// <summary>
        /// Get Current Stamina Level And Adjust UI accordingly.
        /// </summary>
        private void GetStamina()
        {
            // If the player actor does not exist, then do not continue any further.
            if (PlayerActor.instance == null)
            {
                return;
            }

            float value = PlayerActor.instance.actorStats.currentStamina / PlayerActor.instance.actorStats.maxStamina;
            if (float.IsNaN(value))
            {
                value = 0f;
            }
            // Get the current magic divided by the magic magic.
            m_Scrollbar.size = value;
        }

        // Update is called once per frame
        protected void Update()
        {
            // Update the magic UI
            GetStamina();
        }
    }
}
