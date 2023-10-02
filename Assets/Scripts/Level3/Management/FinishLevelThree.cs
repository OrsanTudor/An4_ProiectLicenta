using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class FinishLevelThree : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel3 character;
    private LogicManagerLevelThree logicManagerLevelThree;
    public GameObject endTransition;
    private bool endOfTransition;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel3>();

        logicManagerLevelThree = GameObject.FindGameObjectWithTag("LogicManagerLevelThree")
            .GetComponent<LogicManagerLevelThree>();

        endOfTransition = false;
    }

    void Update()
    {
    }

    //Functii noi:

    //Pentru cand termini levelul:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            logicManagerLevelThree.levelPass.Play();
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
        logicManagerLevelThree.RestartPlayerLevelNextScene();

        if (PlayerPrefs.GetInt("Level3") == 0)
        {
            PlayerPrefs.SetInt("Level3", 1);
            int numberOfFinishedLevels = PlayerPrefs.GetInt("NumberFinishedLevels");
            PlayerPrefs.SetInt("NumberFinishedLevels", numberOfFinishedLevels + 1);
        }

        Time.timeScale = 1;

        character.charState = false;

        SceneManager.LoadScene(sceneId);
    }

    //Pentru scena de la final:
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

