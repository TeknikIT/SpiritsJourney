using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float movSpeed = 3f;
    public float rotSpeed = 150f;
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * movSpeed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
	}
}
