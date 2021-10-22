using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWeaponCorpAcopr : MonoBehaviour
{
    //public float attackRangeM;
    // public float attackDamageM;
    
    Transform attackPoint;

    [Header ("Look Othe Weapon")]
    public Sprite textureWeapon;

    [Header ("Settings Of The Weapon")]
    public float attackRange;
    public int attackDamage;
    public float speedOfTheAttack;
    public float reachSpeedAttack;

    Quaternion initialRot;
    public bool IsFiring = false;
    public float AttackAngle = 0; // From the owner
    public GameObject Attacker;

    // Start is called before the first frame update
    void Start()
    {
        attackPoint = gameObject.transform.GetChild(0).transform;
 
        reachSpeedAttack = speedOfTheAttack;
        initialRot = transform.parent.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFiring)
        {
           // print("yobg");
            float targetAngle = AttackAngle-45;
            float turnSpeed = 5;  // new Quaternion(.x, transform.parent.parent.rotation.y, transform.parent.parent.rotation.z, transform.parent.parent.rotation.w)
            //transform.parent.parent.rotation = Quaternion.Slerp(transform.parent.parent.rotation, Quaternion.Euler(0, 0, AttackAngle ), turnSpeed * Time.deltaTime);
            transform.parent.parent.rotation = Quaternion.Slerp(transform.parent.parent.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);

            if (reachSpeedAttack <= speedOfTheAttack)
            {
                //transform.parent.Rotate(new Vector3(0,0, 0));
                //transform.parent.rotation = Quaternion.Lerp(initialRot, new Quaternion(0, 0, 0, 90), 0.5f * Time.deltaTime);

                reachSpeedAttack += Time.deltaTime;


                IsFiring = true;
                if (reachSpeedAttack >= speedOfTheAttack)
                {

                    Debug.Log("coucou");

                 
                    //pivotWeapon = initPivotWeapon;
                    reachSpeedAttack = 0;
                    IsFiring = false;
                }

            }
        }
    }

    public void Attack(float Angle = 0)
    {
        AttackAngle = Angle;
         IsFiring = true;
        transform.parent.parent.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle+45));

        Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemie in hitenemy)
        {
            print("BOY");
            if (enemie.gameObject == Attacker || enemie.gameObject.CompareTag(gameObject.tag))
                print("Attack himself ");  //return;

            else
            {
                //if (enemie.tag == "Ennemies")
               // {
                    // enemie.GetComponent<EnemyManager>().takedamage(attackDamage);
                   
                if (Attacker.CompareTag("Player"))
                {
                    print("TOUCHED");
                }
                enemie.GetComponent<EnemyManager>().health -= attackDamage;
                    //Destroy(collision.gameObject);

                    enemie.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = enemie.GetComponent<EnemyManager>().flashDamage;
                    enemie.GetComponent<EnemyManager>().isDamaged = true;
               
                //}
            }

        }
        // if (IsFiring)
        //    return;


    }

    private void OnDrawGizmos()
    {
      //  if (attackPoint == null)
        //    return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
