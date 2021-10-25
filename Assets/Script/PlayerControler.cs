using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public int health;
    public int MaxHealth;
    public int PlayerShield;
    [Range(0, 10)]
    public int PlayerLifes;
    [Range(0, 10)]
    public int PlayerMaxLifes;


    public float playerMoveSpeed = 1;
    public GameObject armeCorpACorp;
    public GameObject armeDistance;
    //public MeshRenderer armeCorpACorpMesh;
    public GameObject CurrentWeapon;
    int distOrCorp;

    GameObject listC;
    GameObject listD;

    int selectCorpACorp = 0;
    int selectDist = 0;

    float AttackAngle;
    private void Start()
    {
        Debug.Log("LE BAD");
        print("Bas Oui tu as raisond c'est le bad");
        listC = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        listD = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;

        for (int i = 0; i <= listC.transform.childCount - 1; i++)
        {
            armeCorpACorp = listC.transform.GetChild(i).gameObject;
            armeCorpACorp.SetActive(false);
            armeCorpACorp.GetComponent<ManagerWeaponCorpAcopr>().Attacker = gameObject;
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
        AimManager();
        CheckHealth();
    }
    private void CheckHealth()
    {
        if(health < 0)
        {
            health = 0;
            if (PlayerLifes > 0)
            {
                health = MaxHealth;
                PlayerLifes--;
            }
            else // Player is dead
            {
                Debug.Log("The player is dead");
            }
        }
    }
    private void AimManager()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Camera.main.gameObject.transform.GetChild(0).transform.position = mousePosition; // On met le viseur sur la position de la souris � l'�cran
        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
        AttackAngle = AngleBetweenTwoPoints(transform.position, mousePosition);
        //listC.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle));
        //listD.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle));
        if(CurrentWeapon != null)
        {
            AimManagerForWeapon();
        }
       
    }
    void AimManagerForWeapon()
    {
        if (CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>() != null )
        {
            if (!CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>().IsFiring )
                listD.transform.parent.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle));
        }
        else if(CurrentWeapon.GetComponent<WeaponManager>() != null)
            listD.transform.parent.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle));

        if ((AttackAngle >= -45 && AttackAngle <= 45) || (AttackAngle >= -135 && AttackAngle <= 45)) // on baisselayer wweapons
        {
            CurrentWeapon.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else if ((AttackAngle >= 45 && AttackAngle <= 135) || (AttackAngle >= 135 && AttackAngle <= 225))
        {
            CurrentWeapon.transform.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
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
            if (CurrentWeapon != null)
            {
                CurrentWeapon.SetActive(false);
            }
            armeCorpACorp.SetActive(true);
            

            CurrentWeapon = armeCorpACorp;
           // CurrentWeapon.GetComponent<SpriteRenderer>().flipX = true;
            distOrCorp = 1;

            armeCorpACorp.GetComponent<ManagerWeaponCorpAcopr>().Attacker = gameObject;

        }
      if (distOrCorp == 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0) // On change d'arme avec la molette
            {
                armeCorpACorp.SetActive(false);
                selectCorpACorp += Mathf.FloorToInt(Input.GetAxis("Mouse ScrollWheel") * 10);

                if (listC.transform.childCount - 1 < selectCorpACorp)   selectCorpACorp = 0;
              
                if (selectCorpACorp < 0)  selectCorpACorp = listC.transform.childCount - 1;

                armeCorpACorp = listC.transform.GetChild(selectCorpACorp).gameObject;
                armeCorpACorp.SetActive(true);
                CurrentWeapon = armeCorpACorp;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            armeCorpACorp.SetActive(false);
            armeDistance.SetActive(true);
            CurrentWeapon = armeDistance;
            distOrCorp = 2;
        }

        if (distOrCorp == 2)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0) // On change d'arme avec la molette
            {
                armeDistance.SetActive(false);
                print(selectDist);
                selectDist += Mathf.FloorToInt(Input.GetAxis("Mouse ScrollWheel") * 10);
                if (listD.transform.childCount - 1 < selectDist)
                {
                    selectDist = 0;
                    
                }
                if (selectDist < 0)
                {
                    selectDist = listD.transform.childCount - 1;
                    
                }
                armeDistance = listD.transform.GetChild(selectDist).gameObject;
                armeDistance.SetActive(true);
                CurrentWeapon = armeDistance;

            }

        
        }
        if (CurrentWeapon != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.gameObject.transform.GetChild(0).transform.position = mousePosition; // On met le viseur sur la position de la souris � l'�cran
            


            if (Input.GetKey(KeyCode.Mouse0) && distOrCorp == 1) 
            {
                ManagerWeaponCorpAcopr cc = armeCorpACorp.GetComponent<ManagerWeaponCorpAcopr>();
                cc.Attack(AttackAngle);
            }
            if (Input.GetKey(KeyCode.Mouse0) && distOrCorp == 2)
            {
                armeDistance.GetComponent<WeaponManager>().Attack(gameObject, AttackAngle);

            }
        }

    }
}
