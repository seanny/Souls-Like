using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public enum WeaponType
    {
        None = 0,
        OneHanded,
        TwoHanded,
        Bow,
    };

    public enum BowStage
    {
        Idle = 0,
        Aiming,
        Fire
    };

    [Range(0, 1)]
    public float vertical;

    public bool playAnim;
    public string[] oneHandedAttacks;
    public string[] twoHandedAttacks;
    public string bowIdle;
    public string bowAimIdle;
    public string bowAimFire;

    public WeaponType weaponType;
    public BowStage bowStage;
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
        if (enableRootMotion)
            return;

        animator.SetInteger("WeaponType", (int)weaponType);
        if(playAnim)
        {
            string targetAnim = string.Empty;
            int rand;

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
            else if (weaponType == WeaponType.Bow)
            {
                if(bowStage == BowStage.Aiming)
                {
                    animator.SetBool("AimingBow", true);
                    targetAnim = bowAimIdle;
                }
                else if (bowStage == BowStage.Fire)
                {
                    animator.SetBool("AimingBow", false);
                    targetAnim = bowAimIdle;
                    bowStage = BowStage.Idle;
                }
                else targetAnim = bowIdle;
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

        animator.SetFloat("Vertical", vertical);
    }
}
