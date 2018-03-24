using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwing : MonoBehaviour {

    float slashTime;
    float currentSlashTime;
    Vector3 startRot;
    Vector3 currentRot;
    Vector3 centerRot;
    Vector3 endRot;
    public Character createdBy;
    bool charLocked;
    void Start()
    {
        slashTime = 0.15f;
        startRot = transform.eulerAngles;
        currentRot = startRot;
        endRot = new Vector3(startRot.x, startRot.y - 120f, startRot.z);
        charLocked = false;
        centerRot = (endRot - startRot) / 2;

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
            Destroy(gameObject);
        }
        float perc = currentSlashTime / slashTime;
        currentRot = new Vector3(startRot.x, Mathf.LerpAngle(startRot.y, endRot.y, perc), startRot.z);
        transform.eulerAngles = currentRot;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(gameObject.GetComponent<Collider>().bounds.center, gameObject.GetComponent<Collider>().bounds.size);
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Enemy"))
        {
            Debug.Log(new Vector3(0, 0, -1));
            //Change to use aim direction instead!
            c.gameObject.GetComponent<EnemyManager>().TakeDamageWithKnockback(20, 10f,
                transform.TransformDirection(new Vector3(0, 0, 1)));
        }
        if (c.CompareTag("EnemyChild"))
        {
            c.gameObject.GetComponentInParent<EnemyManager>().TakeDamageWithKnockback(20, 10f,
                transform.TransformDirection(centerRot));

        }
    }
}
