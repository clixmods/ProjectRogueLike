using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LootManager : MonoBehaviour
{
    public enum ItemType 
    {
        life,
        Slotlife,
        health,
        weapon,
        key
    }
   // public Texture2D texture;
    public ItemType objectType;

    float RotationSpeed = 250;
    // Flick Light
    bool Flick = true;

    public GameObject reward;
    Collider2D lootCollider;
    public Light2D Light;
    public SpriteRenderer renderer;
    private float maxFlickIntensity = 2;
    private float minFlickIntensity = 1;
    private float flickIncreaser = 2;

    // Start is called before the first frame update
    void Start()
    {
       if(objectType == ItemType.weapon)
       {
            if (reward.TryGetComponent<WeaponManager>(out WeaponManager wpnMan))
            {
                if(wpnMan.IsMagical)
                    Light.color = Color.yellow;
                else
                    Light.color = Color.cyan;

                renderer.sprite = wpnMan.HUDIcon;
            }
            else if (reward.TryGetComponent<ManagerWeaponCorpAcopr>(out ManagerWeaponCorpAcopr wpnMelee))
            {
                if (wpnMelee.IsMagical)
                    Light.color = Color.yellow;
                else
                    Light.color = Color.cyan;

                renderer.sprite = wpnMelee.HUDIcon;
            }

       }
    }

    // Update is called once per frame
    void Update()
    {
        doRotation();
    }
    void doRotation()
    {
        if(Flick)
        {
            if(Light.intensity <= maxFlickIntensity)
                Light.intensity += Time.deltaTime* flickIncreaser;
            else
                Flick = false;
        }
        else
        {
            if (Light.intensity > minFlickIntensity)
                Light.intensity -= Time.deltaTime* flickIncreaser;
            else
            {
                Flick = true;
            }
            
        }
        
        transform.Rotate(Vector2.up, Time.deltaTime * RotationSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject a_guy = collision.gameObject;
        if(a_guy.tag == "Player")
        {
            switch(objectType)
            {
                case ItemType.life:
                    AddLifeToPlayer(a_guy);
                    break;
                case ItemType.Slotlife:
                    AddSlotLifeToPlayer(a_guy);
                    break;
                case ItemType.health:
                    AddHealthToPlayer(a_guy);
                    break;
                case ItemType.key:
                    break;
                case ItemType.weapon:
                    if(reward.TryGetComponent<WeaponManager>(out WeaponManager wpnMan))
                    {
                        AddDistanceWeaponToPlayer(wpnMan,a_guy);
                    }
                    else if(reward.TryGetComponent<ManagerWeaponCorpAcopr>(out ManagerWeaponCorpAcopr wpnMelee))
                    {
                        AddMeleeWeaponToPlayer(wpnMelee, a_guy);
                    }
                    break;
            }
            Destroy(gameObject);

        }
    }
    private void AddDistanceWeaponToPlayer(WeaponManager weapon, GameObject owner)
    {
        GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.SwitchMetD);
        HUDManager.HUDUtility.SetMiddleMsg(4, "Gained " + weapon.WeaponName);
        GameObject wpn = Instantiate(reward, owner.transform.position, Quaternion.identity, owner.GetComponent<PlayerControler>().listD.transform);
        wpn.transform.localPosition = Vector3.zero;
        wpn.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wpn.SetActive(false);
    }
    private void AddMeleeWeaponToPlayer(ManagerWeaponCorpAcopr weapon, GameObject owner)
    {
        GameManager.GameUtil.ActiveTutorial((int)TutorialPhase.ChangementWeapon);
        HUDManager.HUDUtility.SetMiddleMsg(4, "Gained "+ weapon.WeaponName);
        GameObject wpn = Instantiate(reward, owner.transform.position, Quaternion.identity, owner.GetComponent<PlayerControler>().listC.transform);
        wpn.transform.localPosition = Vector3.zero;
        wpn.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wpn.SetActive(false);
    }
    private void AddLifeToPlayer(GameObject player)
    {
        if(player.TryGetComponent<PlayerControler>(out PlayerControler plrCont))
        {
            if(plrCont.PlayerLifes != plrCont.PlayerMaxLifes)
                plrCont.PlayerLifes++;

            HUDManager.HUDUtility.SetMiddleMsg(4, "Added Life");
        }
    }
    private void AddSlotLifeToPlayer(GameObject player)
    {
        if (player.TryGetComponent<PlayerControler>(out PlayerControler plrCont))
        {
            HUDManager.HUDUtility.SetMiddleMsg(4, "Unlocked Life Slot");
            plrCont.PlayerMaxLifes++;
        }
    }
    private void AddHealthToPlayer(GameObject player)
    {
        if (player.TryGetComponent<PlayerControler>(out PlayerControler plrCont))
        {
            if(plrCont.health + 20 > plrCont.MaxHealth)
            {
                plrCont.health = plrCont.MaxHealth;
            }
            else
                plrCont.health += 20;
            
            HUDManager.HUDUtility.SetMiddleMsg(4, "Gained Health");
            //if()
            //plrCont.health += ;
        }
    }
}
