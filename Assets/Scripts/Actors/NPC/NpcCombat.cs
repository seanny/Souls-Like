using UnityEngine;

namespace SoulsLike
{
    public enum AggressionLevel
    {
        // Does not attack, unless attacked.
        Neutral,

        // Attacks everyone, except allies.
        Agressive,

        // Attacks everyone, including allies.
        HatesEveryone
    };

    public class NpcCombat : MonoBehaviour
    {
        public AggressionLevel AggressionLevel => m_AggressionLevel;
        public Actor FightingActor => m_FightingActor;

        [SerializeField] private AggressionLevel m_AggressionLevel;
        [SerializeField] private Actor m_FightingActor;

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
