using System;

namespace SoulsLike
{
    [Serializable]
    public class QuestData
    {
        public string Id;
        public int stage;
        public bool completed;
        public bool failed;
    }
}
