using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonceSurJoueur : MonoBehaviour
{
    public float forceAddForRulce;
    public GameObject player;
    bool percut;

    float timeIsReach;
    float timeIsReachF;
    public float timeForKnockOut;
    public float timeForeFonce;

    public bool suivrePlayer;
    public float vitesseDeSuivie;

    public bool attckFonce;
    public float minDistOfPlayer;
    Vector2 pPose;
    bool fonceActivé;
    public float vitesseDeFonce;
    
    // Start is called before the first frame update
    void Start()
    {
        timeIsReach = 0;
        timeIsReachF = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (percut)
        {
            timerDeKnockOut(timeForKnockOut);
        }

        if (attckFonce)
        {
            if (!fonceActivé)
            {
                timer(timeForeFonce);
                print("tyoyoyoy");
            }
            else
            {
                checkPos();
                fonce(vitesseDeFonce);
            }
        }
        else if(suivrePlayer)
        {
            pPose = player.transform.position;
            fonce(vitesseDeSuivie);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print("tyoyoyoy");

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - gameObject.transform.position).normalized * forceAddForRulce, ForceMode2D.Impulse);
            player = collision.gameObject;
            percut = true;
            //player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            fonceActivé = false;
        }
    }

    void timerDeKnockOut(float timeToreach)
    {

        if (timeIsReach < timeToreach)
        {
            timeIsReach += Time.deltaTime;
        }
        else
        {
            player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            timeIsReach = 0;
            percut = false;
        }
    }

    void fonce(float vitesse)
    {
        
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, pPose, vitesse * Time.deltaTime);
    }

    void timer(float timeToreach)
    {

        if (timeIsReachF < timeToreach)
        {
            timeIsReachF += Time.deltaTime;
        }
        else
        {
            pPose = player.transform.position;
            timeIsReachF = 0;
            fonceActivé = true;
            

        }
    }

    void checkPos()
    {
        float dist = Vector2.Distance(pPose, gameObject.transform.position);
        if (dist < minDistOfPlayer)
        {
            fonceActivé = false;
        }
    }
    
}
