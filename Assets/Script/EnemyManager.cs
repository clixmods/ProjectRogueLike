using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{ 
    private Transform enemy;
    // Get au start
    NavMeshAgent navMeshAgent;
    Animator animator;
    GameObject weaponObject;

    [Header("ACTOR INFO")]
    [Range(0, 3)]
    public float SpeedVariationMultiplier = 1;
    public int health = 100;
    [Range(1, 100)]
    public int AttackChance = 5;
    public GameObject CurrentWeapon;
    [Range(0, 5)]
    [Tooltip("Distance to keep between the attacker (self) and his target")]
    public float DistanceTarget = 1;
    public bool isDamaged;

    
    // If no CurrentWeapon 
    [Header("IF NO CURRENT WEAPON")]
    public float attackRange;
    public int damageAmount;
    //[Space]
    
    public float speedOfTheAttack;
    public float reachSpeedAttack;

    [Header("DAMAGE EVENT")]
    // Damage Event
    public Material flashDamage;
    public Material mtlDefault;

    
    private float count = 0;
    private float toCount = 0.25f;

    
    [Header("UPDATED BY THE GAME")]
    [Tooltip("maxhealth is setup directly in the start")]
    public int maxHealth;
    public GameObject HealthBar;
    public GameObject aPotentialTarget;

    private void Start()
    {
        // On change le speed de l'ennemi pour varier un peu les mouvements à l'écran,
        // comme ça des ennemies de même type ne seront pas forcèment coller entre eux
        float newSpeed = Random.Range(1 , SpeedVariationMultiplier);
        transform.GetComponent<NavMeshAgent>().speed = transform.GetComponent<NavMeshAgent>().speed * newSpeed;


        maxHealth = health;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        //animeFront = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        animator = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();


        InitWeapon();
    }
    void InitWeapon()
    {
        if(CurrentWeapon != null)
        {
            weaponObject = Instantiate(CurrentWeapon, transform.position, new Quaternion(0,0,0,0), transform.GetChild(1).GetChild(0));
            weaponObject.transform.localPosition = new Vector2(0, 0);
            weaponObject.transform.localRotation = new Quaternion(0,0,0, 0);

            // On l'attribue le propriétaire de l'arme en attacker dans l'arme pour que les attaques puissent être identifier.
            if(weaponObject.GetComponent<ManagerWeaponCorpAcopr>() != null)
                weaponObject.GetComponent<ManagerWeaponCorpAcopr>().Attacker = gameObject;
            else if(weaponObject.GetComponent<WeaponManager>() != null)
                weaponObject.GetComponent<WeaponManager>().Attacker = gameObject;


        }
        else
        {
           // Debug.Log("Arme crée");
            weaponObject = Instantiate(GameManager.GameUtil.DefaultWeapon, transform.position, new Quaternion(0, 0, 0, 0), transform.GetChild(1).GetChild(0));
            weaponObject.transform.localPosition = new Vector2(0, 0);
            weaponObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            weaponObject.GetComponent<ManagerWeaponCorpAcopr>().Attacker = gameObject;
            weaponObject.GetComponent<ManagerWeaponCorpAcopr>().attackDamage = damageAmount;
            // On l'attribue le propriétaire de l'arme en attacker dans l'arme pour que les attaques puissent être identifier.
            //if (weaponObject.GetComponent<ManagerWeaponCorpAcopr>() != null)

        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (!NeverChangeTarget)  // Si la cible n'est pas forcé, on lance le dectector
        aPotentialTarget = transform.GetChild(0).GetComponent<DetectionTarget>().target;
        MovementBehavior();


        MeleyEnemyMovment();
        checkHealth();


        if (isDamaged)
        {
            if(count < toCount )
            {
                
                count += 0.1f * Time.deltaTime;
    
                if (count <= toCount / 8f && count >= toCount / 10f)
                {
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = flashDamage;
                }
                else if (count <= toCount / 6f && count >= toCount / 8f)
                {
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = mtlDefault;
                }
                else if (count <= toCount / 4f && count >= toCount / 6f)
                {
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = flashDamage;
                }
                else if (count <= toCount / 2f && count >= toCount / 4f)
                {
                    gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = mtlDefault;
                }

            }
            else
            {
                isDamaged = false;
                gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().material = mtlDefault;
                count = 0;
            }
        }
    }
    private void MovementBehavior()
    {
        if (aPotentialTarget == null)
            return;    

        // Fonce attaquez au corps à corps sur sa cible
        if (weaponObject.GetComponent<ManagerWeaponCorpAcopr>() != null)
        {
           navMeshAgent.SetDestination(aPotentialTarget.transform.position);
        }
        // Reste à distance pour attaquer sa cible de loin
        if (weaponObject.GetComponent<WeaponManager>() != null)
        {
           // print(Vector3.Distance(transform.position, aPotentialTarget.transform.position));
            if(Vector3.Distance(transform.position, aPotentialTarget.transform.position) > DistanceTarget)
                navMeshAgent.SetDestination(aPotentialTarget.transform.position);
            
        }
    }
    private void checkHealth()
    {
        if (health <= 0)
        {
            // au cas ou on test nos ennemies sans spawner
            if (gameObject.transform.parent != null &&
                gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<TriggerSalle>() != null)
            {
                TriggerSalle trigSalle = gameObject.transform.parent.parent.parent.parent.gameObject.GetComponent<TriggerSalle>();
                trigSalle.countEnnemie--;
            }
            Destroy(HealthBar);
            Destroy(gameObject);
        }
    }
    private void MeleyEnemyMovment()
    {
        if (aPotentialTarget != null)
        {
            float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
            {
                return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;

            }
            float AttackAngle = AngleBetweenTwoPoints(transform.position, aPotentialTarget.transform.position);

            if (weaponObject.GetComponent<ManagerWeaponCorpAcopr>() != null)
            {
                if (!weaponObject.GetComponent<ManagerWeaponCorpAcopr>().IsFiring)
                    transform.GetChild(1).transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle));
            }
            else
                transform.GetChild(1).transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, AttackAngle));

            ChangeOrderLayerWithAngle(AttackAngle);
            float random = Random.Range(0, 100);

            print(random);
            if (CurrentWeapon == null)
            {
                if (weaponObject.GetComponent<ManagerWeaponCorpAcopr>() != null && random > 100-AttackChance
                    && Vector3.Distance(transform.position, aPotentialTarget.transform.position) < 3)
                {
                    weaponObject.GetComponent<ManagerWeaponCorpAcopr>().Attack(AttackAngle);
                    //PlayAnimation ici;
                }
            }
            else
            {
                
                if (weaponObject.GetComponent<ManagerWeaponCorpAcopr>() != null && random > 100- AttackChance
                    && Vector3.Distance(transform.position, aPotentialTarget.transform.position) < 3  )
                {
                    weaponObject.GetComponent<ManagerWeaponCorpAcopr>().Attack(AttackAngle);

                }

                if (weaponObject.GetComponent<WeaponManager>() != null && random > 100 - AttackChance)
                {
                    weaponObject.GetComponent<WeaponManager>().Attack(gameObject, AttackAngle);
                }
            }

            
                
        }
        /* This function is used when the ennemi have no weapon, 
            - an animation for attack is required (state : 5)
            - damageAmount 
        */
        void Attack()
        {
            
        }

        void ChangeOrderLayerWithAngle(float AttackAngle)
        {
            if ((AttackAngle >= -45 && AttackAngle <= 45) || (AttackAngle >= -135 && AttackAngle <= 45))
            {
                weaponObject.transform.GetComponent<SpriteRenderer>().sortingOrder = 1;
                //animeFront.Play("LeftWalkPlayer");
            }
            else if ((AttackAngle >= 45 && AttackAngle <= 135) || (AttackAngle >= 135 && AttackAngle <= 225))
            {
                weaponObject.transform.GetComponent<SpriteRenderer>().sortingOrder = 3;
                //animeFront.Play("FrontWalkPlayer");7
            }

            if (AttackAngle >= -45 && AttackAngle <= 45)
            {
                PlayAnimation(1);
           
            }
            else if (AttackAngle >= 45 && AttackAngle <= 135)
            {
                PlayAnimation(2);
  
            }
            else if (AttackAngle >= 135 && AttackAngle <= 225)
            {
                PlayAnimation(3);          
            }
            else if (AttackAngle >= -135 && AttackAngle <= 45)
            {
                PlayAnimation( 4);            
            }
        }
        void PlayAnimation(int id)
        {
            if (animator != null)
                animator.SetInteger("state", id);
            else
                Debug.Log("Animation not set for " + gameObject.name);
        }

    }

}
