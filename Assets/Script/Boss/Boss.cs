using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D rigidbody;
    GameObject player;
   public  GameObject prefabBoule;

    Vector2 playerPosition;
    Vector2 gameobjectPosition;
    Vector3 playerPos;

    public bool arr;
    bool tst;
    public bool boule;
    public bool timer;
    public int bossHealth = 100;

    int ball1 = 1;
    int ball2 = 0;

    Timer ti;
    TimerFonce ra;
    TimerLaser ma;

    int checkBoule;
    public int maxSpawnCheckBoule;
    int checkFonce;
    public int maxCheckFonce;
    int checkLaser;
    public int maxCheckLaser;
    public int damageAmount = 10;

    public int health;

    bool test;
    int passageAttack;
    // ui
    public GameObject HealthBar;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        arr = false;
        tst = false;
        boule = false;
        ti = gameObject.GetComponent<Timer>();
        ra = gameObject.GetComponent<TimerFonce>();
        ma = gameObject.GetComponent<TimerLaser>();

        checkBoule = 1;
        maxSpawnCheckBoule = 3;
        checkFonce = 0;
        maxCheckFonce = 3;
        checkLaser = 0;
        maxCheckLaser = 3;

        test = false;
        passageAttack = 1;
        health = bossHealth;

}

    // Update is called once per frame
    void Update()
    {
        if(health <= bossHealth/3) // phase3
        {
            if (checkLaser <= maxCheckLaser && passageAttack == 1)
            {
                Laser();
            }
            else if (checkLaser >= maxCheckLaser && passageAttack == 1)
            {
                checkFonce = 0;
                passageAttack = 2;
            }

            if(checkFonce<= maxCheckFonce && passageAttack == 2)
            {
                Fonce();
            }
            else if (checkFonce>=maxCheckFonce && passageAttack == 2)
            {
                checkBoule = 0;
                passageAttack = 3;
            }
            
            if (checkBoule <= maxSpawnCheckBoule && passageAttack == 3)
            {
                BouleDefeu();
            }
            else if (checkBoule>=maxSpawnCheckBoule && passageAttack == 3)
            {
                checkLaser = 0;
                passageAttack = 0;
            }
            
        }
        if(health > bossHealth/3 && health <= (bossHealth/3)*2)
        {
            if (checkLaser <= maxCheckLaser && test)
            {
                Laser();
            }
            else if(checkLaser >= maxCheckLaser &&test)
            {
                test = false;
                checkBoule = 0;
            }

            if ((checkBoule <= maxSpawnCheckBoule) && !test)
            {

                BouleDefeu();

            }
            else if (checkBoule >= maxSpawnCheckBoule && !test)
            {
                test = true;
                checkLaser = 0;
            }

        }
        if (health > (bossHealth/3)*2)
        {
            

            
            if ((checkBoule <= maxSpawnCheckBoule) && !test)
             {
                Debug.Log("Pog");
                 BouleDefeu();

             }
             else if (checkBoule >= maxSpawnCheckBoule && !test)
             {
                 test = true;
                 checkFonce = 0;
             }

             print(checkFonce);
             if (checkFonce <= maxCheckFonce && test)
             {
                 Fonce();
             }
             else if(checkFonce >= maxCheckFonce && test)
             {
                 test = false;
                 checkBoule = 1;
             }

        }

        if(HealthBar != null)
        {

        }
    }

    void BouleDefeu(  )
    {
        if (ball1 == 1)
        {
            List<GameObject> balla = new List<GameObject>();
            if (!timer)
            {
                ti.verfi = true;
            }
            else if (timer)
            {
                
                for (int f = 0; f < gameObject.transform.GetChild(0).transform.childCount; f++)
                {
                    
                    GameObject bally = Instantiate(prefabBoule, gameObject.transform.GetChild(0).gameObject.transform.GetChild(f).gameObject.transform.position, gameObject.transform.GetChild(0).gameObject.transform.GetChild(f).gameObject.transform.rotation, gameObject.transform.GetChild(0).gameObject.transform.GetChild(f).gameObject.transform);
                    balla.Add(bally);
                }
                for (int f = 0; f < balla.Count; f++)
                {
                    
                    GameObject bally = balla[f];
                    bally.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1f, 0f) * 100);
                }
               
                timer = false;
                ball2 = 1;
                ball1 = 0;
            }
           
            
        }

        else if (ball2 == 1)
        {
            List<GameObject> balla = new List<GameObject>();
            if (!timer)
            {

                ti.verfi = true;
                


            }
            else if (timer)
            {
                checkBoule++;
                for (int f = 0; f < gameObject.transform.GetChild(1).transform.childCount; f++)
                {
                    
                    GameObject bally = Instantiate(prefabBoule, gameObject.transform.GetChild(1).gameObject.transform.GetChild(f).gameObject.transform.position, gameObject.transform.GetChild(1).gameObject.transform.GetChild(f).gameObject.transform.rotation, gameObject.transform.GetChild(1).gameObject.transform.GetChild(f).gameObject.transform);
                    balla.Add(bally);
                }
                for (int f = 0; f < balla.Count; f++)
                {
                   
                    GameObject bally = balla[f];
                    bally.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1f, 0f) * 100);
                }
                timer = false;
                ball1 = 1;
                ball2 = 0;
                
            }
            
        }
        

    }



    void Laser ()
    {
        if (!tst)
        {
            PositionPlayer();

            tst = true;
        }
        if (!arr)
        {
            ma.verfi = true;
        }
        else
        {
           
            Vector3 direction = playerPos - gameObject.transform.position;
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, 100f);
            Debug.DrawRay(transform.position, direction * 100, Color.red);


            for (int i = 0; i < hit.Length; i++)
            {
                Debug.Log(hit[i].transform.name);
                if (hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    hit[i].transform.GetComponent<PlayerControler>().health -= 10;// TODO damage a def
                 
                    tst = false;
                    arr = false;
                   
                }
            }
            tst = false;
            arr = false;
            checkLaser++;
        }
       
           
           

       

        

    }

    void PositionPlayer()
    {
        playerPos = player.transform.position;

    }

    void Fonce ()
    {
        if (!boule)
        {
            ra.verfi = true;
        }
        else if (boule)
        {
            if (!tst)
            {
                PositionPlayer();

                tst = true;
            }
            if (gameObject.transform.position != playerPos)
            {
                force();

            }
            else
            {
                tst = false;
                boule = false;
                checkFonce++;

            }
            
        }
    }

    void force()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerPos, 15f * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.transform.Translate(((collision.transform.position - gameObject.transform.position).normalized) * 20 * Time.deltaTime);

            //collision.transform.GetComponent<Rigidbody2D>().AddForce(((gameObject.transform.position - collision.transform.position).normalized) * 5);
        
             collision.gameObject.GetComponent<PlayerControler>().health -= 10; // TODO damage à définir
           
        }
    }
}
