using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotating the blades of the temporary Enemy
/// and dealing damage to the player
/// </summary>
public class Blades : MonoBehaviour {


    public float rotationSpeed = 10f; //The speed at which the blade rotates at in degrees per tick
    public int damage; // The damage that the blade inflicts
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotationSpeed);
        damage = (int)transform.parent.GetComponent<EnemyManager>().damage;
	}

    private void OnTriggerEnter(Collider other)
    {
        //Check if player has entered the collider of the blade
        if(other.tag == "Player")
        {
            PlayerManager.instance.TakeDamage(damage); //Calling the Take damage funkction on the instance of the player
        }
    }
}
