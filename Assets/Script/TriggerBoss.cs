using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public GameObject prefabBoss;
    public GameObject porte;
    public GameObject porte1;
    bool check;
    // Start is called before the first frame update
    void Start()
    {
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") && !check)
        {
            Instantiate(prefabBoss, gameObject.transform.position, Quaternion.identity);
            for(int i = 0; i<gameObject.transform.childCount; i ++)
            {
                int port = gameObject.transform.GetChild(i).gameObject.GetComponent<ScriptPorte>().rightOrLeft;
                if(port == 1)
                {
                    Instantiate(porte, gameObject.transform.GetChild(i).transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(porte1, gameObject.transform.GetChild(i).transform.position, Quaternion.identity);
                }
            }
            check = true;
        }
    }
}
