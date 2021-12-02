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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(instance && ennemie == null)
        {
            Destroy(gameObject.transform.GetChild(0).GetChild(1));
            Destroy(gameObject.transform.GetChild(0).GetChild(0));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Input.GetKey(KeyCode.P))
        {
            InstancePhantome();
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
