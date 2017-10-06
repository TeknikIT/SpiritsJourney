using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float cameraHeight = 12f;
    public float cameraDistance = 5.5f;
    //public float cameraTilt = -40f;
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = player.transform.position;
        pos.y += cameraHeight;
        pos.z -= cameraDistance;
        transform.position = pos;
        transform.LookAt(player.transform);
	}
}
