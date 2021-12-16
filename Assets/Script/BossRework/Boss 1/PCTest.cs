using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCTest : MonoBehaviour
{
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            gameObject.transform.Translate(Vector2.up * Time.deltaTime * speed) ;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            gameObject.transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }
}
