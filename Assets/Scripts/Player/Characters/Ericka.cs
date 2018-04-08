using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player character: Ericka
/// </summary>
namespace Assets.Scripts.Player.Characters
{
    class Ericka : Character
    {
        GameObject projectile, meleeHitbox; // Subordinate gameobject
        public float dashDistance, dashTime, dashSpeed; // Dash variables
        public bool dashing; // Is the character dashing
        public Vector3 target, distanceTraveled; // Target is dashtarget. Distance traveled during dash
        public bool collisionAttack; // Is the character using a collision attack
        public int collisionAttackDamage; // Collision attack damage



        private void Start()
        {
            //Loading the attack prefabs
            projectile = (GameObject)Resources.Load("Prefabs/Bullet"); 
            meleeHitbox = (GameObject)Resources.Load("Prefabs/MeleeAttackHB");

            dashing = false;
            collisionAttack = false;
            collisionAttackDamage = 10;

        }

        public void Update()
        {
            //Basic Attack Aim
            if (Input.GetButton("Basic"))
            {
                AimCone(0.2f);
            }
            //Basic Attack Use
            if (Input.GetButtonUp("Basic"))
            {
                BasicAbility();
            }

            //Character Specific Abillity Aim
            if (Input.GetButton("CSA"))
            {
                AimArrow();
            }
            //Character Specific Abillity Use
            if (Input.GetButtonUp("CSA"))
            {
                CharacterSpecificAbility();
            }

            //Used for dashing. Not currently used
            if (dashing)
            {
                collisionAttack = true;
                dashSpeed = dashDistance / dashTime;
                Vector3 moveBy = target * dashSpeed * Time.deltaTime;
                PlayerManager.instance.GetComponent<CharacterController>().Move(moveBy);
                distanceTraveled += moveBy;
                if (Mathf.Abs(Vector3.Distance(Vector3.zero, distanceTraveled)) >= dashDistance)
                {
                    dashing = false;
                    collisionAttack = false;
                    distanceTraveled = Vector3.zero;
                }

            }

        }
        /// <summary>
        /// Basic ability activated
        /// </summary>
        /// <returns>Boolean</returns>
        public override bool BasicAbility()
        {
            if (base.BasicAbility())
            {
				var hitBox = Instantiate(meleeHitbox, PlayerManager.instance.transform.position, Quaternion.Euler(0, cone.transform.rotation.eulerAngles.y + 60, cone.transform.rotation.eulerAngles.z));
                hitBox.transform.parent = transform;
                hitBox.GetComponent<MeleeSwing>().createdBy = this;
                hitBox.GetComponent<MeleeSwing>().damage = baseDamage[0] + damageModifier;
            }
            cone.SetActive(false);
            return true;
        }

        /// <summary>
        /// Character Specific Ability activated
        /// </summary>
        /// <returns>Boolean</returns>
        public override bool CharacterSpecificAbility()
        {
            if (base.CharacterSpecificAbility())
            {
                var bullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), 
                    Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z));
                bullet.GetComponent<Bullet>().damage = (int)(baseDamage[1] + damageModifier);
            }
            arrow.SetActive(false);
            return true;


        }

        /// <summary>
        /// Recover Ability
        /// </summary>
        /// <returns></returns>
        public override bool RecoveryAbility()
        {
            if (base.RecoveryAbility())
            {

            }
            return true;
        }

        /// <summary>
        /// Utillity Ability
        /// </summary>
        /// <returns>Boolean</returns>
        public override bool UtilityAbility()
        {
            if (base.UtilityAbility())
            {
                target = Vector3.forward;
                if (!dashing)
                {
                    target = transform.TransformDirection(target);
                }
                dashing = true;
            }
            arrow.SetActive(false);
            return true;
          

        }

        /// <summary>
        /// Special ability activated
        /// </summary>
        /// <returns>Boolean</returns>
        public override bool SpecialAbility()
        {
            if (base.SpecialAbility())
            {

            }
            return true;
        }

        /// <summary>
        /// Aim with arrow
        /// </summary>
        public override void AimArrow()
        {
            base.AimArrow();
        }

        /// <summary>
        /// Aim with cone
        /// </summary>
        /// <param name="scale">Scale of the cone</param>
        public override void AimCone(float scale)
        {
            base.AimCone(scale);
        }

        public void OnCollisionEnter(Collision c)
        {
            // Used for dash attack
            if(c.gameObject.tag == "Enemy")
            {
                if (collisionAttack)
                {
                    this.gameObject.layer = 9;
                    c.gameObject.GetComponent<EnemyManager>().TakeDamage(collisionAttackDamage);
                }
            }
            else if(c.gameObject.tag == "EnemyChild")
            {
                if (collisionAttack)
                {
                    this.gameObject.layer = 9;
                    c.gameObject.GetComponentInParent<EnemyManager>().TakeDamage(collisionAttackDamage);
                }
            }
        }

        public void OnCollisionExit(Collision c)
        {
            // Used for dash attack
            if (c.gameObject.tag == "Enemy")
            {
                if (collisionAttack)
                {
                    this.gameObject.layer = 0;

                }
            }
            else if (c.gameObject.tag == "EnemyChild")
            {
                if (collisionAttack)
                {
                    this.gameObject.layer = 0;

                }
            }
        }
    }
}
