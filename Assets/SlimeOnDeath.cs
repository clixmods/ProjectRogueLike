using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlimeOnDeath : MonoBehaviour
{
    [Range(0.05f, 1f)]
    public float ScalarDiscreaser = 0.25f;
    [Range(0.5f, 0.90f)]
    public float ScalarMin = 0.5f;
    public bool willSplit = false;
    public GameObject prefabBaby;
    // Start is called before the first frame update

    private void Start()
    {
        Vector2 scale = transform.localScale;
        while(scale.x >= ScalarMin)
        {
            GameObject babySlime = Instantiate(gameObject, transform.position, Quaternion.identity, transform) ;
            babySlime.SetActive(false);
            scale.x -= ScalarDiscreaser;
        }

        //prefabBaby = gameObject.
        if (gameObject.transform.localScale.x > ScalarMin)
            willSplit = true;


    }
   
    private void OnDestroy()
    {
        if(willSplit)
        {
            //GameObject babySlime = Instantiate(prefabBaby, transform.position, transform.rotation, null);
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).transform.parent = null;
                foreach (Behaviour behaviour in transform.GetChild(i).gameObject.GetComponentsInChildren<Behaviour>())
                    behaviour.enabled = true;

                EnemyManager col = transform.GetChild(i).transform.GetComponent<EnemyManager>();
                if (col != null)
                    col.enabled = false;

                //MeshRenderer mr = transform.GetChild(i).transform.GetComponent<MeshRenderer>();
                //if (mr != null)
                //    mr.enabled = false;
                //Component[] Yo = transform.GetChild(i).GetComponents(typeof(Component));
                //foreach(Component Compo in Yo)
                //{
                //Compo.GetComponent<>.enabled = true;
                //}
            }
            //GameObject babySlime = Instantiate(Resources.Load("Assets\\prefabs\\Ennemies\\slime.prefab", typeof(GameObject))) as GameObject;
           // babySlime.SetActive(true);

            // babySlime.GetComponent<EnemyManager>().Scale -= ScalarDiscreaser;
            /*Component[] p = babySlime.GetComponents(default);
            foreach(Component o in  p)
            {
                
            }
            */
        }
    }
    private void enableComponent( )
    {
       // if (col != null)
         //   col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        string path = AssetDatabase.GetAssetPath(prefabBaby);
        var prefabGameObject = PrefabUtility.GetCorrespondingObjectFromSource(prefabBaby);
        //print(path);
        print(prefabGameObject.name);
        //prefabBaby = Resources.Load<GameObject>(path);
    }
}
