using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private Transform Player;
    public int moveSpeed = 5;
    public int minDistance = 1;
    public float gravity = 20.0F;
    public bool isActive, isKnockbacked;
    private float timer;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    // Use this for initialization
    void Start()
    {
        Player = PlayerManager.instance.transform;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        controller = GetComponent<CharacterController>();
        if (isActive)
        {
            transform.LookAt(Player);
            if (Vector3.Distance(transform.position, Player.position) >= minDistance)
            {
                moveDirection = transform.forward * moveSpeed;
            }
            controller.Move(moveDirection * Time.deltaTime);
            moveDirection.y -= gravity * Time.deltaTime;
        }  
        
    }

    public void Knockback(float strength, Vector3 hitDirection)
    {
        isActive = false;
        timer += Time.deltaTime;
        isKnockbacked = true;
        if (timer >= 0.2)
        {
            isKnockbacked = false;
            timer = 0;
            isActive = true;
        }
        else
        {
            controller.Move(hitDirection * -1 * Time.deltaTime * strength);
        }
        if (isKnockbacked)
        {
            Knockback(strength, hitDirection);
        }
    }
}
