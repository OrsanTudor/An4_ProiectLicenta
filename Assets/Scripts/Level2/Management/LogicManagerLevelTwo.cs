using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;



public class LogicManagerLevelTwo : MonoBehaviour
{
    //Variabile:
    public GameObject gameOverObject;
    public GameObject pausedObject;
    private CharacterScriptLevel2 character;
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

    public AudioSource level2Music;
    public AudioSource levelPass;
    public AudioSource pause;
    public AudioSource buttonPress;

    //Teleport red 1:
    private TeleportRedLevel2Script teleportRedScript;
    //Teleport red 2:
    private TeleportRed2Level2Script teleportRed2Script;
    //Teleport green 1:
    private TeleportGreenLevel2Script teleportGreenScript;
    //Teleport green 2:
    private TeleportGreen2Level2Script teleportGreen2Script;
    //Teleport yellow 1:
    private TeleportYellowLevel2Script teleportYellowScript;
    //Teleport yellow 2:
    private TeleportYellow2Level2Script teleportYellow2Script;
    //Teleport blue 1:
    private TeleportBlueLevel2Script teleportBlueScript;
    //Teleport blue 2:
    private TeleportBlue2Level2Script teleportBlue2Script;
    //Teleport blue 3:
    private TeleportBlue3Level2Script teleportBlue3Script;
    //Teleport grey 1:
    private TeleportGreyLevel2Script teleportGreyScript;
    //Teleport grey 2:
    private TeleportGrey2Level2Script teleportGrey2Script;
    //Teleport grey 3:
    private TeleportGrey3Level2Script teleportGrey3Script;

    //Breakable platforms:
    private BreakPlatformLevel2Script breakPlatform1;
    private BreakPlatformLevel2Script breakPlatform2;
    private BreakPlatformLevel2Script breakPlatform3;
    private BreakPlatformLevel2Script breakPlatform4;
    private BreakPlatformLevel2Script breakPlatform5;
    private BreakPlatformLevel2Script breakPlatform6;
    private BreakPlatformLevel2Script breakPlatform7;
    private BreakPlatformLevel2Script breakPlatform8;
    private BreakPlatformLevel2Script breakPlatform9;
    private BreakPlatformLevel2Script breakPlatform10;

    //Spikes:
    private AppearingDeathObjectScript spikes;

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
            .GetComponent<CharacterScriptLevel2>();

        //Teleport red 1:
        teleportRedScript = GameObject.FindGameObjectWithTag("TeleportRed1")
            .GetComponent<TeleportRedLevel2Script>();
        //Teleport red 2:
        teleportRed2Script = GameObject.FindGameObjectWithTag("TeleportRed2")
            .GetComponent<TeleportRed2Level2Script>();
        //Teleport green 1:
        teleportGreenScript = GameObject.FindGameObjectWithTag("TeleportGreen1")
            .GetComponent<TeleportGreenLevel2Script>();
        //Teleport green 2:
        teleportGreen2Script = GameObject.FindGameObjectWithTag("TeleportGreen2")
            .GetComponent<TeleportGreen2Level2Script>();
        //Teleport yellow 1:
        teleportYellowScript = GameObject.FindGameObjectWithTag("TeleportYellow1")
            .GetComponent<TeleportYellowLevel2Script>();
        //Teleport yellow 2:
        teleportYellow2Script = GameObject.FindGameObjectWithTag("TeleportYellow2")
            .GetComponent<TeleportYellow2Level2Script>();
        //Teleport blue 1:
        teleportBlueScript = GameObject.FindGameObjectWithTag("TeleportBlue1")
            .GetComponent<TeleportBlueLevel2Script>();
        //Teleport blue 2:
        teleportBlue2Script = GameObject.FindGameObjectWithTag("TeleportBlue2")
            .GetComponent<TeleportBlue2Level2Script>();
        //Teleport blue 3:
        teleportBlue3Script = GameObject.FindGameObjectWithTag("TeleportBlue3")
            .GetComponent<TeleportBlue3Level2Script>();
        //Teleport grey 1:
        teleportGreyScript = GameObject.FindGameObjectWithTag("TeleportGrey1")
            .GetComponent<TeleportGreyLevel2Script>();
        //Teleport grey 2:
        teleportGrey2Script = GameObject.FindGameObjectWithTag("TeleportGrey2")
            .GetComponent<TeleportGrey2Level2Script>();
        //Teleport grey 3:
        teleportGrey3Script = GameObject.FindGameObjectWithTag("TeleportGrey3")
            .GetComponent<TeleportGrey3Level2Script>();

        //Breakable platforms:
        breakPlatform1 = GameObject.FindGameObjectWithTag("BreakPlatform")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform2 = GameObject.FindGameObjectWithTag("BreakPlatform1")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform3 = GameObject.FindGameObjectWithTag("BreakPlatform2")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform4 = GameObject.FindGameObjectWithTag("BreakPlatform3")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform5 = GameObject.FindGameObjectWithTag("BreakPlatform4")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform6 = GameObject.FindGameObjectWithTag("BreakPlatform5")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform7 = GameObject.FindGameObjectWithTag("BreakPlatform6")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform8 = GameObject.FindGameObjectWithTag("BreakPlatform7")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform9 = GameObject.FindGameObjectWithTag("BreakPlatform8")
            .GetComponent<BreakPlatformLevel2Script>();
        breakPlatform10 = GameObject.FindGameObjectWithTag("BreakPlatform9")
            .GetComponent<BreakPlatformLevel2Script>();

        spikes = GameObject.FindGameObjectWithTag("Spikes")
            .GetComponent<AppearingDeathObjectScript>();

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

            character.hitPlatformSE.UnPause();
            character.jump.UnPause();
            character.icon1SE.UnPause();
            character.icon2SE.UnPause();
            character.icon3SE.UnPause();
            character.noIcon.UnPause();
            character.noIcon2.UnPause();
            character.noIcon3.UnPause();
            teleportRedScript.teleportRedSound.UnPause();
            teleportRed2Script.teleportRedSound.UnPause();
            teleportGreenScript.teleportGreenSound.UnPause();
            teleportGreen2Script.teleportGreenSound.UnPause();
            teleportYellowScript.teleportYellowSound.UnPause();
            teleportYellow2Script.teleportYellowSound.UnPause();
            teleportBlueScript.teleportBlueSound.UnPause();
            teleportBlue2Script.teleportBlueSound.UnPause();
            teleportBlue3Script.teleportBlueSound.UnPause();
            teleportGreyScript.teleportGreySound.UnPause();
            teleportGrey2Script.teleportGreySound.UnPause();
            teleportGrey3Script.teleportGreySound.UnPause();

            if (breakPlatform1 != null)
            {
                breakPlatform1.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform1.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform2 != null)
            {
                breakPlatform2.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform2.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform3 != null)
            {
                breakPlatform3.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform3.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform4 != null)
            {
                breakPlatform4.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform4.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform5 != null)
            {
                breakPlatform5.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform5.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform6 != null)
            {
                breakPlatform6.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform6.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform7 != null)
            {
                breakPlatform7.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform7.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform8 != null)
            {
                breakPlatform8.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform8.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform9 != null)
            {
                breakPlatform9.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform9.breakPlatformBlockSoundLeave.UnPause();
            }
            if (breakPlatform10 != null)
            {
                breakPlatform10.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform10.breakPlatformBlockSoundLeave.UnPause();
            }

            spikes.spikesSE.UnPause();

            level2Music.Play();
            pause.Stop();
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) &&
            startGame == true && gameIsOver == false
            )
        {
            startGame = false;
            gameIsPaused = true;

            Time.timeScale = 0;

            level2Music.Stop();
            pause.Play();

            character.hitPlatformSE.Pause();
            character.jump.Pause();
            character.icon1SE.Pause();
            character.icon2SE.Pause();
            character.icon3SE.Pause();
            character.noIcon.Pause();
            character.noIcon2.Pause();
            character.noIcon3.Pause();
            teleportRedScript.teleportRedSound.Pause();
            teleportRed2Script.teleportRedSound.Pause();
            teleportGreenScript.teleportGreenSound.Pause();
            teleportGreen2Script.teleportGreenSound.Pause();
            teleportYellowScript.teleportYellowSound.Pause();
            teleportYellow2Script.teleportYellowSound.Pause();
            teleportBlueScript.teleportBlueSound.Pause();
            teleportBlue2Script.teleportBlueSound.Pause();
            teleportBlue3Script.teleportBlueSound.Pause();
            teleportGreyScript.teleportGreySound.Pause();
            teleportGrey2Script.teleportGreySound.Pause();
            teleportGrey3Script.teleportGreySound.Pause();

            if (breakPlatform1 != null)
            {
                breakPlatform1.breakPlatformBlockSoundEnter.Pause();
                breakPlatform1.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform2 != null)
            {
                breakPlatform2.breakPlatformBlockSoundEnter.Pause();
                breakPlatform2.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform3 != null)
            {
                breakPlatform3.breakPlatformBlockSoundEnter.Pause();
                breakPlatform3.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform4 != null)
            {
                breakPlatform4.breakPlatformBlockSoundEnter.Pause();
                breakPlatform4.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform5 != null)
            {
                breakPlatform5.breakPlatformBlockSoundEnter.Pause();
                breakPlatform5.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform6 != null)
            {
                breakPlatform6.breakPlatformBlockSoundEnter.Pause();
                breakPlatform6.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform7 != null)
            {
                breakPlatform7.breakPlatformBlockSoundEnter.Pause();
                breakPlatform7.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform8 != null)
            {
                breakPlatform8.breakPlatformBlockSoundEnter.Pause();
                breakPlatform8.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform9 != null)
            {
                breakPlatform9.breakPlatformBlockSoundEnter.Pause();
                breakPlatform9.breakPlatformBlockSoundLeave.Pause();
            }
            if (breakPlatform10 != null)
            {
                breakPlatform10.breakPlatformBlockSoundEnter.Pause();
                breakPlatform10.breakPlatformBlockSoundLeave.Pause();
            }

            spikes.spikesSE.Pause();

            pausedObject.SetActive(true);
            GameObject.FindGameObjectWithTag("ReplayLevel1").GetComponent<Button>().Select();

            CheckpointRefresh();
        }
    }

    //Functii noi:

    //Pentru a merge pe Hub Area:
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

    //Restart scene pentru cand mori de la collision:
    public void RestartSceneCollision()
    {
        StopMusic();

        gameOverObject.SetActive(true);
        gameIsOver = true;
    }

    //Select button:
    public void SelectReplayLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ReplayLevel1")
            .GetComponent<Button>().colors;

        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ReplayLevel1")
            .GetComponent<Button>().colors = colorBlock;
    }

    //Select button:
    public void SelectReplayCheckpointLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
            .GetComponent<Button>().colors;

        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ReplayCheckpointLevel1")
            .GetComponent<Button>().colors = colorBlock;
    }

    //Select button:
    public void SelectExitPausedLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ExitPausedLevel1").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ExitPausedLevel1").GetComponent<Button>().colors = colorBlock;
    }

    //Select button:
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

    //Select button:
    public void SelectExitGameOverLevel1Button()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ExitGameOverLevel1").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("ExitGameOverLevel1").GetComponent<Button>().colors = colorBlock;
    }

    //Pentru cand sa se intample tranzitia:
    private IEnumerator DisableSceneTransitionStart(float timeLeftTransition)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        startTransition.SetActive(false);
        startOfTransition = false;
    }

    //Pentru cand sa se intample tranzitia:
    private IEnumerator DisableSceneTransitionEnd(float timeLeftTransition, int sceneId)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        SceneManager.LoadScene(sceneId);
    }

    //Restart cu valorile corecte:
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

    //Restart pentru urmatoarea scena:
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
        character.hitPlatformSE.Stop();
        character.jump.Stop();
        character.icon1SE.Stop();
        character.icon2SE.Stop();
        character.icon3SE.Stop();
        character.noIcon.Stop();
        character.noIcon2.Stop();
        character.noIcon3.Stop();
        teleportRedScript.teleportRedSound.Stop();
        teleportRed2Script.teleportRedSound.Stop();
        teleportGreenScript.teleportGreenSound.Stop();
        teleportGreen2Script.teleportGreenSound.Stop();
        teleportYellowScript.teleportYellowSound.Stop();
        teleportYellow2Script.teleportYellowSound.Stop();
        teleportBlueScript.teleportBlueSound.Stop();
        teleportBlue2Script.teleportBlueSound.Stop();
        teleportBlue3Script.teleportBlueSound.Stop();
        teleportGreyScript.teleportGreySound.Stop();
        teleportGrey2Script.teleportGreySound.Stop();
        teleportGrey3Script.teleportGreySound.Stop();

        if (breakPlatform1 != null)
        {
            breakPlatform1.breakPlatformBlockSoundEnter.Stop();
            breakPlatform1.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform2 != null)
        {
            breakPlatform2.breakPlatformBlockSoundEnter.Stop();
            breakPlatform2.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform3 != null)
        {
            breakPlatform3.breakPlatformBlockSoundEnter.Stop();
            breakPlatform3.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform4 != null)
        {
            breakPlatform4.breakPlatformBlockSoundEnter.Stop();
            breakPlatform4.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform5 != null)
        {
            breakPlatform5.breakPlatformBlockSoundEnter.Stop();
            breakPlatform5.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform6 != null)
        {
            breakPlatform6.breakPlatformBlockSoundEnter.Stop();
            breakPlatform6.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform7 != null)
        {
            breakPlatform7.breakPlatformBlockSoundEnter.Stop();
            breakPlatform7.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform8 != null)
        {
            breakPlatform8.breakPlatformBlockSoundEnter.Stop();
            breakPlatform8.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform9 != null)
        {
            breakPlatform9.breakPlatformBlockSoundEnter.Stop();
            breakPlatform9.breakPlatformBlockSoundLeave.Stop();
        }
        if (breakPlatform10 != null)
        {
            breakPlatform10.breakPlatformBlockSoundEnter.Stop();
            breakPlatform10.breakPlatformBlockSoundLeave.Stop();
        }

        spikes.spikesSE.Stop();
    }

    //Pentru afisare buton checkpoint:
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
