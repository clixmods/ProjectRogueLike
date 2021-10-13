using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{

    public List<GameObject> prefabWeapon;

    public int rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(rand > -1)
        {
            prefabWeapon.Remove(prefabWeapon[rand]);
            rand = -1;
        }
    }
}
