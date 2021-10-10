using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float playerMoveSpeed = 1;
    public GameObject armeCorpACorp;
    public GameObject armeDistance;
    //public MeshRenderer armeCorpACorpMesh;
    GameObject weaponAssignement;
    int distOrCorp;

    GameObject listC;
    GameObject listD;

    int selectCorpACorp = 0;
    int selectDist = 0;
    private void Start()
    {
        listC = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        listD = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;


        for (int i = 0; i <= listC.transform.childCount - 1; i++)
        {
            armeCorpACorp = listC.transform.GetChild(i).gameObject;
            armeCorpACorp.SetActive(false);
        }
        for (int i = 0; i <= listD.transform.childCount - 1; i++)
        {
            armeDistance = listD.transform.GetChild(i).gameObject;
            armeDistance.SetActive(false);
        }
        armeCorpACorp = listC.transform.GetChild(selectCorpACorp).gameObject;
        armeDistance = listD.transform.GetChild(selectDist).gameObject;
        armeCorpACorp.SetActive(false);
        armeDistance.SetActive(false);
       
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
            transform.Translate(Vector2.up * playerMoveSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * playerMoveSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector2.left * playerMoveSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * playerMoveSpeed * Time.deltaTime);
        }
    }

    private void Weapon()
    { 
      if(Input.GetKey(KeyCode.A))
        {
            if (weaponAssignement != null)
            {
                weaponAssignement.SetActive(false);
            }
            armeCorpACorp.SetActive(true);
            weaponAssignement = armeCorpACorp;
            distOrCorp = 1;
            
            
            
        }
      if (distOrCorp == 1)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                armeCorpACorp.SetActive(false);
                selectCorpACorp = selectCorpACorp + 1;
                if (listC.transform.childCount - 1 >= selectCorpACorp)
                {
                    Debug.Log(listC.transform.childCount);
                    armeCorpACorp = listC.transform.GetChild(selectCorpACorp).gameObject;
                }
                else if (listC.transform.childCount - 1 < selectCorpACorp)
                {
                    selectCorpACorp = 0;
                    armeCorpACorp = listC.transform.GetChild(selectCorpACorp).gameObject;
                }
                armeCorpACorp.SetActive(true);
                weaponAssignement = armeCorpACorp;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            armeCorpACorp.SetActive(false);
            armeDistance.SetActive(true);
            weaponAssignement = armeDistance;
            distOrCorp = 2;
        }
        if (distOrCorp == 2)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                armeDistance.SetActive(false);
                selectDist = selectDist + 1;
                if (listD.transform.childCount - 1 >= selectDist)
                {
                    Debug.Log(listD.transform.childCount);
                    armeDistance = listD.transform.GetChild(selectDist).gameObject;
                }
                else if (listD.transform.childCount - 1 < selectDist)
                {
                    selectDist = 0;
                    armeDistance = listD.transform.GetChild(selectDist).gameObject;
                }
                armeDistance.SetActive(true);
                weaponAssignement = armeDistance;
            }
        }
        if (weaponAssignement != null)
        {
            if (Input.GetKey(KeyCode.Space) && distOrCorp == 1)
            {
                ManagerWeaponCorpAcopr cc = armeCorpACorp.GetComponent<ManagerWeaponCorpAcopr>();
                cc.Attack();
            }
            if (Input.GetKey(KeyCode.Space) && distOrCorp == 2)
            {
                armeDistance.GetComponent<WeaponManager>().Attack(armeDistance); 

            }
        }

    }
}
