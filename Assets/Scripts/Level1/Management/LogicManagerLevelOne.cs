using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;



public class LogicManagerLevelOne : MonoBehaviour
{
    //Variabile:
    public GameObject gameOverObject;
    public GameObject pausedObject;
    private CharacterScriptLevel1 character;
    //No Gravity Blocks:
    private NoGravityBlockLevel1Script noGravityBlock1;
    private NoGravityBlockLevel1Script2 noGravityBlock2;
    private NoGravityBlockLevel1Script3 noGravityBlock3;
    private NoGravityBlockLevel1Script4 noGravityBlock4;
    private NoGravityBlockLevel1Script5 noGravityBlock5;
    private NoGravityBlockLevel1Script6 noGravityBlock6;
    private NoGravityBlockLevel1Script7 noGravityBlock7;
    private NoGravityBlockLevel1Script8 noGravityBlock8;
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
            .GetComponent<CharacterScriptLevel1>();

        //No Gravity Blocks:
        noGravityBlock1 = GameObject.FindGameObjectWithTag("NoGravityPlatform")
            .GetComponent<NoGravityBlockLevel1Script>();
        noGravityBlock2 = GameObject.FindGameObjectWithTag("NoGravityPlatform1")
            .GetComponent<NoGravityBlockLevel1Script2>();
        noGravityBlock3 = GameObject.FindGameObjectWithTag("NoGravityPlatform2")
            .GetComponent<NoGravityBlockLevel1Script3>();
        noGravityBlock4 = GameObject.FindGameObjectWithTag("NoGravityPlatform3")
            .GetComponent<NoGravityBlockLevel1Script4>();
        noGravityBlock5 = GameObject.FindGameObjectWithTag("NoGravityPlatform4")
            .GetComponent<NoGravityBlockLevel1Script5>();
        noGravityBlock6 = GameObject.FindGameObjectWithTag("NoGravityPlatform5")
            .GetComponent<NoGravityBlockLevel1Script6>();
        noGravityBlock7 = GameObject.FindGameObjectWithTag("NoGravityPlatform6")
            .GetComponent<NoGravityBlockLevel1Script7>();
        noGravityBlock8 = GameObject.FindGameObjectWithTag("NoGravityPlatform7")
            .GetComponent<NoGravityBlockLevel1Script8>();

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
            )
        {
            startGame = true;
            gameIsPaused = false;
            Time.timeScale = 1;

            pausedObject.SetActive(false);

            character.jump.UnPause();
            character.icon1SE.UnPause();
            character.icon2SE.UnPause();
            character.icon3SE.UnPause();
            character.noIcon.UnPause();
            character.noIcon2.UnPause();
            character.noIcon3.UnPause();
            noGravityBlock1.noGravityBlockSound.UnPause();
            noGravityBlock2.noGravityBlockSound.UnPause();
            noGravityBlock3.noGravityBlockSound.UnPause();
            noGravityBlock4.noGravityBlockSound.UnPause();
            noGravityBlock5.noGravityBlockSound.UnPause();
            noGravityBlock6.noGravityBlockSound.UnPause();
            noGravityBlock7.noGravityBlockSound.UnPause();
            noGravityBlock8.noGravityBlockSound.UnPause();

            level1Music.Play();
            pause.Stop();
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) &&
            startGame == true && gameIsOver == false
            )
        {
            startGame = false;
            gameIsPaused = true;

            Time.timeScale = 0;

            level1Music.Stop();
            pause.Play();

            character.jump.Pause();
            character.icon1SE.Pause();
            character.icon2SE.Pause();
            character.icon3SE.Pause();
            character.noIcon.Pause();
            character.noIcon2.Pause();
            character.noIcon3.Pause();
            noGravityBlock1.noGravityBlockSound.Pause();
            noGravityBlock2.noGravityBlockSound.Pause();
            noGravityBlock3.noGravityBlockSound.Pause();
            noGravityBlock4.noGravityBlockSound.Pause();
            noGravityBlock5.noGravityBlockSound.Pause();
            noGravityBlock6.noGravityBlockSound.Pause();
            noGravityBlock7.noGravityBlockSound.Pause();
            noGravityBlock8.noGravityBlockSound.Pause();

            pausedObject.SetActive(true);
            GameObject.FindGameObjectWithTag("ReplayLevel1").GetComponent<Button>().Select();

            CheckpointRefresh();
        }
    }

    //Functii noi:
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

    public void RestartSceneCollision()
    {
        StopMusic();

        gameOverObject.SetActive(true);
        gameIsOver = true;
    }

    public void SelectReplayLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ReplayLevel1")
            .GetComponent<Button>().colors;

        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ReplayLevel1")
            .GetComponent<Button>().colors = colorBlock;
    }

    public void SelectReplayCheckpointLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
            .GetComponent<Button>().colors;

        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
            .GetComponent<Button>().colors = colorBlock;
    }

    public void SelectExitPausedLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ExitPausedLevel1").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ExitPausedLevel1").GetComponent<Button>().colors = colorBlock;
    }

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

    public void SelectExitGameOverLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ExitGameOverLevel1").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ExitGameOverLevel1").GetComponent<Button>().colors = colorBlock;
    }

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

    //Restart all prefs:
    public void RestartPlayerLevel()
    {
        Debug.Log("Reset player.");

        PlayerPrefs.SetFloat("PozitieX_Checkpoint1", -122.3f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint1", -90f);

        PlayerPrefs.SetInt("PowerUp1_Checkpoint1", 3); 
        PlayerPrefs.SetInt("PowerUp2_Checkpoint1", 3); 
        PlayerPrefs.SetInt("PowerUp3_Checkpoint1", 3);

        PlayerPrefs.SetFloat("PozitieX_Checkpoint2", -1f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint2", -1f);

        PlayerPrefs.SetInt("PowerUp1_Checkpoint2", 3); 
        PlayerPrefs.SetInt("PowerUp2_Checkpoint2", 3); 
        PlayerPrefs.SetInt("PowerUp3_Checkpoint2", 3); 
       
        PlayerPrefs.SetInt("LastCheckpoint", 0); 
    }

    public void RestartPlayerLevelNextScene()
    {
        Debug.Log("Reset player next scene.");

        PlayerPrefs.SetFloat("PozitieX_Checkpoint1", -122.3f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint1", -90f);

        PlayerPrefs.SetInt("PowerUp1_Checkpoint1", 3);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint1", 3);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint1", 3);

        PlayerPrefs.SetFloat("PozitieX_Checkpoint2", -1f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint2", -1f);

        PlayerPrefs.SetInt("PowerUp1_Checkpoint2", 3);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint2", 3);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint2", 3);

        PlayerPrefs.SetInt("LastCheckpoint", 0);
    }

    //Stop all music:
    private void StopMusic()
    {
        character.jump.Stop();
        character.icon1SE.Stop();
        character.icon2SE.Stop();
        character.icon3SE.Stop();
        character.noIcon.Stop();
        character.noIcon2.Stop();
        character.noIcon3.Stop();
        noGravityBlock1.noGravityBlockSound.Stop();
        noGravityBlock2.noGravityBlockSound.Stop();
        noGravityBlock3.noGravityBlockSound.Stop();
        noGravityBlock4.noGravityBlockSound.Stop();
        noGravityBlock5.noGravityBlockSound.Stop();
        noGravityBlock6.noGravityBlockSound.Stop();
        noGravityBlock7.noGravityBlockSound.Stop();
        noGravityBlock8.noGravityBlockSound.Stop();
    }

    //Checkpoint afisat in functie de unde sunt:
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
