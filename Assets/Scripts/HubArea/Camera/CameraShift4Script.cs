using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;




public class CameraShift4Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    private FollowCamera4 followCamera4;
    private bool oneTime;
    public Camera followCamera;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript4>();

        followCamera4 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCamera4>();

        oneTime = true;
    }

    void Update()
    {
    }

    //Functii noi:

    //Pentru shift la camera atunci cand se atinge trigger-ul:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character") && oneTime == true)
        {
            StartCoroutine(ChangeCameraSize(0.25f, 0.01f, 75));
        }
    }

    //Camera size change:
    private IEnumerator ChangeCameraSize(float ammoutToChange, float timeToChange, int finalAmmout)
    {
        //Astept pana sa o fac:
        while (followCamera.orthographicSize < finalAmmout)
        {
            yield return new WaitForSeconds(timeToChange);
            followCamera.orthographicSize = followCamera.orthographicSize + ammoutToChange;
        }
    }
}

