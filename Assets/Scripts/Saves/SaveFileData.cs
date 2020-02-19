using System;
using System.Collections.Generic;

namespace SoulsLike
{
    [Serializable]
    public class SaveFileData
    {
        public float gameVersion;
        public ActorStats playerStats;
        public string playerLevel;
        public List<QuestData> questData = new List<QuestData>();
        public DateTime saveCreation = DateTime.Now;
        public List<ActorStats> actorStats = new List<ActorStats>();
    }
}
