using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject HudPrefab;
    public GameObject prefabSpawnRoom; //AMettre
    GameObject spawnRoom; 
    public GameObject prefabListRoom; //AMettre TotalSalle
    GameObject listRoom;
    public GameObject prefabPlayerSpawn;
    GameObject playerSpawn;
    public GameObject prefabListWeapons;
    public GameObject listWeapons;
    public int numberOfRooms;
    public int numberOfRoomToDo;
    public int roomDone;
    public int roomDoneBoss;
    public int chestGot;
    public GameObject chest;
    bool checkReceve;
    public GameObject porteBoss; //AMettre



    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()

    {
        listRoom = Instantiate(prefabListRoom, new Vector3(0f, 0f, 0f), Quaternion.identity);
       
        spawnRoom = Instantiate(prefabSpawnRoom, new Vector3(0f, 0f, 0f), Quaternion.identity);
       
        

    }

    // Update is called once per frame
    void Update()
    {
        if (listRoom.transform.GetComponent<TotalScript>().finishall)
        {
           if (!checkReceve)
                {
                listWeapons = Instantiate(prefabListWeapons, new Vector3(0f, 0f, 0f), Quaternion.identity);
                Instantiate(HudPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                
                playerSpawn = Instantiate(prefabPlayerSpawn, spawnRoom.transform.position, Quaternion.identity);
                GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.Mouvement);
                checkReceve = true;
            }
            Invoke("SendInformation", 2f);
            
        }
        if(numberOfRooms == roomDoneBoss)
        {
            CreationDuPortailVersLeBoss();
        }
    }

    public void SendInformation()
    {
        checkReceve = true;
        numberOfRooms = listRoom.transform.GetComponent<TotalScript>().salle.Count -1;
        numberOfRoomToDo = Random.Range(((numberOfRooms/2)/2)+ 1, numberOfRooms/ 2);
        HUDManager.HUDUtility.SetMiddleMsg(10, "A strange entity controls the dungeon, find him and kill him.");
    }

    public void CreationDuPortailVersLeBoss ()
    {
        //Instantiate(porteBoss, playerSpawn.transform.GetChild(0).transform.position, Quaternion.identity);
    }
}
