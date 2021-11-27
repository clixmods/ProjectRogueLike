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

    public List<GameObject> porte;
    public List<GameObject> porteRééle;

    public GameObject portePrefab;

    bool check;
    bool check1;
    bool check2;
    bool check3;
    bool porteCheck;

    float timeToReach;
    float time;
    public bool timerfinish;

    bool t;
    // Start is called before the first frame update
    void Start()
    {
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
            Invoke("MaxSalle", 0.1f);
        }
        if (check)
        {
            if (!porteCheck)
            {
               CreationDePorte();
            }
            
            Invoke("invokelemenage", 10f);
        }
        if (check1 && check2 && !check3)
        {
            CreationDesCollider();
            
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
            Destroy(salle[i].transform.GetChild(2).gameObject);
            for(int f = 0; f<salle[i].transform.GetChild(1).childCount; f++)
            {
                //Destroy(salle[i].transform.GetChild(1).GetChild(f).GetChild(0).gameObject);
                Destroy(salle[i].transform.GetChild(1).GetChild(f).GetComponent<NumCouloir>());
            }
            
        }
        check1 = true;
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
        }

    void CreationDePorte()
    {
        for(int i = 0; i < porte.Count; i ++)
        {
            if(porte[i]!= null)
            {
                porteRééle.Add(porte[i]);
            }
        }
        porte = new List<GameObject>();
        for (int i = 0; i < porteRééle.Count; i++)
        {
            Instantiate(portePrefab, porteRééle[i].transform.position, Quaternion.identity, gameObject.transform);
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
            couloir[0].transform.GetChild(0).GetComponent<NumSalle>().listingSalle = listSalle;
            couloir[0].transform.GetChild(1).GetComponent<NumSalle>().listingSalle = listSalle;
            print("yo");
            for (int i = 0; i< couloir[0].transform.GetChild(0).childCount; i++)
            {
                Destroy(couloir[0].transform.GetChild(0).GetChild(i).gameObject);
            }
            for (int i = 0; i < couloir[0].transform.GetChild(1).childCount; i++)
            {
               
                Destroy(couloir[0].transform.GetChild(1).GetChild(i).gameObject);
            }
            list = new List<GameObject>();
            couloir[0].transform.GetChild(0).GetComponent<NumSalle>().reinstance = true;
            couloir[0].transform.GetChild(1).GetComponent<NumSalle>().reinstance = true;



            for (int i = 0; i<salle.Count; i ++)
            {

                salle.RemoveAt(i);
            }
            salle = new List<GameObject>();
            for(int i = 1; i<couloir.Count; i++)
            {
                couloir.RemoveAt(i);
            }
           couloirStock.Add(couloir[0]);
            couloir = new List<GameObject>();
            couloir.Add(couloirStock[0]);
            couloirStock = new List<GameObject>();
            wall = new List<GameObject>();
            
           
          
        }
    }
}
