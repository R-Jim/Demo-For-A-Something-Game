using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChange : MonoBehaviour {

    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
        {
            animator.SetInteger("Stage", 1);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetInteger("Stage",0);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetInteger("Stage", 2);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            animator.SetInteger("Stage", 0);
        }
    }
}
