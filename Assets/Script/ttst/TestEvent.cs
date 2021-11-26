using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEvent : MonoBehaviour
{
    public UnityEvent OnEventDone;
    //public GameEvent onEvent;

    // Start is called before the first frame update
    void Start()
    {
        OnEventDone.AddListener(() => Coucou());
        OnEventDone.AddListener(() => Coucou2());

    }

    // Update is called once per frame
    void Update()
    {
        OnEventDone?.Invoke();
    }

    public void Coucou()
    {
        Debug.Log("coucou");
    }

    public void Coucou2()
    {

        Debug.Log("coucou2");
    }
}
