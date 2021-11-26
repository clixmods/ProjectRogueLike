using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumSalle : MonoBehaviour
{
    public int numSal;
    ListSalle listsal;
    public GameObject listingSalle;

    GameObject coul;
    int rand;

    public bool spawn;
    bool instance;

    TotalScript salles;

    int maxSalle;

    public bool reinstance;


    // Start is called before the first frame update
    void Start()
    {

        salles = GameObject.Find("TotalSalle").GetComponent<TotalScript>();
        if (!spawn)
        {
            listingSalle = Instantiate(listingSalle, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            salles.list.Add(listingSalle);
        }
        listsal = listingSalle.gameObject.GetComponent<ListSalle>();
        salles.porte.Add(gameObject);
        
        maxSalle = salles.maxSalle;
    }

    // Update is called once per frame
    void Update()
    {
        if ( reinstance)
        {
            listingSalle = Instantiate(listingSalle, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            salles.list.Add(listingSalle);
            listsal = listingSalle.gameObject.GetComponent<ListSalle>();
            reinstance = false;
        }
        Invoke("spawnSalle", 0.5f);
        if (spawn && instance && coul == null)
        {

            spawn = false;
        }
        


    }

    void spawnSalle()
    {

        if (!spawn && !salles.timerfinish && !reinstance)
        {
            if (salles.salle.Count < maxSalle)
            {
                if (numSal == 1)
                {
                    if (listsal.salleBot.Count != 0)
                    {
                        // gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScriptCouloir>().position = false;
                        gameObject.transform.parent.transform.GetChild(3).GetComponent<ScriptCouloir>().position = false;
                        rand = Random.Range(0, listsal.salleBot.Count);
                        coul = Instantiate(listsal.salleBot[rand], transform.position, Quaternion.identity, gameObject.transform);
                        
                        listsal.salleBot.Remove(listsal.salleBot[rand]);
                        print(coul);

                        if (coul.transform.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().coterCouloir == 3)
                        {

                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(0).transform.position);

                            coul.transform.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().spawn = true;
                            //coul.transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                            //gameObject.transform.parent.GetComponent<ScriptCouloir>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(1).GetComponent<NumCouloir>().coterCouloir == 3)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(1).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(1).GetComponent<NumCouloir>().spawn = true;
                            // coul.transform.GetChild(1).transform.GetChild(1).GetComponent<ScriptSalle>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(2).GetComponent<NumCouloir>().coterCouloir == 3)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(2).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(2).GetComponent<NumCouloir>().spawn = true;
                            // coul.transform.GetChild(1).transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(3).GetComponent<NumCouloir>().coterCouloir == 3)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(3).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(3).GetComponent<NumCouloir>().spawn = true;
                            // coul.transform.GetChild(1).transform.GetChild(3).GetComponent<ScriptSalle>().position = true;
                        }
                        coul.transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                    }
                    else if (listsal.salleBot.Count == 0 && !salles.timerfinish)
                    {
                        salles.porte.Remove(gameObject);
                        print("gesgesgesges");
                        salles.couloir.Remove(gameObject.transform.parent.gameObject);
                        Destroy(gameObject.transform.parent.gameObject);
                    }

                }
                if (numSal == 4)
                {
                    if (listsal.salleDroite.Count != 0)
                    {
                        // gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScriptCouloir>().position = false;
                        gameObject.transform.parent.transform.GetChild(3).GetComponent<ScriptCouloir>().position = false;
                        rand = Random.Range(0, listsal.salleDroite.Count);
                        coul = Instantiate(listsal.salleDroite[rand], transform.position, Quaternion.identity, gameObject.transform);
                        listsal.salleDroite.Remove(listsal.salleDroite[rand]);
                        print(coul);
                        if (coul.transform.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().coterCouloir == 2)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(0).transform.position);

                            coul.transform.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().spawn = true;
                            //coul.transform.GetChild(0).transform.GetChild(0).GetComponent<ScriptSalle>().position = true;
                            //gameObject.transform.parent.GetComponent<ScriptCouloir>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(1).GetComponent<NumCouloir>().coterCouloir == 2)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(1).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(1).GetComponent<NumCouloir>().spawn = true;
                            // coul.transform.GetChild(1).transform.GetChild(1).GetComponent<ScriptSalle>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(2).GetComponent<NumCouloir>().coterCouloir == 2)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(2).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(2).GetComponent<NumCouloir>().spawn = true;
                            //coul.transform.GetChild(1).transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(3).GetComponent<NumCouloir>().coterCouloir == 2)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(3).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(3).GetComponent<NumCouloir>().spawn = true;
                            // coul.transform.GetChild(1).transform.GetChild(3).GetComponent<ScriptSalle>().position = true;
                        }
                        coul.transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                    }
                    else if (listsal.salleDroite.Count == 0 && !salles.timerfinish)
                    {
                        salles.porte.Remove(gameObject);
                        print("gesgesgesges");
                        salles.couloir.Remove(gameObject.transform.parent.gameObject);
                        Destroy(gameObject.transform.parent.gameObject);
                    }
                }
                if (numSal == 3)
                {
                    if (listsal.salletop.Count != 0)
                    {
                        // gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScriptCouloir>().position = false;
                        gameObject.transform.parent.transform.GetChild(3).GetComponent<ScriptCouloir>().position = false;
                        rand = Random.Range(0, listsal.salletop.Count);
                        coul = Instantiate(listsal.salletop[rand], transform.position, Quaternion.identity, gameObject.transform);
                        listsal.salletop.Remove(listsal.salletop[rand]);
                        print(coul);
                        if (coul.transform.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().coterCouloir == 1)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(0).transform.position);

                            coul.transform.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().spawn = true;
                            // coul.transform.GetChild(0).transform.GetChild(0).GetComponent<ScriptSalle>().position = true;
                            //gameObject.transform.parent.GetComponent<ScriptCouloir>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(1).GetComponent<NumCouloir>().coterCouloir == 1)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(1).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(1).GetComponent<NumCouloir>().spawn = true;
                            //coul.transform.GetChild(1).transform.GetChild(1).GetComponent<ScriptSalle>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(2).GetComponent<NumCouloir>().coterCouloir == 1)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(2).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(2).GetComponent<NumCouloir>().spawn = true;
                            //coul.transform.GetChild(1).transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(3).GetComponent<NumCouloir>().coterCouloir == 1)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(3).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(3).GetComponent<NumCouloir>().spawn = true;
                            // coul.transform.GetChild(1).transform.GetChild(3).GetComponent<ScriptSalle>().position = true;
                        }
                        coul.transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                    }
                    else if (listsal.salletop.Count == 0 && !salles.timerfinish)
                    {
                        salles.porte.Remove(gameObject);
                        print("gesgesgesges");
                        salles.couloir.Remove(gameObject.transform.parent.gameObject);
                        Destroy(gameObject.transform.parent.gameObject);
                    }
                }
                if (numSal == 2)
                {
                    if (listsal.salleGauche.Count != 0)
                    {
                        // gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ScriptCouloir>().position = false;
                        gameObject.transform.parent.transform.GetChild(3).GetComponent<ScriptCouloir>().position = false;
                        rand = Random.Range(0, listsal.salleGauche.Count);
                        coul = Instantiate(listsal.salleGauche[rand], transform.position, Quaternion.identity, gameObject.transform);
                        listsal.salleGauche.Remove(listsal.salleGauche[rand]);
                        print(coul);
                        if (coul.transform.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().coterCouloir == 4)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(0).transform.position);

                            coul.transform.GetChild(1).transform.GetChild(0).GetComponent<NumCouloir>().spawn = true;
                            // coul.transform.GetChild(0).transform.GetChild(0).GetComponent<ScriptSalle>().position = true;
                            //gameObject.transform.parent.GetComponent<ScriptCouloir>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(1).GetComponent<NumCouloir>().coterCouloir == 4)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(1).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(1).GetComponent<NumCouloir>().spawn = true;
                            //coul.transform.GetChild(1).transform.GetChild(1).GetComponent<ScriptSalle>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(2).GetComponent<NumCouloir>().coterCouloir == 4)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(2).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(2).GetComponent<NumCouloir>().spawn = true;
                            //coul.transform.GetChild(1).transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                        }
                        else if (coul.transform.GetChild(1).transform.GetChild(3).GetComponent<NumCouloir>().coterCouloir == 4)
                        {
                            coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.GetChild(3).transform.position);
                            coul.transform.GetChild(1).transform.GetChild(3).GetComponent<NumCouloir>().spawn = true;
                            //coul.transform.GetChild(1).transform.GetChild(3).GetComponent<ScriptSalle>().position = true;
                        }
                        coul.transform.GetChild(2).GetComponent<ScriptSalle>().position = true;
                    }
                    else if (listsal.salleGauche.Count == 0 && !salles.timerfinish)
                    {
                        salles.porte.Remove(gameObject);
                        print("gesgesgesges");
                        salles.couloir.Remove(gameObject.transform.parent.gameObject);
                        Destroy(gameObject.transform.parent.gameObject);
                    }
                }
            }
            else if (salles.salle.Count >= maxSalle)
            {
                salles.porte.Remove(gameObject);
                salles.couloir.Remove(gameObject.transform.parent.gameObject);
                Destroy(gameObject.transform.parent.gameObject);
            }
           
            spawn = true;
            instance = true;

        }

        if (salles.salle.Count >= maxSalle && !spawn)
        {
            salles.porte.Remove(gameObject);
            salles.couloir.Remove(gameObject.transform.parent.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
