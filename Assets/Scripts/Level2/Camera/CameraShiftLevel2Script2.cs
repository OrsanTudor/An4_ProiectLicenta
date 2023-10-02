using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;




public class CameraShiftLevel2Script2 : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    private FollowCameraLevel2 followCameraLv2;
    private bool oneTime;
    public Image checkpoint;
    public Camera followCamera;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        followCameraLv2 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCameraLevel2>();

        oneTime = true;
    }

    void Update()
    {
    }

    //Functii noi:

    //Pentru cand vrei sa schimbi perspectiva, atingi trigger:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character") && oneTime == true)
        {
            Debug.Log("Checkpoint 2 reached.");
            Debug.Log("Last Checkpoint before: " + PlayerPrefs.GetInt("LastCheckpoint"));

            oneTime = false;

            StartCoroutine(ChangePerspective(0.3f));

            //StartCoroutine(ChangeCameraSize(0.25f, 0.01f, 75));

            float newCharacterPositionX = transform.position.x + 4f;
            PlayerPrefs.SetFloat("PozitieX_Checkpoint2", newCharacterPositionX);

            character.transform.position =
                new Vector3(PlayerPrefs.GetFloat("PozitieX_Checkpoint2"),
                                                 character.transform.position.y,
                                                 character.transform.position.z
                                                 );

            PlayerPrefs.SetFloat("PozitieY_Checkpoint2", character.transform.position.y);

            PlayerPrefs.SetInt("PowerUp1_Checkpoint2", character.powerUp1);
            PlayerPrefs.SetInt("PowerUp2_Checkpoint2", character.powerUp2);
            PlayerPrefs.SetInt("PowerUp3_Checkpoint2", character.powerUp3);

            PlayerPrefs.SetInt("LastCheckpoint", 2);
            Debug.Log("Last Checkpoint after: " + PlayerPrefs.GetInt("LastCheckpoint"));

            checkpoint.enabled = true;
            StartCoroutine(EndCheckpoint());
        }
    }

    //End checkpoint vizual:
    private IEnumerator EndCheckpoint()
    {
        yield return new WaitForSeconds(4f);
        checkpoint.enabled = false;

        Debug.Log("Animation done.");
    }

    //Pentru camera size:
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
        //followCameraLv2.offsetX = 60;
        //followCameraLv2.offsetY = 0;

        //2) Caracter dreapta: Nu:
        //followCameraLv2.offsetX = -60;
        //followCameraLv2.offsetY = 0;

        //3) Caracter sus:
        followCameraLv2.offsetX = 0;
        followCameraLv2.offsetY = -25;

        //4) Caracter jos:
        //followCameraLv2.offsetX = 0;
        //followCameraLv2.offsetY = 25;
    }
}

