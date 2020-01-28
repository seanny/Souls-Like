using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace SoulsLike
{
    public class DialogueController : MonoBehaviour
    {
        [Serializable]
        public class DialogueChoice
        {
            public Button dialogueButton;
            public TextMeshProUGUI dialogueText;
            public string dialogueId;
        }

        public static DialogueController instance { get; private set; }

        public GameObject dialogueUi;
        public Actor speakingToActor;
        public List<DialogueChoice> dialogueChoices;

        public static void ShowDialogue(string dialogueID, Actor actor)
        {
            Dialogue dialogue = Resources.Load<Dialogue>($"Dialogue/{dialogueID}");

            for(int i = 0; i < instance.dialogueChoices.Count; i++)
            {
                instance.dialogueChoices[i].dialogueText.text = dialogue.dialogueChoices[i].choiceString;
                instance.dialogueChoices[i].dialogueId = dialogue.dialogueChoices[i].dialogueID;
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            instance.dialogueUi.SetActive(true);
        }

        public static void HideDialogue()
        {
            instance.dialogueUi.SetActive(false);
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            dialogueUi.SetActive(false);
        }

        private void LateUpdate()
        {
            Debug.Log($"LateUpdate InputUtility.instance.Interaction = {InputUtility.instance.Interaction}");
            if (InputUtility.instance.Interaction == true)
            {
                Debug.Log($"LateUpdate InteractableUI.instance.nearestActor = {InteractableUI.instance.nearestActor}");
                speakingToActor = InteractableUI.instance.nearestActor;
                Debug.Log($"LateUpdate speakingToActor = {speakingToActor}");
                ShowDialogue("TestDialogue", speakingToActor);
            }
        }

        public void OnDialogueChoice(int dialogueChoice)
        {
            Debug.Log($"Selected choice #{dialogueChoice}");
            GameplayScript[] dialogueScripts = FindObjectsOfType<GameplayScript>();
            foreach(GameplayScript gameplayScript in dialogueScripts)
            {
                Type t = gameplayScript.GetType();
                bool containsInterface = t.GetInterfaces().Contains(typeof(IDialogue));
                if (containsInterface)
                {
                    try
                    {
                        Debug.Log($"dialogue.OnDialogue({dialogueChoices[dialogueChoice].dialogueId}, {speakingToActor})");
                        IDialogue dialogue = (IDialogue)gameplayScript;
                        dialogue.OnDialogue(dialogueChoices[dialogueChoice].dialogueId, speakingToActor);
                    }
                    catch(Exception exception)
                    {
                        Debug.LogError($"Error during OnDialogueChoice: {exception.Message}\n" +
                            $"{exception.StackTrace}");
                    }
                }
            }
        }
    }
}