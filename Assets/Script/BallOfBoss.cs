using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfBoss : MonoBehaviour
{
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
        if (collision.gameObject.GetComponent<PlayerControler>() != null)
        {
            collision.gameObject.GetComponent<PlayerControler>().health -= 10; // TODO damage à defs
            Destroy(gameObject);
        }


    }
}
