using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChar : Character {

    GameObject projectile;
    public float dashDistance, dashSpeed;
    bool dashing;
    public Vector3 target, distanceTraveled;

    private void Start()
    {
        projectile = (GameObject)Resources.Load("Prefabs/Bullet");
        dashing = false;

    }

    public void Update()
    {
        if (Input.GetButton("Basic"))
        {
            Aim();
        }
        if (Input.GetButtonUp("Basic"))
        {
            BasicAbility();
        }

        if (Input.GetButton("CSA"))
        {
            Aim();
        }
        if (Input.GetButtonUp("CSA"))
        {
            CharacterSpecificAbility();
        }


        if (dashing)
        {
            target = transform.InverseTransformDirection(target);
            Vector3 moveBy = target * Time.deltaTime;
            
            PlayerManager.instance.GetComponent<CharacterController>().Move(moveBy);
            distanceTraveled += moveBy;
            if(Vector3.Distance(Vector3.zero, distanceTraveled) >= dashDistance)
            {
                dashing = false;
                distanceTraveled = Vector3.zero;
            }
            
        }

    }

    public override void BasicAbility()
    {
        Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z));
        arrow.SetActive(false);
  
        base.BasicAbility();
    }

    public override void CharacterSpecificAbility()
    {
        dashing = true;
        target = (Vector3.forward * dashSpeed);
        base.CharacterSpecificAbility();
    }

    public override void RecoveryAbility()
    {
        base.RecoveryAbility();
    }

    public override void UtilityAbility()
    {
        base.UtilityAbility();
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
    }

    public override void Aim()
    {
        base.Aim();
    }
}
