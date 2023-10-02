using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;



public class LogicManagerLevelFinal : SingletonScript<LogicManagerLevelFinal>
    //MonoBehaviour
{
    //Variabile:
    public GameObject gameOverObject;
    public GameObject pausedObject;
    private CharacterScriptLevelFinal character;
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

    public AudioSource levelFinalMusic;
    public AudioSource levelPass;
    public AudioSource pause;
    public AudioSource buttonPress;

    private PandaScript pandaScript1;
    private PandaScript pandaScript2;
    private PandaScript pandaScript3;
    private PandaScript pandaScript4;
    private PandaScript pandaScript5;
    private PandaScript pandaScript6;
    private PandaScript pandaScript7;
    private PandaScript pandaScript8;

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
        //SelectReplayCheckpointLevel1Button();
        SelectExitPausedLevel1Button();

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevelFinal>();

        pandaScript1 = GameObject.FindGameObjectWithTag("FinalPanda1")
            .GetComponent<PandaScript>();
        pandaScript2 = GameObject.FindGameObjectWithTag("FinalPanda2")
            .GetComponent<PandaScript>();
        pandaScript3 = GameObject.FindGameObjectWithTag("FinalPanda3")
            .GetComponent<PandaScript>();
        pandaScript4 = GameObject.FindGameObjectWithTag("FinalPanda4")
            .GetComponent<PandaScript>();
        pandaScript5 = GameObject.FindGameObjectWithTag("FinalPanda5")
            .GetComponent<PandaScript>();
        pandaScript6 = GameObject.FindGameObjectWithTag("FinalPanda6")
            .GetComponent<PandaScript>();
        pandaScript7 = GameObject.FindGameObjectWithTag("FinalPanda7")
            .GetComponent<PandaScript>();
        pandaScript8 = GameObject.FindGameObjectWithTag("FinalPanda8")
            .GetComponent<PandaScript>();

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
            pandaScript1.pandaNoise1.UnPause();
            pandaScript2.pandaNoise2.UnPause();
            pandaScript3.pandaNoise3.UnPause();
            pandaScript4.pandaNoise4.UnPause();
            pandaScript5.pandaNoise5.UnPause();
            pandaScript6.pandaNoise6.UnPause();
            pandaScript7.pandaNoise7.UnPause();
            pandaScript8.pandaNoise8.UnPause();
            //character.icon1SE.UnPause();
            //character.icon2SE.UnPause();
            //character.icon3SE.UnPause();
            //character.noIcon.UnPause();
            //character.noIcon2.UnPause();
            //character.noIcon3.UnPause();

            levelFinalMusic.Play();
            pause.Stop();
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) &&
            startGame == true && gameIsOver == false
            )
        {
            startGame = false;
            gameIsPaused = true;

            Time.timeScale = 0;

            levelFinalMusic.Stop();
            pause.Play();

            character.jump.Pause();
            pandaScript1.pandaNoise1.Pause();
            pandaScript2.pandaNoise2.Pause();
            pandaScript3.pandaNoise3.Pause();
            pandaScript4.pandaNoise4.Pause();
            pandaScript5.pandaNoise5.Pause();
            pandaScript6.pandaNoise6.Pause();
            pandaScript7.pandaNoise7.Pause();
            pandaScript8.pandaNoise8.Pause();
            //character.icon1SE.Pause();
            //character.icon2SE.Pause();
            //character.icon3SE.Pause();
            //character.noIcon.Pause();
            //character.noIcon2.Pause();
            //character.noIcon3.Pause();

            pausedObject.SetActive(true);
            GameObject.FindGameObjectWithTag("ReplayLevel1").GetComponent<Button>().Select();

            //CheckpointRefresh();
        }
    }

    //Functii noi:

    //Pentru hub area:
    public void MoveToFrontPage(int sceneId)
    {
        StopMusic();

        //RestartPlayerLevel();

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

        //RestartPlayerLevel();

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

    //Pentru restart on collision cu death object:
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

    //public void SelectReplayCheckpointLevel1Button()
    //{
    //    colorBlock = GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
    //        .GetComponent<Button>().colors;

    //    colorBlock.selectedColor = Color.yellow;
    //    GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
    //        .GetComponent<Button>().colors = colorBlock;
    //}

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

    //Disable start transition:
    private IEnumerator DisableSceneTransitionStart(float timeLeftTransition)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        startTransition.SetActive(false);
        startOfTransition = false;
    }

    //Disable end transition:
    private IEnumerator DisableSceneTransitionEnd(float timeLeftTransition, int sceneId)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        SceneManager.LoadScene(sceneId);
    }

    //Puterile resetate la restart:
    //public void RestartPlayerLevel()
    //{
    //    Debug.Log("Reset player.");

    //    PlayerPrefs.SetFloat("PozitieX_Checkpoint1", -122.3f);
    //    PlayerPrefs.SetFloat("PozitieY_Checkpoint1", -90f);

    //    PlayerPrefs.SetInt("PowerUp1_Checkpoint1", 3);
    //    PlayerPrefs.SetInt("PowerUp2_Checkpoint1", 3);
    //    PlayerPrefs.SetInt("PowerUp3_Checkpoint1", 3);

    //    PlayerPrefs.SetFloat("PozitieX_Checkpoint2", -1f);
    //    PlayerPrefs.SetFloat("PozitieY_Checkpoint2", -1f);

    //    PlayerPrefs.SetInt("PowerUp1_Checkpoint2", 3);
    //    PlayerPrefs.SetInt("PowerUp2_Checkpoint2", 3);
    //    PlayerPrefs.SetInt("PowerUp3_Checkpoint2", 3);

    //    PlayerPrefs.SetInt("LastCheckpoint", 0);
    //}

    //Puterile pentru urmatoarea scena:
    //public void RestartPlayerLevelNextScene()
    //{
    //    Debug.Log("Reset player next scene.");

    //    PlayerPrefs.SetFloat("PozitieX_Checkpoint1", -122.3f);
    //    PlayerPrefs.SetFloat("PozitieY_Checkpoint1", -90f);

    //    PlayerPrefs.SetInt("PowerUp1_Checkpoint1", 3);
    //    PlayerPrefs.SetInt("PowerUp2_Checkpoint1", 3);
    //    PlayerPrefs.SetInt("PowerUp3_Checkpoint1", 3);

    //    PlayerPrefs.SetFloat("PozitieX_Checkpoint2", -1f);
    //    PlayerPrefs.SetFloat("PozitieY_Checkpoint2", -1f);

    //    PlayerPrefs.SetInt("PowerUp1_Checkpoint2", 3);
    //    PlayerPrefs.SetInt("PowerUp2_Checkpoint2", 3);
    //    PlayerPrefs.SetInt("PowerUp3_Checkpoint2", 3);

    //    PlayerPrefs.SetInt("LastCheckpoint", 0);
    //}

    //Stop music:
    private void StopMusic()
    {
        character.jump.Stop();
        pandaScript1.pandaNoise1.Stop();
        pandaScript2.pandaNoise2.Stop();
        pandaScript3.pandaNoise3.Stop();
        pandaScript4.pandaNoise4.Stop();
        pandaScript5.pandaNoise5.Stop();
        pandaScript6.pandaNoise6.Stop();
        pandaScript7.pandaNoise7.Stop();
        pandaScript8.pandaNoise8.Stop();
        //character.icon1SE.Stop();
        //character.icon2SE.Stop();
        //character.icon3SE.Stop();
        //character.noIcon.Stop();
        //character.noIcon2.Stop();
        //character.noIcon3.Stop();
    }

    //Pentru afisarea butonului de checkpoint:
    //private void CheckpointRefresh()
    //{
    //    int lastCheckpointHit = PlayerPrefs.GetInt("LastCheckpoint");

    //    Button replayCheckpointButton = GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
    //        .GetComponent<Button>();

    //    string newText = "Restart Checkpoint (";

    //    if (lastCheckpointHit == 0)
    //    {
    //        newText = newText + "0)";
    //    }
    //    else if (lastCheckpointHit == 1)
    //    {
    //        newText = newText + "1)";
    //    }
    //    else if (lastCheckpointHit == 2)
    //    {
    //        newText = newText + "2)";
    //    }

    //    replayCheckpointButton.GetComponentInChildren<Text>().text = newText;
    //}
}
