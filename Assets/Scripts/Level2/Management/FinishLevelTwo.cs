using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class FinishLevelTwo : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    private LogicManagerLevelTwo logicManagerLevelTwo;
    public GameObject endTransition;
    private bool endOfTransition;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        logicManagerLevelTwo = GameObject.FindGameObjectWithTag("LogicManagerLevelTwo")
            .GetComponent<LogicManagerLevelTwo>();

        endOfTransition = false;
    }

    void Update()
    {
    }

    //Functii noi:

    //Pentru finalizare level:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            logicManagerLevelTwo.levelPass.Play();
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
        logicManagerLevelTwo.RestartPlayerLevelNextScene();

        if (PlayerPrefs.GetInt("Level2") == 0)
        {
            PlayerPrefs.SetInt("Level2", 1);
            int numberOfFinishedLevels = PlayerPrefs.GetInt("NumberFinishedLevels");
            PlayerPrefs.SetInt("NumberFinishedLevels", numberOfFinishedLevels + 1);
        }

        Time.timeScale = 1;

        character.charState = false;

        SceneManager.LoadScene(sceneId);
    }

    //Pentru terminare tranzitie:
    private IEnumerator DisableSceneTransition(float timeLeftTransition)
    {
        Time.timeScale = 1;

        character.charState = false;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        //To Hub Area!
        MoveToNextLevel(4);
    }
}

