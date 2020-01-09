using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulsLike
{
    public class Helper : MonoBehaviour
    {
        public enum WeaponType
        {
            None = 0,
            OneHanded,
            TwoHanded
        };

        [Range(-1, 1)]
        public float vertical;
        [Range(-1, 1)]
        public float horizontal;

        public bool shieldBlock;
        public bool lockOn;

        public bool playAnim;
        public string[] oneHandedAttacks;
        public string[] twoHandedAttacks;

        public WeaponType weaponType;
        public bool enableRootMotion;

        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            enableRootMotion = !animator.GetBool("CanMove");
            animator.applyRootMotion = enableRootMotion;

            if (!lockOn)
            {
                horizontal = 0;
                vertical = Mathf.Clamp01(vertical);
            }

            animator.SetBool("LockOn", lockOn);

            if (enableRootMotion)
                return;

            animator.SetInteger("WeaponType", (int)weaponType);
            if (playAnim)
            {
                string targetAnim = string.Empty;
                int rand;

                animator.SetBool("ShieldBlock", false);
                if (weaponType == WeaponType.OneHanded)
                {
                    rand = Random.Range(0, oneHandedAttacks.Length - 1);
                    targetAnim = oneHandedAttacks[rand];

                    if (vertical > 0.5f)
                        targetAnim = oneHandedAttacks[oneHandedAttacks.Length - 1];
                }
                else if (weaponType == WeaponType.TwoHanded)
                {
                    rand = Random.Range(0, twoHandedAttacks.Length);
                    targetAnim = twoHandedAttacks[rand];

                    if (vertical > 0.5f)
                        targetAnim = twoHandedAttacks[twoHandedAttacks.Length - 1];
                }
                else
                {
                    playAnim = false;
                    return;
                }

                vertical = 0;
                animator.CrossFade(targetAnim, 0.2f);
                playAnim = false;
            }

            animator.SetBool("ShieldBlock", shieldBlock);
            animator.SetFloat("Vertical", vertical);
            animator.SetFloat("Horizontal", horizontal);
        }
    }
}