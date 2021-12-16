using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfBoss : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
          collision.gameObject.GetComponent<PlayerControler>().health -= damage; // TODO damage à defs
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(gameObject);
        }


    }
}
