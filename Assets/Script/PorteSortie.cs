using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PorteSortie : MonoBehaviour
{
    public int sceneLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
          
            GameManager.GameUtil.ChangeLevel(sceneLoad);
            //SceneManager.LoadScene("TstLvlManager");
        }
    }
}
