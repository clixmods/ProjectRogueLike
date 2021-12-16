using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireBouleDeFeu : MonoBehaviour
{
    public List<GameObject> bouleDeFeuUn;
    bool spawn1;
    public List<GameObject> bouleDeFeuDeux;
    bool spawn2;

    int nombreDeSp;
  
    public GameObject bouleDeFeu;
    public int forceAdd;

    public int damageBigBall;
    public int damageBall;
   
    float timeIsReach;
    public float timeToReachForBouleDeFeu;
    public float timeToReachBigAttack;
    public float timeToReachForBouleDeFeuExplose;

    public bool doThisAttackTireBouleDeFeu;
    
    public bool rotateAttack;

    public GameObject pivotForRotate;
    public int rotateSpeed;


    public bool BigAttack;
    public GameObject player;
    public int grosseurDeLaBouleBigAttack;

    public bool exploseAttack;
    public GameObject bouleDeFeuExplose;

    BallExploseWithOtherBall scriptExploseBall;

    public int nombreDeSpawnMinExplose, nombreDeSpawnMaxExplose, forceALaBallExplose, nombreDeFoisExploseExplose;
    public float timeToReacchExplose, timeBetweenExploseExplose;
    // Start is called before the first frame update
    void Start()
    {
        spawn2 = true;
        nombreDeSp = bouleDeFeuUn.Count + bouleDeFeuDeux.Count;
    }

    // Update is called once per frame
    void Update()
    {

        if (doThisAttackTireBouleDeFeu)
        {
            if(rotateAttack)
            {
                RotationSpawnBall();
            }
            timerForSpawn(timeToReachForBouleDeFeu);
        }
        if(BigAttack)
        {
            timerForSpawn(timeToReachBigAttack);
            
        }
        if(exploseAttack)
        {
            timerForSpawn(timeToReachForBouleDeFeuExplose);
        }
        



    }

    void SpawnBouleDeFeu(List<GameObject> bouleDeFeuASpawn)
    { 
    
            for(int i = 0; i<bouleDeFeuASpawn.Count; i++)
        {
            GameObject boule = Instantiate(bouleDeFeu, bouleDeFeuASpawn[i].transform.position, bouleDeFeuASpawn[i].transform.rotation) ;
            boule.GetComponent<BallOfBoss>().damage = damageBall;
            boule.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1, 0) * forceAdd) ;
        }
        
    }

    void timerForSpawn (float timeToreach)
    {
      
        if(timeIsReach < timeToreach)
        {
            timeIsReach += Time.deltaTime;
        }
        else
        {
            if (doThisAttackTireBouleDeFeu)
            {
                if (spawn1)
                {
                    SpawnBouleDeFeu(bouleDeFeuDeux);
                    spawn2 = true;
                    spawn1 = false;
                }
                else
                {
                    SpawnBouleDeFeu(bouleDeFeuUn);
                    spawn2 = false;
                    spawn1 = true;
                }
            }
            else if (BigAttack)
            {
                BigAtTackBoule();
            }
            else if (exploseAttack)
            {
                BouleDeFeuExplose();
            }
            timeIsReach = 0;
            
        }
    }

    void RotationSpawnBall()
    {
        pivotForRotate.transform.eulerAngles += new Vector3(0,0,1f) * Time.deltaTime *rotateSpeed;
    }

    void BigAtTackBoule ()
    {

        List<GameObject> toutLesSP = ToutLesSPawnPoint();
        int indexs = TrouverLePlusProcheSpawnPoint(toutLesSP);




        GameObject boule = Instantiate(bouleDeFeu, toutLesSP[indexs].transform.position, toutLesSP[indexs].transform.rotation);
        boule.GetComponent<BallOfBoss>().damage = damageBigBall;
        boule.transform.localScale = new Vector3(1f, 1f, 0f)* grosseurDeLaBouleBigAttack;
        boule.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1, 0) * forceAdd);

    }

    List<GameObject> ToutLesSPawnPoint ()
    {
        List<GameObject> toutLesSP = new List<GameObject>();
        for (int i = 0; i < bouleDeFeuUn.Count; i++)
        {

            toutLesSP.Add(bouleDeFeuUn[i]);

        }
        for (int i = 0; i < bouleDeFeuDeux.Count; i++)
        {
            toutLesSP.Add(bouleDeFeuDeux[i]);

        }
        return toutLesSP;

    }

    int TrouverLePlusProcheSpawnPoint (List<GameObject> toutLesSP)
    {
        int indexs = 0;

        

        List<float> pluspetiteDistance = new List<float>();


        // print((pluspetiteDistance[0]));
       
        for (int i = 0; i < toutLesSP.Count; i++)
        {

            float dist = Vector3.Distance(toutLesSP[i].transform.position, player.transform.position);

            pluspetiteDistance.Add(dist);
            if (dist < pluspetiteDistance[0])
            {
                pluspetiteDistance[0] = dist;
                indexs = i;
            }

        }
        return indexs;
    }

    void BouleDeFeuExplose ()
    {
        List<GameObject> toutLesSP = ToutLesSPawnPoint();
        int indexs = TrouverLePlusProcheSpawnPoint(toutLesSP);

        GameObject boule = Instantiate(bouleDeFeuExplose, toutLesSP[indexs].transform.position, toutLesSP[indexs].transform.rotation);
        boule.GetComponent<BallExploseWithOtherBall>().boss = gameObject;
        boule.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1, 0) * forceAdd);
        scriptExploseBall = boule.GetComponent<BallExploseWithOtherBall>();
        BallExplose(nombreDeSpawnMinExplose, nombreDeSpawnMaxExplose, timeToReacchExplose, forceALaBallExplose, nombreDeFoisExploseExplose, timeBetweenExploseExplose);
    
}

public  void BallExplose(int nombreDeSpawnMin, int nombreDeSpawnMax, float timeToReacch, int forceALaBall, int nombreDeFoisExplose, float timeBetweenExplose)
    {
        scriptExploseBall.nombreDeSpawnMin = nombreDeSpawnMin;
        scriptExploseBall.nombreDeSpawnMax = nombreDeSpawnMax;
        scriptExploseBall.timeToReachForExplose = timeToReacch;
        scriptExploseBall.forceAdd = forceALaBall;
        scriptExploseBall.nombreDeFois = nombreDeFoisExplose;
        scriptExploseBall.timeBetweenOtherEplose = timeBetweenExplose;
        scriptExploseBall.damage = damageBall;
    }



}
