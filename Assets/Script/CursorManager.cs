using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Sprite cursorIcon;
    public Collider2D target;

    [Header("Cursor Panels")]
    public Sprite CursorChest;
    public Sprite CursorNormal;
    public Sprite CursorAttackMelee;
    public Sprite CursorAttackDistance;
    public Sprite CursorAttackNull;
    private SpriteRenderer CursorRenderer;
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision;
        
        Debug.Log(collision.name + " enter");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == target)
        {
            target = null;
        }
        

        Debug.Log(collision.name + " exit");
    }

    // Start is called before the first frame update
    void Start()
    {
        CursorRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            switch (target.tag)
            {
                case "Player":
                    CursorRenderer.sprite = CursorAttackDistance;
                    break;
                case "Ennemies":

                    CursorRenderer.sprite = CursorAttackMelee;
                    break;
                case "Finish":
                    CursorRenderer.sprite = CursorChest;
                    break;
                default:
                    CursorRenderer.sprite = CursorNormal;
                    break;
            }
        }
        else
        {
            CursorRenderer.sprite = CursorNormal;
        }
        

    }
}
