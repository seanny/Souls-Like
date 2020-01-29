namespace SoulsLike
{
    public interface IDialogue
    {
        void OnDialogue(string dialogueID, string dialogueChoice, Actor actor);
    }
}
