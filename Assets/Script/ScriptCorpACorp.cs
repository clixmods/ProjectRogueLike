using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCorpACorp : MonoBehaviour
{
    Transform attackPoint;
    public float attackRange;
    public float attackDamage;
    int time = 1;
    float timeplus = 1;
    // Transform initPosition;
    // public LayerMask enemieCorpAcorp;

    // Start is called before the first frame update
    void Start()
    {
        attackPoint = gameObject.transform.GetChild(0).transform;
       // initPosition = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("coucou");
            //transform.Translate(new Vector2(0f, 1f));
            Collider2D[] hitenemy =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

            foreach(Collider2D enemie in hitenemy)
            {
                if(enemie.tag == "EnemieCorpACorp")
                {
                    //enemie.GetComponent<EnemieScript>().takedamage(attackDamage);
                }
            }
        }*/

       // gameObject.transform = initPosition;
    }

    public void Attack()
    {
        
        if (timeplus <= time)
        {
            timeplus += Time.deltaTime;

            if (timeplus >= time)
            {
                Debug.Log("coucou");
                //transform.Translate(new Vector2(0f, 1f));
                Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

                foreach (Collider2D enemie in hitenemy)
                {
                    if (enemie.tag == "EnemieCorpACorp")
                    {
                        //enemie.GetComponent<EnemieScript>().takedamage(attackDamage);
                    }
                }

                timeplus = 0;
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
