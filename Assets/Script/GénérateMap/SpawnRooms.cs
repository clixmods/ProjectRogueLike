using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    private ListeRoomsForSpawn listRoomsForSpawn;
    GameObject listRoomsForSpawnG;
    int rand;
    bool spawnOrNot = false;

    public int doorsOpen;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
        listRoomsForSpawnG = GameObject.FindGameObjectWithTag("Room");
        listRoomsForSpawn = listRoomsForSpawnG.GetComponent<ListeRoomsForSpawn>();


        Invoke("SpawnSalle", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnSalle()
    {

        if (spawnOrNot == false)
        {
            
           /* if(doorsOpen == 1)
            {
                rand = Random.Range(0, listRoomsForSpawn.roomsOnTop.Length);
                Instantiate(listRoomsForSpawn.roomsOnTop[rand], gameObject.transform.position, Quaternion.identity);
            }
            else if (doorsOpen == 2)
            {
                rand = Random.Range(0, listRoomsForSpawn.roomsOnDown.Length);
                Instantiate(listRoomsForSpawn.roomsOnDown[rand], gameObject.transform.position, Quaternion.identity);
            }
            else if (doorsOpen == 3)
            {
                rand = Random.Range(0, listRoomsForSpawn.roomsOnRight.Length);
                Instantiate(listRoomsForSpawn.roomsOnRight[rand], gameObject.transform.position, Quaternion.identity);
            }
            else if (doorsOpen == 4)
            {
                rand = Random.Range(0, listRoomsForSpawn.roomsOnLeft.Length);
                Instantiate(listRoomsForSpawn.roomsOnLeft[rand], gameObject.transform.position, Quaternion.identity);
            }*/
            
            switch (doorsOpen)
            {
                case 1:
                    rand = Random.Range(0, listRoomsForSpawn.roomsOnTop.Length);
                    GameObject InitSalle1 = Instantiate(listRoomsForSpawn.roomsOnTop[rand], gameObject.transform.position, Quaternion.identity, listRoomsForSpawnG.transform);
                    InitSalle1.transform.position = new Vector3(InitSalle1.transform.position.x, InitSalle1.transform.position.y, 0f) ;
                    listRoomsForSpawn.numberOfRooms.Add(InitSalle1);
                    break;

                case 2:
                    rand = Random.Range(0, listRoomsForSpawn.roomsOnDown.Length);
                    GameObject InitSalle2 = Instantiate(listRoomsForSpawn.roomsOnDown[rand], gameObject.transform.position, Quaternion.identity, listRoomsForSpawnG.transform);
                    InitSalle2.transform.position = new Vector3(InitSalle2.transform.position.x, InitSalle2.transform.position.y, 0f);
                    listRoomsForSpawn.numberOfRooms.Add(InitSalle2);
                    break;

                case 3:
                    rand = Random.Range(0, listRoomsForSpawn.roomsOnRight.Length);
                    GameObject InitSalle3 = Instantiate(listRoomsForSpawn.roomsOnRight[rand], gameObject.transform.position, Quaternion.identity, listRoomsForSpawnG.transform);
                    InitSalle3.transform.position = new Vector3(InitSalle3.transform.position.x, InitSalle3.transform.position.y, 0f);
                    listRoomsForSpawn.numberOfRooms.Add(InitSalle3);
                    break;


                case 4:
                    rand = Random.Range(0, listRoomsForSpawn.roomsOnLeft.Length);
                    GameObject InitSalle4 = Instantiate(listRoomsForSpawn.roomsOnLeft[rand], gameObject.transform.position, Quaternion.identity, listRoomsForSpawnG.transform);
                    InitSalle4.transform.position = new Vector3(InitSalle4.transform.position.x, InitSalle4.transform.position.y, 0f);
                    listRoomsForSpawn.numberOfRooms.Add(InitSalle4);
                    break;
            }

            spawnOrNot = true;
        }



    }


   /* private void OnTriggerEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("SpawnPointSalle"))
        {
            if(collision.transform.GetComponent<SpawnRooms>().spawnOrNot == false && spawnOrNot == false)
            {
                Instantiate(listRoomsForSpawn.closedRoom, gameObject.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPointSalle") && collision.gameObject.layer != LayerMask.NameToLayer("SpawnPointMid"))
        {
            if (listRoomsForSpawn.roomsOnLeft[5] != null
                && collision.transform.GetComponent<SpawnRooms>() != null
                && collision.transform.GetComponent<SpawnRooms>().spawnOrNot == false 
                && spawnOrNot == false 
                )
            {
                Instantiate(listRoomsForSpawn.roomsOnLeft[5], gameObject.transform.position, Quaternion.identity);
                //Instantiate(listRoomsForSpawn.closedRoom, gameObject.transform.position, Quaternion.identity);
                //Instantiate(listRoomsForSpawn.roomsOnLeft[rand], gameObject.transform.position, Quaternion.identity);
            }

            
        }
        Destroy(gameObject);
    }
}
