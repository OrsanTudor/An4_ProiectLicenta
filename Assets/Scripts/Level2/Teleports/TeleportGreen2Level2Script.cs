using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportGreen2Level2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportGreenPS;
    public AudioSource teleportGreenSound;
    public GameObject teleportGreen1;
    public bool teleport2;

    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        teleport2 = false;
    }

    //Update:
    void Update()
    {
    }

    //Functii noi:

    //Pentru teleport la celalalt teleport:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && character.canIcon1
            && character.canIcon2
            && character.canIcon3
            )
        {
            teleportGreenSound.Play();

            if (teleportGreen1.GetComponent<TeleportGreenLevel2Script>().teleport1 == false)
            {
                teleportGreenPS.Play();

                teleport2 = true;

                character.transform.position = new Vector3(teleportGreen1.transform.position.x,
                                                           teleportGreen1.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportGreenPS.Stop();

                teleportGreen1.GetComponent<TeleportGreenLevel2Script>().teleport1 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportGreenPS.Play();
    }
}
