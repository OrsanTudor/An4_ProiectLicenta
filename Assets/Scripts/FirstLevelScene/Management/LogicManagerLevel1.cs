using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;



public class LogicManagerLevel1 : MonoBehaviour
{
    //Variabile:
    public GameObject gameOverObject;
    public GameObject pausedObject;
    private CharacterScript character;
    private ColorBlock colorBlock;
    public bool oneTimeDeath;
    public bool startGame;
    private bool gameIsOver;
    public bool gameIsPaused;
    public bool oneTimeGameOverSelect;
    public GameObject startTransition;
    public GameObject endTransition;
    public bool startOfTransition;
    public bool endOfTransition;
    private TutorialScript tutorialScript;
    public AudioSource level1Music;
    public AudioSource levelPass;
    public AudioSource pause;
    public AudioSource buttonPress;


    //Functii predefinite:
    //Start:
    void Start()
    {
        startGame = true;
        Time.timeScale = 0;

        startTransition.SetActive(true);
        startOfTransition = true;

        float timeLeftTransition = 1f;
        StartCoroutine(DisableSceneTransitionStart(timeLeftTransition));

        gameIsPaused = false;

        pausedObject.SetActive(true);
        gameIsOver = false;

        GameObject.FindGameObjectWithTag("ReplayLevel1")
            .GetComponent<Button>().Select();
        SelectReplayLevel1Button();
        SelectExitPausedLevel1Button();

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript>();

        tutorialScript = GameObject.FindGameObjectWithTag("TutorialLevel1")
            .GetComponent<TutorialScript>();

        pausedObject.SetActive(false);

        oneTimeGameOverSelect = false;
        oneTimeDeath = false;

        endOfTransition = false;
    }

    //Update:
    void Update()
    {
        if(startOfTransition == true
            || endOfTransition == true)
        {
            return;
        }

        //Pentru not pauza:
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) &&
            startGame == false && gameIsOver == false
            && tutorialScript.showingTutorial == false)
        {
            startGame = true;
            gameIsPaused = false;
            Time.timeScale = 1;

            pausedObject.SetActive(false);

            character.dashRL.UnPause();
            character.jump.UnPause();
            character.noGravity.UnPause();
            character.noIcon.UnPause();

            //Muzica:
            level1Music.Play();
            pause.Stop();
        }
        //Pause:
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) &&
            startGame == true && gameIsOver == false
            && tutorialScript.showingTutorial == false)
        {
            startGame = false;
            gameIsPaused = true;
            Time.timeScale = 0;

            level1Music.Stop();
            pause.Play();

            character.dashRL.Pause();
            character.jump.Pause();
            character.noGravity.Pause();
            character.noIcon.Pause();

            pausedObject.SetActive(true);
            GameObject.FindGameObjectWithTag("ReplayLevel1").GetComponent<Button>().Select();
        }
    }

    //Functii noi:

    //Inapoi la hub area (nu front page)
    public void MoveToFrontPage(int sceneId)
    {
        StopMusic();

        buttonPress.Play();

        Time.timeScale = 0;

        endTransition.SetActive(true);
        endOfTransition = true;

        float timeLeftTransition = 1f;
        StartCoroutine(DisableSceneTransitionEnd(timeLeftTransition, sceneId));
    }

    //Restart scene:
    public void RestartScene()
    {
        StopMusic();

        buttonPress.Play();

        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Restart if dead:
    public void RestartSceneCollision()
    {
        StopMusic();

        gameOverObject.SetActive(true);
        gameIsOver = true;
    }

    //Select Replay button:
    public void SelectReplayLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ReplayLevel1")
            .GetComponent<Button>().colors;

        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ReplayLevel1")
            .GetComponent<Button>().colors = colorBlock;
    }

    //Select Exit from Paused Button:
    public void SelectExitPausedLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ExitPausedLevel1").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ExitPausedLevel1").GetComponent<Button>().colors = colorBlock;
    }

    //Select Try Again button:
    public void SelectTryAgainLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("TryAgainLevel1")
            .GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.yellow;

        GameObject.FindGameObjectWithTag("TryAgainLevel1")
            .GetComponent<Button>().colors = colorBlock;
        if (oneTimeGameOverSelect == false)
        {
            oneTimeGameOverSelect = true;
            GameObject.FindGameObjectWithTag("TryAgainLevel1")
                .GetComponent<Button>().Select();
        }
    }

    //Select Exit from Game Over Button:
    public void SelectExitGameOverLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ExitGameOverLevel1").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ExitGameOverLevel1").GetComponent<Button>().colors = colorBlock;
    }

    //Disable scena start:
    private IEnumerator DisableSceneTransitionStart(float timeLeftTransition)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        startTransition.SetActive(false);
        startOfTransition = false;
    }

    //Disable scena end:
    private IEnumerator DisableSceneTransitionEnd(float timeLeftTransition, int sceneId)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        SceneManager.LoadScene(sceneId);
    }

    //Stop music:
    private void StopMusic()
    {
        character.dashRL.Stop();
        character.jump.Stop();
        character.noGravity.Stop();
        character.noIcon.Stop();
    }
}
