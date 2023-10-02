using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




//Pentru levelul final:
public class FinishLevelFinal : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    private LogicManagerHubArea logicManagerHubArea;
    public GameObject endTransition;
    private bool endOfTransition;
    private GameObject iconNotDone;
    private GameObject iconDone;
    public GameObject finalLevelGate;
    private bool oneTimeCheck;
    public Image iconOn;
    public Image iconOff;

    //Functii predefinite:
    void Start()
    {
        oneTimeCheck = true;

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript4>();

        logicManagerHubArea = GameObject.FindGameObjectWithTag("LogicManagerHubArea")
            .GetComponent<LogicManagerHubArea>();

        iconNotDone = GameObject.FindGameObjectWithTag("LevelFinalNotDone");

        iconDone = GameObject.FindGameObjectWithTag("LevelFinalDone");

        if (PlayerPrefs.GetInt("LevelFinal") == 0)
        {
            iconDone.SetActive(false);
            iconNotDone.SetActive(true);

            iconOn.enabled = false;
            iconOff.enabled = true;
        }
        else if (PlayerPrefs.GetInt("LevelFinal") == 1)
        {
            iconDone.SetActive(true);
            iconNotDone.SetActive(false);

            iconOn.enabled = true;
            iconOff.enabled = false;
        }

        endOfTransition = false;
    }

    void Update()
    {
        if(
            PlayerPrefs.GetInt("Level1") == 1
            && PlayerPrefs.GetInt("Level2") == 1
            && PlayerPrefs.GetInt("Level3") == 1
            && oneTimeCheck == true
            )
        {
            finalLevelGate.SetActive(false);
            oneTimeCheck = false;
        }
    }

    //Functii noi:

    //Pentru a termina jocul;
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

        PlayerPrefs.SetFloat("CharacterPositionX", 874.7f);
        PlayerPrefs.SetFloat("CharacterPositionY", -4.8f);

        MoveToNextLevel(8);
    }
}

