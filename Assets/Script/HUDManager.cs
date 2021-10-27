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
    public GameObject UIHealthBarDamaged;
    public Text UIHealthText;
    public GameObject UILifes;
    public GameObject UILifePrefab;
    public GameObject UIShieldBar;
    public GameObject UIPlayerScore;
    public GameObject UIPlayerScoreMultiplier;
    public GameObject UIPlayerAmmoCount;
    public GameObject UIPlayerWeaponMelee;
    public GameObject UIPlayerWeaponDistance;
    public GameObject UIHealthEnnemiPrefab;
    public GameObject UIHealthBoss;
    [Header("HUD INFO")]
    public string PlayerAmmoCount;
    //public string PlayerHealth;
    //public string PlayerMaxHealth;
    //public string PlayerLife;
    //public string PlayerShield;
    //public string PlayerScore;
    //public string PlayerScoreMultiplier;


    private GameObject GameManager;
    public GameObject[] Ennemies;
    public GameObject[] EnnemiesHWidgets;
    private GameObject oof;
    public GameObject Ennemi;
    
    // isdamagedanim var
    private bool isDamaged = false;
    private float counter = 0;
    private float timeToDoAnim = 5;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameController");
        InstanceRef = GameManager.GetComponent<GameManager>().CurrentPlayer;
        if(InstanceRef != null)
            InstanceRefController = InstanceRef.GetComponent<PlayerControler>();

    }
    
    // Update is called once per frame
    void Update()
    {
        AddHealthToEnnemies();
   
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

            if(isDamaged)
            {

            }
        }
        else
        {
            if (GameManager.GetComponent<GameManager>().CurrentPlayer != null)
            {
                InstanceRef = GameManager.GetComponent<GameManager>().CurrentPlayer;
                InstanceRefController = InstanceRef.GetComponent<PlayerControler>();
            }
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
        GetAndSetHealthValue();
        GetAndSetLifesValue();
        GetAndSetWeaponDistanceIcon();
        GetAndSetWeaponCACIcon();
    }
    void GetAndSetLifesValue()
    {
        // On check si le nombre de maxlife sur lHUD est correspondant � ce qu'a le joueur
        if (InstanceRefController.PlayerMaxLifes < 1)
            return;

        if (UILifes.transform.childCount != InstanceRefController.PlayerMaxLifes)
        {
            for (int i = 0; i < InstanceRefController.PlayerMaxLifes; i++)
            {
                if (UILifes.transform.childCount < i + 1)
                {
                    GameObject Life = Instantiate(UILifePrefab, UILifes.transform.position, Quaternion.identity, UILifes.transform);
                    Vector3 newPosition = new Vector3(-186, 283, 0);
                    newPosition.x += 40 * i;
                    Life.transform.localPosition = newPosition;
                }

            }
            for (int i = 0; i < UILifes.transform.childCount; i++)
            {
                if (i > InstanceRefController.PlayerMaxLifes)
                    Destroy(UILifes.transform.GetChild(i).gameObject);
            }

        }
        // On check si les lifes sur l'hud correspondent � ceux du joueur
        for (int i = 0; i < InstanceRefController.PlayerLifes; i++)
        {
            if (!UILifes.transform.GetChild(i).GetChild(0).gameObject.activeSelf)
                UILifes.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
        for (int i = InstanceRefController.PlayerLifes ; i < InstanceRefController.PlayerMaxLifes; i++)
        {
            UILifes.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
    }
    void GetAndSetHealthValue()
    {
        if (InstanceRefController == null)
        {
            Debug.Log("func GetAndSetHealthValue : InstanceRefController is not defined");
            return;
        }
        Vector3 localScale = UIHealthBar.GetComponent<RectTransform>().localScale; // TODO : faut cr�e la variable HUDIcon 
        localScale.x = (float)InstanceRefController.health / (float)InstanceRefController.MaxHealth;
        if (localScale.x > 1)
            localScale.x = 1;
        else if (localScale.x < 0)
            localScale.x = 0;



        UIHealthText.text = InstanceRefController.health.ToString();
        UIHealthBar.GetComponent<RectTransform>().localScale = localScale;
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
    void GetAndSetWeaponCACIcon()
    {
        if (InstanceRef != null &&
       InstanceRefController.CurrentWeapon != null &&
       InstanceRefController.CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>() != null)
        {
            //if (UIPlayerWeaponMelee.GetComponent<Image>().sprite == InstanceRefController.CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>().HUDIcon)
             //   return;

            ReduceOpacity(UIPlayerWeaponDistance.GetComponent<Image>());
            Opacity(UIPlayerWeaponMelee.GetComponent<Image>());
           // Debug.Log("UI UPDATE : WeaponDistanceIcon");
            UIPlayerWeaponMelee.GetComponent<Image>().sprite = InstanceRefController.CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>().HUDIcon;
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
           // if (UIPlayerWeaponDistance.GetComponent<Image>().sprite == InstanceRefController.CurrentWeapon.GetComponent<WeaponManager>().HUDIcon)
            //    return;
            
           // Debug.Log("UI UPDATE : WeaponDistanceIcon");
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
    void AddHealthToEnnemies()
    {
        Ennemies = GameObject.FindGameObjectsWithTag("Ennemies");
        for (int i = 0; i < Ennemies.Length; i++)
        {
            if (Ennemies[i].GetComponent<Boss>() != null)
            {
                if (Ennemies[i].GetComponent<Boss>().HealthBar == null)
                {
                    Ennemies[i].GetComponent<Boss>().HealthBar = UIHealthBoss;
                    UIHealthBoss.SetActive(true);
                }
                else if (Ennemies[i].GetComponent<Boss>().HealthBar != null)
                {
                    Vector3 Scale = UIHealthBoss.transform.GetChild(1).GetComponent<RectTransform>().localScale;

                    Scale.x = (float)Ennemies[i].GetComponent<Boss>().health / (float)Ennemies[i].GetComponent<Boss>().bossHealth;
                    if (Scale.x < 0) Scale.x = 0;
                    if (Scale.x > 1) Scale.x = 1;
                    UIHealthBoss.transform.GetChild(2).GetComponent<RectTransform>().localScale = Scale;
                }
                continue;
            }
    

            if (Ennemies[i].GetComponent<EnemyManager>().HealthBar == null)
                Ennemies[i].GetComponent<EnemyManager>().HealthBar = Instantiate(UIHealthEnnemiPrefab, Ennemies[i].transform.position, Quaternion.identity, transform);
            else
            {
                GameObject HealthBar = Ennemies[i].GetComponent<EnemyManager>().HealthBar;
                Vector2 gfgfg = Camera.main.WorldToScreenPoint(Ennemies[i].transform.position + new Vector3(0, 0.6f, 0));
                HealthBar.transform.position = gfgfg;
                Vector3 Scale = HealthBar.transform.GetChild(1).GetComponent<RectTransform>().localScale;

                Scale.x = (float)Ennemies[i].GetComponent<EnemyManager>().health / (float)Ennemies[i].GetComponent<EnemyManager>().maxHealth;
                if (Scale.x < 0) Scale.x = 0;
                if (Scale.x > 1) Scale.x = 1;
                HealthBar.transform.GetChild(2).GetComponent<RectTransform>().localScale = Scale;
            }
        }
        /*
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
            //EnnemiesHWidgets[i].transform.GetChild(1).localScale = EnnemiesHWidgets[i].transform.GetChild(2).localScale; // pour eviter de voir la bardamaged quand le widget est attribu� � un autre ennemies
            Vector3 Scale = EnnemiesHWidgets[i].transform.GetChild(1).GetComponent<RectTransform>().localScale;

            Scale.x = (float)EnnemiesSorted[i].GetComponent<EnemyManager>().health / (float)EnnemiesSorted[i].GetComponent<EnemyManager>().maxHealth;
            if (Scale.x < 0) Scale.x = 0;
            if (Scale.x > 1) Scale.x = 1;
            EnnemiesHWidgets[i].transform.GetChild(2).GetComponent<RectTransform>().localScale = Scale;
        }
        // On d�sac les healthbar non utilis�
        for (int i = Ennemies.Length; i < EnnemiesHWidgets.Length; i++) 
        {
            EnnemiesHWidgets[i].SetActive(false);
        }
        */
    }
}
