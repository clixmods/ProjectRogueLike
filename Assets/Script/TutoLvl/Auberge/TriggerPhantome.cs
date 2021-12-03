using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhantome : MonoBehaviour
{
    bool instance;
    public GameObject phantome;
    public GameObject porte;
    GameObject p;
    GameObject ennemie;
    public LevelManagerTuto levelManager;
    bool destroy;
    bool pass;
    bool crea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(instance && ennemie == null && ! destroy)
        {
            Destroy(gameObject.transform.GetChild(0).GetChild(1).gameObject);
            Destroy(gameObject.transform.GetChild(0).GetChild(0).gameObject);
            levelManager.etape1 = true;
            destroy = true;
            
        }
        if (Input.GetKey(KeyCode.P) && pass)
        {
            InstancePhantome();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelManager.parlerAuPnj && !levelManager.finishTuto && !pass)
        {
            if (!crea)
            {
                HUDManager.HUDUtility.CreateHintString(gameObject, "Click On [P] to see the dead man", 1f);
                crea = true;
            }
            pass = true;
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (levelManager.parlerAuPnj && !levelManager.finishTuto && !instance)
        {
            pass = false;
        }
    }

    void InstancePhantome ()
    {
        if(!instance)
        {
            p = Instantiate(porte, gameObject.transform.GetChild(0).GetChild(1).transform.position, Quaternion.identity, gameObject.transform.GetChild(0).GetChild(1).transform);
            p.GetComponent<Collider2D>().enabled = true;
            p = Instantiate(porte, gameObject.transform.GetChild(0).GetChild(0).transform.position, Quaternion.identity, gameObject.transform.GetChild(0).GetChild(0).transform);
            p.GetComponent<Collider2D>().enabled = true;
            ennemie = Instantiate(phantome, gameObject.transform.position, Quaternion.identity);
            instance = true;
        }
    }
}
