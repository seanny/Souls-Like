namespace SoulsLike
{
    public interface IDialogue
    {
        /// <summary>
        /// On Dialogue
        /// </summary>
        /// <param name="dialogueID"></param>
        /// <param name="dialogueChoice"></param>
        /// <param name="actor"></param>
        void OnDialogue(string dialogueID, string dialogueChoice, Actor actor);
    }
}
