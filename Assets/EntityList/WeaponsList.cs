using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu(fileName = "WeaponsList", menuName = "Bite / WeaponsList")]
public class WeaponsList : ScriptableObject
{

    //public Dictionary<string, int> stats = new Dictionary<string, int>();
    public GameObject[] weaponGameobject;
    public string[] prefabName;

    //public void Awake()
    //{
    //    prefabName = new string[weaponGameobject.Length];
    //    for(int i = 0; i< weaponGameobject.Length; i++)
    //    {
    //        prefabName[i] = PrefabUtility.GetCorrespondingObjectFromSource(weaponGameobject[i]).name;
    //    }
    //}
    

}
