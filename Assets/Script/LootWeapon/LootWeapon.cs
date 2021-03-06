using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("yo");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject weapon = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            GameObject pivot = gameObject.transform.GetChild(0).gameObject;
            if (weapon.layer == LayerMask.NameToLayer("CorpACorp"))
            {
                gameObject.transform.position = collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.position;
                //GameObject weapon = gameObject.transform.GetChild(0).gameObject;
                // HUDManager.HUDUtility.MSG
                HUDManager.HUDUtility.SetMiddleMsg(2, "New melee weapon unlocked: "+ weapon.name);
                weapon.SetActive(false);
                pivot.transform.GetChild(0).gameObject.transform.SetParent(collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform, false);
                //Destroy(gameObject.transform.GetChild(0).gameObject);
                Destroy(gameObject);
            }
            if (weapon.layer == LayerMask.NameToLayer("Distance"))
            {
                gameObject.transform.position = collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform.position;
                //GameObject weapon = gameObject.transform.GetChild(0).gameObject;
                weapon.SetActive(false);
                HUDManager.HUDUtility.SetMiddleMsg(2, "New distance weapon unlocked: " + weapon.name);
                pivot.transform.GetChild(0).gameObject.transform.SetParent(collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).transform, false);
                //Destroy(gameObject.transform.GetChild(0).gameObject);
                Destroy(gameObject);
            }
        }
    }


   
}
