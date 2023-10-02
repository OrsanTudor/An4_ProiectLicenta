using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;



public class LogicManagerLevelThree : MonoBehaviour
{
    //Variabile:
    public GameObject gameOverObject;
    public GameObject pausedObject;
    private CharacterScriptLevel3 character;
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
    public GameObject breakPlatformObjectInstantiate;
    public bool noPlatform1;
    private Vector3 breakPlatformObject1Position;
    private BreakPlatformLevel3Script breakPlatform1;
    public bool noPlatform2;
    private Vector3 breakPlatformObject2Position;
    private BreakPlatformLevel3Script breakPlatform2;
    public bool noPlatform3;
    private Vector3 breakPlatformObject3Position;
    private BreakPlatformLevel3Script breakPlatform3;
    public bool noPlatform4;
    private Vector3 breakPlatformObject4Position;
    private BreakPlatformLevel3Script breakPlatform4;
    public bool noPlatform5;
    private Vector3 breakPlatformObject5Position;
    private BreakPlatformLevel3Script breakPlatform5;
    public bool noPlatform6;
    private Vector3 breakPlatformObject6Position;
    private BreakPlatformLevel3Script breakPlatform6;
    public bool noPlatform7;
    private Vector3 breakPlatformObject7Position;
    private BreakPlatformLevel3Script breakPlatform7;
    public bool noPlatform8;
    private Vector3 breakPlatformObject8Position;
    private BreakPlatformLevel3Script breakPlatform8;
    public bool noPlatform9;
    private Vector3 breakPlatformObject9Position;
    private BreakPlatformLevel3Script breakPlatform9;
    public bool noPlatform10;
    private Vector3 breakPlatformObject10Position;
    private BreakPlatformLevel3Script breakPlatform10;
    public bool noPlatform11;
    private Vector3 breakPlatformObject11Position;
    private BreakPlatformLevel3Script breakPlatform11;
    public bool noPlatform12;
    private Vector3 breakPlatformObject12Position;
    private BreakPlatformLevel3Script breakPlatform12;

    public AudioSource level3Music;
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
            .GetComponent<CharacterScriptLevel3>();

        noPlatform1 = false;
        breakPlatform1 = GameObject.FindGameObjectWithTag("BreakPlatform")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject1Position = breakPlatform1.transform.position;
        noPlatform2 = false;
        breakPlatform2 = GameObject.FindGameObjectWithTag("BreakPlatform1")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject2Position = breakPlatform2.transform.position;
        noPlatform3 = false;
        breakPlatform3 = GameObject.FindGameObjectWithTag("BreakPlatform2")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject3Position = breakPlatform3.transform.position;
        noPlatform4 = false;
        breakPlatform4 = GameObject.FindGameObjectWithTag("BreakPlatform3")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject4Position = breakPlatform4.transform.position;
        noPlatform5 = false;
        breakPlatform5 = GameObject.FindGameObjectWithTag("BreakPlatform4")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject5Position = breakPlatform5.transform.position;
        noPlatform6 = false;
        breakPlatform6 = GameObject.FindGameObjectWithTag("BreakPlatform5")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject6Position = breakPlatform6.transform.position;
        noPlatform7 = false;
        breakPlatform7 = GameObject.FindGameObjectWithTag("BreakPlatform6")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject7Position = breakPlatform7.transform.position;
        noPlatform8 = false;
        breakPlatform8 = GameObject.FindGameObjectWithTag("BreakPlatform7")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject8Position = breakPlatform8.transform.position;
        noPlatform9 = false;
        breakPlatform9 = GameObject.FindGameObjectWithTag("BreakPlatform8")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject9Position = breakPlatform9.transform.position;
        noPlatform10 = false;
        breakPlatform10 = GameObject.FindGameObjectWithTag("BreakPlatform9")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject10Position = breakPlatform10.transform.position;
        noPlatform11 = false;
        breakPlatform11 = GameObject.FindGameObjectWithTag("BreakPlatform10")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject11Position = breakPlatform11.transform.position;
        noPlatform12 = false;
        breakPlatform12 = GameObject.FindGameObjectWithTag("BreakPlatform11")
            .GetComponent<BreakPlatformLevel3Script>();
        breakPlatformObject12Position = breakPlatform12.transform.position;

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

        if (true)
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
            if (noPlatform6 == true && PlayerNotInPlatforms(breakPlatformObject6Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject6Position, 6));
                noPlatform6 = false;
            }
            if (noPlatform7 == true && PlayerNotInPlatforms(breakPlatformObject7Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject7Position, 7));
                noPlatform7 = false;
            }
            if (noPlatform8 == true && PlayerNotInPlatforms(breakPlatformObject8Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject8Position, 8));
                noPlatform8 = false;
            }
            if (noPlatform9 == true && PlayerNotInPlatforms(breakPlatformObject9Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject9Position, 9));
                noPlatform9 = false;
            }
            if (noPlatform10 == true && PlayerNotInPlatforms(breakPlatformObject10Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject10Position, 10));
                noPlatform10 = false;
            }
            if (noPlatform11 == true && PlayerNotInPlatforms(breakPlatformObject11Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject11Position, 11));
                noPlatform11 = false;
            }
            if (noPlatform12 == true && PlayerNotInPlatforms(breakPlatformObject12Position))
            {
                StartCoroutine(MakePlatformAppear(timeForRespawn, breakPlatformObject12Position, 12));
                noPlatform12 = false;
            }
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
            if (noPlatform1 == false)
            {
                breakPlatform1.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform2 == false)
            {
                breakPlatform2.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform3 == false)
            {
                breakPlatform3.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform4 == false)
            {
                breakPlatform4.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform5 == false)
            {
                breakPlatform5.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform6 == false)
            {
                breakPlatform6.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform7 == false)
            {
                breakPlatform7.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform8 == false)
            {
                breakPlatform8.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform9 == false)
            {
                breakPlatform9.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform10 == false)
            {
                breakPlatform10.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform11 == false)
            {
                breakPlatform11.breakPlatformBlockSoundEnter.UnPause();
            }
            if (noPlatform12 == false)
            {
                breakPlatform12.breakPlatformBlockSoundEnter.UnPause();
            }

            level3Music.Play();
            pause.Stop();
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) &&
            startGame == true && gameIsOver == false
            )
        {
            startGame = false;
            gameIsPaused = true;

            Time.timeScale = 0;

            level3Music.Stop();
            pause.Play();

            character.jump.Pause();

            character.icon1SE.Pause();
            character.icon2SE.Pause();
            character.icon3SE.Pause();

            character.noIcon.Pause();
            character.noIcon2.Pause();
            character.noIcon3.Pause();
    
            if (noPlatform1 == false)
            {
                breakPlatform1.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform2 == false)
            {
                breakPlatform2.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform3 == false)
            {
                breakPlatform3.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform4 == false)
            {
                breakPlatform4.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform5 == false)
            {
                breakPlatform5.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform6 == false)
            {
                breakPlatform6.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform7 == false)
            {
                breakPlatform7.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform8 == false)
            {
                breakPlatform8.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform9 == false)
            {
                breakPlatform9.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform10 == false)
            {
                breakPlatform10.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform11 == false)
            {
                breakPlatform11.breakPlatformBlockSoundEnter.Pause();
            }
            if (noPlatform12 == false)
            {
                breakPlatform12.breakPlatformBlockSoundEnter.Pause();
            }

            pausedObject.SetActive(true);
            GameObject.FindGameObjectWithTag("ReplayLevel1").GetComponent<Button>().Select();

            CheckpointRefresh();
        }
    }

    //Functii noi:

    //Mergi la hub area:
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

    //Restart scena cand faci coliziune cu un deathobject:
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

    //Disable la start:
    private IEnumerator DisableSceneTransitionStart(float timeLeftTransition)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        startTransition.SetActive(false);
        startOfTransition = false;
    }

    //Disable la end:
    private IEnumerator DisableSceneTransitionEnd(float timeLeftTransition, int sceneId)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        SceneManager.LoadScene(sceneId);
    }

    //Restart date din prefs:
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

    //Restart date din prefs pentru urmatoarea scena:
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
        if (noPlatform6 == false)
        {
            breakPlatform6.breakPlatformBlockSoundEnter.Stop();
            breakPlatform6.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform7 == false)
        {
            breakPlatform7.breakPlatformBlockSoundEnter.Stop();
            breakPlatform7.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform8 == false)
        {
            breakPlatform8.breakPlatformBlockSoundEnter.Stop();
            breakPlatform8.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform9 == false)
        {
            breakPlatform9.breakPlatformBlockSoundEnter.Stop();
            breakPlatform9.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform10 == false)
        {
            breakPlatform10.breakPlatformBlockSoundEnter.Stop();
            breakPlatform10.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform11 == false)
        {
            breakPlatform11.breakPlatformBlockSoundEnter.Stop();
            breakPlatform11.breakPlatformBlockSoundLeave.Stop();
        }
        if (noPlatform12 == false)
        {
            breakPlatform12.breakPlatformBlockSoundEnter.Stop();
            breakPlatform12.breakPlatformBlockSoundLeave.Stop();
        }
    }

    //Nu se afla caracterul in platforma:
    private bool PlayerNotInPlatforms(Vector3 breakPlatformPosition)
    {
        if (
            Mathf.Abs(character.transform.position.x - breakPlatformPosition.x) > 60
            || Mathf.Abs(character.transform.position.y - breakPlatformPosition.y) > 15
            )
        {
            return true;
        }

        return false;
    }

    //Spawn pentru platforma:
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

        if (platformNumber == 1 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform1 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>(); 
            newPlatform.tag = "BreakPlatform";
        }

        if (platformNumber == 2 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform2 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>(); 
            newPlatform.tag = "BreakPlatform1";
        }

        if (platformNumber == 3 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform3 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform2";
        }

        if (platformNumber == 4 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform4 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform3";
        }

        if (platformNumber == 5 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform5 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform4";
        }

        if (platformNumber == 6 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform6 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform5";
        }

        if (platformNumber == 7 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform7 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform6";
        }

        if (platformNumber == 8 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform8 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform7";
        }

        if (platformNumber == 9 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform9 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform8";
        }

        if (platformNumber == 10 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform10 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform9";
        }

        if (platformNumber == 11 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform11 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform10";
        }

        if (platformNumber == 12 && PlayerNotInPlatforms(breakPlatformObject1PositionLocal))
        {
            breakPlatform12 = newPlatform
            .GetComponent<BreakPlatformLevel3Script>();
            newPlatform.tag = "BreakPlatform11";
        }

        if (noRespawn == 0)
        {
            newPlatform.SetActive(false);

            yield return new WaitForSeconds(timeToWait);

            newPlatform.SetActive(true);
        }
    }

    //Refresh checkpoint button:
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
