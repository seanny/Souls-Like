using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    /*public enum StateType
    {
        None, // Do nothing (same as idle?)
        Wander, // Randomly move between different positions
        Travel, // Move to a position
        Follow, // Follow an actor
        Idle, // Stand in a position
        Face, // Look towards a position
        Combat // Fight with an actor
    };*/

    public abstract class AiState : MonoBehaviour
    {
        public static readonly float MIN_DISTANCE = 1.5f;

        public List<AiCondition> aiConditions;
        public AiSchedule aiSchedule;
        [SerializeField] protected NonPlayerActor actor;

        protected virtual void Start()
        {
            Debug.Log($"[AiState] Assigned {actor} to {this.name}");
            this.actor = GetComponentInParent<NonPlayerActor>();
        }

        /// <summary>
        /// Execute Ai State
        /// </summary>
        public virtual void Execute()
        {
            throw new NotImplementedException("Execute() must be overriden!");
        }
    }
}
