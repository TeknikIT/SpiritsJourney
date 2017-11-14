using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public string characterName;
    public int health;
    public GameObject arrow;
    private void Start()
    {
        arrow = gameObject.transform.Find("Arrow").gameObject;
    }

    public virtual void BasicAbility()
    {
        Debug.Log("Basic Ability Used!");
    }

    public virtual void CharacterSpecificAbility()
    {
        Debug.Log("Character Specific Ability Used!");
    }

    public virtual void RecoveryAbility()
    {
        Debug.Log("Recovery Ability Used!");
    }

    public virtual void UtilityAbility()
    {
        Debug.Log("Utility Ability Used!");
    }

    public virtual void SpecialAbility()
    {
        Debug.Log("Special Ability Used!");
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
