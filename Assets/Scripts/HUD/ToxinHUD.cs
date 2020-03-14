namespace SoulsLike
{
    public class ToxinHUD : BaseHUD
    {
        /// <summary>
        /// Get Current Toxin Level And Adjust UI accordingly.
        /// </summary>
        private void GetToxins()
        {
            // If the player actor does not exist, then do not continue any further.
            if (PlayerActor.instance == null)
            {
                return;
            }

            // Get the current magic divided by the magic magic.
            m_Scrollbar.size = PlayerActor.instance.actorStats.currentToxins / PlayerActor.instance.actorStats.maxToxins;
        }

        // Update is called once per frame
        protected void Update()
        {
            // Update the magic UI
            GetToxins();
        }
    }
}
