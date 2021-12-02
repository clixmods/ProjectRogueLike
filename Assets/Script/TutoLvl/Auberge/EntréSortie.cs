using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntréSortie : MonoBehaviour
{
    public GameObject PointDeSpawn;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(Input.GetKey(KeyCode.P))
            {
                collision.gameObject.transform.position = PointDeSpawn.transform.position;
            }
        }

    }
}
