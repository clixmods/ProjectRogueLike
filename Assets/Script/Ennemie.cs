using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemie : MonoBehaviour
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
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            TriggerSalle trigSalle = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<TriggerSalle>();
            trigSalle.countEnnemie--;
            Destroy(gameObject);
        }
    }
}
