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
    }
}