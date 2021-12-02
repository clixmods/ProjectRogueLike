using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool inRange;

    public Text interactUI;

    

    private void Awake()
    {

        // r�cup�re le premier �l�ment du tableu des gameObjects contenant le tag 
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }
    void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player"))
        {
            // affiche le message en entrant dans le trigger
            inRange = true;
            interactUI.enabled = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 

        if (collision.CompareTag("Player"))
        {
            inRange = false;
            interactUI.enabled = false;

        }
    }

    void TriggerDialogue() 
    {
        DialogueManager.instance.StartDialogue(dialogue);
        
    }
}
