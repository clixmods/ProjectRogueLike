using UnityEngine;

[System.Serializable]    
public class Dialogue 
{
    // variable pour le nom
    public string name;

    // création d'une array pour enchaîner les différents dialogues 
    // avec des zones de textes avec une certaine taille
    [TextArea(3,10)]
    public string[] sentences;
}
