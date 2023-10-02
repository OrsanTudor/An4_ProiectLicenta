using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class TitleScreenLogic : MonoBehaviour
{
    //Variabile:
    private ColorBlock colorBlock;
    public GameObject endTransition;
    private bool endOfTransition;
    public AudioSource buttonPress;

    //Functii predefinite:

    // Start:
    void Start()
    {
        SelectPlayTitleScreen();
        SelectRestartGameTitleScreen();
        SelectExitTitleScreen();
        endOfTransition = false;
        RestartGameText();
    }

    // Update:
    void Update()
    {
    }

    //Functii noi:

    //Move to another scene:
    public void MoveToAnotherScene(int sceneId)
    {
        buttonPress.Play();

        Time.timeScale = 0;

        endTransition.SetActive(true);
        endOfTransition = true;

        float timeLeftTransition = 1f;
        StartCoroutine(DisableSceneTransition(timeLeftTransition, sceneId));
    }

    //Delete game progress:
    public void RestartGame()
    {
        buttonPress.Play();

        PlayerPrefs.SetInt("LevelTutorial", 0);
        PlayerPrefs.SetInt("Level1", 0);
        PlayerPrefs.SetInt("Level2", 0);
        PlayerPrefs.SetInt("Level3", 0);
        PlayerPrefs.SetInt("LevelFinal", 0);
        PlayerPrefs.SetInt("NumberFinishedLevels", 0);

        endTransition.SetActive(true);
        endOfTransition = true;

        float timeLeftTransition = 1f;
        StartCoroutine(DisableSceneTransitionRestart(timeLeftTransition));
    }

    //Exit game:
    public void ExitGame()
    {
        buttonPress.Play();

        Application.Quit();
    }

    //Select button play + exit: (+ restul butoanelor)
    public void SelectPlayTitleScreen()
    {
        colorBlock = GameObject.FindGameObjectWithTag("PlayTitleScreen").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.blue; 
        GameObject.FindGameObjectWithTag("PlayTitleScreen").GetComponent<Button>().colors = colorBlock;
        GameObject.FindGameObjectWithTag("PlayTitleScreen").GetComponent<Button>().Select();
    }
    public void SelectRestartGameTitleScreen()
    {
        colorBlock = GameObject.FindGameObjectWithTag("RestartGameTitleScreen").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.yellow;
        GameObject.FindGameObjectWithTag("RestartGameTitleScreen").GetComponent<Button>().colors = colorBlock;

    }
    public void SelectExitTitleScreen()
    {
        colorBlock = GameObject.FindGameObjectWithTag("ExitTitleScreen").GetComponent<Button>().colors;
        colorBlock.selectedColor = Color.blue; 
        GameObject.FindGameObjectWithTag("ExitTitleScreen").GetComponent<Button>().colors = colorBlock;
    }

    //Disable movement for scene transition:
    private IEnumerator DisableSceneTransition(float timeLeftTransition, int sceneId)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        SceneManager.LoadScene(sceneId);
    }

    //Disable movement for scene transition:
    private IEnumerator DisableSceneTransitionRestart(float timeLeftTransition)
    {
        Time.timeScale = 1;

        yield return new WaitForSeconds(timeLeftTransition);

        endTransition.SetActive(false);
        endOfTransition = false;

        RestartGameText();
    }

    //Restart the game text for how many levels you completed;
    private void RestartGameText()
    {
        string restartGameObjectText = "Restart Game (Reset ";
        restartGameObjectText += PlayerPrefs.GetInt("NumberFinishedLevels");
        restartGameObjectText += " Levels)";
        GameObject.FindGameObjectWithTag("RestartGameTitleScreen").GetComponent<Button>().GetComponentInChildren<Text>().text = restartGameObjectText;
    }
}
