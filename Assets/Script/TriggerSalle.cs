using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSalle : MonoBehaviour
{
    public List<GameObject> spawner;
    public List<GameObject> ennemieL;
    public int countEnnemie = -1;

    
    GameObject spawnSpawnEnnemie;
    GameObject spawnEnnemie;

    public GameObject prefabSpawnennemie;
    public GameObject prefabPorte;
    public GameObject prefabPorte2;

    int verifPassage;

    int q;

    LevelManager levelManager;

    Animator animatorPorte;

    // Start is called before the first frame update
    void Start()
    {
        q = 0;
       // levelManager = GameObject.Find("LevelManager").gameObject.transform.GetComponent<LevelManager>();
       // animatorPorte = prefabPorte.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(ennemieL.Count);
        //print(countEnnemie);


        //Watch number of ennemies
        if(countEnnemie > 0)
        {
            int counter = 0;
            for(int i = 0; i < ennemieL.Count; i++)
            {
                if (ennemieL[i] != null)
                    counter++;
            }
            countEnnemie = counter;
        }
            
        if(countEnnemie == 0)  // Clement: j'ai enlever le = car ca glitcher a -1 defois
        {
            levelManager.roomDone++;
            levelManager.roomDoneBoss++;
            if (levelManager.roomDone == levelManager.numberOfRoomToDo && levelManager.chestGot != 2)
            {
                GameObject chest = Instantiate(levelManager.chest, gameObject.transform.GetChild(2).gameObject.transform.position, Quaternion.identity);
                chest.transform.GetComponent<Chest>().list = levelManager.listWeapons.transform.GetComponent<WeaponList>();
                levelManager.chestGot++;
                levelManager.roomDone = 0;
            }

            for (int i = 0; i < gameObject.transform.GetChild(1).gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(1).GetChild(i).GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("CloseDoor", false);
                gameObject.transform.GetChild(1).GetChild(i).GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("OpenDoor", true);
                Destroy(gameObject.transform.GetChild(1).GetChild(i).gameObject.AddComponent<BoxCollider2D>());

            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            verifPassage++;
            
            InstanceSpawn();



        }
        

    }

    void InstanceSpawn ()
    {
        if (verifPassage == 1)
        {
            FermetureDesPorte();




                for (int i = 0; i < gameObject.transform.GetChild(0).gameObject.transform.childCount; i++)
            {
                //spawner.Add(gameObject.transform.GetChild(i).gameObject);
                spawnSpawnEnnemie = gameObject.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject;
                spawnEnnemie = Instantiate(prefabSpawnennemie, spawnSpawnEnnemie.transform.position, Quaternion.identity, spawnSpawnEnnemie.transform);
                spawner.Add(spawnEnnemie);
               
            }

            StartCoroutine(MyCoroutine());
        }
    }

    void FermetureDesPorte ()
    {
        for (int i = 0; i < gameObject.transform.GetChild(1).gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(1).GetChild(i).GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("CloseDoor", true);
            gameObject.transform.GetChild(1).GetChild(i).GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("OpenDoor", false);
            gameObject.transform.GetChild(1).GetChild(i).gameObject.AddComponent<BoxCollider2D>();

        }
    }



  /*  void ChoixInitiatePorte()
    {
        ScriptPorte scriptPorte = gameObject.transform.GetChild(1).gameObject.transform.GetChild(q).gameObject.GetComponent<ScriptPorte>();
        if (scriptPorte.rightOrLeft == 1)
        {
            Instantiate(prefabPorte, gameObject.transform.GetChild(1).gameObject.transform.GetChild(q).gameObject.transform.position, Quaternion.identity, gameObject.transform.GetChild(1).gameObject.transform.GetChild(q).gameObject.transform);
        }
        else if (scriptPorte.rightOrLeft == 2)
        {
            Instantiate(prefabPorte2, gameObject.transform.GetChild(1).gameObject.transform.GetChild(q).gameObject.transform.position, Quaternion.identity, gameObject.transform.GetChild(1).gameObject.transform.GetChild(q).gameObject.transform);
        }
        else
        {
            print("yotest");
        }

        q++;

        if (q < gameObject.transform.GetChild(1).gameObject.transform.childCount)
        {

            ChoixInitiatePorte();
        }

    }*/

    IEnumerator MyCoroutine()
    {
        print("ca commence");
        yield return new WaitForSecondsRealtime(1f);
        SpawnSpawnEnnemie();
    }
    void SpawnSpawnEnnemie ()
    {

        for (int i = 0; i < spawner.Count; i++)
        {
            print("yo");
            for (int f = 0; f < spawner[i].transform.childCount; f++)
            {
                print("yoyo");
                ennemieL.Add(spawner[i].transform.GetChild(f).gameObject);
                if (spawner[i].transform.GetChild(f).TryGetComponent<EnemyManager>(out EnemyManager component))
                {
                    if (!component.isChild)
                    {
                        // On specifie qu'il vienne dun spawner, et on lui attribue le trigger salle
                        component.FromSpawner = true;
                        component.TriggerSalle = this;
                        

                    }
                }    
            }
        }

        countEnnemie = ennemieL.Count;
        verifPassage = 0;

        //GameManager.GameUtil.isLoading = false;
    }

}
