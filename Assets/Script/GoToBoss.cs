using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToBoss : MonoBehaviour
{
    GameObject player;
    bool Check;
    public GameObject Text;
    public GameObject point;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Check)
        {
            Text.SetActive(true);
            if(Input.GetKey(KeyCode.P))
            {

                player.transform.position = point.transform.position;
            }
        }
        if(!Check)
        {
            Text.SetActive(true);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = collision.gameObject;
            Check = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Check = false;
        }

        }
}
