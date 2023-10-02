using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class EventSystemLevelFinalScript : MonoBehaviour
{
    //Variabile:
    [SerializeField] private EventSystem eventSystem;
    private GameObject lastSelected;

    //Functii predefinite:

    //Start:
    void Start()
    {
        if (lastSelected == null)
        {
            eventSystem = gameObject.GetComponent<EventSystem>();
        }
    }

    //Update:
    void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(lastSelected);
        }
        else
        {
            lastSelected = eventSystem.currentSelectedGameObject;
        }
    }
}
