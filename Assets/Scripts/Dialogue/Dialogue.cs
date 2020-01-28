using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public string dialogueString;
        public List<DialogueChoice> dialogueChoices;
        public List<string> linkedScripts;
    }

    [Serializable]
    public struct DialogueChoice
    {
        public string choiceID;
        public string choiceString;
        public string dialogueID;
    }
}
