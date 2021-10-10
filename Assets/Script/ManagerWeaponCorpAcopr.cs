using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWeaponCorpAcopr : MonoBehaviour
{
    public float attackRangeM;
    public float attackDamageM;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ScriptCorpACorp>().attackRange = attackRangeM;
        gameObject.GetComponent<ScriptCorpACorp>().attackDamage = attackDamageM;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* public void Attack()
    {

    }*/
}
