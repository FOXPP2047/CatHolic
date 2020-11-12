using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();

        animator.SetBool("clicked", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!rigidbody.useGravity)
        {
            animator.SetBool("clicked", true);
        }
        else
        {
            animator.SetBool("clicked", false);
        }
    }
}
