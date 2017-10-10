using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 6.0F;
    public float gravity = 20.0F;
    public Vector3 originPosition;
    private Vector3 moveDirection = Vector3.zero;
    void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    void Start()
    {
        originPosition = transform.position;
    }

    private void Shoot()
    {
        
    }
}
