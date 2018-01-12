using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls the enemy's movement
/// </summary>

public class EnemyController : MonoBehaviour {
    private Transform Player; //The player's transform
    public int moveSpeed = 5; // Movementspeed of the enemy
    public int minDistance = 1; //The minimum distance the enemy strives to get to
    public float gravity = 20.0F; //Gravitymodifier
    public bool isActive; //Displays if the character should move
    public float timer; // timer used for counting
    private Vector3 moveDirection = Vector3.zero; //A vector of the movement direction
    private CharacterController controller; //The charactercontroller component on the enemty
    //Mostly used for movement
    private IEnumerator coroutine; //A coroutine. Allows code to run simulaniously
    //Especially knockback

    //Used for initialization
    void Start()
    {
        Player = PlayerManager.instance.transform;
        isActive = false; //Default state of the enemy.
        //Makes sure that the enemy doesnt move before a character hasn't entered the room.
    }

    // Update is called once per frame
    void Update()
    {
        controller = GetComponent<CharacterController>(); //Gets the character controller
        if (isActive) //Checks if the character should move
        {
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
            controller.Move(moveDirection * Time.deltaTime);
            
        }  
        
    }
    //Starts the knockback coroutine
    public void StartKnockback(float strength, Vector3 hitDirection)
    {
        timer = 0;
        coroutine = Knockback(strength, hitDirection);
        StartCoroutine(coroutine);
    }

    private IEnumerator Knockback(float strength, Vector3 hitDirection)
    {
        isActive = false; //Makes the character not move during knockback
        while (timer < 0.1) //moves during the 0.1 seconds
        {
            timer += Time.deltaTime;
            //Moves the character during the knockback
            yield return controller.Move(hitDirection * Time.deltaTime * strength);
        }
        isActive = true;//Makes the character move again
    }
}
