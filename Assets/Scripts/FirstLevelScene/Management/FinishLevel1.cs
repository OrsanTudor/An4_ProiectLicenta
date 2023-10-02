using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class FinishLevel1 : MonoBehaviour
{
    //Variabile:
    private CharacterScript character;
    private LogicManagerLevel1 logicManagerLevel1;
    public GameObject endTransition;
    private bool endOfTransition;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript>();

        logicManagerLevel1 = GameObject.FindGameObjectWithTag("LogicManagerLevel1")
            .GetComponent<LogicManagerLevel1>();

        endOfTransition = false;
    }

    void Update()
    {
    }

    //Functii noi:

    //Cand face coliziune caracterul cu acest obiect, se termina levelul;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            logicManagerLevel1.levelPass.Play();

            Time.timeScale = 0;

            endTransition.SetActive(true);
            endOfTransition = true;

            float timeLeftTransition = 1f;
            StartCoroutine(DisableSceneTransition(timeLeftTransition));
        }
    }

    //Pentru terminarea levelului:
    public void MoveToNextLevel(int sceneId)
    {
        Time.timeScale = 1;

        character.charState = false;

        SceneManager.LoadScene(sceneId);
    }

    //Disable scena inceput:
    private IEnumerator DisableSceneTransition(float timeLeftTransition)
    {
        Time.timeScale = 1;

        character.charState = false;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        MoveToNextLevel(2);
    }
}

