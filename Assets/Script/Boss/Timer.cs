using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float tim;
    public float timeToReach;
    public bool verfi;

    // Start is called before the first frame update
    void Start()
    {
        tim = 0;
        timeToReach = 0.5f;
        verfi = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (verfi)
        {
            Timers();
        }
    }

    public void Timers ()
    {
        print("yo");
        if (tim < timeToReach)
        {
            print("cc");
            tim = tim + Time.deltaTime;
            
            
        }
        else
        {
            print("gesgesg");
            
            gameObject.GetComponent<Boss>().timer = true;
            print(gameObject.GetComponent<Boss>().timer);
            tim = 0;
            verfi = false;
        }
    
    }
}
