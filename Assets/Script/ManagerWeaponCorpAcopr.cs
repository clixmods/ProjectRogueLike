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
    public float attackDamage;
    public float speedOfTheAttack;
    float reachSpeedAttack;



    // Start is called before the first frame update
    void Start()
    {
        attackPoint = gameObject.transform.GetChild(0).transform;
 
        reachSpeedAttack = speedOfTheAttack;
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
       
        if (reachSpeedAttack <= speedOfTheAttack)
        {
            reachSpeedAttack += Time.deltaTime;

            if (reachSpeedAttack >= speedOfTheAttack)
            {
                Debug.Log("coucou");
                
                Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

                foreach (Collider2D enemie in hitenemy)
                {
                    if (enemie.tag == "EnemieCorpACorp")
                    {
                        //enemie.GetComponent<EnemieScript>().takedamage(attackDamage);
                    }
                }
                //pivotWeapon = initPivotWeapon;
                reachSpeedAttack = 0;
                
            }
            
        }
      
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
