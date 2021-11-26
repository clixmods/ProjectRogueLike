using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SlimeOnDeath : MonoBehaviour
{
    [Range(0.05f, 1f)]
    public float ScalarDiscreaser = 0.25f;
    [Range(0.5f, 0.90f)]
    public float ScalarMin = 0.5f;
    public bool willSplit = false;
    public GameObject prefabBaby;
    // Start is called before the first frame update

    private void Awake()
    {
    
        //Vector2 scale = transform.localScale;
        //if (transform.localScale.x > ScalarMin)
        //{
        //    GameObject Baby = Instantiate(prefabBaby, transform.position, Quaternion.identity);
        //    GameObject Baby2 = Instantiate(prefabBaby, transform.position, Quaternion.identity);
        //    Baby.GetComponent<EnemyManager>().ChangeScale(scale.x - ScalarDiscreaser);
        //    Baby2.GetComponent<EnemyManager>().ChangeScale(scale.x - ScalarDiscreaser);


        //    willSplit = true;
        //}
        //else
        //{
        //    willSplit = false;
        //}

    }
    private void Start()
    {
        Vector2 scale = transform.localScale;
        if (scale.x - ScalarDiscreaser >= ScalarMin)
        {
            willSplit = true;
            GameObject Baby = Instantiate(prefabBaby, transform.position, Quaternion.identity,transform);
            GameObject Baby2 = Instantiate(prefabBaby, transform.position, Quaternion.identity,transform);
            Baby.SetActive(false);
            Baby2.SetActive(false);
            
            Baby.GetComponent<EnemyManager>().ChangeScale(scale.x - ScalarDiscreaser);
            Baby.GetComponent<EnemyManager>().FromSpawner = false;
            Baby.GetComponent<EnemyManager>().TriggerSalle = null;
            Baby.GetComponent<EnemyManager>().isChild = true;

            Baby2.GetComponent<EnemyManager>().ChangeScale(scale.x - ScalarDiscreaser);
            Baby2.GetComponent<EnemyManager>().FromSpawner = false;
            Baby2.GetComponent<EnemyManager>().TriggerSalle = null;
            Baby2.GetComponent<EnemyManager>().isChild = true;

        }
        //if (transform.localScale.x > ScalarMin)
        //{
        //    GameObject Baby = Instantiate(prefabBaby, transform.position, Quaternion.identity);
        //    GameObject Baby2 = Instantiate(prefabBaby, transform.position, Quaternion.identity);
        //    willSplit = true;
        //}

        //else
        //    willSplit = false;

    }

    private void OnEnable()
    {
     //   Debug.Log(" OnEnable() "+gameObject.name);
     //   Vector2 scale = transform.localScale;
     //   transform.GetComponent<EnemyManager>().ChangeScale(scale.x - ScalarDiscreaser);
     ///*   
     //   if (scale.x >= ScalarMin)
     //   {
     //       scale.x -= ScalarDiscreaser;
     //       scale.y -= ScalarDiscreaser;
     //       transform.localScale = scale; 
     //   }
     //*/
     //   if (gameObject.transform.localScale.x > ScalarMin)
     //       willSplit = true;
     //   else
     //       willSplit = false;
    }
   
    // Quand l'ai est détruit, on check si il va split
    // si c'est le cas, on active tout les components des enfants car ils sont off par défaut 
    // pk ? je sais pas gros
    private void OnDestroy() 
    {
        if(willSplit)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                enableComponents(transform.GetChild(i).gameObject);
            }
        }
    }

    private void enableComponents( GameObject ai_object)
    {
        ai_object.SetActive(true);
        BoxCollider2D box = ai_object.GetComponent<BoxCollider2D>();
        NavMeshAgent agent = ai_object.GetComponent<NavMeshAgent>();
        EnemyManager aimanager = ai_object.GetComponent<EnemyManager>();
        SlimeOnDeath slimeondeath = ai_object.GetComponent<SlimeOnDeath>();
        box.enabled = true;
        agent.enabled = true;
        aimanager.enabled = true;
        slimeondeath.enabled = true;
        ai_object.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
