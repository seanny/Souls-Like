using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    [Range(0, 1)]
    public float vertical;

    public bool playAnim;
    public string[] oneHandedAttacks;
    public string[] twoHandedAttacks;

    public bool twoHanded;
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

        animator.SetBool("TwoHanded", twoHanded);
        if(playAnim)
        {
            string targetAnim;
            int rand;

            if (!twoHanded)
            {
                rand = Random.Range(0, oneHandedAttacks.Length);
                targetAnim = oneHandedAttacks[rand];
            }
            else
            {
                rand = Random.Range(0, twoHandedAttacks.Length);
                targetAnim = twoHandedAttacks[rand];
            }

            vertical = 0;
            animator.CrossFade(targetAnim, 0.2f);
            animator.SetBool("CanMove", false);
            enableRootMotion = true;
            playAnim = false;
        }

        animator.SetFloat("Vertical", vertical);
    }
}
