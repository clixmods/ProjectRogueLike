using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool inRange;

    public Text interactUI;

    bool check;

    public LevelManagerTuto levelManager;

    public bool tuto;

    public GameObject portail1;
    public GameObject portail2;
    

    private void Awake()
    {

        // récupère le premier élément du tableu des gameObjects contenant le tag 
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }
    void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.P) && !check)
        {
            TriggerDialogue();
            check = true;
        }
        if(check && Input.GetKeyDown(KeyCode.P))
        {
            DialogueManager.instance.DisplayNextSentence();
        }
        if (!tuto)
        {
            if (DialogueManager.instance.fin && !levelManager.parlerAuPnj)
            {
                levelManager.parlerAuPnj = true;
                DialogueManager.instance.fin = false;
                Destroy(gameObject);
            }
            if (DialogueManager.instance.fin && levelManager.parlerAuPnj && levelManager.etape1)
            {
                levelManager.etape3 = true;
                Destroy(gameObject);

            }
        }
        if(tuto)
        {
            if (DialogueManager.instance.fin)
            {
                portail1.SetActive(true);
                portail2.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player") && !check)
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
