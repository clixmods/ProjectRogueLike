using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTuto : MonoBehaviour
{
    public GameObject spawnPlayer;
    public GameObject pointOfSpawn;
    GameObject player;
    public GameObject HudPrefab;
    public GameObject InstancePnj;
     public GameObject PNJ2;
    GameObject pn;

    public bool parlerAuPnj;
    public bool etape1;
    public bool etape2;
    public bool etape3;
    public bool finishTuto;


    bool check;
    // Start is called before the first frame update
    void Start()
    {
        // finishTuto = false;
        if(GameManager.GameUtil != null)
            finishTuto = GameManager.GameUtil.tutoIsFinished;

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
        if (parlerAuPnj && etape1 && etape2 && !check)
        {
            pn = Instantiate(PNJ2, InstancePnj.transform.position, Quaternion.identity);
            pn.GetComponent<DialogueTrigger>().levelManager = gameObject.GetComponent<LevelManagerTuto>();
           // PNJ2.SetActive(true);
            check = true;
        }

        GameManager.GameUtil.tutoIsFinished = finishTuto;

    }
}
