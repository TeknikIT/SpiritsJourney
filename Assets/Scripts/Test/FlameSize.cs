using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for setting the correct size on the flame
/// </summary>
public class FlameSize : MonoBehaviour {

	void Start () {
        Rescale(transform.parent.localScale);
        
	}

    private void Rescale(Vector3 parentScale)
    {
        transform.localScale = Vector3.Scale(transform.localScale, parentScale);
    }
	
}
