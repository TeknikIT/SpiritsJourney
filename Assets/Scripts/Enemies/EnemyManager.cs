using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int health;
    public RoomManager room;
    private void Start()
    {
        health = 100;
        
    }
    public void Kill()
    {
        room.monsters.Remove(this.gameObject);
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }
    public void TakeDamageWithKnockback(int damage, float knockbackForce, Vector3 knockbackDirection)
    {
        health -= damage;
        GetComponent<EnemyController>().Knockback(knockbackForce, knockbackDirection);
        if (health <= 0)
        {
            Kill();
        }
    }
}
