using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticleCulling : MonoBehaviour {
    public float cullingRadius = 10;
    public ParticleSystem target;

    private CullingGroup m_CullingGroup;

	void Start () {
        m_CullingGroup = new CullingGroup();
        m_CullingGroup.targetCamera = Camera.main;
        m_CullingGroup.SetBoundingSpheres(new BoundingSphere[] { new BoundingSphere(transform.position, cullingRadius) });
        m_CullingGroup.SetBoundingSphereCount(1);
        m_CullingGroup.onStateChanged += OnStateChanged;
    }

    void OnStateChanged(CullingGroupEvent sphere)
    {
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
