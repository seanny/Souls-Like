using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        /// <summary>
        /// Dialogue String
        /// </summary>
        public string dialogueString;

        /// <summary>
        /// Dialogue Choices
        /// </summary>
        public List<DialogueChoice> dialogueChoices;

        /// <summary>
        /// Linked Scripts
        /// </summary>
        public List<string> linkedScripts;
    }

    [Serializable]
    public struct DialogueChoice
    {
        /// <summary>
        /// Dialogue Choice ID
        /// </summary>
        public string choiceID;

        /// <summary>
        /// Dialogue Choice String
        /// </summary>
        public string choiceString;

        /// <summary>
        /// Dialogue ID
        /// </summary>
        public string dialogueID;
    }
}
