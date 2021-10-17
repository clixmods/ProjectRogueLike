using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject InstanceRef;
    public PlayerControler InstanceRefController;
    [Header("HUD ELEMENTS")]
    public GameObject UIHealthBar;
    public GameObject UILifes;
    public GameObject UIShieldBar;
    public GameObject UIPlayerScore;
    public GameObject UIPlayerScoreMultiplier;
    public GameObject UIPlayerAmmoCount;
    public GameObject UIPlayerWeaponMelee;
    public GameObject UIPlayerWeaponDistance;
    public GameObject UIHealthEnnemiPrefab;
    [Header("HUD INFO")]
    public string PlayerHealth;
    public string PlayerLife;
    public string PlayerShield;
    public string PlayerScore;
    public string PlayerScoreMultiplier;
    public string PlayerAmmoCount;
    private GameObject GameManager;
    public GameObject[] Ennemies;
    public GameObject[] EnnemiesHWidgets;
    private GameObject oof;
    public GameObject Ennemi;
    GameObject HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameController");
        InstanceRef = GameManager.GetComponent<GameManager>().CurrentPlayer;
        if(InstanceRef != null)
            InstanceRefController = InstanceRef.GetComponent<PlayerControler>();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = transform.position;
        position.x = mousePosition.x; // On met le viseur sur la position de la souris � l'�cran
        position.y = mousePosition.y; // On met le viseur sur la position de la souris � l'�cran

        //oof = Instantiate(UIHealthEnnemiPrefab, position,Quaternion.identity);
        // Ennemies[0] = Instantiate(UIHealthEnnemiPrefab);
        // Ennemies[1] = Instantiate(UIHealthEnnemiPrefab);
      //   HealthBar = Instantiate(UIHealthEnnemiPrefab, transform);
      //  Vector2 mousePositiona = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      //  Vector3 positiona = transform.position;
       // position.x = mousePositiona.x; 
      //  position.y = mousePositiona.y; 

       // oof = Instantiate(UIHealthEnnemiPrefab, positiona, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
       // if (oof == null )
         //   oof = Instantiate(UIHealthEnnemiPrefab);
        if(Ennemi != null)
        {
         
         
            
        }
    //    if(Camera.main.transform.GetChild(0).GetComponent<CursorManager>().target != null && Camera.main.transform.GetChild(0).GetComponent<CursorManager>().target.tag == "Ennemies" )
      //  {
        //    Vector3 APostion = HealthBar.transform.position;
       //     APostion.x = Camera.main.transform.GetChild(0).GetComponent<CursorManager>().target.transform.position.x;
       //     APostion.y = Camera.main.transform.GetChild(0).GetComponent<CursorManager>().target.transform.position.y;

      //      Vector2 mousePosition = Camera.main.ScreenToWorldPoint(APostion);
           
     //       HealthBar.transform.position = Camera.main.transform.GetChild(0).GetComponent<CursorManager>().target.transform.position;
      //  }
        if (Ennemi != null)
        {
            
            Vector3 position = oof.gameObject.transform.position;
            
            oof.transform.position = Ennemi.transform.position;
        }
        /*
        Ennemies = GameObject.FindGameObjectsWithTag("Ennemies");
        if (Ennemies.Length > 0)
        {
            for(int i = 0; i< Ennemies.Length; i++)
            {
                if(EnnemiesHWidgets.Length < 1)
                {
                    print("Helatber spawned");
                      
                    EnnemiesHWidgets[EnnemiesHWidgets.Length] = Instantiate(UIHealthEnnemiPrefab, transform);

                }
                //else
                EnnemiesHWidgets[EnnemiesHWidgets.Length].transform.position = Ennemies[i].transform.position;
            }
            
        }
        */
        if (InstanceRef != null)
        {
            UpdateUIElements();
            if (InstanceRefController.CurrentWeapon != null)
            {
                WeaponManager wpnManager = InstanceRefController.CurrentWeapon.GetComponent<WeaponManager>();
                if (wpnManager != null)
                {
                    if (wpnManager.AmmoTypeId == 0)
                        PlayerAmmoCount = wpnManager.CurrentAmmoCount.ToString();
                    else if (wpnManager.AmmoTypeId == 1)
                        PlayerAmmoCount = wpnManager.pourcentageHeating.ToString();
                    else
                        PlayerAmmoCount = "0";
                }
                else
                {
                    PlayerAmmoCount = "0";
                }
            }
        }
        else
        { 
            InstanceRef = GameManager.GetComponent<GameManager>().CurrentPlayer;
            InstanceRefController = InstanceRef.GetComponent<PlayerControler>();
        }
    }

    void UpdateUIElements()
    {/*
        public string UIHealthBar;
        public string UILifes;
        public string UIShieldBar;
        public string UIPlayerScore;
        public string UIPlayerScoreMultiplier;
        public string UIPlayerAmmoCount;
        public string UIPlayerWeaponMelee;
        public string UIPlayerWeaponDistance;
        [Header("HUD INFO")]
        public string PlayerHealth;
        public string PlayerLife;
        public string PlayerShield;
        public string PlayerScore;
        public string PlayerScoreMultiplier;
        public string PlayerAmmoCount;
    */
        void GetAndSetHealthValue()
        {
           // UIHealthBar.GetComponent<>; // TODO : faut cr�e la variable HUDIcon 
            
        }


        void Opacity(Image image)
        {
            Color color = image.color;
            color.a = 1f;
            image.color = color;
        }
        void ReduceOpacity(Image image)
        {
            Color color = image.color;
            color.a = 0.5f;
            image.color = color;
        }
        GetAndSetWeaponDistanceIcon();
        GetAndSetWeaponCACIcon();
        void GetAndSetWeaponCACIcon()
        {
            if (InstanceRef != null &&
           InstanceRefController.CurrentWeapon != null &&
           InstanceRefController.CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>() != null)
            {
                ReduceOpacity(UIPlayerWeaponDistance.GetComponent<Image>() );
                Opacity(UIPlayerWeaponMelee.GetComponent<Image>());
                //Debug.Log("UI UPDATE : WeaponDistanceIcon");
                //UIPlayerWeaponMelee.GetComponent<Image>().sprite = InstanceRefController.CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>().HUDIcon; 
            }
            else
            {
                //Debug.Log("UI UPDATE : WeaponDistanceIcon n'est pas d�fini");
            }
        }
        void GetAndSetWeaponDistanceIcon()
        {
            if (InstanceRef != null &&
           InstanceRefController.CurrentWeapon != null &&
           InstanceRefController.CurrentWeapon.GetComponent<WeaponManager>() != null)
            {
                //Debug.Log("UI UPDATE : WeaponDistanceIcon");
                ReduceOpacity(UIPlayerWeaponMelee.GetComponent<Image>());
                Opacity(UIPlayerWeaponDistance.GetComponent<Image>());
                UIPlayerAmmoCount.GetComponent<Text>().text = PlayerAmmoCount;
                UIPlayerWeaponDistance.GetComponent<Image>().sprite = InstanceRefController.CurrentWeapon.GetComponent<WeaponManager>().HUDIcon;
            }
            else
            {
                //Debug.Log("UI UPDATE : WeaponDistanceIcon n'est pas d�fini");
            }
        }
    }
}
