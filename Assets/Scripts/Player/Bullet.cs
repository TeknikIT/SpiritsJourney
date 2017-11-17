using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float moveSpeed = 10f;
    public int damage = 25;
    private float timer;
    public float maxAliveTime;
    public Vector3 movementDirection;
	// Use this for initialization
	void Start () {
        movementDirection = Vector3.forward;
        Debug.Log(movementDirection);
        timer = 0;
	}
    
	
	// Update is called once per frame
	void Update () {
        //transform.position += movementDirection * moveSpeed * Time.deltaTime;
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.Self);
        timer += Time.deltaTime;
        if(timer > maxAliveTime)
        {
            Debug.Log("Destroyed!");
            Destroy(gameObject);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyManager>().TakeDamage(damage);
        }
        if(other.tag == "EnemyChild")
        {
            other.GetComponentInParent<EnemyManager>().TakeDamage(damage);
        }
        if(other.tag == "Utillity" || other.tag == "Player")
        {
            //EVERYITHING EXECT UNTILITY AND PLAYER
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
