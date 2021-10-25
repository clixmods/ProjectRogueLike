using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().Attacker && 
            !collision.gameObject.CompareTag(gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().Attacker.tag))
        {
            //if (gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().Attacker.CompareTag("Player"))
            //{
            //    print("TOUCHED");
            //}
            if (collision.gameObject.GetComponent<PlayerControler>() != null)
            {
                collision.gameObject.GetComponent<PlayerControler>().health -= gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().attackDamage;
            }
            if (collision.gameObject.GetComponent<EnemyManager>() != null)
            {
                collision.gameObject.GetComponent<EnemyManager>().health -= gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().attackDamage;
                collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = collision.gameObject.GetComponent<EnemyManager>().flashDamage;
                collision.gameObject.GetComponent<EnemyManager>().isDamaged = true;
            }
            if (collision.gameObject.GetComponent<Boss>() != null)
            {
                collision.gameObject.GetComponent<Boss>().health -= gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().attackDamage;
               // collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = collision.gameObject.GetComponent<EnemyManager>().flashDamage;
               // collision.gameObject.GetComponent<Boss>().isDamaged = true;
            }


        }
    }
}
