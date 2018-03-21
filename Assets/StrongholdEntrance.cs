using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongholdEntrance : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HubManager.instance.ToDungeon();
        }
    }
}
