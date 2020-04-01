#if UNITY_EDITOR
using EasyButtons;
#endif
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace SoulsLike
{
    public class ActorCombat : MonoBehaviour
    {
        private Actor m_Actor;
        private Animator m_Animator;
        [SerializeField] private WeaponType m_WeaponType;
        private GameObject m_WeaponObject;
        private IWeapon m_Weapon = null;
        [SerializeField] private List<Weapon> m_WeaponPrefabs = new List<Weapon>();

        private void Start()
        {
            m_Actor = GetComponent<Actor>();
            m_Animator = GetComponentInChildren<Animator>();
            m_WeaponPrefabs = Resources.LoadAll<Weapon>("Weapons").ToList();
        }

        /// <summary>
        /// Add Weapon to players inventory
        /// </summary>
        /// <param name="weaponName"></param>
        public void AddWeapon(string weaponName)
        {
            foreach (var item in m_WeaponPrefabs)
            {
                if (item.name == weaponName)
                {
                    m_Actor.actorStats.meleeWeapons.Add(item.name, item.WeaponType);
                }
            }
        }

        private IWeapon GiveWeapon(string weaponName)
        {
            foreach (var item in m_WeaponPrefabs)
            {
                if (m_Actor.actorStats.meleeWeapons.ContainsKey(weaponName))
                {
                    Transform weaponPlacement = gameObject.GetComponentInChildren<WeaponPlacementPoint>().transform;
                    m_WeaponObject = Instantiate(item.WeaponMesh);
                    m_WeaponObject.transform.parent = weaponPlacement;
                    m_Weapon = m_WeaponObject.GetComponent<IWeapon>();
                    m_WeaponObject.transform.localPosition = m_Weapon.weaponAttackPoint;
                    m_WeaponObject.transform.localRotation = Quaternion.Euler(m_Weapon.weaponAttackRotation);
                    m_WeaponObject.transform.localScale = m_Weapon.weaponAttackScale;
                    return m_Weapon;
                }
            }
            return null;
        }

        /// <summary>
        /// Remove weapon from players inventory.
        /// </summary>
        /// <param name="weaponName"></param>
        public void RemoveWeapon(string weaponName)
        {
            if (m_Actor.actorStats.meleeWeapons.ContainsKey(weaponName))
            {
                m_Actor.actorStats.meleeWeapons.Remove(weaponName);
            }
        }

        private void DestroyWeapon()
        {
            if(m_Weapon != null && m_WeaponObject != null)
            {
                Destroy(m_WeaponObject);
                m_Weapon = null;
            }
        }

        /// <summary>
        /// Place weapon in players hand.
        /// </summary>
        /// <param name="weaponType"></param>
        public void ReadyWeapon(WeaponType weaponType)
        {
            m_WeaponType = weaponType;
            var currentWep = m_Actor.actorStats.meleeWeapons.Keys.ToList()[m_Actor.actorStats.currentMeleeWeapon];
            switch (m_WeaponType)
            {
                case WeaponType.OneHanded:
                    m_Animator.Play("OneHandedWithdraw");
                    Debug.Log($"Current Wep: {currentWep}");
                    m_Weapon = GiveWeapon(currentWep);
                    break;
            }
        }

#if UNITY_EDITOR
        [Button]
        private void ReadyWeaponEditor()
        {
            if(!m_Actor.actorStats.meleeWeapons.ContainsKey("AnotherSword"))
            {
                AddWeapon("AnotherSword");
            }
            ReadyWeapon(WeaponType.OneHanded);
        }
#endif

#if UNITY_EDITOR
        [Button]
#endif
        public void PutAwayWeapon()
        {
            switch(m_WeaponType)
            {
                case WeaponType.OneHanded:
                    m_Animator.Play("OneHandedSheath");
                    break;
            }
            m_WeaponType = WeaponType.None;
            DestroyWeapon();
        }

        public void FightingAnimation()
        {
            switch (m_WeaponType)
            {
                case WeaponType.OneHanded:
                    m_Animator.Play("OneHandedSheath");
                    break;
            }
        }

#if UNITY_EDITOR
        [Button]
#endif
        public void Attack01()
        {
            switch (m_WeaponType)
            {
                case WeaponType.OneHanded:
                    m_Animator.Play("OneHandedAttack01");
                    break;
            }
        }

#if UNITY_EDITOR
        [Button]
#endif
        public void MagicAttack()
        {
            switch (m_WeaponType)
            {
                case WeaponType.OneHanded:
                    m_Animator.Play("TwoHandMagicAttack");
                    break;
            }
        }

#if UNITY_EDITOR
        [Button]
#endif
        public void SwordAttack02()
        {
            switch (m_WeaponType)
            {
                case WeaponType.OneHanded:
                    m_Animator.Play("OneHandedAttack03");
                    break;
            }
        }
    }
}
