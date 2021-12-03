using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public bool fin;

    //variable ressemblant � une liste ou une array
    private Queue<string> sentences;

    public static DialogueManager instance;
    private void Awake() //Singleton
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de DialogueManager dans la sc�ne");
            return;
        }

        instance = this;

        //Initialisation de la varaible
        sentences = new Queue<string>();
    }

    // peut �tre utiliser par plusieurs PNJ
    public void StartDialogue(Dialogue dialogue)
    {
        // r�cup�re l'animation dans l'animator
        animator.SetBool("OpenDialogue", true);

        // permet d'afficher le nom lors de l'ouverture de la bo�te de dialogue
        nameText.text = dialogue.name;

        // permet de vider la liste d'attente  
        sentences.Clear();

        // ajoute toutes les phrases du PNJ 
        foreach (string sentence in dialogue.sentences)
        {
            // permet de rentrer dans la file d'attente 
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // �vite l'erreur en fin de dialogue
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // permet de r�cup�rer le prochain �l�ment de la file d'attente 
        string firstSentence = sentences.Dequeue();

        // permet de stopper toutes les coroutines de ce script 
        StopAllCoroutines();
        StartCoroutine(TypeSentence(firstSentence));
    }

    // permet d'afficher les charat�res un par un 
    IEnumerator TypeSentence(string sentence)
    {
        // a la base, la case sera vide
        dialogueText.text = "";

        // cela va d�couper la phrase en array
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            // ajoute une pause courte entre chaque character
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("OpenDialogue", false);
        Debug.Log("fin du dialogue");
        fin = true;
    }
    

}
