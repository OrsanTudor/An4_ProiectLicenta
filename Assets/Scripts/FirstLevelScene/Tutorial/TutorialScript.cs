using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TutorialScript : MonoBehaviour
{
    //Variabile:
    private CharacterScript character;
    private LogicManagerLevel1 logicManagerLevel1;
    public GameObject tutorial;
    private bool oneTimeTutorial;
    public bool showingTutorial;

    //Functii predefinite:
    void Start()
    {
        logicManagerLevel1 = GameObject.FindGameObjectWithTag("LogicManagerLevel1")
            .GetComponent<LogicManagerLevel1>();

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript>();

        oneTimeTutorial = true;
        showingTutorial = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && character.charState == true)
        {
            tutorial.SetActive(false);
            showingTutorial = false;

            Time.timeScale = 1;
        }
    }

    //Functii noi:


    //Cand face coliziune caracterul cu acest obiect, arata tutorialul:

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character")
            && oneTimeTutorial == true)
        {
            oneTimeTutorial = false;
            logicManagerLevel1.buttonPress.Play();

            Time.timeScale = 0;
            tutorial.SetActive(true);
            showingTutorial = true;
        }
    }
}
