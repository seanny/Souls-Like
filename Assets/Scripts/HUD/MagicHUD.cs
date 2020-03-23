namespace SoulsLike
{
    public class MagicHUD : BaseHUD
    {

        /// <summary>
        /// Get Current Magic Level And Adjust UI accordingly.
        /// </summary>
        private void GetMagic()
        {
            // If the player actor does not exist, then do not continue any further.
            if(PlayerActor.instance == null)
            {
                return;
            }

            float value = PlayerActor.instance.actorStats.currentMagic / PlayerActor.instance.actorStats.maxMagic;
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
            GetMagic();
        }
    }
}