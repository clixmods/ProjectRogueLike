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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(instance && ennemie == null && ! destroy)
        {
            Destroy(gameObject.transform.GetChild(0).GetChild(1));
            Destroy(gameObject.transform.GetChild(0).GetChild(0));
            levelManager.etape1 = true;
            destroy = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelManager.parlerAuPnj && !levelManager.finishTuto)
        {
            HUDManager.HUDUtility.CreateHintString(gameObject, "Click On [P] to see the dead man", 0.5f);
            if (Input.GetKey(KeyCode.P))
            {
                InstancePhantome();
            }
        }
    }

    void InstancePhantome ()
    {
        if(!instance)
        {
            p = Instantiate(porte, gameObject.transform.GetChild(0).GetChild(1).transform.position, Quaternion.identity);
            p.GetComponent<Collider2D>().enabled = true;
            p = Instantiate(porte, gameObject.transform.GetChild(0).GetChild(0).transform.position, Quaternion.identity);
            p.GetComponent<Collider2D>().enabled = true;
            ennemie = Instantiate(phantome, gameObject.transform.position, Quaternion.identity);
            instance = true;
        }
    }
}
