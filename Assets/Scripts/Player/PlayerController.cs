using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 6.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public Vector3 originPosition;
    void Update () {

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.rotation = Quaternion.LookRotation(moveDirection);
        //moveDirection.y -= gravity * Time.deltaTime;
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.Self);
       

    }
    void Start()
    {
        originPosition = transform.position;
        //arrow.transform.position = new Vector3(arrow.transform.position.x, 0, arrow.transform.position.z);
    }

}
