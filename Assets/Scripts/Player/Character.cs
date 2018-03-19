using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class for all characters
/// </summary>


public class Character : MonoBehaviour {

    public string characterName; //Name of the character
    public int health; //Character health
    public float moveSpeed;
    public float damageModifier;
    public GameObject arrow, cone; //The aim arrow/cone
    public float[] coolDowns, timeStamps; //Arrays containing both coolDowns and the last time the abillity was used
    
    public List<BuffItem> buffItems; 

    private void Start()
    {
        //Initializing arrow and cd arrays
        arrow = gameObject.transform.Find("Arrow").gameObject;
        cone = gameObject.transform.Find("Cone").gameObject;
        coolDowns = new float[5];
        timeStamps = new float[5];
    }


    //Basic abillity, left mouse button
    public virtual bool BasicAbility()
    {
        if (timeStamps[0] <= Time.time)
        {
            timeStamps[0] = Time.time + coolDowns[0];
            return true;
        }
        else
        {
            return false;
        }
    }

    //Character Specific Ability, right mouse button
    public virtual bool CharacterSpecificAbility()
    {
        if (timeStamps[1] <= Time.time)
        {
            timeStamps[1] = Time.time + coolDowns[1];
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool RecoveryAbility()
    {
        if (timeStamps[2] <= Time.time)
        {
            timeStamps[2] = Time.time + coolDowns[2];
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool UtilityAbility()
    {
        if (timeStamps[3] <= Time.time)
        {
            timeStamps[3] = Time.time + coolDowns[3];
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool SpecialAbility()
    {
        if (timeStamps[4] <= Time.time)
        {
            timeStamps[4] = Time.time + coolDowns[4];
            return true;
        }
        else
        {
            return false;
        }
    }

    //The function used for aiming
    public virtual void AimArrow()
    {
        arrow.SetActive(true);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrain")))
        {
            arrow.transform.LookAt(hit.point);
            arrow.transform.rotation = Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z);
        }
    }
    public virtual void AimCone(float scale)
    {
        cone.SetActive(true);
        cone.transform.localScale = Vector3.one * scale;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1 << LayerMask.NameToLayer("Terrain")))
        {
            cone.transform.LookAt(hit.point);
            cone.transform.rotation = Quaternion.Euler(0, cone.transform.rotation.eulerAngles.y, cone.transform.rotation.eulerAngles.z);
        }
    }
    public virtual void ToggleCharacterMovement()
    {
        PlayerController pc = PlayerManager.instance.GetComponent<PlayerController>();
        if (pc.movementIsLocked)
            pc.movementIsLocked = false; 
        else
            pc.movementIsLocked = true;
    }

    public virtual void AddPickupItem(BuffItem buffItem)
    {
        buffItems.Add(buffItem);
        health += buffItem.healthIncrease;
        moveSpeed *= buffItem.speedIncrease;
        for(int i = 0; i < coolDowns.Length; i++)
        {
            coolDowns[i] *= buffItem.cooldownDecrease;
        }

    }
}
