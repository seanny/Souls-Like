using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public enum StageType
    {
        Travel,
        Fetch,
        Kill,
        Talk,
        Complete,
        Fail
    };

    [Serializable]
    public class QuestStage
    {
        public int id;
        public string description;
        public StageType stageType;
        public string entity;
        public Vector3 position;
    }

    [CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
    public class Quest : ScriptableObject
    {
        public string Name;
        public string Description;
        public List<QuestStage> Stages;
    }
}
