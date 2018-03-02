using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Character used for testing different abilities.
/// </summary>


public class TestChar : Character {

    GameObject projectile;
    public float dashDistance, dashTime;
    private float dashSpeed;
    public bool dashing;
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
            AimArrow();
        }
        if (Input.GetButtonUp("Basic"))
        {
            BasicAbility();
        }

        if (Input.GetButton("CSA"))
        {
            AimArrow();
        }
        if (Input.GetButtonUp("CSA"))
        {
            CharacterSpecificAbility();
        }

        if (dashing)
        {
            // THIS IS THE PROBLEM
            
            dashSpeed = dashDistance / dashTime;
            Vector3 moveBy = target * dashSpeed * Time.deltaTime;
            PlayerManager.instance.GetComponent<CharacterController>().Move(moveBy);
            distanceTraveled += moveBy;
            if(Mathf.Abs(Vector3.Distance(Vector3.zero, distanceTraveled)) >= dashDistance)
            {
                dashing = false;
                distanceTraveled = Vector3.zero;
            }
            
        }

    }

    public override bool BasicAbility()
    {
        if (base.BasicAbility())
        {
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z));
        }
        arrow.SetActive(false);
        return true;
    }

    public override bool CharacterSpecificAbility()
    {
        if (base.CharacterSpecificAbility())
        {
            target = Vector3.forward;
            if (!dashing)
            {
                target = transform.TransformDirection(target);
            }
            dashing = true;
            Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z));
        }
        
        arrow.SetActive(false);
        return true;

        
    }

    public override bool RecoveryAbility()
    {
        if (base.RecoveryAbility())
        {

        }
        return true;
    }

    public override bool UtilityAbility()
    {
        if (base.UtilityAbility())
        {

        }
        return true;
    }

    public override bool SpecialAbility()
    {
        if (base.SpecialAbility())
        {

        }
        return true;
    }

    public override void AimArrow()
    {
        base.AimArrow();
    }
}
