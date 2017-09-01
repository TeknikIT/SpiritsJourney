using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    public float minIntensity;
    public float maxIntensity;


    // Update is called once per frame
    void Update () {
        if(Random.Range(0,100) <= 10)
            gameObject.GetComponent<Light>().intensity = Random.Range(minIntensity, maxIntensity);
	}
}
