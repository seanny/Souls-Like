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
            switch(comparisonOperator)
            {
                case ComparisonOperator.EqualTo:
                    isMet = HandleEqualTo();
                    IsTrue()
                    break;
                case ComparisonOperator.LessThan:
                    isMet = HandleLessThan();
                    break;
                case ComparisonOperator.LessOrEqualTo:
                    isMet = HandleLessOrEqualTo();
                    break;
                case ComparisonOperator.MoreOrEqualTo:
                    isMet = HandleMoreOrEqualTo();
                    break;
                case ComparisonOperator.MoreThan:
                    isMet = HandleMoreThan();
                    break;
                case ComparisonOperator.NotEqualTo:
                    isMet = HandleNotEqualTo();
                    break;
            }
            return isMet;
        }

        public bool IsTrue<T, U>(T value1, ComparisonOperator comparisonOperator, U value2)
            where T : U
            where U : IComparable
        {
            bool retValue = false;
            switch(comparisonOperator)
            {
                case ComparisonOperator.EqualTo:
                    retValue = value1.CompareTo(value2) == 0;
                    break;
                case ComparisonOperator.LessOrEqualTo:
                    retValue = value1.CompareTo(value2) <= 0;
                    break;
                case ComparisonOperator.LessThan:
                    retValue = value1.CompareTo(value2) < 0;
                    break;
                case ComparisonOperator.MoreOrEqualTo:
                    retValue = value1.CompareTo(value2) >= 0;
                    break;
                case ComparisonOperator.MoreThan:
                    retValue = value1.CompareTo(value2) > 0;
                    break;
                case ComparisonOperator.NotEqualTo:
                    retValue = value1.CompareTo(value2) != 0;
                    break;
            }
            return retValue;
        }

        /// <summary>
        /// Handle Equal To (==) operator
        /// </summary>
        /// <returns>True or False</returns>
        private bool HandleEqualTo()
        {
            // There's got to be a better way of doing this
            switch (scriptMethod)
            {
                case ScriptMethod.GetActorLevel:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.level == (int)value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetHealth:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentHealth == value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetMagic:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentMagic == value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetPlayerHealth:
                    if (PlayerActor.instance.actorStats.currentHealth == value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerLevel:
                    if (PlayerActor.instance.actorStats.level == value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerMagic:
                    if (PlayerActor.instance.actorStats.currentMagic == value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetQuestStage:
                    if (scriptParamaters[0].GetType() == typeof(string))
                    {
                        string questID = (string)scriptParamaters[1];
                        if (QuestController.instance.GetQuestStage(questID) == value)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Debug.LogError("GetQuestStage must have it's 1st paramater as a string.");
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Handle Less Than (<) operator
        /// </summary>
        /// <returns>True or False</returns>
        private bool HandleLessThan()
        {
            // There's got to be a better way of doing this
            switch (scriptMethod)
            {
                case ScriptMethod.GetActorLevel:
                    if(scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.level < (int)value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetHealth:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentHealth < value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetMagic:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentMagic < value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetPlayerHealth:
                    if (PlayerActor.instance.actorStats.currentHealth < value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerLevel:
                    if (PlayerActor.instance.actorStats.level < value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerMagic:
                    if (PlayerActor.instance.actorStats.currentMagic < value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetQuestStage:
                    if (scriptParamaters[0].GetType() == typeof(string))
                    {
                        string questID = (string)scriptParamaters[1];
                        if (QuestController.instance.GetQuestStage(questID) < value)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Debug.LogError("GetQuestStage must have it's 1st paramater as a string.");
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Handle Less Than Or Equal To (<=) operator
        /// </summary>
        /// <returns>True or False</returns>
        private bool HandleLessOrEqualTo()
        {
            // There's got to be a better way of doing this
            switch (scriptMethod)
            {
                case ScriptMethod.GetActorLevel:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.level <= (int)value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetHealth:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentHealth <= value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetMagic:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentMagic <= value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetPlayerHealth:
                    if (PlayerActor.instance.actorStats.currentHealth <= value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerLevel:
                    if (PlayerActor.instance.actorStats.level <= value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerMagic:
                    if (PlayerActor.instance.actorStats.currentMagic <= value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetQuestStage:
                    if (scriptParamaters[0].GetType() == typeof(string))
                    {
                        string questID = (string)scriptParamaters[1];
                        if (QuestController.instance.GetQuestStage(questID) <= value)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Debug.LogError("GetQuestStage must have it's 1st paramater as a string.");
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Handle More Or Equal To (>=) operator
        /// </summary>
        /// <returns>True or False</returns>
        private bool HandleMoreOrEqualTo()
        {
            // There's got to be a better way of doing this
            switch (scriptMethod)
            {
                case ScriptMethod.GetActorLevel:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.level >= (int)value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetHealth:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentHealth >= value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetMagic:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentMagic >= value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetPlayerHealth:
                    if (PlayerActor.instance.actorStats.currentHealth >= value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerLevel:
                    if (PlayerActor.instance.actorStats.level >= value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerMagic:
                    if (PlayerActor.instance.actorStats.currentMagic >= value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetQuestStage:
                    if (scriptParamaters[0].GetType() == typeof(string))
                    {
                        string questID = (string)scriptParamaters[1];
                        if (QuestController.instance.GetQuestStage(questID) >= value)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Debug.LogError("GetQuestStage must have it's 1st paramater as a string.");
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Handle More Than (>) operator
        /// </summary>
        /// <returns>True or False</returns>
        private bool HandleMoreThan()
        {
            // There's got to be a better way of doing this
            switch (scriptMethod)
            {
                case ScriptMethod.GetActorLevel:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.level > (int)value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetHealth:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentHealth > value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetMagic:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentMagic > value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetPlayerHealth:
                    if (PlayerActor.instance.actorStats.currentHealth > value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerLevel:
                    if (PlayerActor.instance.actorStats.level > value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerMagic:
                    if (PlayerActor.instance.actorStats.currentMagic > value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetQuestStage:
                    if (scriptParamaters[0].GetType() == typeof(string))
                    {
                        string questID = (string)scriptParamaters[1];
                        if (QuestController.instance.GetQuestStage(questID) > value)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Debug.LogError("GetQuestStage must have it's 1st paramater as a string.");
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Handle Not Equal To (!=) operator
        /// </summary>
        /// <returns>True or False</returns>
        private bool HandleNotEqualTo()
        {
            // There's got to be a better way of doing this
            switch (scriptMethod)
            {
                case ScriptMethod.GetActorLevel:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.level != (int)value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetHealth:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentHealth != value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetMagic:
                    if (scriptParamaters[0].GetType() == typeof(Actor))
                    {
                        Actor actor = (Actor)scriptParamaters[0];
                        if (actor.actorStats.currentMagic != value)
                        {
                            return true;
                        }
                    }
                    break;
                case ScriptMethod.GetPlayerHealth:
                    if (PlayerActor.instance.actorStats.currentHealth != value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerLevel:
                    if (PlayerActor.instance.actorStats.level != value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetPlayerMagic:
                    if (PlayerActor.instance.actorStats.currentMagic != value)
                    {
                        return true;
                    }
                    break;
                case ScriptMethod.GetQuestStage:
                    if (scriptParamaters[0].GetType() == typeof(string))
                    {
                        string questID = (string)scriptParamaters[1];
                        if (QuestController.instance.GetQuestStage(questID) != value)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        Debug.LogError("GetQuestStage must have it's 1st paramater as a string.");
                    }
                    break;
            }
            return false;
        }
    }
}
