using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TotalScript : MonoBehaviour
{
    public List<GameObject> salle;
    public int maxSalle;

    public List<GameObject> couloir;

    public List<GameObject> list;

    public List<GameObject> wall;

    public List<GameObject> couloirStock;

    public GameObject listSalle;
    public GameObject listCouloir;

    public List<GameObject> porte;
    public List<GameObject> portRoole;

    public GameObject portePrefab;

    bool check;
    bool check1;
    bool check2;
    bool check3;
    bool porteCheck;

    float timeToReach;
    float time;
    public bool timerfinish;

    public bool finishall;

    bool t;
    // Start is called before the first frame update
    void Start()
    {
        porteCheck = true;
        time = 0;
        //timeToReach = 2f;
        timeToReach = maxSalle * 0.5f + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerfinish && !check)
        {
            timerfinish = false;
            restart();
           
        }
        if (!timerfinish)
        {
            Timer();
        }
        print(check + "yo"); 
        if (!check)
        {
            Invoke("MaxSalle", timeToReach);
        }
        if (check)
        {
            
            
            Invoke("invokelemenage", 1f);
            
        }
        if (check1 && check2 && !check3)
        {
            CreationDesCollider();
            if (!porteCheck)
            {
                CreationDePorte();
            }

        }
    }

    void MaxSalle ()
    {
        if (salle.Count >= maxSalle)
        {
            while (salle.Count == maxSalle)
            {
                Destroy(salle[salle.Count - 1]);
                salle.Remove(salle[salle.Count - 1]);
                print("fesfesfesfesfsfese");
            }
            check = true;
        }
    }

    void invokelemenage()
    {
        if (!check1)
        {
            Menage();
        }
        if (!check2)
        {
            Menage2();
        }
    }

    void Menage ()
    {
        for(int i = 0; i<salle.Count; i++)
        {
            if (salle[i] != null)
            {
                Destroy(salle[i].transform.GetChild(2).gameObject);
                for (int f = 0; f < salle[i].transform.GetChild(1).childCount; f++)
                {
                    //Destroy(salle[i].transform.GetChild(1).GetChild(f).GetChild(0).gameObject);
                    Destroy(salle[i].transform.GetChild(1).GetChild(f).GetComponent<NumCouloir>());
                }
            }
            
        }
        check1 = true;
        porteCheck = false;
    }

    void Menage2()
    {
        for (int i = 0; i < couloir.Count; i++)
        {
            Destroy(couloir[i].transform.GetChild(3).gameObject);
           
              //  Destroy(couloir[i].transform.GetChild(0).GetChild(0).gameObject);
              //  Destroy(couloir[i].transform.GetChild(1).GetChild(0).gameObject);
            Destroy(couloir[i].transform.GetChild(0).GetComponent<NumSalle>());
            Destroy(couloir[i].transform.GetChild(1).GetComponent<NumSalle>());

        }
        for(int i = 0; i < list.Count; i ++)
        {
            Destroy(list[i]);
           
        }
        list = new List<GameObject>();
        check2 = true;
    }

    void CreationDesCollider ()
    {
        for (int i = 0; i < salle.Count; i++)
        {
            salle[i].transform.GetChild(0).GetChild(0).GetChild(0).gameObject.AddComponent<TilemapCollider2D>();
                
        }
        for (int i = 0; i < couloir.Count; i++)
        {
            couloir[i].transform.GetChild(2).GetChild(0).GetChild(0).gameObject.AddComponent<TilemapCollider2D>();
        }
        for (int i = 0; i < wall.Count; i ++)
        {
            for (int f = 0; f < wall[i].transform.GetChild(0).childCount; f++)
            {
                wall[i].transform.GetChild(0).GetChild(f).gameObject.AddComponent<BoxCollider2D>();
            }
        }
        check3 = true;
        finishall = true;
        }

    void CreationDePorte()
    {
        for(int i = 0; i < porte.Count; i ++)
        {
            if(porte[i]!= null)
            {
                portRoole.Add(porte[i]);
            }
        }
        porte = new List<GameObject>();
        for (int i = 0; i < portRoole.Count; i++)
        {
            if (portRoole[i].transform.childCount == 0 || portRoole[i].transform.GetChild(0).gameObject.layer != LayerMask.NameToLayer("Wall"))
            {
                Instantiate(portePrefab, portRoole[i].transform.position, Quaternion.identity, portRoole[i].transform.parent.parent.GetChild(0).GetChild(0).GetChild(4));
            }
        }
        porteCheck = true;

    }

    void Timer ()
    {
        if(time < timeToReach)
        {
            time += Time.deltaTime;
        }
        else
        {
            print("TimerFinish");
            timerfinish = true;
            time = 0;
        }
    }

    void restart ()
    {
        if(salle.Count < maxSalle)
        {
            for(int i = 0; i < salle[0].transform.GetChild(1).childCount; i++)
            {
                salle[0].transform.GetChild(1).GetChild(i).GetComponent<NumCouloir>().listingCouloir = listCouloir;
                for (int f = 0; f < salle[0].transform.GetChild(1).GetChild(i).childCount; f++)
                {
                    Destroy(salle[0].transform.GetChild(1).GetChild(i).GetChild(f).gameObject);
                }
                salle[0].transform.GetChild(1).GetChild(i).GetComponent<NumCouloir>().reinstance = true;
            }
           /* couloir[0].transform.GetChild(0).GetComponent<NumSalle>().listingSalle = listSalle;
            couloir[0].transform.GetChild(1).GetComponent<NumSalle>().listingSalle = listSalle;
            print("yo");
            for (int i = 0; i< couloir[0].transform.GetChild(0).childCount; i++)
            {
                Destroy(couloir[0].transform.GetChild(0).GetChild(i).gameObject);
            }
            for (int i = 0; i < couloir[0].transform.GetChild(1).childCount; i++)
            {
               
                Destroy(couloir[0].transform.GetChild(1).GetChild(i).gameObject);
            }*/
            list = new List<GameObject>();
            /*couloir[0].transform.GetChild(0).GetComponent<NumSalle>().reinstance = true;
            couloir[0].transform.GetChild(1).GetComponent<NumSalle>().reinstance = true;*/



            for (int i = 0; i<couloir.Count; i ++)
            {

                couloir.RemoveAt(i);
            }
            couloir = new List<GameObject>();
            for(int i = 1; i<salle.Count; i++)
            {
                salle.RemoveAt(i);
            }
           couloirStock.Add(salle[0]);
            salle = new List<GameObject>();
            salle.Add(couloirStock[0]);
            couloirStock = new List<GameObject>();
            wall = new List<GameObject>();
            couloirStock.Add(porte[0]);
            couloirStock.Add(porte[1]);
            porte = new List<GameObject>();
            porte.Add(couloirStock[0]);
            porte.Add(couloirStock[1]);
            couloirStock = new List<GameObject>();



        }
    }
}
