using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSalle : MonoBehaviour
{
    public bool position;
    bool verif;
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
        if (!verif)
        {
            Invoke("changementEtat", 0.1f);
        }
        else
        {
            GameManager.GameUtil.isLoading = true;
        }
        print(verif);


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

    void changementEtat()
    {
        if (gameObject.transform.parent.GetChild(1).childCount == 1)
        {
            if (gameObject.transform.parent.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().spawn == true)
            {
                position = false;
                // salles.salle.Add(gameObject.transform.parent.gameObject);
                verif = true;
            }
        }
        else if (gameObject.transform.parent.GetChild(1).childCount > 1)
        {
            //print("yoyo" + gameObject.name);
            //salles.salle.Add(gameObject.transform.parent.gameObject);
            verif = true;
        }
    }

    void AjoutList()
    {
        for (int i = 0; i < gameObject.transform.parent.GetChild(1).childCount; i++)
        {
            salles.porte.Add(gameObject.transform.parent.GetChild(1).GetChild(i).gameObject);
        }
        salles.salle.Add(gameObject.transform.parent.gameObject);

    }

}
