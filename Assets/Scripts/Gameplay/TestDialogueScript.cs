using System;
using UnityEngine;

namespace SoulsLike
{
    public class TestDialogueScript : GameplayScript, IDialogue
    {
        private void Start()
        {
            // Create test quest on load
            QuestController.instance.AddQuest("TestQuest");
            QuestController.instance.SetQuestStage("TestQuest", 1);
        }

        public void OnDialogue(string dialogueID, string dialogueChoice, Actor actor)
        {
            Debug.Log($"OnDialogue({dialogueID}, {actor})");
            if(dialogueID == "TestDialogue")
            {
                Dialogue dialogue = Resources.Load<Dialogue>($"Dialogue/{dialogueID}");
                if(dialogueChoice == "TestChoice01")
                {
                    // Test Complete Quest
                    QuestController.instance.SetQuestStage("TestQuest", 2);
                }
                else if (dialogueChoice == "TestChoice02")
                {
                    // Test Fail Quest
                    QuestController.instance.SetQuestStage("TestQuest", 3);
                }
            }
        }
    }
}
