using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortailPourMenu : MonoBehaviour
{
    bool pass;
    bool crea;
    bool check;
    public int indexBuild;
    public string affichage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pass)
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                GameManager.GameUtil.ChangeLevel(indexBuild);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (!crea)
            {
                HUDManager.HUDUtility.CreateHintString(gameObject, affichage, 1f);
                crea = true;
            }
            pass = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pass = false;
    }
}
