using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAnimPorte : MonoBehaviour
{
     Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        
        animator.SetBool("CloseDoor", true);
   

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
