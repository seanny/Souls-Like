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

        public void PlayWeaponAnim(WeaponType weaponType)
        {
            this.weaponType = weaponType;
            playAnim = true;
        }

        private void OnPlayWeaponAnim(WeaponType weaponType)
        {
            animator.SetInteger("WeaponType", (int)weaponType);
            if (playAnim)
            {
                string targetAnim = string.Empty;
                int rand;

                animator.SetBool("ShieldBlock", false);
                if (weaponType == WeaponType.OneHanded)
                {
                    rand = Random.Range(0, oneHandedAttacks.Length);
                    targetAnim = oneHandedAttacks[rand];
                }
                else if (weaponType == WeaponType.TwoHanded)
                {
                    rand = Random.Range(0, twoHandedAttacks.Length);
                    targetAnim = twoHandedAttacks[rand];
                }
                else
                {
                    playAnim = false;
                    return;
                }

                animator.CrossFade(targetAnim, 0.2f);
                playAnim = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            enableRootMotion = !animator.GetBool("CanMove");
            animator.applyRootMotion = enableRootMotion;

            animator.SetBool("LockOn", lockOn);

            if (enableRootMotion)
                return;

            OnPlayWeaponAnim(weaponType);

            animator.SetBool("ShieldBlock", shieldBlock);
        }
    }
}