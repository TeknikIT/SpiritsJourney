using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwing : MonoBehaviour {

    float slashTime;
    float currentSlashTime;
    float rotateDistance;
    Quaternion startRot;
    Quaternion endRot;
    void Start() {
        slashTime = 0.5f;
        rotateDistance = 120f;
        startRot = Quaternion.Euler(new Vector3(0, 60, 0));
        endRot = Quaternion.Euler(transform.rotation.x, transform.rotation.y + rotateDistance, transform.rotation.z);
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentSlashTime = 0f;
        }
        currentSlashTime += Time.deltaTime;
        if(currentSlashTime > slashTime)
        {
            currentSlashTime = slashTime;
        }
        float perc = currentSlashTime / slashTime;
        transform.rotation = Quaternion.Lerp(startRot, endRot, perc);
	}
}
