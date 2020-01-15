using UnityEngine;

namespace SoulsLike
{
    public interface IWeapon
    {
        /// <summary>
        /// Weapon Type
        /// </summary>
        WeaponType weaponType { get; }

        /// <summary>
        /// Base Weapon Damage
        /// </summary>
        float weaponDamage { get; }

        /// <summary>
        /// On Weapon Attack (i.e. when used against an actor)
        /// </summary>
        void OnWeaponAttack(Actor actor);

        /// <summary>
        /// On Weapon Equip (i.e. when an actor grabs a weapon from their inventory)
        /// </summary>
        void OnWeaponEquip();

        /// <summary>
        /// On Weapon Store (i.e. when an actor places a weapon in their inventory)
        /// </summary>
        void OnWeaponStore();
    }
}
