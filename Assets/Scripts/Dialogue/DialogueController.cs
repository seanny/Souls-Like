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
        public static DialogueController instance { get; private set; }

        public Actor speakingToActor;
        public List<Button> dialogueChoices;
        public List<TextMeshProUGUI> dialogueText;
        public List<string> dialogueIDs;

        public static void ShowDialogue(string dialogueID)
        {
            Dialogue dialogue = Resources.Load<Dialogue>($"Dialogue/{dialogueID}");
            instance.dialogueIDs.Clear();

            for(int i = 0; i < instance.dialogueText.Count; i++)
            {
                instance.dialogueText[i].text = dialogue.dialogueChoices[i].choiceString;
                instance.dialogueIDs.Add(dialogue.dialogueChoices[i].dialogueID);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            instance.gameObject.SetActive(true);
        }

        private void Awake()
        {
            instance = this;
            for(int i = 0; i < dialogueChoices.Count; i++)
            {
                dialogueChoices[i].onClick.AddListener(() => OnDialogueChoice(i));
                dialogueText.Add(dialogueChoices[i].GetComponentInChildren<TextMeshProUGUI>());
            }
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnDialogueChoice(int dialogueChoice)
        {
            Debug.Log($"Selected choice #{dialogueChoice}");
            GameplayScript[] dialogueScripts = FindObjectsOfType<GameplayScript>();
            foreach(GameplayScript gameplayScript in dialogueScripts)
            {
                Type t = gameplayScript.GetType();
                bool containsInterface = t.GetInterfaces().Contains(typeof(IDialogue));
                Debug.Log(containsInterface);
                if (containsInterface)
                {
                    try
                    {
                        Debug.Log($"dialogue.OnDialogue({dialogueIDs[dialogueChoice]}, {speakingToActor})");
                        IDialogue dialogue = (IDialogue)gameplayScript;
                        dialogue.OnDialogue(dialogueIDs[dialogueChoice], speakingToActor);
                    }
                    catch(Exception exception)
                    {
                        Debug.LogError($"Error during OnDialogueChoice: {exception.Message}\n" +
                            $"{exception.StackTrace}");
                    }
                }
            }


            /*foreach (Type t in GetType().Assembly.GetTypes())
            {
                bool containsInterface = t.GetInterfaces().Contains(typeof(IDialogue));
                if (containsInterface)
                {
                    MethodInfo methodInfo = t.GetMethod("OnDialogue");
                    Debug.Log($"MethodInfo = {methodInfo}");
                    if (methodInfo != null)
                    {
                        Debug.Log($"MethodInfo != null");
                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        object classInstance = Activator.CreateInstance(t, null);

                        object[] parametersArray = new object[] { dialogueIDs[dialogueChoice], speakingToActor };

                        object result = methodInfo.Invoke(classInstance, parametersArray);
                        Debug.Log($"result = {result}");
                    }
                }
            }*/
        }
    }
}