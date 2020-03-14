using UnityEngine;

namespace SoulsLike
{
    public class Sword : MonoBehaviour, IWeapon
    {
        private Transform holder;
        Actor holdingActor;
        public bool IsAttacking;

        public WeaponType weaponType { get => WeaponType.OneHanded; }
        public float weaponDamage { get => 1.5f; }

        [SerializeField] private Vector3 m_WeaponAttackPoint = new Vector3(0, 0, -0.02f);
        public Vector3 weaponAttackPoint => m_WeaponAttackPoint;

        [SerializeField] private Vector3 m_WeaponAttackRotation = new Vector3(0, 0, -0.02f);
        public Vector3 weaponAttackRotation => m_WeaponAttackRotation;

        [SerializeField] private Vector3 m_WeaponAttackScale = new Vector3(0.1f, 0.1f, 0.1f);
        public Vector3 weaponAttackScale => m_WeaponAttackScale;

        // Start is called before the first frame update
        void Start()
        {
            holder = transform.root;
            holdingActor = holder.GetComponent<Actor>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsAttacking && other.transform != holder)
            {
                if (other.gameObject.TryGetComponent(out Actor actor))
                {
                    Debug.Log($"{holder.name} attacking {other.name}");
                    OnWeaponAttack(actor);
                    IsAttacking = false;
                }
            }
        }

        public void OnWeaponAttack(Actor actor)
        {
            actor.OnActorAttacked(holdingActor, weaponDamage);
        }

        public void OnWeaponEquip()
        {
            throw new System.NotImplementedException();
        }

        public void OnWeaponStore()
        {
            throw new System.NotImplementedException();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.25f);
            Gizmos.DrawSphere(transform.position, 1.5f);
        }
    }
}