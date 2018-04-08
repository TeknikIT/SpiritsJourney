using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Depricated
/// </summary>

public class AnimationTest : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        Animator anim = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.O) && anim.GetBool("IsOpen"))
        {
            anim.SetBool("IsOpen", false);
        }
        else if(Input.GetKeyDown(KeyCode.O) && !anim.GetBool("IsOpen"))
        {
            anim.SetBool("IsOpen", true);
        }

        //Karolina var här!
	}
}
