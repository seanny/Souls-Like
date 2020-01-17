using System;
using UnityEngine;

namespace SoulsLike
{
    public enum StateTypeID
    {
        None = -1,
        Wander, // Random wandering around the map.
        Travel, // Travel to a particular point in the map.
        Follow, // Follow a particular actor.
        Pursue, // Pursue a partucular actor.
        Combat, // Attack a partucular actor.
        Face, // Look at a particular actor.
        Patrol // Follow a particular path.
    }

    public class AiState
    {

        protected const float AI_REACTION_TIME = 0.25f;
        public static readonly float MIN_DISTANCE = 1.025f;
        public NonPlayerActor actor { get; protected set; }
        public StateTypeID stateType { get; protected set; }
        public Vector3 targetPosition { get; protected set; }
        public bool canRepeat { get; protected set; }

        public virtual bool Execute(float delta)
        {
            return false;
        }
    }
}
