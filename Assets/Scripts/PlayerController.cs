using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float speed = 6.0F;
    public float gravity = 20.0F;
    public Vector3 originPosition;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 positionOnScreen, mouseOnScreen;
    GameObject bullet;
    GameObject arrow;
    void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        bullet = (GameObject)Resources.Load("Prefabs/Bullet");
        /*
        positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);
        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = Mathf.Atan2(positionOnScreen.x - mouseOnScreen.x, positionOnScreen.y - mouseOnScreen.y);
        Debug.Log(angle);*/
        if (Input.GetButton("Fire1"))
        {
            Aim();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Shoot();
        }
    }
    void Start()
    {
        originPosition = transform.position;
        arrow = gameObject.transform.Find("Arrow").gameObject;
        //arrow.transform.position = new Vector3(arrow.transform.position.x, 0, arrow.transform.position.z);
    }

    private void Shoot()
    {
        
        Debug.Log("BANG!");
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z));
        arrow.SetActive(false);
    }
    private void Aim()
    {
        arrow.SetActive(true);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrain")))
        {
            arrow.transform.LookAt(hit.point);
            arrow.transform.rotation = Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z);
        }
        Debug.DrawLine(transform.position, new Vector3(hit.point.x, 0, hit.point.z), Color.green, 10);
    }
}
