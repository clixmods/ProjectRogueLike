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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (passage == 1)

            {
                HUDManager.HUDUtility.CreateHintString(gameObject, message, 0.5f);
                if (Input.GetKey(KeyCode.P))
                {
                    collision.gameObject.transform.position = PointDeSpawn.transform.position;
                }
            }
            if(passage == 0)
            {
                HUDManager.HUDUtility.CreateHintString(gameObject, message2, 0.5f);
            }
        }

    }
}
