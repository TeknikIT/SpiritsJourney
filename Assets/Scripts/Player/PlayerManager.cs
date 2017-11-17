using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton
    public static PlayerManager instance;
    public int health;
    private float timer = 0.0f;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;
    public void KillPlayer()
    {
        LevelManager.instance.Reload();
    }
    private void Start()
    {
        health = 100;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public void TakeDamage(int damage)
    {
        if(timer >= 0.5)
        {
            health -= damage;
            if (health <= 0)
            {
                KillPlayer();
            }
            timer = 0;
        } 
    } 
}
