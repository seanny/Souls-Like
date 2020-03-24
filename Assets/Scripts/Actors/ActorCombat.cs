using UnityEngine;

namespace SoulsLike
{

    public class ActorCombat : MonoBehaviour
    {
        private Animator m_Animator;

        private void Start()
        {
            m_Animator = GetComponentInChildren<Animator>();
        }

        public void SwordAttack()
        {
            m_Animator.SetBool("Attacking", true);
        }
    }
}
