using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blades : MonoBehaviour {

    public float rotationSpeed = 10f;
    public int damageCoefficient = 10;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotationSpeed);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.instance.TakeDamage(damageCoefficient);
        }
    }
}
