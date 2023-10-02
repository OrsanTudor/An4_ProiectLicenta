using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Tutorial2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript2 character;
    private LogicManagerLevel2 logicManagerLevel2;
    public GameObject tutorial;
    private bool oneTimeTutorial;
    public bool showingTutorial;

    //Functii predefinite:
    void Start()
    {
        logicManagerLevel2 = GameObject.FindGameObjectWithTag("LogicManagerLevel2")
            .GetComponent<LogicManagerLevel2>();

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript2>();

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

            logicManagerLevel2.buttonPress.Play();

            Time.timeScale = 0;

            tutorial.SetActive(true);
            showingTutorial = true;
        }
    }
}
