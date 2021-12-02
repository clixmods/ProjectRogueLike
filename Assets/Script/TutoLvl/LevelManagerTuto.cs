using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTuto : MonoBehaviour
{
    public GameObject spawnPlayer;
    public GameObject pointOfSpawn;
    GameObject player;
    public GameObject HudPrefab;

    public bool parlerAuPnj;
    public bool etape1;
    public bool etape2;
    public bool etape3;
    public bool finishTuto;
    // Start is called before the first frame update
    void Start()
    {
        finishTuto = false;
        player = Instantiate(spawnPlayer, pointOfSpawn.transform.position, Quaternion.identity);
        Instantiate(HudPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(etape1 && etape2 && etape3)
        {
            finishTuto = true;
        }
    }
}
