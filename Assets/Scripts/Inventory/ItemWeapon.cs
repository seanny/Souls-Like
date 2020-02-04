using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "Item", menuName = "Items/Weapon")]
    public class ItemWeapon : Item
    {
        private void Awake()
        {
            Type = ItemType.Weapon;
        }
    }
}
