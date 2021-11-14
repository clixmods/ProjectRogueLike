using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
   // GameObject[] toucheds;
    [SerializeField]
    private List<GameObject> toucheds = new List<GameObject>();

    
       
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().IsFiring && toucheds.Count > 0)
        {
            toucheds = new List<GameObject>();
        }
    }
    private bool isAlreadyTouched(GameObject victim)
    {
        for(int i = 0; i < toucheds.Count; i++)
        {
            if (toucheds[i] == victim)
                return true;
        }
        return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject != gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().Attacker && 
            !collision.gameObject.CompareTag(gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().Attacker.tag) &&
            !isAlreadyTouched(collision.gameObject) )
        {
            //if (gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().Attacker.CompareTag("Player"))
            //{
            //    print("TOUCHED");
            //}
            if (collision.gameObject.GetComponent<PlayerControler>() != null && !collision.gameObject.GetComponent<PlayerControler>().isDamaged)
            {
                //collision.GetComponent<PlayerControler>().isDamaged = true;
                collision.gameObject.GetComponent<PlayerControler>().health -= gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().attackDamage;
                collision.gameObject.GetComponent<PlayerControler>().isDamaged = true;
                toucheds.Add(collision.gameObject);

            }
            if (collision.gameObject.GetComponent<EnemyManager>() != null)
            {
                collision.gameObject.GetComponent<EnemyManager>().health -= gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().attackDamage;
                ///collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = collision.gameObject.GetComponent<EnemyManager>().flashDamage;
                collision.gameObject.GetComponent<EnemyManager>().isDamaged = true;
                toucheds.Add(collision.gameObject);
            }
            if (collision.gameObject.GetComponent<Boss>() != null)
            {
                collision.gameObject.GetComponent<Boss>().health -= gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>().attackDamage;
                // collision.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = collision.gameObject.GetComponent<EnemyManager>().flashDamage;
                // collision.gameObject.GetComponent<Boss>().isDamaged = true;

                //toucheds.Add(collision.gameObject);
            }


        }
    }
}
