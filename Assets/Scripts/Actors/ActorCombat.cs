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
        private Animator m_Animator;
        [SerializeField] private WeaponType m_WeaponType;
        private GameObject m_WeaponObject;
        private IWeapon m_Weapon;
        private List<Weapon> m_WeaponPrefabs = new List<Weapon>();

        private void Start()
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_WeaponPrefabs = Resources.LoadAll<Weapon>("Weapons").ToList();
        }

        private IWeapon GiveWeapon(string weaponName)
        {
            foreach(var item in m_WeaponPrefabs)
            {
                if(item.name == weaponName)
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

        public void ReadyWeapon(WeaponType weaponType)
        {
            m_WeaponType = weaponType;
            switch(m_WeaponType)
            {
                case WeaponType.OneHanded:
                    m_Animator.Play("OneHandedWithdraw");
                    m_Weapon = GiveWeapon("TestSword");
                    break;
            }
        }

#if UNITY_EDITOR
        [Button]
        private void ReadyWeaponEditor()
        {
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
