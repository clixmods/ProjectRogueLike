using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float playerMoveSpeed = 1;
    public GameObject armeCorpACorp;
    public GameObject armeDistance;
    //public MeshRenderer armeCorpACorpMesh;
    public GameObject weaponAssignement;
    int distOrCorp;

    private void Start()
    {
        armeCorpACorp = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        armeDistance = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        armeCorpACorp.SetActive(false);
        armeDistance.SetActive(false);
        //armeCorpACorpMesh = armeCorpACorp.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Movment();
        Weapon();
    }

    private void Movment()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            transform.Translate(Vector2.up / playerMoveSpeed);
        }

        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down / playerMoveSpeed);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector2.left / playerMoveSpeed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right / playerMoveSpeed);
        }
    }

    private void Weapon()
    { 
      if(Input.GetKey(KeyCode.A))
        {
            armeDistance.SetActive(false);
            armeCorpACorp.SetActive(true);
            weaponAssignement = armeCorpACorp;
            distOrCorp = 1;
            
            //armeCorpACorp = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            
        }
      if(Input.GetKeyDown(KeyCode.E))
        {
            armeCorpACorp.SetActive(false);
            armeDistance.SetActive(true);
            weaponAssignement = armeDistance;
            distOrCorp = 2;
            
            //armeDistance = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        }

        if (weaponAssignement != null)
        {
            if (Input.GetKey(KeyCode.Space) && distOrCorp == 1)
            {
                ScriptCorpACorp cc = armeCorpACorp.GetComponent<ScriptCorpACorp>();
                cc.Attack();
            }
            if (Input.GetKey(KeyCode.Space) && distOrCorp == 2)
            {
                armeDistance.GetComponent<WeaponManager>().Attack(armeDistance); // mettre le correct argument

            }
        }

    }
}
