using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerManager : MonoBehaviour {

    #region Singleton
    public static PlayerManager instance;
    

    private void Awake()
    {
        instance = this;
    }
    #endregion
    /// <summary>
    /// Used for keeping track over health and damage
    /// </summary>
    public GameObject player;
    public Character character;
    public int health;
    private float timer = 0.0f;
    public bool movementLocked = false, locked = false;
    private Vector3 holdPosition;
    public void KillPlayer()
    {
        GameManager.instance.PlayerDied();
    }
    private void Start()
    {
        character = GetComponent<Character>();
        health = character.health;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(GameObject.Find("Text") != null && character.characterName != "Cursor")
            GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Health: " + health;
        if (movementLocked)
        {
            holdPosition = transform.position;
            locked = true;
        }
        if (locked)
        {
            transform.position = holdPosition;
        }
        health = character.health;
    }
    public void TakeDamage(int damage)
    {
        if(timer >= 0.5)
        {
            character.health -= damage;
            if (health <= 0)
            {
                KillPlayer();
            }
            timer = 0;
        } 
    } 
}
