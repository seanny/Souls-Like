using System;
using UnityEngine;

namespace SoulsLike
{
    public class TestDialogueScript : GameplayScript, IDialogue
    {
        private void Start()
        {
        }

        public void OnDialogue(string dialogueID, Actor actor)
        {
            Debug.Log($"OnDialogue({dialogueID}, {actor})");
        }
    }
}
