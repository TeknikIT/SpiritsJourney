using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEntrence : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //HubManager.instance.ToPreviousScene();
        }
    }
}
