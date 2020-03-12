using System;
using UnityEngine;

namespace SoulsLike
{
    public enum ComparisonOperator
    {
        MoreThan, // >
        LessThan, // <
        MoreOrEqualTo, // >=
        LessOrEqualTo, // <=
        EqualTo, // ==
        NotEqualTo // !=
    };
    
    public enum ScriptMethod
    {

    }

    [Serializable]
    public class AiCondition
    {
        public Transform target;
        public string scriptClass;
        public string scriptMethod;
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
        public AiCondition(Transform target, string scriptClass, string scriptMethod, object[] scriptParamaters, ComparisonOperator comparisonOperator, float value)
        {
            this.target = target;
            this.scriptClass = scriptClass;
            this.scriptMethod = scriptMethod;
            this.scriptParamaters = scriptParamaters;
            this.comparisonOperator = comparisonOperator;
            this.value = value;
        }
    }
}
