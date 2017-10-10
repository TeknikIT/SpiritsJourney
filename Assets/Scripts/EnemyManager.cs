using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int health;
    private void Start()
    {
        health = 100;
    }
    public void Kill()
    {

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Kill();
        }
    }
}
