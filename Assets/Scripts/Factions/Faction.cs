using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "Faction", menuName = "Faction")]
    public class Faction : ScriptableObject
    {
        public new string name;
        public List<string> ranks;
        public List<Faction> enemies;

        public static Faction GetFactionByFileName(string fileName)
        {
            string filePath = $"Factions/{fileName}";
            Faction faction = Resources.Load<Faction>(fileName);
            return faction;
        }

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
