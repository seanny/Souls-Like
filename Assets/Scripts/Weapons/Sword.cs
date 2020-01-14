using UnityEngine;

namespace SoulsLike
{
    public class Sword : MonoBehaviour
    {
        private Transform holder;
        public bool IsAttacking;

        // Start is called before the first frame update
        void Start()
        {
            holder = transform.root;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsAttacking && other.transform != holder)
            {
                if (other.gameObject.TryGetComponent(out Actor actor))
                {
                    Debug.Log($"{holder.name} attacking {other.name}");
                    IsAttacking = false;
                }
            }
        }
    }
}