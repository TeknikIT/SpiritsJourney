using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to cull the amount of particles off screen
/// </summary>
public class CustomParticleCulling : MonoBehaviour {

    public float cullingRadius = 10; //Radius of the culling
    public ParticleSystem target; // The targeted particle system

    private CullingGroup m_CullingGroup; // The culling group

	void Start () {

        m_CullingGroup = new CullingGroup
        {
            targetCamera = Camera.main //Setting the camera
        };
        m_CullingGroup.SetBoundingSpheres(new BoundingSphere[] { new BoundingSphere(transform.position, cullingRadius) });
        m_CullingGroup.SetBoundingSphereCount(1);
        m_CullingGroup.onStateChanged += OnStateChanged;
    }

    void OnStateChanged(CullingGroupEvent sphere)
    {
        //Check if the particle system is within the screen
        if (sphere.isVisible)
        {
            target.Play(true);
        }
        else
        {
            target.Pause();
        }
    }

    void OnDestroy()
    {
        if (m_CullingGroup != null)
            m_CullingGroup.Dispose();
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, cullingRadius);
    }

}
