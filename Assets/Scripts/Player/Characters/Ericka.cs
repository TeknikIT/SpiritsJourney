using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Characters
{
    class Ericka : Character
    {
        GameObject projectile, meleeHitbox;
        public float dashDistance, dashTime;
        private float dashSpeed;
        public bool dashing;
        public Vector3 target, distanceTraveled;
        public bool collisionAttack;
        public int collisionAttackDamage;

        private void Start()
        {
            projectile = (GameObject)Resources.Load("Prefabs/Bullet");
            meleeHitbox = (GameObject)Resources.Load("Prefabs/MeleeAttackHB");
            dashing = false;
            collisionAttack = false;
            collisionAttackDamage = 10;

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

        public override bool BasicAbility()
        {
            if (base.BasicAbility())
            {
                Transform prevTransform = transform.parent;
                //transform.parent.rotation = arrow.transform.rotation;
                var hitBox = Instantiate(meleeHitbox, transform); 
                hitBox.transform.parent = transform; /*?Hur tänkte jag här?*/;
            }
            return true;
        }

        public override bool CharacterSpecificAbility()
        {
            if (base.CharacterSpecificAbility())
            {
                Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), 
                    Quaternion.Euler(0, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z));
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

        public override bool SpecialAbility()
        {
            if (base.SpecialAbility())
            {

            }
            return true;
        }

        public override void Aim()
        {
            base.Aim();
        }

        public void OnCollisionEnter(Collision c)
        {
            Debug.Log("1");
            if(c.gameObject.tag == "Enemy")
            {
                Debug.Log(2);
                if (collisionAttack)
                {
                    this.gameObject.layer = 9;
                    c.gameObject.GetComponent<EnemyManager>().TakeDamage(collisionAttackDamage);
                }
            }
            else if(c.gameObject.tag == "EnemyChild")
            {
                Debug.Log(3);
                if (collisionAttack)
                {
                    Debug.Log(4);
                    this.gameObject.layer = 9;
                    c.gameObject.GetComponentInParent<EnemyManager>().TakeDamage(collisionAttackDamage);
                }
            }
        }

        public void OnCollisionExit(Collision c)
        {
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
