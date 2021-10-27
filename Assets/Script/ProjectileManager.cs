using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Sprite ProjectileTexture;
    public int DamageAmount;
    public GameObject Attacker;
    // Start is called before the first frame update
    void Start()
    {
       // DamageAmount = transform.parent.DamageAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        GameObject gObjct = collision.gameObject;
        if (Attacker != null && // ca se peut que l'attacker meurt et que le projectile soit toujours la
            gObjct != Attacker &&
            !gObjct.CompareTag(Attacker.tag))
        {
            if (gObjct.GetComponent<PlayerControler>() != null && !gObjct.GetComponent<PlayerControler>().isDamaged)
            {
                //collision.GetComponent<PlayerControler>().isDamaged = true;
                gObjct.GetComponent<PlayerControler>().health -= DamageAmount;
                gObjct.GetComponent<PlayerControler>().isDamaged = true;
            }
            if (gObjct.GetComponent<EnemyManager>() != null)
            {
                gObjct.GetComponent<EnemyManager>().health -= DamageAmount;
                gObjct.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = gObjct.GetComponent<EnemyManager>().flashDamage;
                gObjct.GetComponent<EnemyManager>().isDamaged = true;
            }
            if (gObjct.GetComponent<Boss>() != null)
            {
                gObjct.GetComponent<Boss>().health -= DamageAmount;
                // collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = collision.gameObject.GetComponent<EnemyManager>().flashDamage;
                // collision.gameObject.GetComponent<Boss>().isDamaged = true;
            }
            if (collision.transform.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
}
