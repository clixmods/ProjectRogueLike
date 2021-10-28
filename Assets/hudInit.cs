using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
