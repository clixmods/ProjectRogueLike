using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerZombie : MonoBehaviour
{
    public GameObject zombiePrefab;
    public LevelManagerTuto levelManager;
    public List<GameObject> spawnZombie;
    public List<GameObject> Zombiespawned;
    bool instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(instance && gameObject.transform.GetChild(1).childCount == 0)
        {
            levelManager.etape2 = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(levelManager.etape1 && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            InstanceZombie();
        }

    }

    void InstanceZombie ()
    {
        if(!instance)
        {
            for(int i = 0; i<spawnZombie.Count; i++)
            {
                GameObject zom = Instantiate(zombiePrefab, spawnZombie[i].transform.position, Quaternion.identity, gameObject.transform.GetChild(1).transform);
               
            }
            instance = true;
        }
    }
}
