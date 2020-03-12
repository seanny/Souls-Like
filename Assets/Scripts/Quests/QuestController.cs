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

        public List<QuestItem> quests = new List<QuestItem>();
        public List<QuestItem> completedQuests = new List<QuestItem>();
        public QuestItem currentQuest;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(this);
        }

        private void Start()
        {
            quests.Clear();
            completedQuests.Clear();
        }

        /// <summary>
        /// Set Current Quest
        /// </summary>
        /// <param name="questID"></param>
        public void SetCurrentQuest(string questID)
        {
            foreach(QuestItem questItem in quests)
            {
                if(questItem.questData.Id == questID)
                {
                    currentQuest = questItem;
                    break;
                }
            }
        }

        /// <summary>
        /// Add a quest to the quest journal
        /// </summary>
        /// <param name="questID"></param>
        public void AddQuest(string questID)
        {
            Quest quest = Resources.Load<Quest>($"Quests/{questID}");
            QuestItem questItem = new QuestItem();
            questItem.quest = quest;
            questItem.questData.Id = quest.name;
            quests.Add(questItem);
            QuestView.instance.ShowQuestRecieve(quest.Name);
            QuestView.instance.AddQuestToJournal(questItem.questData.Id, quest.Name);
        }

        /// <summary>
        /// Set the stage for a specific quest.
        /// </summary>
        /// <param name="questID"></param>
        /// <param name="stage"></param>
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
                                }
                                completedQuests.Add(questItem);
                                quests.Remove(questItem);
                            }
                            else
                            {
                                QuestView.instance.ShowQuestObjective(questStage.description);
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
