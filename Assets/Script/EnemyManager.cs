using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{ 
    private Transform enemy;
    public Transform player;

    public int health = 100;
    public int maxHealth;
    NavMeshAgent navMeshAgent;
    
    Animator animeFront;
    // Damage Event
    public Material flashDamage;
    public bool isDamaged;

    private void Start()
    {
        maxHealth = health;
         navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        animeFront = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        MeleyEnemyMovment();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void MeleyEnemyMovment()
    {


        if (player != null)
        {
            float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
            {
                return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;

            }
            float AttackAngle = AngleBetweenTwoPoints(transform.position, player.position);
            
            navMeshAgent.SetDestination(player.position);

            

           
            




        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            TriggerSalle trigSalle = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<TriggerSalle>();
            trigSalle.countEnnemie--;
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("CorpACorp") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Distance"))
        {
            Debug.Log(gameObject.name+" have been attacked");
            //LastAttacker = collision.gameObject.transform.GetComponent<BulletProperty>().attacker;
            health -= collision.gameObject.transform.GetComponent<ProjectileManager>().DamageAmount;
            Destroy(collision.gameObject);

            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = flashDamage;
            isDamaged = true;
            isDamaged = false;

        }
        
    }

}
