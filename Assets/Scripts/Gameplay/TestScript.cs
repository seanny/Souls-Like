using System;
using UnityEngine;

namespace SoulsLike
{
    class TestScript : MonoBehaviour, IDialogue
    {
        private void Start()
        {
            string[] choices = { "Test01", "Test02", "Test03" };
            DialogController.ShowDialogue("TestDialogue", choices);
        }

        public void OnDialogue(string dialogueID, Actor actor)
        {
            Debug.Log(dialogueID);
        }
    }
}
