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
        if (collision.gameObject != Attacker &&
            !collision.gameObject.CompareTag(Attacker.tag))
        {
            if (collision.gameObject.GetComponent<PlayerControler>() != null)
            {
                collision.gameObject.GetComponent<PlayerControler>().health -= DamageAmount;
            }
            if (collision.gameObject.GetComponent<EnemyManager>() != null)
            {
                collision.gameObject.GetComponent<EnemyManager>().health -= DamageAmount;
                collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = collision.gameObject.GetComponent<EnemyManager>().flashDamage;
                collision.gameObject.GetComponent<EnemyManager>().isDamaged = true;
            }
            if (collision.gameObject.GetComponent<Boss>() != null)
            {
                collision.gameObject.GetComponent<Boss>().health -= gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().attackDamage;
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
