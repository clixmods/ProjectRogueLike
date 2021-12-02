using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintStringProperty : MonoBehaviour
{
    public GameObject relatedObject; 
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (relatedObject == null)
        {
            Debug.Log("Hintstring destroy because the related gameOject is killed");
            Destroy(gameObject);
        }
    }
}
