using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public float count = 0;
    public float ToCount = 2;
    public bool isDamaged = false;
    public bool isDamagedGo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
                if(transform.GetChild(1).localScale.x > transform.GetChild(2).localScale.x)
                {
                    Vector3 tempScale = transform.GetChild(1).localScale;
                    tempScale.x -= Time.deltaTime;
                    transform.GetChild(1).localScale = tempScale;
                }
                if (transform.GetChild(1).localScale.x < transform.GetChild(2).localScale.x)
                {
                    Vector3 tempScale = transform.GetChild(1).localScale;
                    tempScale.x += Time.deltaTime;
                    transform.GetChild(1).localScale = tempScale;
                }
                else
                {
                    count = 0;
                    //isDamagedGo = false;
                }
       
    }
}
