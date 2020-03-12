using UnityEngine;

namespace SoulsLike
{
    /// <summary>
    /// Item Type
    /// </summary>
    public enum ItemType
    {
        None,
        Weapon,
        Armour,
        Food,
        Book,
        Scroll,
        Potion,
        Ingredient,
        Misc,
    }

    [CreateAssetMenu(fileName = "Item", menuName = "Items/Generic Item")]
    public class Item : ScriptableObject
    {
        /// <summary>
        /// Item name
        /// </summary>
        public string Name;

        /// <summary>
        /// Item Description
        /// </summary>
        [TextArea(15,20)]
        public string Description;

        /// <summary>
        /// Item Weight
        /// </summary>
        public int Weight;

        /// <summary>
        /// Item Type
        /// </summary>
        public ItemType Type;

        /// <summary>
        /// Item Value
        /// </summary>
        public int Value;

        /// <summary>
        /// Item Mesh
        /// </summary>
        public GameObject Mesh;

        /// <summary>
        /// Item Pickup Sound
        /// </summary>
        public AudioClip PickupSound;

        /// <summary>
        /// Item Drop Sound
        /// </summary>
        public AudioClip DropSound;
    }
}
