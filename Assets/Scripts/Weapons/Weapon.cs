using UnityEngine;

namespace SoulsLike
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
    public class Weapon : ScriptableObject
    {
        /// <summary>
        /// Weapon Localisable Name ID
        /// </summary>
        [Tooltip("ID of the localisation ID for the name, i.e. TEST_SWORD => Test Sword")]
        public string WeaponNameID;

        /// <summary>
        /// Weapon Script
        /// </summary>
        public IWeapon WeaponScript;

        /// <summary>
        /// Weapon Mesh (3D Model)
        /// </summary>
        public GameObject WeaponMesh;

        /// <summary>
        /// Weapon Type
        /// </summary>
        public WeaponType WeaponType;
    }
}
