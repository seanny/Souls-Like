using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "Faction", menuName = "Faction")]
    public class Faction : ScriptableObject
    {
        /// <summary>
        /// Faction Name
        /// </summary>
        public new string name;

        /// <summary>
        /// Faction Ranks
        /// </summary>
        public List<string> ranks;

        /// <summary>
        /// Enemy Factions
        /// </summary>
        public List<Faction> enemies;

        /// <summary>
        /// Get Faction By Resource File Name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Faction GetFactionByFileName(string fileName)
        {
            string filePath = $"Factions/{fileName}";
            Faction faction = Resources.Load<Faction>(fileName);
            return faction;
        }

        /// <summary>
        /// Get Faction by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Faction GetFactionByName(string name)
        {
            Faction[] factions = Resources.LoadAll<Faction>("Factions");
            foreach(Faction faction in factions)
            {
                if(faction.name == name)
                {
                    return faction;
                }
            }
            return null;
        }
    }
}
