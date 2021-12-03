using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerFin : MonoBehaviour
{
    public GameObject pointspawn;
    public GameObject spawnPlayer;
    public GameObject hud;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnPlayer, pointspawn.transform.position, Quaternion.identity);
        Instantiate(hud, gameObject.transform.position, Quaternion.identity);
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
