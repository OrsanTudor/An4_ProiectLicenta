using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;




public class CameraShiftLevel1Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel1 character;
    private FollowCameraLevel1 followCameraLv1;
    private bool oneTime;
    public Image checkpoint;
    public Camera followCamera;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel1>();

        followCameraLv1 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCameraLevel1>();

        oneTime = true;
    }

    void Update()
    {
    }

    //Functii noi:

    //Atunci cand intri pe trigger, se schimba perspectiva:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character") && oneTime == true)
        {
            Debug.Log("Checkpoint 1 reached.");
            Debug.Log("Last Checkpoint before: " + PlayerPrefs.GetInt("LastCheckpoint"));

            oneTime = false;

            StartCoroutine(ChangePerspective(0.3f));

            //StartCoroutine(ChangeCameraSize(0.25f, 0.01f, 75));

            PlayerPrefs.SetFloat("PozitieX_Checkpoint1", character.transform.position.x);

            character.transform.position = new Vector3(PlayerPrefs.GetFloat("PozitieX_Checkpoint1"),
                                                       character.transform.position.y,
                                                       character.transform.position.z
                                                       );

            PlayerPrefs.SetFloat("PozitieY_Checkpoint1", character.transform.position.y);

            PlayerPrefs.SetInt("PowerUp1_Checkpoint1", character.powerUp1);
            PlayerPrefs.SetInt("PowerUp2_Checkpoint1", character.powerUp2);
            PlayerPrefs.SetInt("PowerUp3_Checkpoint1", character.powerUp3);

            PlayerPrefs.SetInt("LastCheckpoint", 1);
            Debug.Log("Last Checkpoint after: " + PlayerPrefs.GetInt("LastCheckpoint"));

            checkpoint.enabled = true;
            StartCoroutine(EndCheckpoint());
        }
    }

    //Checkpoint:
    private IEnumerator EndCheckpoint()
    {
        yield return new WaitForSeconds(4f);
        checkpoint.enabled = false;

        Debug.Log("Animation done.");
    }

    //Camera size:
    private IEnumerator ChangeCameraSize(float ammountToChange, float timeToChange, int finalAmmount)
    {
        while (followCamera.orthographicSize < finalAmmount)
        {
            yield return new WaitForSeconds(timeToChange);
            followCamera.orthographicSize = followCamera.orthographicSize + ammountToChange;
        }
    }

    //1 din 4 perspective:
    private IEnumerator ChangePerspective(float timeToChange)
    {
        yield return new WaitForSeconds(timeToChange);

        //1) Caracter stanga:
        //followCameraLv1.offsetX = 60;
        //followCameraLv1.offsetY = 0;

        //2) Caracter dreapta:
        //followCameraLv1.offsetX = -60;
        //followCameraLv1.offsetY = 0;

        //3) Caracter sus:
        //followCameraLv1.offsetX = 0;
        //followCameraLv1.offsetY = -25;

        //4) Caracter jos:
        followCameraLv1.offsetX = 0;
        followCameraLv1.offsetY = 25;
    }
}

