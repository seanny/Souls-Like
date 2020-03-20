using System;
using UnityEngine;

namespace SoulsLike
{
    /// <summary>
    /// Comparison Operator
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// \>
        /// </summary>
        MoreThan, // >

        /// <summary>
        /// \<
        /// </summary>
        LessThan, // <

        /// <summary>
        /// \>=
        /// </summary>
        MoreOrEqualTo, // >=

        /// <summary>
        /// \<=
        /// </summary>
        LessOrEqualTo, // <=

        /// <summary>
        /// ==
        /// </summary>
        EqualTo, // ==

        /// <summary>
        /// !=
        /// </summary>
        NotEqualTo // !=
    };
    
    /// <summary>
    /// Script Method
    /// </summary>
    public enum ScriptMethod
    {
        GetQuest,
        GetQuestStage,
        GetHealth,
        GetMagic,
        GetActorLevel,
        GetPlayerLevel,
        GetPlayerHealth,
        GetPlayerMagic,
        GetGlobal,
        GetHour
    };

    /// <summary>
    /// Ai Condition
    /// </summary>
    [Serializable]
    public class AiCondition
    {
        public Transform target;
        public ScriptMethod scriptMethod;
        public object[] scriptParamaters;
        public ComparisonOperator comparisonOperator;
        public float value;

        /// <summary>
        /// Ai Condition Constructor
        /// </summary>
        /// <param name="target"></param>
        /// <param name="scriptClass"></param>
        /// <param name="scriptMethod"></param>
        /// <param name="scriptParamaters"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="value"></param>
        public AiCondition(Transform target, ScriptMethod scriptMethod, object[] scriptParamaters, ComparisonOperator comparisonOperator, float value)
        {
            this.target = target;
            this.scriptMethod = scriptMethod;
            this.scriptParamaters = scriptParamaters;
            this.comparisonOperator = comparisonOperator;
            this.value = value;
        }

        /// <summary>
        /// Is Ai Condition Met?
        /// </summary>
        /// <returns></returns>
        public bool IsConditionMet()
        {
            bool isMet = false;
            switch (scriptMethod)
            {
                case ScriptMethod.GetActorLevel:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        return ComparisonCheck.IsTrue(actor.actorStats.level, comparisonOperator, (int)value);
                    }
                    break;
                case ScriptMethod.GetHealth:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        return ComparisonCheck.IsTrue(actor.actorStats.currentHealth, comparisonOperator, value);
                    }
                    break;
                case ScriptMethod.GetMagic:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        return ComparisonCheck.IsTrue(actor.actorStats.currentMagic, comparisonOperator, value);
                    }
                    break;
                case ScriptMethod.GetPlayerHealth:
                    return ComparisonCheck.IsTrue(PlayerActor.instance.actorStats.currentHealth, comparisonOperator, value);
                    break;
                case ScriptMethod.GetPlayerLevel:
                    return ComparisonCheck.IsTrue(PlayerActor.instance.actorStats.level, comparisonOperator, (int)value);
                    break;
                case ScriptMethod.GetPlayerMagic:
                    return ComparisonCheck.IsTrue(PlayerActor.instance.actorStats.currentMagic, comparisonOperator, value);
                    break;
                case ScriptMethod.GetQuestStage:
                    if (scriptParamaters[0].GetType() == typeof(string))
                    {
                        string questID = (string)scriptParamaters[1];
                        return ComparisonCheck.IsTrue(QuestController.instance.GetQuestStage(questID), comparisonOperator, (int)value);
                    }
                    else
                    {
                        Debug.LogError("GetQuestStage must have it's 1st paramater as a string.");
                    }
                    break;
            }
            return isMet;
        }
    }
}
