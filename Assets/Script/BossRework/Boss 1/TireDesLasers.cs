using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireDesLasers : MonoBehaviour
{
    public LineRenderer lineRende;
    public GameObject player;
    Vector3 pos;

    public Transform OrigineLaser;

    bool chekcMur;
    Vector2 dir;
    Vector2 dirToAdd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!chekcMur)
        {
            dir = VersAvecMur();
        }
        else 
        {

            CreationDURayLaser(dir);
        }
    }

    Vector2 VersAvecMur()
    {
        Vector2 distmur = new Vector2(0,0);
        Vector2 dir =   player.transform.position - OrigineLaser.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll(OrigineLaser.position, dir, 20);
        Debug.DrawRay(transform.position, dir * 20, Color.red);
        for (int i =0; i< hit.Length; i++)
        {
            if (hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                distmur = hit[i].transform.position;
            }
        }
       
        distmur -= new Vector2(OrigineLaser.position.x, OrigineLaser.position.y);
        return distmur;
        
    }


    void CreationDURayLaser (Vector2  direction)
    {
       
        RaycastHit2D[] hit = Physics2D.RaycastAll(OrigineLaser.position,direction, 20);
        Debug.DrawRay(transform.position, VersAvecMur() * 20, Color.blue);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //Dégat;
            }
        }
    }
}
