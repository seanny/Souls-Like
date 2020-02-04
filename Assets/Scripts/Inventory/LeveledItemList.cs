using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "Leveled List", menuName = "Leveled List")]
    public class LeveledItemList : ScriptableObject
    {
        public int MinLevel;
        public List<Item> inventoryItems;
    }
}