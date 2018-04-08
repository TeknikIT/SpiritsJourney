using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pickup handler
/// </summary>
public class PickUp : MonoBehaviour {

    public BuffItem buffItem;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.instance.GetComponent<Character>().AddPickupItem(buffItem);
            Destroy(gameObject);
        }
    }
}
