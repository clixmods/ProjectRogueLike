using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("Info")]
    public string WeaponName;
    public int CurrentAmmoCount;
    [Range(0, 100)]
    public float pourcentageHeating;

    [Header("Projectile Settings")]
    public GameObject Projectile;
    public Sprite ProjectileTexture;
    public bool IsProjectileEmissive = false;
    //[ConditionalField("IsProjectileEmissive")] public float WanderDistance = 5;
    public Material ProjectileMaterial;
    [ColorUsageAttribute(true, true)]
    public Color ProjectileEmissiveColor;

    
    [Header("Weapon Settings")]
    public Sprite HUDIcon;
    public float ProjectileScale = 1;
    public float Speed = 1;
    public float FireRate = 1;
    public float Cooldown = 0;
    public float ProjectileLifeTime = 5;
    public int DamageAmount = 1;
    public int AmmoTypeId = 0;

    public bool IsMagical = false;
   
    [Header("Ammo Type [ID : 0]")]
    public int MaxAmmoCount;
    [Header("Heat Type [ID : 1]")]
    public float HeatingMultiplier = 1;
    public float CooldownHeating = 3;
    private float CurrentCooldownHeating = 0;
    public float DiscreaseHeatingMultiplier = 1;
    public GameObject Attacker;

    public GameObject projectilDoss;

    // Start is called before the first frame update
    void Start()
    {
        


        projectilDoss = Instantiate(projectilDoss, new Vector3(0f, 0f, 0f), Quaternion.identity);
        projectilDoss.name = gameObject.name + "Projectil";
        //projectilDoss = GameObject.Find("ProjectilDoss");
        CurrentAmmoCount = MaxAmmoCount;
    }

  
    
    // Update is called once per frame
    void Update() // debug
    {

        

        if (Cooldown > 0)
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
    // Permet de clean
    private void OnDestroy() 
    {
         DestroyImmediate(projectilDoss);
    }


    public void Attack(GameObject attacker, float AttackAngle)
    {
        if (Cooldown > 0)
            return;

        if (AmmoTypeId == 0 && CurrentAmmoCount <= 0)
            return;

        if (AmmoTypeId == 1 && pourcentageHeating >= 100)
            return;

        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle));
        GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.Euler(new Vector3(0f, 0f, AttackAngle)), projectilDoss.transform);
        // Apply material 
        projectile.transform.GetComponent<SpriteRenderer>().material = ProjectileMaterial;
        projectile.transform.GetComponent<SpriteRenderer>().material.SetColor("_e_color", ProjectileEmissiveColor) ;

        projectile.transform.GetComponent<ProjectileManager>().DamageAmount = DamageAmount;
        projectile.transform.GetComponent<ProjectileManager>().Attacker = attacker;
        projectile.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle)); // Permet au projectile d'avoir la bonne rotation au niveau texture
        projectile.transform.GetComponent<Rigidbody2D>().AddForce(-transform.right * Speed * 1000);
        // On assigne au projectile le scale assigné 
        Vector3 projectScale = projectile.transform.localScale;
        projectScale = projectScale * ProjectileScale;
        projectile.transform.localScale = projectScale;
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

}
