using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class for all characters
/// </summary>


public class Character : MonoBehaviour {

    public string characterName; // Name of the character

    public int health; // Character health
    public float moveSpeed; // Character move speed
    public float damageModifier; // Damage modifier
    public float cooldownModifier; // Cooldown modifier

    // The statistics from the token system (not fully implemented)
    public PlayerStatistics localPlayerData = new PlayerStatistics(); 

    public GameObject arrow, cone; //The aim arrow/cone
    public float[] coolDowns, timeStamps, baseDamage; //Arrays containing both coolDowns and the last time the abillity was used
    
    //Picked up items
    public List<BuffItem> buffItems;
    public List<Consumable> consumables;

    private void Start()
    {
        //Loading the data from the global control
        localPlayerData = GlobalControl.instance.savedGameStatistics.playerStatistics;

        //Initializing arrow and arrays
        arrow = gameObject.transform.Find("Arrow").gameObject;
        cone = gameObject.transform.Find("Cone").gameObject;
        coolDowns = new float[5];
        timeStamps = new float[5];
        baseDamage = new float[5];

        // Adding local player data
        health += (int)localPlayerData.healthStat;
        moveSpeed += localPlayerData.movespeedStat;
        damageModifier += localPlayerData.damageStat;
        cooldownModifier += localPlayerData.cooldownStat;

        //Adds the cooldown modifier
        for(int i = 0; i < coolDowns.Length; i++)
        {
            coolDowns[i] *= cooldownModifier;
        }
    }

    /// <summary>
    /// Saves the statistics
    /// </summary>
    public void SaveStatistics()
    {
        GlobalControl.instance.savedGameStatistics.playerStatistics = localPlayerData;
    }

    /// <summary>
    /// Basic abillity, left mouse button
    /// </summary>
    /// <returns>Boolean</returns>
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

    /// <summary>
    /// Character Specific Ability, right mouse button
    /// </summary>
    /// <returns>Boolean</returns>
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

    /// <summary>
    /// Recovery Ability
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Recovery Ability
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Special Ability
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Used for aiming, arrow
    /// </summary>
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

    /// <summary>
    /// Used for aiming, arrow
    /// </summary>
    /// <param name="scale">Scale of the cone</param>
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

    /// <summary>
    /// Toggles character movement
    /// </summary>
    public virtual void ToggleCharacterMovement()
    {
        PlayerController pc = PlayerManager.instance.GetComponent<PlayerController>();
        if (pc.movementIsLocked)
            pc.movementIsLocked = false; 
        else
            pc.movementIsLocked = true;
    }

    /// <summary>
    /// Pickup item
    /// </summary>
    /// <param name="buffItem">item</param>
    public virtual void AddPickupItem(BuffItem buffItem)
    {
        buffItems.Add(buffItem);
        health += buffItem.healthIncrease;
        moveSpeed *= buffItem.speedIncrease;
        for(int i = 0; i < coolDowns.Length; i++)
        {
            coolDowns[i] *= buffItem.cooldownDecrease;
        }

        damageModifier += buffItem.damageIncrease;

    }

    /// <summary>
    /// Pickup Consumable
    /// </summary>
    /// <param name="consumable">consumable</param>
    public virtual void AddConsumableItem(Consumable consumable)
    {
        consumables.Add(consumable);
        health += consumable.healthIncrease;
    }
}
