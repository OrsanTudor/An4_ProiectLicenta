using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class FinishLevel3 : MonoBehaviour
{
    //Variabile:
    private CharacterScript3 character;
    private LogicManagerLevel3 logicManagerLevel3;
    public GameObject endTransition;
    private bool endOfTransition;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript3>();

        logicManagerLevel3 = GameObject.FindGameObjectWithTag("LogicManagerLevel3")
            .GetComponent<LogicManagerLevel3>();

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
            logicManagerLevel3.levelPass.Play();

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
        logicManagerLevel3.RestartPlayerLevelNextScene();

        if(PlayerPrefs.GetInt("LevelTutorial") == 0)
        {
            PlayerPrefs.SetInt("LevelTutorial", 1);
            int numberOfFinishedLevels = PlayerPrefs.GetInt("NumberFinishedLevels");
            PlayerPrefs.SetInt("NumberFinishedLevels", numberOfFinishedLevels + 1);
        }

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

        MoveToNextLevel(4);
    }
}

