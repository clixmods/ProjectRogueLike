using System;
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

    int healthBarLimit = 15;

    string sssss;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameController");
        InstanceRef = GameManager.GetComponent<GameManager>().CurrentPlayer;
        if(InstanceRef != null)
            InstanceRefController = InstanceRef.GetComponent<PlayerControler>();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = transform.position;
        position.x = mousePosition.x; // On met le viseur sur la position de la souris à l'écran
        position.y = mousePosition.y; // On met le viseur sur la position de la souris à l'écran

        //oof = Instantiate(UIHealthEnnemiPrefab, position,Quaternion.identity);
        // Ennemies[0] = Instantiate(UIHealthEnnemiPrefab);
        // Ennemies[1] = Instantiate(UIHealthEnnemiPrefab);
        //   HealthBar = Instantiate(UIHealthEnnemiPrefab, transform);
        //  Vector2 mousePositiona = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //  Vector3 positiona = transform.position;
        // position.x = mousePositiona.x; 
        //  position.y = mousePositiona.y; 

        // oof = Instantiate(UIHealthEnnemiPrefab, positiona, Quaternion.identity);
        //Ennemies = GameObject.FindGameObjectsWithTag("Ennemies");
        //for( int i =0; i< Ennemies.Length;i++)
        //{
        //    EnnemiesHWidgets[i] = Instantiate(UIHealthEnnemiPrefab, Ennemies[i].transform.position, Quaternion.identity,transform);
        // }
       
        for (int i = 0; i < healthBarLimit; i++)
        {
            EnnemiesHWidgets[i] = Instantiate(UIHealthEnnemiPrefab, Ennemies[i].transform.position, Quaternion.identity, transform.parent);

        }
    }
    void AddHealthToEnnemies()
    {
        Ennemies = GameObject.FindGameObjectsWithTag("Ennemies");

        // utilisez la technique des child
        float[] myfloatarray = new float[Ennemies.Length];
        GameObject[] EnnemiesBIS = new GameObject[Ennemies.Length];
        GameObject[] EnnemiesSorted = new GameObject[Ennemies.Length];
        for (int i = 0; i < myfloatarray.Length; i++)
        {
            myfloatarray[i] = Vector2.Distance(Ennemies[i].transform.position, InstanceRef.transform.position);
            EnnemiesBIS[i] = Ennemies[i];
        }
         Array.Sort(myfloatarray);
   
        for (int i = 0; i < myfloatarray.Length; i++)
        {
            for (int j = 0; j < EnnemiesBIS.Length; j++)
            {
                float temp = Vector2.Distance(EnnemiesBIS[j].transform.position, InstanceRef.transform.position);
                if(myfloatarray[i] == temp)
                {
                    EnnemiesSorted.SetValue(EnnemiesBIS[j], i);
                }

            }
        }
        //print(sssss);


            for (int i = 0; i < EnnemiesSorted.Length; i++)
        {
            Vector2 gfgfg = Camera.main.WorldToScreenPoint(EnnemiesSorted[i].transform.position);
     
            EnnemiesHWidgets[i].SetActive(true);
            EnnemiesHWidgets[i].transform.position = gfgfg + new Vector2(0, 30);

            Vector3 Scale = EnnemiesHWidgets[i].transform.GetChild(1).GetComponent<RectTransform>().localScale;
            
            Scale.x = (float)EnnemiesSorted[i].GetComponent<EnemyManager>().health / (float)EnnemiesSorted[i].GetComponent<EnemyManager>().maxHealth;
            if (Scale.x < 0) Scale.x = 0;
            if (Scale.x > 1) Scale.x = 1;
            //EnnemiesHWidgets[i].GetComponent<HealthBarManager>().isDamaged = EnnemiesSorted[i].GetComponent<EnemyManager>().isDamaged;
            EnnemiesHWidgets[i].transform.GetChild(2).GetComponent<RectTransform>().localScale = Scale;
        }
        // On désac les healthbar non utilisé
        for (int i = Ennemies.Length; i < EnnemiesHWidgets.Length; i++) 
        {
            EnnemiesHWidgets[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        AddHealthToEnnemies();
        if (Ennemi != null)
        {

        }
  
        if (Ennemi != null)
        {
   
        }
     
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
           // UIHealthBar.GetComponent<>; // TODO : faut crée la variable HUDIcon 
            
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
                //Debug.Log("UI UPDATE : WeaponDistanceIcon n'est pas défini");
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
                //Debug.Log("UI UPDATE : WeaponDistanceIcon n'est pas défini");
            }
        }
    }
}
