using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunWalkController : MonoBehaviour {

    public void ChangeState()
    {
        Animator animator = RayCast.aimingObject.transform.parent.GetComponent<Animator>();
        animator.SetBool("Crouch", !animator.GetBool("Crouch"));
    }
}
