using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed;
    public float gravity = 20.0F;
    public Character character;
    public Vector3 moveDirection = Vector3.zero;
    public Vector3 originPosition;
    public bool movementIsLocked = false;
    CharacterController controller;
    void Update () {
        speed = character.moveSpeed;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(moveDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(moveDirection);
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;
        if (!movementIsLocked)
        {
            controller.Move(Vector3.ClampMagnitude(moveDirection, speed) * Time.deltaTime);
            GetComponent<Animator>().SetFloat("Velocity", Vector3.Distance(Vector3.zero, controller.velocity));
        }
        else
        {
            GetComponent<Animator>().SetFloat("Velocity", 0);
        }
       
        //transform.Translate(moveDirection * speed * Time.deltaTime, Space.Self);
        
       

    }
    void Start()
    {
        originPosition = transform.position;
        controller = GetComponent<CharacterController>();
        character = gameObject.GetComponent<Character>();
        

        //arrow.transform.position = new Vector3(arrow.transform.position.x, 0, arrow.transform.position.z);
    }

}
