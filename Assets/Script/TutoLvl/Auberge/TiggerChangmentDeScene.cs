using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerChangmentDeScene : MonoBehaviour
{
    bool crea;
    bool pass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && pass)
        {

            GameManager.GameUtil.ChangeLevel(1);
            pass = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
                if (!crea)
                {
                    HUDManager.HUDUtility.CreateHintString(gameObject, "Click On [P] to enter the donjon", 1f);
                    crea = true;
                }
            //Chargement De Scène

            pass = true;
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
