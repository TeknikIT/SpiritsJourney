using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public string characterName;
    public int health;
    public GameObject arrow;
    public float[] coolDowns, timeStamps;
    private void Start()
    {
        arrow = gameObject.transform.Find("Arrow").gameObject;
        coolDowns = new float[5];
        timeStamps = new float[5];
    }

    public virtual void BasicAbility()
    {
        if (timeStamps[0] <= Time.time)
        {
            timeStamps[0] = Time.time + coolDowns[0];
        }
        else
        {
            
        }
    }

    public virtual void CharacterSpecificAbility()
    {
        if (timeStamps[1] <= Time.time)
        {
            timeStamps[1] = Time.time + coolDowns[1];
        }
        else
        {
            return;
        }
    }

    public virtual void RecoveryAbility()
    {
        if (timeStamps[2] <= Time.time)
        {
            timeStamps[2] = Time.time + coolDowns[2];
        }
        else
        {
            return;
        }
    }

    public virtual void UtilityAbility()
    {
        if (timeStamps[3] <= Time.time)
        {
            timeStamps[3] = Time.time + coolDowns[3];
        }
        else
        {
            return;
        }
    }

    public virtual void SpecialAbility()
    {
        if (timeStamps[4] <= Time.time)
        {
            timeStamps[4] = Time.time + coolDowns[4];
        }
        else
        {
            return;
        }
    }

    public virtual void Aim()
    {
        arrow.SetActive(true);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrain")))
        {
            arrow.transform.LookAt(hit.point);
            arrow.transform.rotation = Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z);
        }
        //Debug.DrawLine(transform.position, new Vector3(hit.point.x, 0, hit.point.z), Color.green, 10);
    }


}
