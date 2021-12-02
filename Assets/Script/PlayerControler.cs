using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public bool isDead = false;
    public int health;
    public int MaxHealth;
    public int PlayerShield;
    [Range(0, 10)]
    public int PlayerLifes;
    [Range(0, 10)]
    public int PlayerMaxLifes;

    public Animator animator;

    public float playerMoveSpeed = 1;
    public GameObject armeCorpACorp;
    public GameObject armeDistance;
    //public MeshRenderer armeCorpACorpMesh;
    public GameObject CurrentWeapon;
    int distOrCorp;

    public GameObject listC;
    public GameObject listD;

    public int selectCorpACorp = 0;
    public int selectDist = 0;

    float AttackAngle;
    //Cooldown between damage
    float currentCooldown = 0;
    public float maxCooldown = 1.5f;
    public bool isDamaged = false;
    // Cooldown between Life
    float currentCooldownLife = 0;
    public float maxCooldownLife = 5;
    public bool isLastStand = false;

    float timePressed = 0;
    [Range(0, 2)]
    public float delayToPress = 0.75f;
    public bool isWeaponWheel;

    private void Start()
    {
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
        UpdateUI();
        if (GameManager.GameUtil.isPaused)
        {
            return;
        }
            

        CheckHealth();
        if (isDead)
            return;

        Movment();
        Weapon();
        AimManager();
        
    }
    private void UpdateUI()
    {
        HUDManager.HUDUtility.PlayerHealth = health;
        HUDManager.HUDUtility.PlayerMaxHealth = MaxHealth;
        HUDManager.HUDUtility.PlayerLife = PlayerLifes;
        HUDManager.HUDUtility.PlayerMaxLifes = PlayerMaxLifes;
        if (CurrentWeapon != null && CurrentWeapon.TryGetComponent<WeaponManager>(out WeaponManager Component))
        {
            switch(Component.AmmoTypeId)
            {
                case 0:
                    HUDManager.HUDUtility.PlayerAmmoCount = Component.CurrentAmmoCount;
                    if(Component.CurrentAmmoCount <= 0)
                        HUDManager.HUDUtility.SetMiddleMsg(2, "No ammo");

                    HUDManager.HUDUtility.isHeating = false;
                    break;
                case 1:
                    HUDManager.HUDUtility.PlayerAmmoCount = (int)Component.pourcentageHeating;
                    HUDManager.HUDUtility.isHeating = true;
                    break;
            }  
        }
    }
    private void CheckHealth()
    {
        if(isLastStand)
        {
            if (currentCooldownLife < maxCooldownLife )
            {
                currentCooldownLife += Time.deltaTime;
                //print((MaxHealth / maxCooldownLife) * Time.deltaTime);
                //health += (int)((int)((float)MaxHealth / (float)maxCooldownLife)*Time.deltaTime);
               // print((int)((int)((float)MaxHealth / (float)maxCooldownLife) * Time.deltaTime));
            }
            else
            {
                health = MaxHealth;
                currentCooldownLife = 0;
                isLastStand = false;
                PlayerLifes--;
            }
        }
        if (isDamaged)
        {
            if(currentCooldown < maxCooldown)
            {
                currentCooldown += Time.deltaTime;
            }
            else
            {
                currentCooldown = 0;
                isDamaged = false;
            }
        }
        if(health < 0 && !isLastStand)
        {
            health = 0;
            if (PlayerLifes > 0 )
            {
                isLastStand = true;
                HUDManager.HUDUtility.SetMiddleMsg(4, "Used Life");

            }
            else // Player is dead
            {
                isDead = true;
                HUDManager.HUDUtility.UIGameOverWidget.SetActive(true);
                GameManager.GameUtil.isGameover = true;
            }
        }
    }
    private void AimManager()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      //  Camera.main.gameObject.transform.GetChild(0).transform.position = mousePosition; // On met le viseur sur la position de la souris � l'�cran
        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
        AttackAngle = AngleBetweenTwoPoints(transform.position, mousePosition);
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

        // On change l'ordre des sprite order suivant l'angle pour que ce soit styler ta vue
        if ((AttackAngle >= -45 && AttackAngle <= 45) || (AttackAngle >= -135 && AttackAngle <= 45)) 
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
        Vector2 movementVector = Vector2.zero;
        if (Input.GetKey(KeyCode.Z))
        {
            movementVector += Vector2.up;
            animator.SetInteger("state", 4);
        }

        if(Input.GetKey(KeyCode.S))
        {
            movementVector += Vector2.down;
            animator.SetInteger("state", 2);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            movementVector += Vector2.left;
            animator.SetInteger("state", 1);
        }

        if(Input.GetKey(KeyCode.D))
        {
            movementVector += Vector2.right;
            animator.SetInteger("state", 3);
        }
        transform.Translate(movementVector.normalized * playerMoveSpeed * Time.deltaTime);

    }
    public void EnableMeleeType()
    {
        if(CurrentWeapon != null)
            CurrentWeapon.SetActive(false);
        
        armeCorpACorp.SetActive(true);
        CurrentWeapon = armeCorpACorp;
        distOrCorp = 1;
        armeCorpACorp.GetComponent<ManagerWeaponCorpAcopr>().Attacker = gameObject;
    }
    public void EnableDistanceType()
    {
        armeCorpACorp.SetActive(false);
        armeDistance.SetActive(true);
        CurrentWeapon = armeDistance;
        distOrCorp = 2;
    }
    private void Weapon()
    { 
      if(Input.GetKey(KeyCode.A))
      {
            EnableMeleeType();
      }
          if(Input.GetKey(KeyCode.A) && !GameManager.GameUtil.isWeaponWheel)
          {
                print("cringe/20");
                
                if(timePressed <= delayToPress)
                {
                    timePressed += Time.deltaTime;
                }
                else
                {
                    GameManager.GameUtil.isWeaponWheel = true;
                    timePressed = 0;
                    Debug.Log("Touche pressed");
                }

          }
     
          else if (Input.GetKey(KeyCode.E) && !GameManager.GameUtil.isWeaponWheel)
          {
          
                if (timePressed <= delayToPress)
                {
                    timePressed += Time.deltaTime;
                }
                else
                {
                    GameManager.GameUtil.isWeaponWheel = true;
                    timePressed = 0;
                    Debug.Log("Touche pressed");
                }

          }
         else
         {
            //GameManager.GameUtil.isWeaponWheel = false;
            timePressed = 0;
         }

          

        if (distOrCorp == 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && !CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>().IsFiring) // On change d'arme avec la molette
            {
                armeCorpACorp.SetActive(false);
                selectCorpACorp += Mathf.FloorToInt(Input.GetAxis("Mouse ScrollWheel") * 10);

                if (listC.transform.childCount - 1 < selectCorpACorp)   selectCorpACorp = 0;
              
                if (selectCorpACorp < 0)  selectCorpACorp = listC.transform.childCount - 1;

                armeCorpACorp = listC.transform.GetChild(selectCorpACorp).gameObject;
                armeCorpACorp.SetActive(true);
                CurrentWeapon = armeCorpACorp;
                armeCorpACorp.GetComponent<ManagerWeaponCorpAcopr>().Attacker = gameObject;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnableDistanceType();
        }

        if (distOrCorp == 2)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0) // On change d'arme avec la molette
            {
                armeDistance.SetActive(false);
                //print(selectDist);
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
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (AttackAngle >= -45 && AttackAngle <= 45)
                {
                    animator.SetInteger("state", 1);

                }
                else if (AttackAngle >= 45 && AttackAngle <= 135)
                {
                    animator.SetInteger("state", 2);

                }
                else if (AttackAngle >= 135 && AttackAngle <= 225)
                {
                    animator.SetInteger("state", 3);
                }
                else if (AttackAngle >= -135 && AttackAngle <= 45)
                {
                    animator.SetInteger("state", 4);
                }
                if (distOrCorp == 1)
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
}
