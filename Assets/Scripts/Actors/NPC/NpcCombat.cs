using UnityEngine;

namespace SoulsLike
{
    /// <summary>
    /// Aggresssion Level
    /// </summary>
    public enum AggressionLevel
    {
        // Does not attack, unless attacked.
        Neutral,

        // Attacks everyone, except allies.
        Agressive,

        // Attacks everyone, including allies.
        HatesEveryone
    };

    /// <summary>
    /// Non Player Character Combat Controller
    /// </summary>
    public class NpcCombat : MonoBehaviour
    {
        /// <summary>
        /// Aggression Level
        /// </summary>
        public AggressionLevel AggressionLevel => m_AggressionLevel;

        /// <summary>
        /// Fighting Actor
        /// </summary>
        public Actor FightingActor => m_FightingActor;

#pragma warning disable 0649
        [SerializeField] private AggressionLevel m_AggressionLevel;
        [SerializeField] private Actor m_FightingActor;
#pragma warning restore 0649

        /// <summary>
        /// Initiate Combat with actor
        /// </summary>
        /// <param name="actor"></param>
        public void InitiateCombat(Actor actor)
        {
            m_FightingActor = actor;
        }
    }
}
