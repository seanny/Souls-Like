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
            GlobalController.SetGlobal("TestSetting", 13);
        }

        public void OnDialogue(string dialogueID, string dialogueChoice, Actor actor)
        {
            Debug.Log($"OnDialogue({dialogueID}, {actor})");
            Notification.Add($"OnDialogue({dialogueID}, {actor})");
            if (dialogueID == "TestDialogue")
            {
                Notification.Add("Test Notification");
                Dialogue dialogue = Resources.Load<Dialogue>($"Dialogue/{dialogueID}");
                if(dialogueChoice == "TestChoice01")
                {
                    // Test Complete Quest
                    Notification.Add("Test Quest Completed");
                    QuestController.instance.SetQuestStage("TestQuest", 2);
                }
                else if (dialogueChoice == "TestChoice02")
                {
                    // Test Fail Quest
                    Notification.Add("Test Quest Failed");
                    QuestController.instance.SetQuestStage("TestQuest", 3);
                }
            }
        }
    }
}
