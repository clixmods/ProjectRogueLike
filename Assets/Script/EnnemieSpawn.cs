using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieSpawn : MonoBehaviour
{
    public EnnemieList ennemieList;
    int rand;
    int verifRandS = 0;
     int numberOfSpawnTotal;
     int numberOfSpawn = 1;

    public int maxOfspawn;
    public int minOfSpawn;

    public float time;
    public float timeToReach;
    GameObject ennemie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (verifRandS == 0)
        {
            RandSpawn();

        }
        //LimiteSpawnEnnemie();
        SpawnEnnemie();
        LimiteSpawnEnnemie();
    }

    public void LimiteSpawnEnnemie()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (time < timeToReach)
            {
                time += Time.deltaTime;


            }
            else if (time >= timeToReach)
            {

               // print("cc");
                if (!gameObject.transform.GetChild(i).gameObject.activeSelf)
                    gameObject.transform.GetChild(i).gameObject.transform.position = gameObject.transform.position;

                gameObject.transform.GetChild(i).gameObject.SetActive(true);
                

                time = 0;
            }
        }
    
    }

   /* IEnumerator MyCoroutine()
    {
        print("ca commence");
        yield return new WaitForSecondsRealtime(0.1f);
        SpawnSpawnEnnemie();
    }*/

    public void SpawnEnnemie ()
    {
        if (numberOfSpawn >= 0 && numberOfSpawn <= numberOfSpawnTotal)
        {
            print("cc2");
            List<GameObject> prefabEnnemie = ennemieList.prefabEnnemies;
            rand = Random.Range(0, prefabEnnemie.Count);
            ennemie = Instantiate(prefabEnnemie[rand], gameObject.transform.position, Quaternion.identity, gameObject.transform);
            ennemie.SetActive(false);
            numberOfSpawn = numberOfSpawn + 1;

        }



        
    }

    public void RandSpawn ()
    {
        numberOfSpawnTotal = Random.Range(minOfSpawn, maxOfspawn);
        verifRandS = 1;

    }
}
