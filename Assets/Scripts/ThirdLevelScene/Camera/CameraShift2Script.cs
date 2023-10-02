using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;




public class CameraShift2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript3 character;
    private FollowCamera3 followCamera3;
    private bool oneTime;
    public Image checkpoint;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript3>();

        followCamera3 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCamera3>();

        oneTime = true;
    }

    void Update()
    {
    }

    //Functii noi:

    //Cand face coliziune caracterul cu acest obiect, se schimba perspectiva camerei:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character") && oneTime == true)
        {
            Debug.Log("Checkpoint 1 reached.");
            Debug.Log("Last Checkpoint before: " + PlayerPrefs.GetInt("LastCheckpoint"));

            oneTime = false;

            followCamera3.offsetX = 0;
            followCamera3.offsetY = 25;

            PlayerPrefs.SetFloat("PozitieX_Checkpoint1", character.transform.position.x);
            character.transform.position = new Vector3(PlayerPrefs.GetFloat("PozitieX_Checkpoint1") + 5,
                character.transform.position.y,
                character.transform.position.z);
 
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

    //End checkpoint dupa un anumit numar de secunde:
    private IEnumerator EndCheckpoint()
    {
        yield return new WaitForSeconds(4f);
        checkpoint.enabled = false;

        Debug.Log("Animation done.");
    }
}

