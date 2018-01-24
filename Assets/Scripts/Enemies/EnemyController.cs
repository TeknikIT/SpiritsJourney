using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private Transform Player;
    public int moveSpeed = 5;
    public int minDistance = 1;
    public float gravity = 20.0F;
    public bool isActive, isKnockbacked;
    public float timer;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private IEnumerator coroutine;
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
            transform.LookAt(new Vector3(Player.position.x, gameObject.GetComponent<Collider>().bounds.center.y, Player.position.z));
            if (Vector3.Distance(transform.position, Player.position) >= minDistance)
            {
                moveDirection = transform.forward * moveSpeed;
            }
            controller.Move(moveDirection * Time.deltaTime);
            moveDirection.y -= gravity * Time.deltaTime;
        }  
        
    }

    public void StartKnockback(float strength, Vector3 hitDirection)
    {
        timer = 0;
        coroutine = Knockback(strength, hitDirection);
        StartCoroutine(coroutine);
    }

    private IEnumerator Knockback(float strength, Vector3 hitDirection)
    {
        isActive = false;
        while (timer < 0.1)
        {
            timer += Time.deltaTime;
            yield return controller.Move(hitDirection * Time.deltaTime * strength);
        }
        isActive = true;
    }
}
