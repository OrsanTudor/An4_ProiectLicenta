using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




//Pentru levelul de tutorial:
public class FinishLevel4 : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    private LogicManagerHubArea logicManagerHubArea;
    public GameObject endTransition;
    private bool endOfTransition;
    private GameObject iconNotDone;
    private GameObject iconDone;
    private GameObject tutorialLevelGate;
    public Image iconOn;
    public Image iconOff;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript4>();

        logicManagerHubArea = GameObject.FindGameObjectWithTag("LogicManagerHubArea")
            .GetComponent<LogicManagerHubArea>();

        iconNotDone = GameObject.FindGameObjectWithTag("LevelTutorialNotDone");

        iconDone = GameObject.FindGameObjectWithTag("LevelTutorialDone");

        tutorialLevelGate = GameObject.FindGameObjectWithTag("TutorialLevelGate");

        if (PlayerPrefs.GetInt("LevelTutorial") == 0)
        {
            iconDone.SetActive(false);
            iconNotDone.SetActive(true);

            iconOn.enabled = false;
            iconOff.enabled = true;

            tutorialLevelGate.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("LevelTutorial") == 1)
        {
            iconDone.SetActive(true);
            iconNotDone.SetActive(false);

            iconOn.enabled = true;
            iconOff.enabled = false;

            tutorialLevelGate.SetActive(false);
        }

        endOfTransition = false;
    }

    void Update()
    {
    }

    //Functii noi:

    //Cand face coliziune caracterul cu acest obiect, se merge la alt level;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            logicManagerHubArea.levelPass.Play();
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
        logicManagerHubArea.RestartPlayerLevelNextScene();
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

        PlayerPrefs.SetFloat("CharacterPositionX", -127.5f);
        PlayerPrefs.SetFloat("CharacterPositionY", 85);

        //Alegere:
        MoveToNextLevel(1);
        //MoveToNextLevel(2);
        //MoveToNextLevel(3);
    }
}

