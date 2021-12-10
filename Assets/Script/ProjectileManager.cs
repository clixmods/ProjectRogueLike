using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Sprite ProjectileTexture;
    public int DamageAmount;
    public GameObject Attacker;
    public bool isMagical;
    public WeaponType typeWpn;
    public bool DestroyOnHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject victim = collision.gameObject;
        if (Attacker != null && // ca se peut que l'attacker meurt et que le projectile soit toujours la
            victim != Attacker &&
            !victim.CompareTag(Attacker.tag))
        {
            if (victim.TryGetComponent<PlayerControler>(out PlayerControler PlayerControler) && !PlayerControler.isDamaged)
            {
                PlayerControler.health -= DamageAmount;
                victim.GetComponent<PlayerControler>().spriteRdr.material = GameManager.GameUtil.DamageMtl;
                PlayerControler.isDamaged = true;
            }
            if (victim.TryGetComponent<EnemyManager>(out EnemyManager VictimManager))
            {
                if ((int)VictimManager.ReceiveDamageOn == (int)typeWpn || VictimManager.ReceiveDamageOn == ReceiveDamageOnType.Both)
                {
                    VictimManager.health -= DamageAmount;
                    victim.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = VictimManager.flashDamage;
                    VictimManager.isDamaged = true;
                }
                else
                    GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.TypeWeapon);

                if(DestroyOnHit)
                    Destroy(gameObject);
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
