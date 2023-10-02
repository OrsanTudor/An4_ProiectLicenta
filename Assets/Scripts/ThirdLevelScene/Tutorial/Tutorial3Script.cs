using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Tutorial3Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript3 character;
    private LogicManagerLevel3 logicManagerLevel3;
    public GameObject tutorial;
    private bool oneTimeTutorial;
    public bool showingTutorial;

    //Functii predefinite:
    void Start()
    {
        logicManagerLevel3 = GameObject.FindGameObjectWithTag("LogicManagerLevel3")
            .GetComponent<LogicManagerLevel3>();

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript3>();

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

            logicManagerLevel3.buttonPress.Play();

            Time.timeScale = 0;

            tutorial.SetActive(true);
            showingTutorial = true;
        }
    }
}
