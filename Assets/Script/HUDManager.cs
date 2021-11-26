using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    public static HUDManager HUDUtility; // permet de get le hud dans nimporte quelle script
    public GameObject InstanceRef;
    public PlayerControler InstanceRefController;
    public bool Debug = false;

    [Header("Distance icon WIDGETS")]
    public GameObject DistanceMagicIcon;
    public GameObject DistancePhyIcon;
    [Header("Melee icon WIDGETS")]
    public GameObject MeleeMagicIcon;
    public GameObject MeleePhyIcon;
    [Header("HUD WIDGETS")]
    public GameObject UIGameOverWidget;
    public Transform UIHealthBar; // contient tout les élément pour la vie du joueur
    public Transform UIHealthBoss;
    //public GameObject UIHealthBarDamaged;
    //public Text UIHealthText;
    public GameObject UILifes;
    public GameObject UILifePrefab;
    //public GameObject UIShieldBar;
    //public GameObject UIPlayerScore;
   // public GameObject UIPlayerScoreMultiplier;
    //public GameObject UIPlayerAmmoCount;
    public GameObject UIPlayerWeaponMelee;
    public GameObject UIPlayerWeaponDistance;
    public GameObject UIHealthEnnemiPrefab;


    public Text UIMiddleScreenMsg;
    private bool isMiddleScreenMsg;

    public Text UIDebugScreenMsg;
    private bool isdebugScreenMsg;
    private float counterdSM = 0;
    private float durationdSM = 2;

    private float counterMSM = 0;
    private float durationMSM = 2;
    private float timeToHide = 2;

    [Header("HUD MODELSVALUE")]
    public int PlayerAmmoCount;
    public bool isHeating = false;

    public int PlayerHealth;
    public int PlayerMaxHealth;
    public int PlayerLife;
    public int PlayerMaxLifes;

    public int BossHealth;
    public int BossMaxHealth;
    public int BossLife;
    public int BossMaxLifes;

    //private GameObject GameManager;
    GameObject[] Ennemies; // Permet de get les ennemies afin de spawn leur bar de vie
 
    // isdamagedanim var
    private bool isDamaged = false;
    private bool isLastStand = false;
    private float counter = 0;
    private float timeToDoAnim = 5;


    // Start is called before the first frame update
    void Start()
    {
        HUDUtility = this;
        //GameManager = GameObject.FindWithTag("GameController");
        InstanceRef = GameManager.GameUtil.GetComponent<GameManager>().CurrentPlayer;
        if(InstanceRef != null)
            InstanceRefController = InstanceRef.GetComponent<PlayerControler>();

    }
    
    // Update is called once per frame
    void Update()
    {
        // Hum apparement HUDUtility n'est plus déf des qu'on update les scripts, du coup je met ça pour préshot
        if (HUDUtility != this) 
            HUDUtility = this;

        AddHealthToEnnemies();
   
        if (InstanceRef != null)
        {
            UpdateUIElements();
            if(isDamaged)
            {

            }
        }
        else
        {
            if (GameManager.GameUtil.CurrentPlayer != null)
            {
                InstanceRef = GameManager.GameUtil.CurrentPlayer;
                InstanceRefController = InstanceRef.GetComponent<PlayerControler>();
            }
        }
        if(isMiddleScreenMsg)
        {
            if(durationMSM > 0)
            {
                if ( counterMSM < durationMSM)
                    counterMSM += Time.deltaTime;
                else
                {
                    counterMSM = 0;
                    durationMSM = 0;
                }
            }
            else
            {
                Color Color = UIMiddleScreenMsg.color;
                Color.a -= Time.deltaTime;
                if(Color.a <= 0)
                {
                    Color.a = 0;
                    isMiddleScreenMsg = false;
                }
                UIMiddleScreenMsg.color = Color;
            }
        }
        if (isdebugScreenMsg)
        {
            if (durationMSM > 0)
            {
                if (counterdSM < durationdSM)
                    counterdSM += Time.deltaTime;
                else
                {
                    counterdSM = 0;
                    durationdSM = 0;
                }
            }
            else
            {
                Color Color = UIDebugScreenMsg.color;
                Color.a -= Time.deltaTime;
                if (Color.a <= 0)
                {
                    Color.a = 0;
                    isdebugScreenMsg = false;
                }
                UIDebugScreenMsg.color = Color;
            }
        }
        //isActiveAndEnabled();

    }

    void UpdateUIElements()
    {
        GetAndSetHealthValue();

        GetAndSetBossHealthValue();

        GetAndSetLifesValue();
        GetAndSetWeaponDistanceIcon();
        GetAndSetWeaponCACIcon();




    }
    public void SetMiddleMsg(float duration = 2 ,string message = "")
    {

        UIMiddleScreenMsg.text = message;

        Color Color = UIMiddleScreenMsg.color;
        Color.a = 1;
        UIMiddleScreenMsg.color = Color;

        counterMSM = 0;
        durationMSM = duration;
        isMiddleScreenMsg = true;

    }
    public void SetDebugMsg(float duration = 2, string message = "")
    {

        UIDebugScreenMsg.text = message;

        Color Color = UIDebugScreenMsg.color;
        Color.a = 1;
        UIDebugScreenMsg.color = Color;

        counterdSM = 0;
        durationdSM = duration;
        isdebugScreenMsg = true;

    }



    void GetAndSetLifesValue()
    {
        // On check si le nombre de maxlife sur lHUD est correspondant � ce qu'a le joueur
        if (PlayerMaxLifes < 1)
            return;

        if (UILifes.transform.childCount != PlayerMaxLifes)
        {
            for (int i = 0; i < PlayerMaxLifes; i++)
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
                if (i > PlayerMaxLifes)
                    Destroy(UILifes.transform.GetChild(i).gameObject);
            }

        }
        // On check si les lifes sur l'hud correspondent � ceux du joueur
        for (int i = 0; i < PlayerLife; i++)
        {
            if (!UILifes.transform.GetChild(i).GetChild(0).gameObject.activeSelf)
                UILifes.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }
        for (int i = PlayerLife; i < PlayerMaxLifes; i++)
        {
            UILifes.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
    }
    void GetAndSetHealthValue()
    {
        Vector3 localScale = UIHealthBar.GetChild(2).GetComponent<RectTransform>().localScale; // TODO : faut cr�e la variable HUDIcon 
        if (PlayerHealth > 0)
            localScale.x = (float)PlayerHealth / (float)PlayerMaxHealth;
        else
        {
            PlayerHealth = 0;
            localScale.x = 0;
        }
            

        localScale.x = (float)PlayerHealth / (float)PlayerMaxHealth;
        if (localScale.x > 1)
            localScale.x = 1;
        else if (localScale.x < 0)
            localScale.x = 0;

        UIHealthBar.GetChild(3).GetComponent<Text>().text = PlayerHealth.ToString();
        UIHealthBar.GetChild(2).GetComponent<RectTransform>().localScale = localScale;
    }
    void GetAndSetBossHealthValue()
    {
        if ( BossMaxHealth == 0)
            return;

        Vector3 localScale = UIHealthBoss.GetChild(2).GetComponent<RectTransform>().localScale; // TODO : faut cr�e la variable HUDIcon 
        localScale.x = (float)BossHealth / (float)BossMaxHealth;
        if (localScale.x > 1)
            localScale.x = 1;
        else if (localScale.x < 0)
            localScale.x = 0;

        if (BossHealth < 0)
            BossHealth = 0;

        UIHealthBoss.GetChild(3).GetComponent<Text>().text = BossHealth.ToString();
        UIHealthBoss.GetChild(2).GetComponent<RectTransform>().localScale = localScale;
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
       InstanceRefController.CurrentWeapon != null)
        {
            if (InstanceRefController.CurrentWeapon.TryGetComponent<ManagerWeaponCorpAcopr>(out ManagerWeaponCorpAcopr WeaponManager))
            {
                //if (UIPlayerWeaponMelee.GetComponent<Image>().sprite == InstanceRefController.CurrentWeapon.GetComponent<ManagerWeaponCorpAcopr>().HUDIcon)
                //   return;
                if (WeaponManager.IsMagical)
                {
                    MeleeMagicIcon.SetActive(true);
                    MeleePhyIcon.SetActive(false);
                }
                else
                {
                    MeleeMagicIcon.SetActive(false);
                    MeleePhyIcon.SetActive(true);
                }
                ReduceOpacity(UIPlayerWeaponDistance.GetComponent<Image>());
                Opacity(UIPlayerWeaponMelee.GetComponent<Image>());
                UIPlayerWeaponMelee.GetComponent<Image>().sprite = WeaponManager.HUDIcon;
            }
            else
            {
                //Debug.Log("UI UPDATE : WeaponDistanceIcon n'est pas d�fini");
            }
        }
    }
    void GetAndSetWeaponDistanceIcon()
    {
        if (InstanceRef != null &&
                InstanceRefController.CurrentWeapon != null )
        {
            if( InstanceRefController.CurrentWeapon.TryGetComponent<WeaponManager>(out WeaponManager WeaponManager))
            {
                if (WeaponManager.IsMagical)
                {
                    DistanceMagicIcon.SetActive(true);
                    DistancePhyIcon.SetActive(false);
                }
                else
                {
                    DistanceMagicIcon.SetActive(false);
                    DistancePhyIcon.SetActive(true);
                }

                    ReduceOpacity(UIPlayerWeaponMelee.GetComponent<Image>());
                Opacity(UIPlayerWeaponDistance.GetComponent<Image>());
                UIPlayerWeaponDistance.GetComponentInChildren<Text>().text = PlayerAmmoCount.ToString();
                UIPlayerWeaponDistance.GetComponent<Image>().sprite = WeaponManager.HUDIcon;

            }
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
                continue;
            }


            if (Ennemies[i].GetComponent<EnemyManager>().HealthBar == null)
            {
                Ennemies[i].GetComponent<EnemyManager>().HealthBar = Instantiate(UIHealthEnnemiPrefab, Ennemies[i].transform.position, Quaternion.identity, transform);
                Ennemies[i].GetComponent<EnemyManager>().HealthBar.SetActive(false);
            }
            else
            {
                GameObject HealthBar = Ennemies[i].GetComponent<EnemyManager>().HealthBar;
                if(GameManager.GameUtil.cursorTarget != null && GameManager.GameUtil.cursorTarget.gameObject == Ennemies[i])
                {
                    HealthBar.SetActive(true);
                }
                else
                {
                    HealthBar.SetActive(false);
                }

                Vector3 gfgfg;
                if (GameManager.GameUtil.CurrentCamera.transform.GetChild(1)! != null)
                {
                    Vector3 OHff = new Vector3(0, 0, 0);
                    HealthBar.transform.position = Camera.main.WorldToScreenPoint(Ennemies[i].transform.position + OHff + new Vector3(0, 0.6f, 0));
                }

                //= gfgfg;
                Vector3 Scale = HealthBar.transform.GetChild(1).GetComponent<RectTransform>().localScale;

                Scale.x = (float)Ennemies[i].GetComponent<EnemyManager>().health / (float)Ennemies[i].GetComponent<EnemyManager>().maxHealth;
                if (Scale.x < 0) Scale.x = 0;
                if (Scale.x > 1) Scale.x = 1;
                HealthBar.transform.GetChild(2).GetComponent<RectTransform>().localScale = Scale;
            }
        }
     
    }
}
