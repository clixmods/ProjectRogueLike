using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntréSortie : MonoBehaviour
{
    public GameObject PointDeSpawn;
    public string message;
    public string message2;
    public int passage;
    public LevelManagerTuto levelManger;
    bool pass;
    GameObject player;
    bool crea1;
    bool crea2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(levelManger.finishTuto)
        {
            passage = 1;
        }
        if (Input.GetKeyDown(KeyCode.P) && pass)
        {
            player.gameObject.transform.position = PointDeSpawn.transform.position;
            pass = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (passage == 1)

            {
                if (!crea1)
                {
                    HUDManager.HUDUtility.CreateHintString(gameObject, message, 1f);
                    crea1 = true;
                }
                player = collision.gameObject;
                pass = true;
            }
            if(passage == 0)
            {
                if (!crea2)
                {
                    HUDManager.HUDUtility.CreateHintString(gameObject, message2, 1f);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pass = false;
        }
        }

}
