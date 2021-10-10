using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("Info")]
    public int CurrentAmmoCount;
    [Range(0, 100)]
    public float pourcentageHeating;
    [Header("Weapon Settings")]
    public Sprite HUDIcon;
    public GameObject Projectile;
    public Sprite ProjectileTexture;
    public float Speed = 1;
    public float FireRate = 1;
    public float Cooldown = 0;
    public float ProjectileLifeTime = 5;
    public int DamageAmount = 1;
    public int AmmoTypeId = 0;
   
    [Header("Ammo Type [ID : 0]")]
    public int MaxAmmoCount;
    [Header("Heat Type [ID : 1]")]
    public float HeatingMultiplier = 1;
    public float CooldownHeating = 3;
    private float CurrentCooldownHeating = 0;
    public float DiscreaseHeatingMultiplier = 1;
    public void Attack(GameObject attacker, float AttackAngle)
    {
        if (Cooldown > 0)
            return;

        if (AmmoTypeId == 0 && CurrentAmmoCount <= 0)
            return;

        if (AmmoTypeId == 1 && pourcentageHeating >= 100)
            return;

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle));
        GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.Euler(new Vector3(0f, 0f, AttackAngle)), null);

        projectile.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle)); // Permet au projectile d'avoir la bonne rotation au niveau texture
        projectile.transform.GetComponent<Rigidbody2D>().AddForce(-transform.right * Speed * 1000);

        // On assigne au projectile la texture désigné dans l'arme
        projectile.GetComponent<SpriteRenderer>().sprite = ProjectileTexture;
        projectile.GetComponent<SpriteRenderer>().flipX = true;
        gameObject.GetComponent<SpriteRenderer>().flipX = true;

        // On check l'ammoType
        switch (AmmoTypeId)
        {
            case 0:
                CurrentAmmoCount--;
                break;
            case 1:
                CurrentCooldownHeating = CooldownHeating;
                pourcentageHeating = pourcentageHeating + 1 * HeatingMultiplier;
                break;

        }


 
        Cooldown = FireRate;

        Destroy(projectile, ProjectileLifeTime);

    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmoCount = MaxAmmoCount;
    }
    
    // Update is called once per frame
    void Update() // debug
    {
       // if (Input.GetKey(KeyCode.Mouse0))
       // {
       //     Attack(gameObject);
       // }
        if(Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
        }

        if(pourcentageHeating > 0)
        {
            if (CurrentCooldownHeating > 0)
                CurrentCooldownHeating -= Time.deltaTime;
            else
                pourcentageHeating -= Time.deltaTime * DiscreaseHeatingMultiplier;

        }




        
    }
}
