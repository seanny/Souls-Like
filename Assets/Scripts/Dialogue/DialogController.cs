using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace SoulsLike
{
    public class DialogController : MonoBehaviour
    {
        public static DialogController instance { get; private set; }

        public Actor speakingToActor;
        public List<Button> dialogueChoices;
        public List<TextMeshProUGUI> dialogueText;
        public List<string> dialogueIDs;

        Type type = null;
        object classInstance = null;

        public static void ShowDialogue(string script, string[] dialogueChoiceIDs)
        {
            instance.type = Type.GetType(script, true);

            instance.classInstance = Activator.CreateInstance(instance.type);

            instance.gameObject.SetActive(true);
            for (int i = 0; i < instance.dialogueChoices.Count; i++)
            {
                instance.dialogueText[i].text = dialogueChoiceIDs[i];
                instance.dialogueIDs[i] = dialogueChoiceIDs[i];
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            for(int i = 0; i < dialogueChoices.Count; i++)
            {
                dialogueChoices[i].onClick.AddListener(() => OnDialogueChoice(i));
                dialogueText.Add(dialogueChoices[i].GetComponentInChildren<TextMeshProUGUI>());
            }
            gameObject.SetActive(false);
        }

        private void OnDialogueChoice(int dialogueChoice)
        {
            Debug.Log($"Selected choice #{dialogueChoice}");
            foreach (Type t in this.GetType().Assembly.GetTypes())
            {
                if(t is IDialogue)
                {
                    MethodInfo methodInfo = t.GetMethod("OnDialogue");
                    if (methodInfo != null)
                    {
                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        object classInstance = Activator.CreateInstance(t, null);

                        object[] parametersArray = new object[] { dialogueIDs[dialogueChoice], speakingToActor };

                        object result = methodInfo.Invoke(classInstance, parametersArray);
                    }
                }
            }
        }
    }
}