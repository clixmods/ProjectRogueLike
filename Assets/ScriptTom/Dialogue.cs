using UnityEngine;

[System.Serializable]    
public class Dialogue 
{
    // variable pour le nom
    public string name;

    // cr�ation d'une array pour encha�ner les diff�rents dialogues 
    // avec des zones de textes avec une certaine taille
    [TextArea(3,10)]
    public string[] sentences;
}
