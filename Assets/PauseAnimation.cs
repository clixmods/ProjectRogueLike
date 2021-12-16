using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] Images;
    public Image imageBg;
    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        imageBg = transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
        time += 0.005f;
        if (time < 0.2f)
            gameObject.GetComponent<Image>().sprite = Images[0];
        else if (time < 0.4f)
            transform.GetComponent<Image>().sprite = Images[1];
        else if (time < 0.6f)
            transform.GetComponent<Image>().sprite = Images[2];
        else if (time < 0.8f)
            transform.GetComponent<Image>().sprite = Images[3];
        else if (time < 1f)
            transform.GetComponent<Image>().sprite = Images[4];
        else
            time = 0;

    }
}
