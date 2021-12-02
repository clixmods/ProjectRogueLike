using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum TutorialPhase : int
{
    Mouvement,
    Attaque,
    ChangementWeapon,
    SwitchMetD,
    TypeWeapon
}


public class WeaponDataMelee
{
    public string prefabName;
}

public class PlayerData
{
    public int health;
    public int maxHealht;
    public int life;
    public int maxLife;
}
public class TutorialData
{
    public bool[] bools;
}

public class GameManager : MonoBehaviour
{
    public static GameManager GameUtil;

    public int PlayerHealth;
    public int PlayerLife;
    public int PlayerShield;
    public int PlayerScore;
    public int PlayerScoreMultiplier;
    public float PlayerAmmoCount;

    [Header("PROPERTY UPDATED BY THE GAME")]
    public GameObject PlayerPrefab;
    public GameObject LevelManager;
    public GameObject HUD;
    public GameObject CurrentCamera;
    public GameObject CurrentPlayer;
    public GameObject PrefabCamera;
    public string CurrentScene;
    public GameObject MainMenu;
    [Tooltip("Some features are not affected by the timescale, so we need to use isPaused to block some functions")]
    public bool isPaused = false; 
    // Cooldown before returntomainmenu
    public bool isGameover = false;
    float cooldownToBackMainMenu = 5;
    float currentCooldown = 0;

    [Tooltip("All prefabs used by other script")]
    [Header("GLOBAL PREFAB")]

    public GameObject DefaultWeapon;
    public Material DamageMtl;
    [Header("CURSORS")]
    public Texture2D[] Cursors;
    public CircleCollider2D MouseCollider;
    Vector2 hotSpot;
    public Collider2D cursorTarget;
    [Header("TUTORIAL")]
    public GameObject TutorialMenu;
    public bool[] tutorielCheck;
    [SerializeField] Sprite[] tutorielImage;
    [SerializeField] string[] tutorielText;

    [Header("WEAPONS LIST")]
    public WeaponsList WeaponsScriptTable;
    public string tamer;

    [Header("SAVED DATA")]
    public TutorialData Data;
    public GameObject[] WeaponsDistDataGameObject;
    public int[]            WeaponsDistAmmoDataGameObject;
    public GameObject[]     WeaponsCaCDataGameObject;
    public int DataHealth;
    public int DataLifes;
    public int DataMaxLifes;

    // Start is called before the first frame update
    void Awake()
    {
        tutorielCheck = new bool[Enum.GetValues(typeof(TutorialPhase)).Length];
        Vector2 hotSpot;
        Vector2  hotSpotAuto = new Vector2(1 , 1);
        hotSpot = hotSpotAuto;
        Cursor.SetCursor(Cursors[0], hotSpot, CursorMode.Auto);
        // on force la position zero pour pas niquer le CircleCollider du cursor
        transform.position = Vector2.zero;
        if(GameManager.GameUtil == null)
        {
            GameUtil = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
        //DontDestroyOnLoad(HUD);
        if (CurrentCamera == null && Camera.main != null)
            CurrentCamera = Camera.main.gameObject; 
        //DontDestroyOnLoad(CurrentCamera);
    }
    void Start()
    {
        MouseCollider = transform.GetComponent<CircleCollider2D>();
    }
    // D�marre le level, faudrait ajouter le nom en parametre
    public void StartLevel()
    {
        SceneManager.LoadSceneAsync("TstLvlManager", LoadSceneMode.Single); 
        CurrentScene = "TestLevel";
        CurrentPlayer = GameObject.FindWithTag("Player");
        CurrentCamera = Camera.main.gameObject;
    }
    public void ChangeLevel(string name, bool dataPlayer = true, bool dataWeaponList = true)
    {
        Scene DesiredScene = SceneManager.GetSceneByName(name);
        TutorialData myObject = new TutorialData();
        myObject.bools = tutorielCheck;
        CurrentPlayer = GameObject.FindWithTag("Player");
        PlayerControler plrControler = CurrentPlayer.GetComponent<PlayerControler>();
        // On get les armes distances du joueur
        GameObject listD = plrControler.listD;
        WeaponsDistDataGameObject = new GameObject[listD.transform.childCount];
        WeaponsDistAmmoDataGameObject = new int[listD.transform.childCount];

        // On stock la vie du joueur
        DataHealth = plrControler.health;
        DataLifes = plrControler.PlayerLifes;
        DataMaxLifes = plrControler.PlayerMaxLifes;


        for (int i = 0; i< listD.transform.childCount; i++)
        {
            WeaponManager wpnManager = listD.transform.GetChild(i).GetComponent<WeaponManager>();
            
            for(int j = 0; j < WeaponsScriptTable.weaponGameobject.Length; j++)
            {
                if(WeaponsScriptTable.weaponGameobject[j].TryGetComponent(out WeaponManager wpnManger))
                {
                    Debug.Log(wpnManger.WeaponName);
                    Debug.Log(wpnManager.WeaponName);
                    if (wpnManger.WeaponName == wpnManager.WeaponName)
                    {
                        WeaponsDistDataGameObject[i] = WeaponsScriptTable.weaponGameobject[j];
                        WeaponsDistAmmoDataGameObject[i] = wpnManager.CurrentAmmoCount;

                    }
                }
            }
        }
        GameObject listC = CurrentPlayer.GetComponent<PlayerControler>().listC;
        WeaponsCaCDataGameObject = new GameObject[listC.transform.childCount];
        for (int i = 0; i < listC.transform.childCount; i++)
        {
            ManagerWeaponCorpAcopr wpnManager = listC.transform.GetChild(i).GetComponent<ManagerWeaponCorpAcopr>();
            for (int j = 0; j < WeaponsScriptTable.weaponGameobject.Length; j++)
            {
                if (WeaponsScriptTable.weaponGameobject[j].TryGetComponent(out ManagerWeaponCorpAcopr wpnManger))
                    if (wpnManger.WeaponName == wpnManager.WeaponName)
                        WeaponsCaCDataGameObject[i] = WeaponsScriptTable.weaponGameobject[j];
            }
        }
        
        StartCoroutine(LoadYourAsyncScene(DesiredScene ));

      
        
    }
        IEnumerator LoadYourAsyncScene(Scene DesiredScene)
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
            // a sceneBuildIndex of 1 as shown in Build Settings.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(DesiredScene.name, LoadSceneMode.Single);
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
                yield return null;
 
            CurrentScene = name;
            CurrentPlayer = GameObject.FindWithTag("Player");
            CurrentCamera = Camera.main.gameObject;
            PlayerControler  plrController = CurrentPlayer.GetComponent<PlayerControler>();
            GameObject listD = plrController.listD;
            GameObject listC = plrController.listC;
            GiveWeaponsToPlayer(WeaponsDistDataGameObject, listD);
            GiveWeaponsToPlayer(WeaponsCaCDataGameObject, listC);

            // On stock la vie du joueur
            plrController.health = DataHealth;
            plrController.PlayerLifes = DataLifes;
            plrController.PlayerMaxLifes = DataMaxLifes;
    }

    void GiveWeaponsToPlayer(GameObject[] weaponsList , GameObject List)
    {
        for(int i = 0; i < weaponsList.Length; i++)
        {
            GameObject Wpn = Instantiate(weaponsList[i], CurrentPlayer.transform.position, Quaternion.identity, List.transform);
            Wpn.transform.localPosition = Vector3.zero;
            Wpn.transform.localRotation = new Quaternion(0, 0, 0, 0);
            if (Wpn.TryGetComponent(out WeaponManager component))
            {
                component.CurrentAmmoCount = WeaponsDistAmmoDataGameObject[i];
                Debug.Log(component.CurrentAmmoCount);
            }
        }
               
    }


    public void BackToMainMenu()
    {
        ClosePauseMenu();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        CurrentScene = "MainMenu";
        GameObject Menu = GameObject.Find("MainMenu");
        Button theButton = Menu.transform.GetChild(0).GetComponent<Button>();
        theButton.onClick.AddListener(delegate () { this.StartLevel(); });
    }

    void FixColliderToCursor()
    {
        MouseCollider.offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    // Update is called once per frame
    void Update()
    {
        

        if (GameManager.GameUtil == null)
        {
            GameUtil = this;
        }
              
       
        
        PauseMenu();
      
        if(CurrentCamera == null)
        {
            CurrentCamera = Instantiate(PrefabCamera);
        }
        if(CurrentCamera != null)
        {
            if (CurrentCamera.transform.childCount > 2 && CurrentCamera.transform.GetChild(1).TryGetComponent<CinemachineVirtualCamera>(out CinemachineVirtualCamera Component))
            {
                if (CurrentPlayer != null && Component.Follow != CurrentPlayer)
                    Component.Follow = CurrentPlayer.transform;
            }
        }

        FixColliderToCursor();

        if (isGameover)
        {
            if (Time.timeScale > 0.2f)
            {
                currentCooldown += Time.deltaTime;
                Time.timeScale -= Time.deltaTime / cooldownToBackMainMenu;
                print(Time.timeScale);
            }
            else
            {
                currentCooldown = 0;
                isGameover = false;
                BackToMainMenu();
            }
        }

        if (CurrentScene == null || CurrentScene == "MainMenu")
            return;

        if (CurrentPlayer != null)
        {
            FixCameraToPlayer();
        }
        else
        {
            TryToGetPlayerEntity();
        }
        //if (HUD == null)
        //{
        //    HUD = GameObject.Find("HUD");
        //}
    }

    public void ActiveTutorial(int tutID)
    {
        if(tutorielCheck[tutID])
        {
            Debug.Log("The tutoriel id : " + tutID + " was already checked");
            return;
        }
        TutorialMenu.SetActive(true);
        TutorialMenu.GetComponent<TutorialProperty>().Text.text = tutorielText[tutID];
        TutorialMenu.GetComponent<TutorialProperty>().Texture.GetComponent<Image>().sprite = tutorielImage[tutID];
        tutorielCheck[tutID] = true;

    }

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainMenu.activeSelf)
                ContinueTheGame();
            else
                MainMenu.SetActive(true);

        }

        if (MainMenu.activeSelf || TutorialMenu.activeSelf)
        {
            isPaused = true;
            Time.timeScale = 0f;
        }
       
            
    }
    public void ContinueTheGame()
    {
        ClosePauseMenu();
    }

    void ClosePauseMenu()
    {
        MainMenu.SetActive(false);
        TutorialMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    void TryToGetPlayerEntity()
    {
        CurrentPlayer = GameObject.FindWithTag("Player");
        //print(CurrentPlayer.transform.position);
    }
    void FixCameraToPlayer()
    {
        if (CurrentCamera == null)
            CurrentCamera = Camera.main.gameObject;

        else if ( CurrentPlayer != null)
             CurrentCamera.transform.position = new Vector3(CurrentPlayer.transform.position.x , CurrentPlayer.transform.position.y,-80);

    }

    // Cursor Management
    private void OnTriggerStay2D(Collider2D collision)
    {


        if (CurrentPlayer != null)
        {
            PlayerControler playerController = CurrentPlayer.GetComponent<PlayerControler>();
            bool oofa = playerController.CurrentWeapon.TryGetComponent<WeaponManager>(out WeaponManager Dweapon);
            bool oofb = playerController.CurrentWeapon.TryGetComponent<ManagerWeaponCorpAcopr>(out ManagerWeaponCorpAcopr Mweapon);


            if (isPaused)
            {
                Cursor.SetCursor(Cursors[0], hotSpot, CursorMode.Auto);
                return;
            }




            if (collision != null && collision.tag == "Ennemies")
            {
                GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.Attaque);
                cursorTarget = collision;
                if (collision.transform.TryGetComponent<EnemyManager>(out EnemyManager target))
                {

                    if (oofa)
                    {
                        print(collision.name);
                        if (target.isMagical == Dweapon.IsMagical)
                        {
                            Cursor.SetCursor(Cursors[2], hotSpot, CursorMode.Auto);
                        }
                        else
                        {
                            Cursor.SetCursor(Cursors[3], hotSpot, CursorMode.Auto);
                            GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.TypeWeapon);

                        }
                    }
                    else if (oofb)
                    {
                        if (target.isMagical == Mweapon.IsMagical)
                        {
                            Cursor.SetCursor(Cursors[2], hotSpot, CursorMode.Auto);
                        }
                        else
                        {
                            Cursor.SetCursor(Cursors[3], hotSpot, CursorMode.Auto);
                            GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.TypeWeapon);
                        }
                    }
                }
            }
            else
            {

            }
        }
        
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision == cursorTarget)
        {
            cursorTarget = null;
            Cursor.SetCursor(Cursors[0], hotSpot, CursorMode.Auto);       
        }
    }


    
}
