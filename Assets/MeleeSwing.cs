using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwing : MonoBehaviour {

    float slashTime;
    float currentSlashTime;
    Vector3 startRot;
    Vector3 currentRot;
    Vector3 endRot;
    void Start()
    {
        slashTime = 0.5f;
        startRot = transform.eulerAngles;
        currentRot = startRot;
        endRot = new Vector3(startRot.x, startRot.y - 120f, startRot.z);
    }
	// Update is called once per frame
	void Update () {
        currentSlashTime += Time.deltaTime;
        if(currentSlashTime > slashTime)
        {
            currentSlashTime = slashTime;
            transform.parent.gameObject.isStatic = false;
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
            c.gameObject.GetComponent<EnemyManager>().TakeDamageWithKnockback(50, 10f,
                transform.TransformDirection(PlayerManager.instance.GetComponent<PlayerController>().moveDirection));
        }
        if (c.CompareTag("EnemyChild"))
        {
            c.gameObject.GetComponentInParent<EnemyManager>().TakeDamageWithKnockback(50, 10f,
                transform.TransformDirection(PlayerManager.instance.GetComponent<PlayerController>().moveDirection));

        }
    }
}
