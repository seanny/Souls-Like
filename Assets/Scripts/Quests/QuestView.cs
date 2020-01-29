using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SoulsLike
{
    public class QuestView : MonoBehaviour
    {
        [Serializable]
        public class QuestItemView
        {
            public string questID;
            public Button questButton;
            public TextMeshProUGUI questButtonText;
        }

        public static QuestView instance { get; private set; }

        public GameObject questButtonPrefab;

        public GameObject questJournal;
        public GameObject questJournalScrollViewContent;
        public TextMeshProUGUI questJournalHeader;
        public TextMeshProUGUI questJournalDescription;
        public TextMeshProUGUI questHeader;
        public TextMeshProUGUI questStage;
        public List<QuestItemView> questItems;
        float buttonWait;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            buttonWait = 0f;
            questJournal.SetActive(false);
            questHeader.gameObject.SetActive(false);
            questStage.gameObject.SetActive(false);
        }

        private void Update()
        {
            Debug.Log($"Time.timeScale = {Time.timeScale}");
            if(buttonWait > 0)
            {
                buttonWait -= Time.deltaTime;
            }
            if(InputUtility.instance.QuestJournal == true && buttonWait < 0.1f)
            {
                buttonWait = 0.75f;
                questJournal.SetActive(!questJournal.activeSelf);
                if(questJournal.activeSelf == true)
                {
                    Debug.Log($"First Quest: {questItems.First().questID}");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    OnClickQuest(questItems.First().questID);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        public void ShowQuestRecieve(string questName)
        {
            questHeader.gameObject.SetActive(true);
            questHeader.text = $"Started: {questName}";
            StartCoroutine(FadeText(questHeader, true));
        }

        public void ShowQuestObjective(string questObjective)
        {
            questStage.gameObject.SetActive(true);
            questStage.text = $"{questObjective}";
            StartCoroutine(FadeText(questStage, true));
        }

        public void ShowQuestComplete(string questName, bool fail)
        {
            questHeader.gameObject.SetActive(true);
            questHeader.text = $"{(fail ? "Failed" : "Completed")}: {questName}";
            StartCoroutine(FadeText(questHeader, true));
        }

        public void AddQuestToJournal(string questID, string questName)
        {
            QuestItemView questItemView = new QuestItemView();
            questItemView.questID = questID;
            questItemView.questButton = Instantiate(questButtonPrefab).GetComponent<Button>();
            questItemView.questButtonText = questItemView.questButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            questItemView.questButtonText.text = questName;
            questItemView.questButton.transform.parent = questJournalScrollViewContent.transform;
            questItemView.questButton.onClick.AddListener(delegate
            {
                OnClickQuest(questID);
            });

            questItems.Add(questItemView);
        }

        private void SetQuestInfo(string header, string description)
        {
            questJournalHeader.text = header;
            questJournalDescription.text = description;
        }

        private void OnClickQuest(string questID)
        {
            Debug.Log($"OnClickQuest({questID})");
            foreach(var item in questItems)
            {
                Debug.Log($"item.questID == {item.questID}");
                if (item.questID == questID)
                {
                    foreach(QuestController.QuestItem questItem in QuestController.instance.quests)
                    {
                        if(questItem.questData.Id == questID)
                        {
                            Debug.Log($"SetQuestInfo({questItem.quest.Name}, {questItem.quest.Description})");
                            SetQuestInfo(questItem.quest.Name, questItem.quest.Description);
                            break;
                        }
                    }
                    break;
                }
            }
        }
        IEnumerator FadeText(TextMeshProUGUI questText, bool fadeAway)
        {
            // fade from opaque to transparent
            if (fadeAway)
            {
                // loop over 1 second backwards
                for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    // set color with i as alpha
                    questText.color = new Color(questText.color.r, questText.color.g, questText.color.b, i);
                    yield return null;
                }
                questText.gameObject.SetActive(false);
            }
            // fade from transparent to opaque
            else
            {
                // loop over 1 second
                for (float i = 0; i <= 1; i += Time.deltaTime)
                {
                    // set color with i as alpha
                    questText.color = new Color(questText.color.r, questText.color.g, questText.color.b, i);
                    yield return null;
                }
                questText.gameObject.SetActive(false);
            }
        }
    }
}
