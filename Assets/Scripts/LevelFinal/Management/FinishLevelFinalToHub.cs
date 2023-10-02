using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class FinishLevelFinalToHub : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevelFinal character;
    private LogicManagerLevelFinal logicManagerLevelFinal;
    public GameObject endTransition;
    private bool endOfTransition;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevelFinal>();

        logicManagerLevelFinal = GameObject.FindGameObjectWithTag("LogicManagerLevelFinal")
            .GetComponent<LogicManagerLevelFinal>();

        endOfTransition = false;
    }

    void Update()
    {
    }

    //Functii noi:

    //Atunci cand atingi triggerul, termini levelul;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            logicManagerLevelFinal.levelPass.Play();
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
        //logicManagerLevelFinal.RestartPlayerLevelNextScene();

        if (PlayerPrefs.GetInt("LevelFinal") == 0)
        {
            PlayerPrefs.SetInt("LevelFinal", 1);
            int numberOfFinishedLevels = PlayerPrefs.GetInt("NumberFinishedLevels");
            PlayerPrefs.SetInt("NumberFinishedLevels", numberOfFinishedLevels + 1);
        }

        Time.timeScale = 1;

        character.charState = false;

        SceneManager.LoadScene(sceneId);
    }

    //Pentru tranzitii:
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

