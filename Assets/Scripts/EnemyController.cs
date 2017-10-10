using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private Transform Player;
    public int moveSpeed = 5;
    public int minDistance = 1;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        Player = PlayerManager.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) >= minDistance)
        {
            moveDirection = transform.forward * moveSpeed;
        }
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;
    }
}
