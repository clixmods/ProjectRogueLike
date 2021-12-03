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
        key,
        ammo,
        healthSource
    }
   // public Texture2D texture;
    public ItemType objectType;

    [SerializeField] float RotationSpeed = 250;
    // Flick Light
    bool Flick = true;

    public GameObject reward;
    Collider2D lootCollider;
    public Light2D Light;
    public SpriteRenderer sprtRend;
    private float maxFlickIntensity = 2;
    private float minFlickIntensity = 1;
    private float flickIncreaser = 2;

    [SerializeField] int AddAmmo = 100;

    [Header("Health type")] public int AddHealth = 10;

    private bool hintstringIsCreated;

    // Start is called before the first frame update
    void Start()
    {
        sprtRend = transform.GetComponent<SpriteRenderer>();
       if (objectType == ItemType.weapon)
       {
            if (reward.TryGetComponent<WeaponManager>(out WeaponManager wpnMan))
            {
                if(wpnMan.IsMagical)
                    Light.color = Color.yellow;
                else
                    Light.color = Color.cyan;

                sprtRend.sprite = wpnMan.HUDIcon;
            }
            else if (reward.TryGetComponent<ManagerWeaponCorpAcopr>(out ManagerWeaponCorpAcopr wpnMelee))
            {
                if (wpnMelee.IsMagical)
                    Light.color = Color.yellow;
                else
                    Light.color = Color.cyan;

                sprtRend.sprite = wpnMelee.HUDIcon;
            }

       }
    }

    // Update is called once per frame
    void Update()
    {
        doRotation();

        if (HUDManager.HUDUtility != null && !hintstringIsCreated)
        {
            //HUDManager.HUDUtility.CreateHintString(gameObject, "Use [E] to interact OOF.");
            hintstringIsCreated = true;
        }
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        bool willDestroy = true;
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
                case ItemType.ammo:
                    if (a_guy.TryGetComponent<PlayerControler>(out PlayerControler plrController))
                    {
                        Transform listDistance = plrController.listD.transform;
                        for(int i = 0; i < listDistance.childCount; i++)
                        {
                            int ammo = listDistance.GetChild(i).GetComponent<WeaponManager>().CurrentAmmoCount;
                            int maxAmmo = listDistance.GetChild(i).GetComponent<WeaponManager>().MaxAmmoCount;
                            
                            HUDManager.HUDUtility.SetMiddleMsg(4, "Gained "+AddAmmo+ " for every distance weapon" );
                            if ( ammo + AddAmmo > maxAmmo)
                            {
                                listDistance.GetChild(i).GetComponent<WeaponManager>().CurrentAmmoCount = maxAmmo;                            
                            }
                            else
                                listDistance.GetChild(i).GetComponent<WeaponManager>().CurrentAmmoCount += AddAmmo;

                        }
                      
                    }
                    break;
                case ItemType.healthSource:
                    AddHealthToPlayer(a_guy);
                    willDestroy = false;
                    break;
            }
            if(willDestroy)
                Destroy(gameObject);

        }
    }
    private void AddAmmoWeaponToPlayer(WeaponManager weapon, GameObject owner)
    {
        HUDManager.HUDUtility.SetMiddleMsg(4, "Gained " + weapon.WeaponName);
       
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
