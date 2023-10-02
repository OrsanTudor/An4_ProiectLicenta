using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;



public class FollowCameraLevel1 : MonoBehaviour
{
    //Variabile:
    public float cameraSpeed; 
    public Transform player;
    public float offsetX;
    public float offsetY;
    public CharacterScriptLevel1 character;
    public bool cameraMode;
    private Text startLevel;
    public float shakeDuration = 0.75f;
    public bool startShake;
    public AnimationCurve shakeCurve;

    //Functii basic:

    //Start:
    void Start()
    {
        cameraSpeed = 6f;
        offsetX = 60;
        offsetY = 0;

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel1>();

        cameraMode = false;
        Debug.Log("Camera mode 2 (Fixed).");

        shakeDuration = 0.3f;
        startShake = false;
    }

    //Update:
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && cameraMode == true)
        {
            cameraMode = false;

            Debug.Log("Camera mode 1 (Character).");
        }
        else if (Input.GetKeyDown(KeyCode.C) && cameraMode == false)
        {
            cameraMode = true;

            Vector3 cameraNewPosition = new Vector3(0, 0, -10f);
            transform.position = Vector3.Slerp(transform.position,
                cameraNewPosition,
                cameraSpeed * Time.deltaTime);

            Debug.Log("Camera mode 2 (Fixed).");
        }

        if (character.charState && cameraMode == false)
        {
            Vector3 cameraNewPosition =
                new Vector3(player.position.x + offsetX,
                player.position.y + offsetY, -10f);

            transform.position =
                Vector3.Slerp(transform.position,
                cameraNewPosition, cameraSpeed * Time.deltaTime);
        }

        if (startShake == true)
        {
            startShake = false;
            StartCoroutine(Shaking());
        }
    }

    //Functii noi:

    //Rutina pentru shake ecran:
    IEnumerator Shaking()
    {
        Vector3 cameraNewPosition;
        float timpTrecut = 0;

        while (timpTrecut < shakeDuration)
        {
            timpTrecut = timpTrecut + Time.deltaTime;

            float shakeStrength = shakeCurve.Evaluate(timpTrecut / shakeDuration) + 0.1f;

            cameraNewPosition = new Vector3(player.position.x + offsetX,
                                            player.position.y + offsetY,
                                            -10f);

            transform.position =
                Vector3.Slerp(transform.position,
                cameraNewPosition,
                cameraSpeed * Time.deltaTime)
                + Random.insideUnitSphere * shakeStrength;

            yield return null;
        }
    }
}
