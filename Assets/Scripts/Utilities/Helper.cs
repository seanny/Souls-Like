using System.Xml;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

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
        public List<string> oneHandedAttacks;
        public List<string> twoHandedAttacks;

        public WeaponType weaponType;
        public bool enableRootMotion;

        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            oneHandedAttacks.Clear();
            twoHandedAttacks.Clear();
            XmlTextReader reader = new XmlTextReader(Path.Combine(Application.streamingAssetsPath, "OneHanded.xml"));
            while (reader.Read())
            {
                // Do some work here on the data.
                if(reader.Name == "anim")
                {
                    oneHandedAttacks.Add(reader.ReadInnerXml());
                }
            }
            reader = new XmlTextReader(Path.Combine(Application.streamingAssetsPath, "TwoHanded.xml"));
            while (reader.Read())
            {
                // Do some work here on the data.
                if (reader.Name == "anim")
                {
                    twoHandedAttacks.Add(reader.ReadInnerXml());
                }
            }
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
                    rand = Random.Range(0, oneHandedAttacks.Count);
                    targetAnim = oneHandedAttacks[rand];
                }
                else if (weaponType == WeaponType.TwoHanded)
                {
                    rand = Random.Range(0, twoHandedAttacks.Count);
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