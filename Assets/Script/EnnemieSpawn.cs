using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemieSpawn : MonoBehaviour
{
    public EnnemieList ennemieList;
    int rand;
    public int numberOfSpawnTotal = 3;
    public int numberOfSpawn = 1;
    public float time = 2;
    public float timeToReach = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LimiteSpawnEnnemie();
    }

    void LimiteSpawnEnnemie()
    {
        if (numberOfSpawn >= 0 && numberOfSpawn <= numberOfSpawnTotal)
        {
            if (time < timeToReach)
            {
                time += Time.deltaTime;


            }
            else if (time >= timeToReach)
            {

                print("cc");
                SpawnEnnemie();
                time = 0;
            }
        }
    
    }

    void SpawnEnnemie ()
    {
        print("cc2");
        List<GameObject> prefabEnnemie = ennemieList.prefabEnnemies;
        rand = Random.Range(0, prefabEnnemie.Count);
        Instantiate(prefabEnnemie[rand], gameObject.transform.position, Quaternion.identity, gameObject.transform);
        numberOfSpawn = numberOfSpawn + 1;
    }
}
