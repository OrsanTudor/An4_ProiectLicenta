using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class FinishLevelOne : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel1 character;
    private LogicManagerLevelOne logicManagerLevelOne;
    public GameObject endTransition;
    private bool endOfTransition;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel1>();

        logicManagerLevelOne = GameObject.FindGameObjectWithTag("LogicManagerLevelOne")
            .GetComponent<LogicManagerLevelOne>();

        endOfTransition = false;
    }

    void Update()
    {
    }

    //Functii noi:

    //Terminare level atunci cand atingi trigger:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            logicManagerLevelOne.levelPass.Play();
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
        logicManagerLevelOne.RestartPlayerLevelNextScene();

        if (PlayerPrefs.GetInt("Level1") == 0)
        {
            PlayerPrefs.SetInt("Level1", 1);
            int numberOfFinishedLevels = PlayerPrefs.GetInt("NumberFinishedLevels");
            PlayerPrefs.SetInt("NumberFinishedLevels", numberOfFinishedLevels + 1);
        }

        Time.timeScale = 1;

        character.charState = false;

        SceneManager.LoadScene(sceneId);
    }

    //Pentru tranzitie:
    private IEnumerator DisableSceneTransition(float timeLeftTransition)
    {
        Time.timeScale = 1;

        character.charState = false;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        MoveToNextLevel(4);
    }
}

