﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Melee attack, swing
/// </summary>
public class MeleeSwing : MonoBehaviour {

    float slashTime;
    float currentSlashTime;
    Vector3 startRot;
    Vector3 currentRot;
    Vector3 centerRot;
    Vector3 endRot;
    public Character createdBy;
    bool charLocked;
    public float damage;
    void Start()
    {
        slashTime = 0.15f;
        startRot = transform.eulerAngles;
        currentRot = startRot;
        endRot = new Vector3(startRot.x, startRot.y - 120f, startRot.z);
        charLocked = false;
        centerRot = (endRot - startRot) / 2;
        PlayerManager.instance.GetComponent<Animator>().SetBool("BasicAttacking", true);

    }
    // Update is called once per frame
    void Update () {
        if(!charLocked)
        {
            createdBy.ToggleCharacterMovement();
            charLocked = true;
        }
        currentSlashTime += Time.deltaTime;
        
        if(currentSlashTime > slashTime)
        {
            currentSlashTime = slashTime;
            transform.parent.gameObject.isStatic = false;
            createdBy.ToggleCharacterMovement();
            PlayerManager.instance.GetComponent<Animator>().SetBool("BasicAttacking", false);
            Destroy(gameObject);
        }
        float perc = currentSlashTime / slashTime;
        currentRot = new Vector3(startRot.x, Mathf.LerpAngle(startRot.y, endRot.y, perc), startRot.z);
        transform.eulerAngles = currentRot;

    }
    /// <summary>
    /// Used for debug
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(gameObject.GetComponent<Collider>().bounds.center, gameObject.GetComponent<Collider>().bounds.size);
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Enemy"))
        {
            c.gameObject.GetComponent<EnemyManager>().TakeDamageWithKnockback((int)damage, 10f,
                transform.TransformDirection(new Vector3(0, 0, 1)));
        }
        if (c.CompareTag("EnemyChild"))
        {
            c.gameObject.GetComponentInParent<EnemyManager>().TakeDamageWithKnockback((int)damage, 10f,
                transform.parent.TransformDirection(new Vector3(0, 0, 1)));

        }
    }
}
