using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallExploseWithOtherBall : MonoBehaviour
{
    public float timeToReachForExplose;
    float timeIsReach;
    public GameObject bouleDeFeu;

    public int nombreDeSpawnMin;
    public int nombreDeSpawnMax;

    public int forceAdd;
    public int nombreDeFois;
    int foisFaite;

    public int damage;

    public float timeBetweenOtherEplose;

    public GameObject boss;

    bool putOffRigi;


    // Start is called before the first frame update
    void Start()
    {
        timeIsReach = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (foisFaite == nombreDeFois)
        {
            DestroyGameObject();
        }

        timerForSpawn(timeToReachForExplose);

      
    }

    void timerForSpawn(float timeToreach)
    {

        if (timeIsReach < timeToreach)
        {
            timeIsReach += Time.deltaTime;
        }
        else
        {
            if (!putOffRigi)
            {
                PutOffRigiSprite();
            }
            else
            {
             
                gameObject.GetComponent<Rigidbody2D>().velocity.Set(0,0);
                

              Explose();
            }
            timeIsReach = 0;
        }
    }

    void PutOffRigiSprite ()
    {
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        putOffRigi = true;

        Explose();
    }

    void Explose ()
    {
       
        int rand = Random.Range(nombreDeSpawnMin, nombreDeSpawnMax);

        
       
            for (int i = 0; i < nombreDeSpawnMax; i++)
            {
                GameObject boule = Instantiate(bouleDeFeu, gameObject.transform.position, Quaternion.identity);
                float randy = Random.Range(-1.0f, 1.0f);
                float randx = Random.Range(-1.0f, 1.0f);
            // if (randy == 0)
            //{
            //    randx = Random.Range(1, 10);
            // }
            // else
            // {
            //   randx = Random.Range(0, 10);
            //  }
            //  Vector2 force = new Vector2(randx, randy);
            //  Vector2 forceNorm = force.normalized;

            //  float disthypo = Vector2.Distance(transform.position, force.normalized);
            //  float distadja = Vector2.Distance(transform.position, new Vector2(forceNorm.x, 0f));
            // // float cos =  distadja / disthypo;

            // // Debug.Log(cos +"cos");
            // // float angle = Mathf.Cos(cos)*100;

            //  float distB = Vector2.Distance(transform.localPosition, boss.transform.position);
            //  float distBoule = Vector2.Distance(transform.localPosition, force.normalized);
            //  float cos = distBoule / distB;
            //  float angle = Mathf.Cos(cos);
            // // Debug.Log(distB + "distB");
            // // Debug.Log(distBoule + "distBoule");
            //  Vector3 direction =  new Vector2(force.x - transform.localPosition.x, force.y - transform.localPosition.y);
            //  //float angles = (Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg );
            //  float angles = (Mathf.Atan2(randx, randy) * Mathf.Rad2Deg);

            //  //Quaternion rotation = Quaternion.LookRotation(new Vector3(randx, randy, 0) - boule.transform.position);
            //// Mathf.
            //  Debug.Log(angles);

            //  boule.transform.GetChild(0).eulerAngles = new Vector3(0, 0, angles);
            // boule.transform.GetChild(0).localRotation = Quaternion.Euler(new Vector3(0,0, rotation.x * -360));
            //  boule.transform.(new Vector3(0,0,gameObject.transform.position.z));

            
            boule.GetComponent<Rigidbody2D>().AddForce(new Vector2(randx, randy).normalized * forceAdd);
            boule.GetComponent<BallOfBoss>().damage = damage;
            float tstx = gameObject.transform.position.x - randx;
            float tsty = gameObject.transform.position.y - randy;
            Debug.Log(tstx + " "+ tsty);
            float angle = Mathf.Atan2(tsty, tstx) * Mathf.Rad2Deg;
            Debug.Log(angle+ "angle");
            boule.transform.GetChild(0).transform.eulerAngles = new Vector3 (0,0,-angle);
                

        }
        foisFaite++;
        timeIsReach = 0;
        timeToReachForExplose = timeBetweenOtherEplose;
        
       
    }

    void DestroyGameObject ()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explose();
        foisFaite = nombreDeFois;

    }
}
