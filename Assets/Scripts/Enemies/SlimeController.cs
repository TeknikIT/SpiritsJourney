using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController {

    public override void Movement()
    {
        controller = GetComponent<CharacterController>(); //Gets the character controller
        if (isActive) //Checks if the character should move
        {
            transform.LookAt(new Vector3(Player.position.x, gameObject.GetComponent<Collider>().bounds.center.y, Player.position.z));
            //Rotates towards the player and sets the y coordinate to the enemies y coord.
            transform.LookAt(new Vector3(Player.position.x, transform.position.y, Player.position.z));
            //Resets the movement direction
            moveDirection = Vector3.zero;
            //Checks if the distance to the player is greater then the minimum allowed distance
            if (Vector3.Distance(transform.position, Player.position) >= minDistance)
            {
                //Sets the movement direction toward the player
                moveDirection = transform.forward * moveSpeed;
            }
            //Applies gravity
            moveDirection.y -= gravity;
            //Moves the enemy
            controller.Move(moveDirection * Time.deltaTime * transform.Find("AnimationModel").GetComponent<Animator>().GetFloat("SlimeVelocity"));

        }

        transform.Find("AnimationModel").GetComponent<Animator>().SetFloat("Velocity", Vector3.Distance(Vector3.zero, new Vector3(moveDirection.x, 0, moveDirection.z)));
        transform.Find("AnimationModel").GetComponent<Animator>().SetFloat("DistanceToChar", Vector3.Distance(transform.position, Player.position));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.instance.TakeDamage(20);
        }
    }
}
