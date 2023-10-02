using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class FinishLevel2 : MonoBehaviour
{
    //Variabile:
    private CharacterScript2 character;
    private LogicManagerLevel2 logicManagerLevel2;
    public GameObject endTransition;
    private bool endOfTransition;

    //Functii predefinite:

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript2>();

        logicManagerLevel2 = GameObject.FindGameObjectWithTag("LogicManagerLevel2")
            .GetComponent<LogicManagerLevel2>();

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
            logicManagerLevel2.levelPass.Play();

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
        logicManagerLevel2.RestartPlayerLevelNextScene();

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

        MoveToNextLevel(3);
    }
}

