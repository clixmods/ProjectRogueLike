using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject HudPrefab;
    public GameObject prefabSpawnRoom;
    GameObject spawnRoom;
    public GameObject prefabListRoom;
    GameObject listRoom;
    public GameObject prefabPlayerSpawn;
    public GameObject prefabListWeapons;
    public GameObject listWeapons;
    public int numberOfRooms;
    public int numberOfRoomToDo;
    public int roomDone;
    public int chestGot;
    public GameObject chest;
    bool checkReceve;

    

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(HudPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        checkReceve = false;
        spawnRoom = Instantiate(prefabSpawnRoom, new Vector3(0f, 0f, 0f), Quaternion.identity);
        listRoom = Instantiate(prefabListRoom, new Vector3(0f, 0f, 0f), Quaternion.identity);
        listWeapons = Instantiate(prefabListWeapons, new Vector3(0f, 0f, 0f), Quaternion.identity);
        
        Instantiate(prefabPlayerSpawn, spawnRoom.transform.position , Quaternion.identity);
        GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.Mouvement);

    }

    // Update is called once per frame
    void Update()
    {
        if (checkReceve == false)
        {
            Invoke("SendInformation", 2f);
            
        }
    }

    public void SendInformation()
    {
        checkReceve = true;
        numberOfRooms = listRoom.transform.GetComponent<ListeRoomsForSpawn>().numberOfRooms.Count - 1;
        numberOfRoomToDo = Random.Range(((numberOfRooms/2)/2)+ 1, numberOfRooms/ 2);
        HUDManager.HUDUtility.SetMiddleMsg(10, "A strange entity controls the dungeon, find him and kill him.");
    }
}
