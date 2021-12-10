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
            ApplyBabySetting(Baby.GetComponent<EnemyManager>());
            ApplyBabySetting(Baby2.GetComponent<EnemyManager>());
        }
    }

    void ApplyBabySetting(EnemyManager babyEnemyManager)
    {
        Vector2 scale = transform.localScale;
        babyEnemyManager.ChangeScale(scale.x - ScalarDiscreaser);
        babyEnemyManager.FromSpawner = false;
        babyEnemyManager.TriggerSalle = null;
        babyEnemyManager.isChild = true;
        babyEnemyManager.maxHealth = babyEnemyManager.maxHealth / 2;
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
