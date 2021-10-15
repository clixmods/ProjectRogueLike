using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{ 
    private Transform enemy;
    public Transform player;

    public int Health = 100;

    NavMeshAgent navMeshAgent;
    
    Animator animeFront;
   

    private void Start()
    {
       
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
    }

}
