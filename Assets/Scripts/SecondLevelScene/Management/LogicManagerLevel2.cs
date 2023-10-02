using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;



public class LogicManagerLevel2 : MonoBehaviour
{
    //Variabile:
    public GameObject gameOverObject;
    public GameObject pausedObject;
    private CharacterScript2 character;
    private JumpPadScript jumpPad;
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
    private Tutorial2Script tutorial2Script;
    //Sound effects:
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
        SelectReplayCheckpointLevel1Button();
        SelectExitPausedLevel1Button();

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript2>();

        tutorial2Script = GameObject.FindGameObjectWithTag("TutorialLevel1")
            .GetComponent<Tutorial2Script>();

        jumpPad = GameObject.FindGameObjectWithTag("JumpPad")
            .GetComponent<JumpPadScript>();

        pausedObject.SetActive(false);

        oneTimeGameOverSelect = false;
        oneTimeDeath = false;

        endOfTransition = false;
    }

    //Update:
    void Update()
    {
        if (startOfTransition == true
            || endOfTransition == true)
        {
            return;
        }

        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) &&
            startGame == false && gameIsOver == false
            && tutorial2Script.showingTutorial == false)
        {
            startGame = true;
            gameIsPaused = false;
            Time.timeScale = 1;

            pausedObject.SetActive(false);

            character.dashRL.UnPause();
            character.jump.UnPause();
            character.noGravity.UnPause();
            jumpPad.jumpPadSound.UnPause();
            character.noIcon.UnPause();
            character.noIcon2.UnPause();

            level1Music.Play();
            pause.Stop();
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) &&
            startGame == true && gameIsOver == false
            && tutorial2Script.showingTutorial == false)
        {
            startGame = false;
            gameIsPaused = true;

            Time.timeScale = 0;

            level1Music.Stop();
            pause.Play();

            character.dashRL.Pause();
            character.jump.Pause();
            character.noGravity.Pause();
            jumpPad.jumpPadSound.Pause();
            character.noIcon.Pause();
            character.noIcon2.Pause();

            pausedObject.SetActive(true);
            GameObject.FindGameObjectWithTag("ReplayLevel1").GetComponent<Button>().Select();

            CheckpointRefresh();
        }
    }

    //Functii noi:

    //Move to front page:
    public void MoveToFrontPage(int sceneId)
    {
        StopMusic();

        RestartPlayerLevel();

        buttonPress.Play();

        Time.timeScale = 0;

        endTransition.SetActive(true);
        endOfTransition = true;

        float timeLeftTransition = 1f;
        StartCoroutine(DisableSceneTransitionEnd(timeLeftTransition, sceneId));
    }

    //Restart scene paused:
    public void RestartScene()
    {
        StopMusic();

        RestartPlayerLevel();

        buttonPress.Play();

        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Restart scene game over:
    public void RestartSceneGameOver()
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

    //Select Replay Checkpoint button:
    public void SelectReplayCheckpointLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
            .GetComponent<Button>().colors;

        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
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

    private IEnumerator DisableSceneTransitionEnd(float timeLeftTransition, int sceneId)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        SceneManager.LoadScene(sceneId);
    }
    
    //Restart Player Prefs:
    public void RestartPlayerLevel()
    {
        Debug.Log("Reset player.");

        PlayerPrefs.SetFloat("PozitieX_Checkpoint1", -122.3f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint1", -90f);
        PlayerPrefs.SetInt("PowerUp1_Checkpoint1", 2);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint1", 1);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint1", 2);

        PlayerPrefs.SetFloat("PozitieX_Checkpoint2", -122.3f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint2", -90f);
        PlayerPrefs.SetInt("PowerUp1_Checkpoint2", 2);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint2", 1);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint2", 2);
        PlayerPrefs.SetInt("LastCheckpoint", 0);
    }

    //Reset Player Prefs:
    public void RestartPlayerLevelNextScene()
    {
        Debug.Log("Reset player next scene.");

        PlayerPrefs.SetFloat("PozitieX_Checkpoint1", -122.3f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint1", -90f);
        PlayerPrefs.SetInt("PowerUp1_Checkpoint1", 2); 
        PlayerPrefs.SetInt("PowerUp2_Checkpoint1", 1);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint1", 2);

        PlayerPrefs.SetFloat("PozitieX_Checkpoint2", -122.3f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint2", -90f);
        PlayerPrefs.SetInt("PowerUp1_Checkpoint2", 2);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint2", 1); 
        PlayerPrefs.SetInt("PowerUp3_Checkpoint2", 2);
        PlayerPrefs.SetInt("LastCheckpoint", 0);
    }

    private void StopMusic()
    {
        character.dashRL.Stop();
        character.jump.Stop();
        character.noGravity.Stop();
        jumpPad.jumpPadSound.Stop();
        character.noIcon.Stop();
        character.noIcon2.Stop();
    }

    //Pentru cand pui textul de ce checkpoint sa fie:
    private void CheckpointRefresh()
    {
        int lastCheckpointHit = PlayerPrefs.GetInt("LastCheckpoint"); 

        Button replayCheckpointButton = GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
            .GetComponent<Button>();

        string newText = "Restart Checkpoint (";

        if (lastCheckpointHit == 0)
        {
            newText = newText + "0)";
        }
        else if (lastCheckpointHit == 1)
        {
            newText = newText + "1)";
        }
        else if (lastCheckpointHit == 2)
        {
            newText = newText + "2)";
        }

        replayCheckpointButton.GetComponentInChildren<Text>().text = newText;
    }
}
