using System;
using UnityEngine;

namespace SoulsLike
{
    public class TestDialogueScript : GameplayScript, IDialogue
    {
        private void Start()
        {
        }

        private void Update()
        {
            if(InputUtility.instance.Interaction == true)
            {
                DialogueController.ShowDialogue("TestDialogue");
            }
        }

        public void OnDialogue(string dialogueID, Actor actor)
        {
            Debug.Log($"OnDialogue({dialogueID}, {actor})");
        }
    }
}
