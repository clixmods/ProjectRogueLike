using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
   // GameObject[] toucheds;
    [SerializeField]
    private List<GameObject> toucheds = new List<GameObject>();
    ManagerWeaponCorpAcopr weaponComponent;
    // Start is called before the first frame update
    void Start()
    {
        weaponComponent = gameObject.GetComponentInParent<ManagerWeaponCorpAcopr>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!weaponComponent.IsFiring && toucheds.Count > 0)
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
        GameObject victim = collision.gameObject;
        if (victim != weaponComponent.Attacker && !victim.CompareTag(weaponComponent.Attacker.tag) && !isAlreadyTouched(victim) )
        {
            // Event player is damaged
            if (victim.GetComponent<PlayerControler>() != null && !victim.GetComponent<PlayerControler>().isDamaged)
            {
                victim.GetComponent<PlayerControler>().health -= weaponComponent.attackDamage;
                victim.GetComponent<PlayerControler>().isDamaged = true;
                toucheds.Add(victim);
            }
            // Event a ennemy is damaged
            if (victim.TryGetComponent<EnemyManager>(out EnemyManager VictimManager))
            {
                Debug.Log("POGGGERRRS " + (int)VictimManager.ReceiveDamageOn + " /  " + (int)weaponComponent.type);
                if ((int)VictimManager.ReceiveDamageOn == (int)weaponComponent.type || VictimManager.ReceiveDamageOn == ReceiveDamageOnType.Both)
                {
                    VictimManager.health -= weaponComponent.attackDamage;
                    VictimManager.isDamaged = true;                  
                }
                toucheds.Add(victim);
                GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.TypeWeapon);
            }
            // Event boss is damaged
            if (victim.GetComponent<Boss>() != null)
            {
                victim.GetComponent<Boss>().health -= weaponComponent.attackDamage;
            }


        }
    }
}
