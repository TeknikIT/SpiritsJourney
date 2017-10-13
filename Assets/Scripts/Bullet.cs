using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float moveSpeed = 10f;
    public int damage = 25;
    private Vector3 movementDirection;
	// Use this for initialization
	void Start () {
        movementDirection = Vector3.forward;	
	}
    
	
	// Update is called once per frame
	void Update () {
        transform.position += movementDirection * moveSpeed * Time.deltaTime;
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyManager>().TakeDamage(damage);
        }
    }
}
