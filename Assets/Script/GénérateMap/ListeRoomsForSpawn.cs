using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListeRoomsForSpawn : MonoBehaviour
{
    public GameObject[] roomsOnTop;
    public GameObject[] roomsOnDown;
    public GameObject[] roomsOnRight;
    public GameObject[] roomsOnLeft;
    public GameObject closedRoom;

    public List<GameObject> numberOfRooms;

    public GameObject spawnBoss;

    bool verif;

    



    // Start is called before the first frame update
    void Start()
    {
        verif = false;
        Invoke("changementDeTrig", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  void changementDeTrig ()
    {
        GameObject lastRoom = numberOfRooms[numberOfRooms.Count - 1].gameObject.transform.GetChild(0).transform.GetChild(numberOfRooms[numberOfRooms.Count - 1].gameObject.transform.GetChild(0).childCount - 1).gameObject;
        Destroy(lastRoom);
        GameObject Trig = numberOfRooms[numberOfRooms.Count - 1];
        Instantiate(spawnBoss, Trig.transform.position, Quaternion.identity, Trig.transform.GetChild(0));
        verif = true;
    }
}
