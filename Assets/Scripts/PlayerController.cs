using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 6.0F;
    public float gravity = 20.0F;
    public Vector3 originPosition;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 positionOnScreen, mouseOnScreen;
    Object bullet;
    void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        bullet = Resources.Load("Prefabs/Bullet");
        positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);
        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = Mathf.Atan2(positionOnScreen.x - mouseOnScreen.x, positionOnScreen.y - mouseOnScreen.y);
        Debug.Log(angle);
        if (Input.GetButton("Fire1"))
        {
            
            Shoot(angle);
        }
    }
    void Start()
    {
        originPosition = transform.position;
    }

    private void Shoot(float angle)
    {
        
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 0f, angle));
    }
}
