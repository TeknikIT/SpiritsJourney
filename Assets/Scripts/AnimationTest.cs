using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        Animator anim = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.A) && anim.GetBool("IsOpen"))
        {
            anim.SetBool("IsOpen", false);
        }
        else if(Input.GetKeyDown(KeyCode.A) && !anim.GetBool("IsOpen"))
        {
            anim.SetBool("IsOpen", true);
        }
	}
}
