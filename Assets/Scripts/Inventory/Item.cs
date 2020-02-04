using UnityEngine;

namespace SoulsLike
{
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
        public string Name;
        [TextArea(15,20)]
        public string Description;
        public int Weight;
        public ItemType Type;
        public int Value;
        public GameObject Mesh;
        public AudioClip PickupSound;
        public AudioClip DropSound;
    }
}
