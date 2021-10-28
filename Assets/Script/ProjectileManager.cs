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

            //CurrentWeapon.TryGetComponent<PlayerControler>(out PlayerControler Component)
            if (gObjct.TryGetComponent<PlayerControler>(out PlayerControler Component) && !Component.isDamaged)
            {
                //collision.GetComponent<PlayerControler>().isDamaged = true;
                Component.health -= DamageAmount;
                Component.isDamaged = true;
            }
            if (gObjct.TryGetComponent<EnemyManager>(out EnemyManager ComponentA))
            {
                ComponentA.health -= DamageAmount;
                gObjct.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = ComponentA.flashDamage;
                ComponentA.isDamaged = true;
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
