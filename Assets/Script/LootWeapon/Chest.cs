using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public WeaponList list;

    public GameObject prefabLoot;
    private int rand;
    int numberOfWeapon;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            List<GameObject> weaponList = list.prefabWeapon;
            numberOfWeapon = weaponList.Count;
            if (numberOfWeapon > 0)
            {
                rand = Random.Range(0, numberOfWeapon);

                GameObject loot = Instantiate(prefabLoot, transform.position, Quaternion.identity);
                GameObject pivotLoot = loot.transform.GetChild(0).gameObject;
                Instantiate(weaponList[rand], pivotLoot.transform.position, Quaternion.identity, pivotLoot.transform);
                list.rand = rand;
                Destroy(gameObject);
            }
            else
            {
                print("sorry fréro y a plus d'arme");
                Destroy(gameObject);
            }
        }
    }
}
