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
        string path = AssetDatabase.GetAssetPath(gameObject);
        prefabBaby = Resources.Load<GameObject>(path);
        //prefabBaby = gameObject.
        if (gameObject.transform.localScale.x > ScalarMin)
            willSplit = true;

    }
   
    private void OnDestroy()
    {
        if(willSplit)
        {
            GameObject babySlime = Instantiate(prefabBaby, transform.position, transform.rotation, null);
            babySlime.SetActive(true);

            // babySlime.GetComponent<EnemyManager>().Scale -= ScalarDiscreaser;
            /*Component[] p = babySlime.GetComponents(default);
            foreach(Component o in  p)
            {
                
            }
            */
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
