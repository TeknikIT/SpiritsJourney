using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages an enemy's health and status
/// </summary>
public class EnemyManager : MonoBehaviour {

    public int health;
    public RoomManager room;
    private void Start()
    {
        health = 100;
        
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
        health -= damage;
        if (health <= 0)
        {
            Kill();
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
        health -= damage;
        GetComponent<EnemyController>().StartKnockback(knockbackForce, knockbackDirection);
        if (health <= 0)
        {
            Kill();
        }
    }
}
