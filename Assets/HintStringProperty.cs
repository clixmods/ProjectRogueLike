using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintStringProperty : MonoBehaviour
{
    public GameObject relatedObject;

    public float MinDistance = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        print(relatedObject == null);
        var isMissing = ReferenceEquals(relatedObject, null);
        if (relatedObject == null && isMissing) 
        {
            Debug.Log("Hintstring destroy because the related gameObject is killed");
            Destroy(gameObject);
        }
    }
}
