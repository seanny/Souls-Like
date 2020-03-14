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

            // Get the current magic divided by the magic magic.
            m_Scrollbar.size = PlayerActor.instance.actorStats.currentMagic / PlayerActor.instance.actorStats.maxMagic;
        }

        // Update is called once per frame
        protected void Update()
        {
            // Update the magic UI
            GetMagic();
        }
    }
}