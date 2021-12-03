using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hudInit : MonoBehaviour
{
    public Button StartButton;
    public Button QuitButton;
    public Button CreditsButton;
    public GameObject CreditsWidget; 
    // Credits Buttons
    public Button CreditsBackButton;

    void Awake()
    {
        ///GameObject Menu = GameObject.Find("MainMenu");
        //Button theButton = transform.GetChild(0).GetComponent<Button>();
        StartButton.onClick.AddListener(delegate () { GameManager.GameUtil.StartLevel(); });
        QuitButton.onClick.AddListener(delegate () { Application.Quit(); });
        CreditsButton.onClick.AddListener(delegate () { Application.Quit(); });
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CreditsWidget.SetActive(true);
    }

    void StartCreditsWidget()
    {

    }
    public void StartLevel()
    {
        // Start First Scene
        GameManager.GameUtil.isLoading = true;
        //SceneManager.LoadSceneAsync("TstLvlManager", LoadSceneMode.Single);
        StartCoroutine(LoadYourAsyncScene("TstLvlManager"));
       
    }

    IEnumerator LoadYourAsyncScene(string DesiredScene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(DesiredScene, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
            yield return null;

        GameManager.GameUtil.CurrentScene = "TestLevel";
        GameManager.GameUtil.CurrentPlayer = GameObject.FindWithTag("Player");
        GameManager.GameUtil.CurrentCamera = Camera.main.gameObject;
        GameManager.GameUtil.isLoading = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
