using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBoss : MonoBehaviour
{
  
    public TireBouleDeFeu scriptTireBouleDeFeu;
   

    public GameObject player;

    int healthActu;
    public int health;
    public int damageBall;
    public int damageBigBall;
    public int damageTouche;
    int attackPrise;
   

    float timeReach;
    float timeTReach;


    bool lanceAttack;

    public GameObject PorteProchainBoss;
    // Start is called before the first frame update
    void Start()
    {

        scriptTireBouleDeFeu.player = player;
        scriptTireBouleDeFeu.damageBall = damageBall;
        scriptTireBouleDeFeu.damageBigBall = damageBigBall;
        healthActu = health;
        timeReach = 10;
        timeTReach = 10;
        HUDManager.HUDUtility.UIHealthBoss.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        Timer();
        if (lanceAttack)
        {
            if (healthActu > (health / 3) * 2)
            {
                scriptTireBouleDeFeu.exploseAttack = false;
                scriptTireBouleDeFeu.doThisAttackTireBouleDeFeu = false;
                scriptTireBouleDeFeu.BigAttack = false;
                attackPrise = Random.Range(1, 4);
                switch (attackPrise)
                {
                    case 1:
                        AttackExplose(10, 20, 2f, 150, 1, 2, 1f, 150, 2);
                        timeTReach = 20;
                        break;
                    case 2:
                        AttackTireBouleDeFeu(false, 1f, 100);
                        timeTReach = 10;
                        break;
                    case 3:
                        AttackBigBall(150, 20, 5);
                        timeTReach = 25;
                        break;
                    default:
                        break;
                }

                lanceAttack = false;

            }
            else if (healthActu > health / 3 && healthActu <= (health/ 3) * 2)
            {
                scriptTireBouleDeFeu.exploseAttack = false;
                scriptTireBouleDeFeu.doThisAttackTireBouleDeFeu = false;
                scriptTireBouleDeFeu.BigAttack = false;
                attackPrise = Random.Range(1, 4);
               
                switch (attackPrise)
                {

                    case 1:
                        AttackExplose(10, 20, 2f, 150, 1, 2, 1f, 150, 2);
                        timeTReach = 20;
                        break;
                    case 2:
                        AttackTireBouleDeFeu(true, 1f, 100, 100);
                        timeTReach = 10;
                        break;
                    case 3:
                        AttackBigBall(150, 20, 5);
                        timeTReach = 25;
                        break;
                    default:
                        break;
                }

                lanceAttack = false;
            }
            else if (healthActu <= health / 3)
            {
                scriptTireBouleDeFeu.exploseAttack = false;
                scriptTireBouleDeFeu.doThisAttackTireBouleDeFeu = false;
                scriptTireBouleDeFeu.BigAttack = false;
                attackPrise = Random.Range(1, 4);
                switch (attackPrise)
                {

                    case 1:
                        AttackExplose(10, 30, 1f, 150, 1, 2, 1f, 150, 2);
                        timeTReach = 5;
                        break;
                    case 2:
                        AttackTireBouleDeFeu(true, 0.5f, 100, 100);
                        timeTReach = 5;
                        break;
                    case 3:
                        AttackBigBall(150, 20, 5);
                        timeTReach = 10;
                        break;
                    default:
                        break;
                }

                lanceAttack = false;
            }
        }
        if (healthActu <= 0)
        {
            HUDManager.HUDUtility.UIHealthBoss.gameObject.SetActive(false);
            Instantiate(PorteProchainBoss, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    void UpdateUI()
    {
        HUDManager.HUDUtility.BossMaxHealth = health;
        HUDManager.HUDUtility.BossHealth = healthActu;
    }

    void Timer ()
    {
         if(timeReach < timeTReach)
        {
            timeReach += Time.deltaTime;
        }
         else
        {
           
            lanceAttack = true;
            timeReach = 0;
        }
    }

    void AttackExplose (int nombreDeSpawnMin, int nombreDeSpawnMax, float timeToReacch, int forceALaBall, int nombreDeFoisExplose, float timeBetweenExplose, float timeBetweenBigBall, int forceAdd , float timeToReachForBouleDeFeuExplose )
    {
        scriptTireBouleDeFeu.exploseAttack = true;
        scriptTireBouleDeFeu.nombreDeSpawnMinExplose = nombreDeSpawnMin;
        scriptTireBouleDeFeu.nombreDeSpawnMaxExplose = nombreDeSpawnMax;
        scriptTireBouleDeFeu.timeToReacchExplose = timeToReacch;
        scriptTireBouleDeFeu.forceALaBallExplose = forceALaBall;
        scriptTireBouleDeFeu.nombreDeFoisExploseExplose = nombreDeFoisExplose;
        scriptTireBouleDeFeu.timeBetweenExploseExplose = timeBetweenExplose;
        scriptTireBouleDeFeu.timeToReacchExplose = timeBetweenBigBall;
        scriptTireBouleDeFeu.forceAdd = forceAdd;
        scriptTireBouleDeFeu.timeToReachForBouleDeFeuExplose = timeToReachForBouleDeFeuExplose;
    }

    void AttackTireBouleDeFeu(bool rotate, float timeForTire, int forceALaBall, int rotateSpeed = 0)
    {
        scriptTireBouleDeFeu.doThisAttackTireBouleDeFeu = true;
        scriptTireBouleDeFeu.rotateAttack = rotate;
        scriptTireBouleDeFeu.timeToReachForBouleDeFeu = timeForTire;
        scriptTireBouleDeFeu.rotateSpeed = rotateSpeed;
        scriptTireBouleDeFeu.forceAdd = forceALaBall;
       
    }

    void AttackBigBall (int forceAdd, int grosseur, float timeToReach)
    {
        scriptTireBouleDeFeu.BigAttack = true;
        scriptTireBouleDeFeu.forceAdd = forceAdd;
        scriptTireBouleDeFeu.grosseurDeLaBouleBigAttack = grosseur;
        scriptTireBouleDeFeu.timeToReachBigAttack = timeToReach;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
              collision.gameObject.GetComponent<PlayerControler>().health -= damageTouche; // TODO damage à defs
        }
    }






    }
