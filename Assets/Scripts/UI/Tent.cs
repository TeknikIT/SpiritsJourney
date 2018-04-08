using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to test some functionally with colliders and stores
/// </summary>
public class Tent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.Find("Options").gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.Find("Options").gameObject.SetActive(false);
        }
    }
}
