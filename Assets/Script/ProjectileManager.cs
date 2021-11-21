using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Sprite ProjectileTexture;
    public int DamageAmount;
    public GameObject Attacker;
    public bool isMagical;

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
        //print(collision.tag);
        GameObject victim = collision.gameObject;
        if (Attacker != null && // ca se peut que l'attacker meurt et que le projectile soit toujours la
            victim != Attacker &&
            !victim.CompareTag(Attacker.tag))
        {

            //CurrentWeapon.TryGetComponent<PlayerControler>(out PlayerControler Component)
            if (victim.TryGetComponent<PlayerControler>(out PlayerControler PlayerControler) && !PlayerControler.isDamaged)
            {
                PlayerControler.health -= DamageAmount;
                PlayerControler.isDamaged = true;
            }
            if (victim.TryGetComponent<EnemyManager>(out EnemyManager VictimManager))
            {
                if(VictimManager.isMagical == isMagical)
                VictimManager.health -= DamageAmount;
                victim.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = VictimManager.flashDamage;
                VictimManager.isDamaged = true;
            }
            if (victim.GetComponent<Boss>() != null)
            {
                victim.GetComponent<Boss>().health -= DamageAmount;
            }
            if (collision.transform.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }
    }
}
