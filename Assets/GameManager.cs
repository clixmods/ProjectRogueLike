using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GameUtil;
    public int PlayerHealth;
    public int PlayerLife;
    public int PlayerShield;
    public int PlayerScore;
    public int PlayerScoreMultiplier;
    public float PlayerAmmoCount;
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

    // Common Prefab
    public GameObject DefaultWeapon;

    // Start is called before the first frame update
    void Awake()
    {
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

    public void StartLevel()
    {
        // Start First Scene
        SceneManager.LoadSceneAsync("TstLvlManager", LoadSceneMode.Single);
        
        CurrentScene = "TestLevel";
        CurrentPlayer = GameObject.FindWithTag("Player");
        CurrentCamera = Camera.main.gameObject;
    }
    public void ChangeLevel(string name, bool dataPlayer = true, bool dataWeaponList = true)
    {
        Scene DesiredScene = SceneManager.GetSceneByName(name);
        if (dataPlayer)
        {
            CurrentPlayer = GameObject.FindWithTag("Player");
            //SceneManager.MoveGameObjectToScene(CurrentPlayer, DesiredScene);
        }

        if (dataWeaponList)
        {
            GameObject Data = GameObject.FindWithTag("Data");
          //  SceneManager.MoveGameObjectToScene(Data, DesiredScene);
        }
        // Start First Scene
         SceneManager.LoadSceneAsync(DesiredScene.name, LoadSceneMode.Single);
        //StartCoroutine(loadScene(DesiredScene.buildIndex));

        CurrentScene = name;
        CurrentPlayer = GameObject.FindWithTag("Player");
        CurrentCamera = Camera.main.gameObject;
    }
    /*
    public GameObject UIRootObject;
    private AsyncOperation sceneAsync;
    private int sceneAsyncIndex;
    //void Start()
    // {
    //     StartCoroutine(loadScene(2));
    // }

    IEnumerator loadScene(int index)
    {
        sceneAsyncIndex = index;
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneAsyncIndex, LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        sceneAsync = scene;

        //Wait until we are done loading the scene
        while (scene.progress < 0.9f)
        {
            Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
            yield return null;
        }
        OnFinishedLoadingAllScene();
    }

    void enableScene(int index)
    {
        //Activate the Scene
        sceneAsync.allowSceneActivation = true;


        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(index);
        if (sceneToLoad.IsValid())
        {
            Debug.Log("Scene is Valid");
            SceneManager.MoveGameObjectToScene(CurrentPlayer, sceneToLoad);
            SceneManager.SetActiveScene(sceneToLoad);
        }
    }

    void OnFinishedLoadingAllScene()
    {
        Debug.Log("Done Loading Scene");
        enableScene(sceneAsyncIndex);
        Debug.Log("Scene Activated!");
    }
   
    */
    public void BackToMainMenu()
    {
        ClosePauseMenu();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        CurrentScene = "MainMenu";

        GameObject Menu = GameObject.Find("MainMenu");
        Button theButton = Menu.transform.GetChild(0).GetComponent<Button>();
        ///theButton.onClick;
        //next, any of these will work:
        //  theButton.onClick += "";
        //theButton.onClick.AddListener("");
        //theButton.onClick.AddListener(delegate { GameManager.GameUtil.StartLevel(); });
        //theButton.onClick.AddListener(() => { this.StartLevel(); });
        theButton.onClick.AddListener(delegate () { this.StartLevel(); });
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
            if(CurrentCamera.transform.GetChild(1).TryGetComponent<CinemachineVirtualCamera>(out CinemachineVirtualCamera Component))
            {
                if (CurrentPlayer != null && Component.Follow != CurrentPlayer)
                    Component.Follow = CurrentPlayer.transform;
            }
        }

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

    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainMenu.activeSelf)
                ContinueTheGame();
            else
                MainMenu.SetActive(true);

        }

        if (MainMenu.activeSelf)
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

}
