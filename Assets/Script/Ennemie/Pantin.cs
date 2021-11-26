using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantin : MonoBehaviour
{
    Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        Animator = gameObject.transform.parent.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("CorpACorp") || collision.gameObject.layer == LayerMask.NameToLayer("Distance"))
        {
            Animator.SetTrigger("hit");
        }
    }
}
