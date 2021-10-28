using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hudInit : MonoBehaviour
{
    void Awake()
    {
        ///GameObject Menu = GameObject.Find("MainMenu");
        Button theButton = transform.GetChild(0).GetComponent<Button>();
        theButton.onClick.AddListener(delegate () { GameManager.GameUtil.StartLevel(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartLevel()
    {
        // Start First Scene
        SceneManager.LoadSceneAsync("TstLvlManager", LoadSceneMode.Single);

        GameManager.GameUtil.CurrentScene = "TestLevel";
        GameManager.GameUtil.CurrentPlayer = GameObject.FindWithTag("Player");
        GameManager.GameUtil.CurrentCamera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
