using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTarget : MonoBehaviour
{
    public GameObject target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("OnCollisionEnter");
        if (collision.gameObject.CompareTag("Player"))
        {
            //transform.parent.GetComponent<SphereCollider>().enabled = false;
            target = collision.gameObject;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            //transform.parent.GetComponent<SphereCollider>().enabled = true;
            target = null;
        }

    }

}
