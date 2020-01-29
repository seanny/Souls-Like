using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SoulsLike
{
    public class QuestController : MonoBehaviour
    {
        public static QuestController instance { get; private set; }

        [Serializable]
        public class QuestItem
        {
            public Quest quest;
            public QuestData questData = new QuestData();
        }

        public List<QuestItem> quests;
        public List<QuestItem> completedQuests;
        public QuestItem currentQuest;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            quests = new List<QuestItem>();
            quests.Clear();
            completedQuests = new List<QuestItem>();
            completedQuests.Clear();
        }

        public void SetCurrentQuest(string questID)
        {
            foreach(QuestItem questItem in quests)
            {
                if(questItem.questData.Id == questID)
                {
                    currentQuest = questItem;
                    Debug.Log($"[QuestController] Set current quest to {questItem.questData.Id}.");
                    break;
                }
            }
        }

        public void AddQuest(string questID)
        {
            Quest quest = Resources.Load<Quest>($"Quests/{questID}");
            QuestItem questItem = new QuestItem();
            questItem.quest = quest;
            questItem.questData.Id = quest.name;
            quests.Add(questItem);
            QuestView.instance.ShowQuestRecieve(quest.Name);
            QuestView.instance.AddQuestToJournal(questItem.questData.Id, quest.Name);
            Debug.Log($"[QuestController] Added {questItem.questData.Id} to quest list.");
        }

        public void SetQuestStage(string questID, int stage)
        {
            foreach (QuestItem questItem in quests)
            {
                if (questItem.questData.Id == questID)
                {
                    foreach(QuestStage questStage in questItem.quest.Stages)
                    {
                        if(questStage.id == stage)
                        {
                            questItem.questData.stage = stage;
                            if (questStage.stageType == StageType.Complete || questStage.stageType == StageType.Fail)
                            {
                                questItem.questData.completed = true;
                                if (questStage.stageType == StageType.Fail)
                                {
                                    questItem.questData.failed = true;
                                    Debug.Log($"[QuestController] Set {questItem.questData.Id} stage to failed.");
                                }
                                else
                                {
                                    Debug.Log($"[QuestController] Set {questItem.questData.Id} stage to completed.");
                                }
                                completedQuests.Add(questItem);
                                quests.Remove(questItem);
                            }
                            else
                            {
                                QuestView.instance.ShowQuestObjective(questStage.description);
                                Debug.Log($"[QuestController] Set {questItem.questData.Id} stage to {questItem.questData.stage}.");
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
