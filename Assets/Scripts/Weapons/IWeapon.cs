using UnityEngine;

namespace SoulsLike
{
    public interface IWeapon
    {
        /// <summary>
        /// Weapon Name
        /// </summary>
        string weaponName { get; }

        /// <summary>
        /// Weapon Type
        /// </summary>
        WeaponType weaponType { get; }

        /// <summary>
        /// Base Weapon Damage
        /// </summary>
        float weaponDamage { get; }

        /// <summary>
        /// Weapon Attack Point
        /// </summary>
        Vector3 weaponAttackPoint { get; }

        /// <summary>
        /// Weapon Attack Rotation (Euler)
        /// </summary>
        Vector3 weaponAttackRotation { get; }

        /// <summary>
        /// Weapon Scale
        /// </summary>
        Vector3 weaponAttackScale { get; }

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
