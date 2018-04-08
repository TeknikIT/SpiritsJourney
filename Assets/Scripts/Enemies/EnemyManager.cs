using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages an enemy's health and status
/// </summary>
public class EnemyManager : MonoBehaviour {
    //Health variables
    public int baseHealth;
    public int health;

    //Movementspeed
    public  float baseMovementSpeed;
    public float movementSpeed;
    public float maxMovementSpeed;

    //Damage
    public float baseDamage;
    public float damage;
    public float maxDamage;

    //The room in which the enemy resides in
    public BaseRoomManager room;

    private float timer; //Used for timing and delay

    private void Start()
    {
        
    }


    private void Update()
    {
        timer += Time.deltaTime;
    }
    /// <summary>
    /// Removes the enemy
    /// </summary>
    public void Kill()
    {
        room.monsters.Remove(this.gameObject);
        Destroy(gameObject);
    }
    /// <summary>
    /// Removes health from the enemy
    /// </summary>
    /// <param name="damage">The damage that will be applied</param>
    public void TakeDamage(int damage)
    {
        if(timer > 0.5f)
        {
            health -= damage;
            if (health <= 0)
            {
                Kill();
            }
            timer = 0;
            Debug.Log(health);
        }

    }
    /// <summary>
    /// Removes health and applies knockback to the enemy
    /// </summary>
    /// <param name="damage">Damage to be applied</param>
    /// <param name="knockbackForce">The force of the knockback</param>
    /// <param name="knockbackDirection">The direction that the knockback should be applied in</param>
    public void TakeDamageWithKnockback(int damage, float knockbackForce, Vector3 knockbackDirection)
    {
        if(timer > 0.5f)
        {
            health -= damage;
            GetComponent<EnemyController>().StartKnockback(knockbackForce, knockbackDirection);
            if (health <= 0)
            {
                Kill();
            }
            timer = 0;
            Debug.Log(health);
        }
    }
}
