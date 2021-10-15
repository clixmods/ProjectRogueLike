using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    private ListeRoomsForSpawn listRoomsForSpawn;

    int rand;
    int spawnOrNot;

    public int doorsOpen;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);

        listRoomsForSpawn = GameObject.FindGameObjectWithTag("Room").GetComponent<ListeRoomsForSpawn>();
        spawnOrNot = 0;

        Invoke("SpawnSalle", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSalle()
    {

        if (spawnOrNot == 0)
        {
            switch (doorsOpen)
            {
                case 1:
                    rand = Random.Range(0, listRoomsForSpawn.roomsOnTop.Length);
                    Instantiate(listRoomsForSpawn.roomsOnTop[rand], gameObject.transform.position, Quaternion.identity);
                    break;

                case 2:
                    rand = Random.Range(0, listRoomsForSpawn.roomsOnDown.Length);
                    Instantiate(listRoomsForSpawn.roomsOnDown[rand], gameObject.transform.position, Quaternion.identity);
                    break;

                case 3:
                    rand = Random.Range(0, listRoomsForSpawn.roomsOnRight.Length);
                    Instantiate(listRoomsForSpawn.roomsOnRight[rand], gameObject.transform.position, Quaternion.identity);
                    break;


                case 4:
                    rand = Random.Range(0, listRoomsForSpawn.roomsOnLeft.Length);
                    Instantiate(listRoomsForSpawn.roomsOnLeft[rand], gameObject.transform.position, Quaternion.identity);
                    break;
            }

            spawnOrNot = 1;
        }



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("SpawnPointSalle"))
        {
            if(collision.transform.GetComponent<SpawnRooms>().spawnOrNot == 0 && spawnOrNot == 0)
            {
                Instantiate(listRoomsForSpawn.closedRoom, gameObject.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
