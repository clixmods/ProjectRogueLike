using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumCouloir : MonoBehaviour
{
    public int coterCouloir;
    ListCouloir listCouloir;

    public bool spawn;
    public bool position;
    bool instance;
    bool instanceWall;

    GameObject coul;
    public GameObject listingCouloir;
    int rand;

    TotalScript salles;

    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;

    public bool reinstance;

    // Start is called before the first frame update
    void Start()
    {
        salles = GameObject.Find("TotalSalle(Clone)").GetComponent<TotalScript>();
        if (!spawn)
        {
            listingCouloir = Instantiate(listingCouloir, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            salles.list.Add(listingCouloir);
        }
        listCouloir = listingCouloir.gameObject.GetComponent<ListCouloir>();
        print(listCouloir.CouloirBas[0]);

    }

    // Update is called once per frame
    void Update()
    {
        if (reinstance)
        {
            listingCouloir = Instantiate(listingCouloir, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            salles.list.Add(listingCouloir);
            listCouloir = listingCouloir.gameObject.GetComponent<ListCouloir>();
            reinstance = false;
        }
        Invoke("spawnCouloir", 0.5f);
        if (spawn && instance && coul == null)
        {
            spawn = false;
        }
    }

    void spawnCouloir()
    {

        if (!spawn && !salles.timerfinish && !reinstance)
        {
            if (coterCouloir == 1)
            {
                if (listCouloir.CouloirHaut.Count != 0)
                {
                    gameObject.transform.parent.transform.parent.transform.GetChild(2).GetComponent<ScriptSalle>().position = false;
                    rand = Random.Range(0, listCouloir.CouloirHaut.Count);
                    coul = Instantiate(listCouloir.CouloirHaut[rand], transform.position, Quaternion.identity, gameObject.transform);
                    listCouloir.CouloirHaut.Remove(listCouloir.CouloirHaut[rand]);

                    if (coul.transform.GetChild(0).GetComponent<NumSalle>().numSal == 3)
                    {
                        coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(0).transform.position);
                        coul.transform.GetChild(0).GetComponent<NumSalle>().spawn = true;
                    }
                    else if (coul.transform.GetChild(1).GetComponent<NumSalle>().numSal == 3)
                    {
                        coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.position);
                        coul.transform.GetChild(1).GetComponent<NumSalle>().spawn = true;

                    }
                    coul.transform.GetChild(3).GetComponent<ScriptCouloir>().position = true;
                }
                else if (listCouloir.CouloirHaut.Count == 0 && !instanceWall)
                {
                    GameObject w = Instantiate(wall1, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - (wall1.transform.localScale.y / 2)), Quaternion.identity, gameObject.transform);
                    salles.wall.Add(w);
                    instanceWall = true;
                    
                }

            }
            if (coterCouloir == 2)
            {
                if (listCouloir.CouloirDroit.Count != 0)
                {
                    gameObject.transform.parent.transform.parent.transform.GetChild(2).GetComponent<ScriptSalle>().position = false;
                    rand = Random.Range(0, listCouloir.CouloirDroit.Count);
                    coul = Instantiate(listCouloir.CouloirDroit[rand], transform.position, Quaternion.identity, gameObject.transform);
                    listCouloir.CouloirDroit.Remove(listCouloir.CouloirDroit[rand]);
                    if (coul.transform.GetChild(0).GetComponent<NumSalle>().numSal == 4)
                    {
                        coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(0).transform.position);
                        coul.transform.GetChild(0).GetComponent<NumSalle>().spawn = true;
                    }
                    else if (coul.transform.GetChild(1).GetComponent<NumSalle>().numSal == 4)
                    {
                        coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.position);
                        coul.transform.GetChild(1).GetComponent<NumSalle>().spawn = true;
                    }
                    coul.transform.GetChild(3).GetComponent<ScriptCouloir>().position = true;
                }
                else if (listCouloir.CouloirDroit.Count == 0 && !instanceWall)
                {
                    GameObject w = Instantiate(wall2, new Vector2(gameObject.transform.position.x + (wall1.transform.localScale.x / 2), gameObject.transform.position.y ), Quaternion.identity, gameObject.transform);
                    salles.wall.Add(w);
                    instanceWall = true;
                }

            }
            if (coterCouloir == 3)
            {
                if (listCouloir.CouloirBas.Count != 0)
                {
                    gameObject.transform.parent.transform.parent.transform.GetChild(2).GetComponent<ScriptSalle>().position = false;
                    rand = Random.Range(0, listCouloir.CouloirBas.Count);
                    coul = Instantiate(listCouloir.CouloirBas[rand], transform.position, Quaternion.identity, gameObject.transform);
                    listCouloir.CouloirBas.Remove(listCouloir.CouloirBas[rand]);

                    if (coul.transform.GetChild(0).GetComponent<NumSalle>().numSal == 1)
                    {
                        coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(0).transform.position);
                        coul.transform.GetChild(0).GetComponent<NumSalle>().spawn = true;
                    }
                    else if (coul.transform.GetChild(1).GetComponent<NumSalle>().numSal == 1)
                    {
                        coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.position);
                        coul.transform.GetChild(1).GetComponent<NumSalle>().spawn = true;
                    }
                    coul.transform.GetChild(3).GetComponent<ScriptCouloir>().position = true;
                }
                else if (listCouloir.CouloirBas.Count == 0 && !instanceWall)
                {
                    GameObject w = Instantiate(wall3, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + (wall1.transform.localScale.y / 2)), Quaternion.identity, gameObject.transform);
                    salles.wall.Add(w);
                    instanceWall = true;
                }

            }
            if (coterCouloir == 4)
            {
                if (listCouloir.CouloirGauche.Count != 0)
                {
                    gameObject.transform.parent.transform.parent.transform.GetChild(2).GetComponent<ScriptSalle>().position = false;
                    rand = Random.Range(0, listCouloir.CouloirGauche.Count);
                    coul = Instantiate(listCouloir.CouloirGauche[rand], transform.position, Quaternion.identity, gameObject.transform);
                    listCouloir.CouloirGauche.Remove(listCouloir.CouloirGauche[rand]);

                    if (coul.transform.GetChild(0).GetComponent<NumSalle>().numSal == 2)
                    {
                        coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(0).transform.position);
                        coul.transform.GetChild(0).GetComponent<NumSalle>().spawn = true;
                    }
                    else if (coul.transform.GetChild(1).GetComponent<NumSalle>().numSal == 2)
                    {
                        coul.transform.position = gameObject.transform.position + (coul.transform.position - coul.transform.GetChild(1).transform.position);
                        coul.transform.GetChild(1).GetComponent<NumSalle>().spawn = true;
                    }
                    coul.transform.GetChild(3).GetComponent<ScriptCouloir>().position = true;
                }
                else if (listCouloir.CouloirGauche.Count == 0 && !instanceWall)
                {
                    GameObject w = Instantiate(wall4, new Vector2(gameObject.transform.position.x - (wall1.transform.localScale.x / 2), gameObject.transform.position.y ), Quaternion.identity, gameObject.transform);
                    salles.wall.Add(w);
                    instanceWall = true;
                }

            }
            spawn = true;
            instance = true;
        }
    }

    
}
