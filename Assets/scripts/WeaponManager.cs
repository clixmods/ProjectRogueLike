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
    public void Attack(GameObject attacker)
    {

        if (Cooldown > 0)
            return;

        if (AmmoTypeId == 0 && CurrentAmmoCount <= 0)
            return;

        if (AmmoTypeId == 1 && pourcentageHeating >= 100)
            return;

        GameObject projectile = Instantiate(Projectile, transform);
        projectile.GetComponent<SpriteRenderer>().sprite = ProjectileTexture;
        projectile.GetComponent<SpriteRenderer>().flipX = true;
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
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

        //print(Input.mousePosition.normalized);
        Vector2 Oh;
        Oh.x = Input.mousePosition.normalized.x;
        Oh.y = Input.mousePosition.normalized.y;

        Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        projectile.transform.GetComponent<Rigidbody2D>().AddForce(mousePosition.normalized * Speed * 1000);



        Vector2 positionOnScreen = transform.position;

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        Camera.main.gameObject.transform.GetChild(0).transform.position = mouseOnScreen;
        print(Camera.main.gameObject.transform.GetChild(0).transform.position);
        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

   


    Cooldown = FireRate;

        Destroy(projectile, ProjectileLifeTime);

    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmoCount = MaxAmmoCount;
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
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




        Vector2 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition).normalized;
        Vector2 positionOnScreen = transform.position;
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ViewportToScreenPoint(Input.mousePosition);
        Camera.main.gameObject.transform.GetChild(0).transform.position = mousePosition;
        print(mousePosition);
        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
    }
}
