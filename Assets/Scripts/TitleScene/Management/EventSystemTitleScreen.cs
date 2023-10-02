using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class EventSystemTitleScreen : MonoBehaviour
{
    //Variabile:
    //Last selected button:
    [SerializeField] private EventSystem eventSystem;
    private GameObject lastSelectedButton;

    //Functii prestabilite:

    // Start:
    void Start()
    {
        if (lastSelectedButton == null)
        {
            eventSystem = gameObject.GetComponent<EventSystem>();
        }
    }

    // Update:
    void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(lastSelectedButton);
        }
        else
        {
            lastSelectedButton = eventSystem.currentSelectedGameObject;
        }
    }
}
