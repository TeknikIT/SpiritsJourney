using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 6.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public Vector3 originPosition;
    CharacterController controller;
    void Update () {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(moveDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDirection);
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(Vector3.ClampMagnitude(moveDirection, speed) * Time.deltaTime);
        GetComponent<Animator>().SetFloat("Velocity", Vector3.Distance(Vector3.zero, controller.velocity));
        //transform.Translate(moveDirection * speed * Time.deltaTime, Space.Self);
        
       

    }
    void Start()
    {
        originPosition = transform.position;
        controller = GetComponent<CharacterController>();
        //arrow.transform.position = new Vector3(arrow.transform.position.x, 0, arrow.transform.position.z);
    }

}
