using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class LogicManagerHubArea : MonoBehaviour
{
    //Variabile:
    public GameObject pausedObject;
    private CharacterScript4 character;

    //JumpPad 1:
    private JumpPad4Script jumpPad1;
    //JumpPad 2:
    private JumpPad4Script jumpPad2;
    //JumpPad 3:
    private JumpPad4Script jumpPad3;

    //No Gravity Block 1:
    private NoGravityBlockScript noGravityBlock1;
    //No Gravity Block 2:
    private NoGravityBlockScript2 noGravityBlock2;
    //No Gravity Block 3:
    private NoGravityBlockScript3 noGravityBlock3;

    //Teleport red 1:
    private TeleportRedScript teleportRedScript;
    //Teleport red 2:
    private TeleportRed2Script teleportRed2Script;
    //Teleport green 1:
    private TeleportGreenScript teleportGreenScript;
    //Teleport green 2:
    private TeleportGreen2Script teleportGreen2Script;
    //Teleport yellow 1:
    private TeleportYellowScript teleportYellowScript;
    //Teleport yellow 2:
    private TeleportYellow2Script teleportYellow2Script;

    //Breakable platforms:
    public GameObject breakPlatformObjectInstantiate;
    
    public bool noPlatform1;
    private Vector3 breakPlatformObject1Position;
    private BreakPlatform1Script breakPlatform1;
    public bool noPlatform2;
    private Vector3 breakPlatformObject2Position;
    private BreakPlatform1Script breakPlatform2;
    public bool noPlatform3;
    private Vector3 breakPlatformObject3Position;
    private BreakPlatform1Script breakPlatform3;
    public bool noPlatform4;
    private Vector3 breakPlatformObject4Position;
    private BreakPlatform1Script breakPlatform4;
    public bool noPlatform5;
    private Vector3 breakPlatformObject5Position;
    private BreakPlatform1Script breakPlatform5;

    private ColorBlock colorBlock;
    public bool startGame;
    public bool gameIsPaused;
    public bool oneTimeGameOverSelect;
    public GameObject startTransition;
    public GameObject endTransition;
    public bool startOfTransition;
    public bool endOfTransition;

    //Sound effects:
    public AudioSource hubAreaMusic;
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

        GameObject.FindGameObjectWithTag("ReplayLevel1")
            .GetComponent<Button>().Select();

        SelectReplayLevel1Button();
        SelectExitPausedLevel1Button();

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript4>();

        //Jump pad 1:
        jumpPad1 = GameObject.FindGameObjectWithTag("JumpPad")
            .GetComponent<JumpPad4Script>();
        //Jump pad 2:
        jumpPad2 = GameObject.FindGameObjectWithTag("JumpPad2")
            .GetComponent<JumpPad4Script>();
        //Jump pad 3:
        jumpPad3 = GameObject.FindGameObjectWithTag("JumpPad3")
            .GetComponent<JumpPad4Script>();

        //No Gravity Block 1:
        noGravityBlock1 = GameObject.FindGameObjectWithTag("NoGravityPlatform")
            .GetComponent<NoGravityBlockScript>();
        //No Gravity Block 2:
        noGravityBlock2 = GameObject.FindGameObjectWithTag("NoGravityPlatform1")
            .GetComponent<NoGravityBlockScript2>();
        //noGravityBlock2.transform.Rotate(0, 90, 0);
        //No Gravity Block 3:
        noGravityBlock3 = GameObject.FindGameObjectWithTag("NoGravityPlatform2")
            .GetComponent<NoGravityBlockScript3>();

        //Teleport red 1:
        teleportRedScript = GameObject.FindGameObjectWithTag("TeleportRed1")
            .GetComponent<TeleportRedScript>();
        //Teleport red 2:
        teleportRed2Script = GameObject.FindGameObjectWithTag("TeleportRed2")
            .GetComponent<TeleportRed2Script>();
        //Teleport green 1:
        teleportGreenScript = GameObject.FindGameObjectWithTag("TeleportGreen1")
            .GetComponent<TeleportGreenScript>();
        //Teleport green 2:
        teleportGreen2Script = GameObject.FindGameObjectWithTag("TeleportGreen2")
            .GetComponent<TeleportGreen2Script>();
        //Teleport yellow 1:
        teleportYellowScript = GameObject.FindGameObjectWithTag("TeleportYellow1")
            .GetComponent<TeleportYellowScript>();
        //Teleport yellow 2:
        teleportYellow2Script = GameObject.FindGameObjectWithTag("TeleportYellow2")
            .GetComponent<TeleportYellow2Script>();

        //Breakable platform 1:
        noPlatform1 = false;
        breakPlatform1 = GameObject.FindGameObjectWithTag("BreakPlatform")
            .GetComponent<BreakPlatform1Script>();
        breakPlatformObject1Position = breakPlatform1.transform.position;

        noPlatform2 = false;
        breakPlatform2 = GameObject.FindGameObjectWithTag("BreakPlatform1")
            .GetComponent<BreakPlatform1Script>();
        breakPlatformObject2Position = breakPlatform2.transform.position;
        
        noPlatform3 = false;
        breakPlatform3 = GameObject.FindGameObjectWithTag("BreakPlatform2")
            .GetComponent<BreakPlatform1Script>();
        breakPlatformObject3Position = breakPlatform3.transform.position;
        
        noPlatform4 = false;
        breakPlatform4 = GameObject.FindGameObjectWithTag("BreakPlatform3")
            .GetComponent<BreakPlatform1Script>();
        breakPlatformObject4Position = breakPlatform4.transform.position;
        
        noPlatform5 = false;
        breakPlatform5 = GameObject.FindGameObjectWithTag("BreakPlatform4")
            .GetComponent<BreakPlatform1Script>();
        breakPlatformObject5Position = breakPlatform5.transform.position;

        pausedObject.SetActive(false);
        oneTimeGameOverSelect = false;
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

        //Respawn platforms:
        if(true)
        {
            float timeForRespawn = 1f;

            if (noPlatform1 == true && PlayerNotInPlatforms(breakPlatformObject1Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject1Position, 1));
                noPlatform1 = false;
            }
            if (noPlatform2 == true && PlayerNotInPlatforms(breakPlatformObject2Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject2Position, 2));
                noPlatform2 = false;
            }
            if (noPlatform3 == true && PlayerNotInPlatforms(breakPlatformObject3Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject3Position, 3));
                noPlatform3 = false;
            }
            if (noPlatform4 == true && PlayerNotInPlatforms(breakPlatformObject4Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject4Position, 4));
                noPlatform4 = false;
            }
            if (noPlatform5 == true && PlayerNotInPlatforms(breakPlatformObject5Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject5Position, 5));
                noPlatform5 = false;
            }
        }

        //Pentru activare pauza:
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            && startGame == false
            )
        {
            startGame = true;
            gameIsPaused = false;
            Time.timeScale = 1;

            pausedObject.SetActive(false);

            character.jump.UnPause();
            jumpPad1.jumpPadSound.UnPause();
            jumpPad2.jumpPadSound.UnPause();
            jumpPad3.jumpPadSound.UnPause();
            noGravityBlock1.noGravityBlockSound.UnPause();
            noGravityBlock2.noGravityBlockSound.UnPause();
            noGravityBlock3.noGravityBlockSound.UnPause();
            teleportRedScript.teleportRedSound.UnPause();
            teleportRed2Script.teleportRedSound.UnPause();
            teleportGreenScript.teleportGreenSound.UnPause();
            teleportGreen2Script.teleportGreenSound.UnPause();
            teleportYellowScript.teleportYellowSound.UnPause();
            teleportYellow2Script.teleportYellowSound.UnPause();
            
            if (noPlatform1 == false)
            {
                breakPlatform1.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform1.breakPlatformBlockSoundLeave.UnPause();
            }
            if (noPlatform2 == false)
            {
                breakPlatform2.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform2.breakPlatformBlockSoundLeave.UnPause();
            }
            if (noPlatform3 == false)
            {
                breakPlatform3.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform3.breakPlatformBlockSoundLeave.UnPause();
            }
            if (noPlatform4 == false)
            {
                breakPlatform4.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform4.breakPlatformBlockSoundLeave.UnPause();
            }
            if (noPlatform5 == false)
            {
                breakPlatform5.breakPlatformBlockSoundEnter.UnPause();
                breakPlatform5.breakPlatformBlockSoundLeave.UnPause();
            }
          
            hubAreaMusic.Play();
            pause.Stop();
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            && startGame == true
            )
        {
            startGame = false;
            gameIsPaused = true;
            Time.timeScale = 0;

            hubAreaMusic.Stop();
            pause.Play();

            character.jump.Pause();
            jumpPad1.jumpPadSound.Pause();
            jumpPad2.jumpPadSound.Pause();
            jumpPad3.jumpPadSound.Pause();
            noGravityBlock1.noGravityBlockSound.Pause();
            noGravityBlock2.noGravityBlockSound.Pause();
            noGravityBlock3.noGravityBlockSound.Pause();
            teleportRedScript.teleportRedSound.Pause();
            teleportRed2Script.teleportRedSound.Pause();
            teleportGreenScript.teleportGreenSound.Pause();
            teleportGreen2Script.teleportGreenSound.Pause();
            teleportYellowScript.teleportYellowSound.Pause();
            teleportYellow2Script.teleportYellowSound.Pause();
            
            if (noPlatform1 == false)
            {
                breakPlatform1.breakPlatformBlockSoundEnter.Pause();
                breakPlatform1.breakPlatformBlockSoundLeave.Pause();
            }
            if (noPlatform2 == false)
            {
                breakPlatform2.breakPlatformBlockSoundEnter.Pause();
                breakPlatform2.breakPlatformBlockSoundLeave.Pause();
            }
            if (noPlatform3 == false)
            {
                breakPlatform3.breakPlatformBlockSoundEnter.Pause();
                breakPlatform3.breakPlatformBlockSoundLeave.Pause();
            }
            if (noPlatform4 == false)
            {
                breakPlatform4.breakPlatformBlockSoundEnter.Pause();
                breakPlatform4.breakPlatformBlockSoundLeave.Pause();
            }
            if (noPlatform5 == false)
            {
                breakPlatform5.breakPlatformBlockSoundEnter.Pause();
                breakPlatform5.breakPlatformBlockSoundLeave.Pause();
            }

            pausedObject.SetActive(true);
            GameObject.FindGameObjectWithTag("ReplayLevel1").GetComponent<Button>().Select();
        }
    }

    //Functii noi:

    //Move to front page: Front page devine hub area!
    public void MoveToFrontPage(int sceneId)
    {
        StopMusic();

        RestartPlayerLevel();

        buttonPress.Play();

        Time.timeScale = 0;

        endTransition.SetActive(true);
        endOfTransition = true;

        PlayerPrefs.SetFloat("CharacterPositionX", -178.5f);
        PlayerPrefs.SetFloat("CharacterPositionY", -89.5f);

        float timeLeftTransition = 1f;
        StartCoroutine(DisableSceneTransitionEnd(timeLeftTransition, sceneId));
    }

    //Restart intreaga scena:
    public void RestartScene()
    {
        StopMusic();

        RestartPlayerLevel();

        buttonPress.Play();

        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    //Restart Player Prefs:
    public void RestartPlayerLevel()
    {
        PlayerPrefs.SetFloat("PozitieX_Checkpoint1", -122.3f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint1", -90f);
        PlayerPrefs.SetInt("PowerUp1_Checkpoint1", 0);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint1", 0);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint1", 0);

        PlayerPrefs.SetFloat("PozitieX_Checkpoint2", -1f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint2", -1f);
        PlayerPrefs.SetInt("PowerUp1_Checkpoint2", 0);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint2", 0);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint2", 0);

        PlayerPrefs.SetInt("LastCheckpoint", 0);
    }

    //Reset Player Prefs:
    public void RestartPlayerLevelNextScene()
    {
        PlayerPrefs.SetFloat("PozitieX_Checkpoint1", -122.3f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint1", -90f);
        PlayerPrefs.SetInt("PowerUp1_Checkpoint1", 0);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint1", 0);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint1", 0);

        PlayerPrefs.SetFloat("PozitieX_Checkpoint2", -1f);
        PlayerPrefs.SetFloat("PozitieY_Checkpoint2", -1f);
        PlayerPrefs.SetInt("PowerUp1_Checkpoint2", 0);
        PlayerPrefs.SetInt("PowerUp2_Checkpoint2", 0);
        PlayerPrefs.SetInt("PowerUp3_Checkpoint2", 0);

        //LastSceneCheckpoint:
        PlayerPrefs.SetInt("LastCheckpoint", 0);
    }

    //Stop all music:
    private void StopMusic()
    {
        character.jump.Stop();
        jumpPad1.jumpPadSound.Stop();
        jumpPad2.jumpPadSound.Stop();
        jumpPad3.jumpPadSound.Stop();
        noGravityBlock1.noGravityBlockSound.Stop();
        noGravityBlock2.noGravityBlockSound.Stop();
        noGravityBlock3.noGravityBlockSound.Stop();
        teleportRedScript.teleportRedSound.Stop();
        teleportRed2Script.teleportRedSound.Stop();
        teleportGreenScript.teleportGreenSound.Stop();
        teleportGreen2Script.teleportGreenSound.Stop();
        teleportYellowScript.teleportYellowSound.Stop();
        teleportYellow2Script.teleportYellowSound.Stop();

        if (noPlatform1 == false)
        {
            breakPlatform1.breakPlatformBlockSoundEnter.Stop();
            breakPlatform1.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform2 == false)
        {
            breakPlatform2.breakPlatformBlockSoundEnter.Stop();
            breakPlatform2.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform3 == false)
        {
            breakPlatform3.breakPlatformBlockSoundEnter.Stop();
            breakPlatform3.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform4 == false)
        {
            breakPlatform4.breakPlatformBlockSoundEnter.Stop();
            breakPlatform4.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform5 == false)
        {
            breakPlatform5.breakPlatformBlockSoundEnter.Stop();
            breakPlatform5.breakPlatformBlockSoundLeave.Stop();
        }
    }

    //Verific daca player-ul este in platforme:
    private bool PlayerNotInPlatforms(Vector3 breakPlatformPosition)
    {
        if(
            Mathf.Abs(character.transform.position.x - breakPlatformPosition.x) > 60
            || Mathf.Abs(character.transform.position.y - breakPlatformPosition.y) > 15
            )
        {
            return true;
        }

        return false;
    }

    //Pentru breakable platform: Sa reapara platforma:
    private IEnumerator MakePlatformAppear(
        float timeToWait, 
        Vector3 breakPlatformObject1PositionLocal, 
        int platformNumber)
    {
        GameObject newPlatform = Instantiate
            (
            breakPlatformObjectInstantiate,
            breakPlatformObject1PositionLocal,
            
            Quaternion.identity
            );

        int noRespawn = 0;

        if(platformNumber == 1 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform1 = newPlatform
            .GetComponent<BreakPlatform1Script>();
            newPlatform.tag = "BreakPlatform";
        }
        
        if (platformNumber == 2 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform2 = newPlatform
            .GetComponent<BreakPlatform1Script>(); 
            newPlatform.tag = "BreakPlatform1";
        }

        if (platformNumber == 3 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform3 = newPlatform
            .GetComponent<BreakPlatform1Script>(); 
            newPlatform.tag = "BreakPlatform2";
        }

        if (platformNumber == 4 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform4 = newPlatform
            .GetComponent<BreakPlatform1Script>(); 
            newPlatform.tag = "BreakPlatform3";
        }

        if (platformNumber == 5 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform5 = newPlatform
            .GetComponent<BreakPlatform1Script>(); 
            newPlatform.tag = "BreakPlatform4";
        }

        if(noRespawn == 0)
        {
            newPlatform.SetActive(false);

            yield return new WaitForSeconds(timeToWait);

            newPlatform.SetActive(true);
        }
    }
}
