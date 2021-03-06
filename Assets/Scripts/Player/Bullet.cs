﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float moveSpeed = 10f;
    public int damage = 1;
    private float timer;
    public float maxAliveTime;
    public Vector3 movementDirection;
	// Use this for initialization
	void Start () {
        movementDirection = Vector3.forward;
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
            other.GetComponent<EnemyManager>().TakeDamageWithKnockback(damage, 5f, transform.TransformDirection(new Vector3(0, 0, 1)));
        }
        if(other.tag == "EnemyChild")
        {
            other.transform.parent.GetComponent<EnemyManager>().TakeDamageWithKnockback(damage, 5f, transform.parent.TransformDirection(new Vector3(0, 0, 1)));
        }
        if (other.tag == "Utillity" || other.tag == "Player")
        {
            //EVERYITHING EXEPCT UNTILITY AND PLAYER
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
