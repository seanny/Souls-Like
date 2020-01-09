using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class Sword : MonoBehaviour
    {
        public Transform holder;
        public bool isAttacking;

        // Start is called before the first frame update
        void Start()
        {
            holder = transform.root;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"OnTriggerEnter({other.name})");
            if (isAttacking && other.transform != holder)
            {
                Debug.Log($"Attacking {other.name}");
                isAttacking = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"OnCollisionEnter({collision.gameObject.name})");
        }
    }
}