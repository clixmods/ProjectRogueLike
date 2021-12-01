using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCouloir : MonoBehaviour
{
    public bool position;

    TotalScript salles;

    bool check;
    // Start is called before the first frame update
    void Start()
    {
        salles = GameObject.Find("TotalSalle(Clone)").GetComponent<TotalScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!check)
        {
            check = true;
            Invoke("AjoutList", 0.1f);
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (position)
        {

            Destroy(gameObject.transform.parent.gameObject);
        }
        else if (!position)
        {
            gameObject.transform.position = gameObject.transform.parent.transform.position;
            gameObject.transform.rotation = gameObject.transform.parent.transform.rotation;
        }
    }

    void AjoutList()
    {
       
        
        salles.couloir.Add(gameObject.transform.parent.gameObject);

    }
}
