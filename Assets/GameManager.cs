using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    public string CurrentScene;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(HUD);
        if (CurrentCamera == null && Camera.main != null)
            CurrentCamera = Camera.main.gameObject; 
        //DontDestroyOnLoad(CurrentCamera);
    }

    public void StartLevel()
    {
        // Start First Scene
        SceneManager.LoadSceneAsync("TestLevel", LoadSceneMode.Single);
        
        CurrentScene = "TestLevel";
        CurrentPlayer = GameObject.FindWithTag("Player");
        CurrentCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
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
             CurrentCamera.transform.position = new Vector3(CurrentPlayer.transform.position.x , CurrentPlayer.transform.position.y,-10);

    }

}
