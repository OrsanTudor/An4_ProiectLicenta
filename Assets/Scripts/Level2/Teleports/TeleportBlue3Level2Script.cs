using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportBlue3Level2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportBluePS;
    public AudioSource teleportBlueSound;
    public GameObject teleportBlue2;
    public GameObject teleportBlue;
    public bool teleport3;

    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        teleport3 = false;
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
            teleportBlueSound.Play();

            if (teleportBlue2.GetComponent<TeleportBlue2Level2Script>().teleport2 == false)
            {
                teleportBluePS.Play();

                teleport3 = true;

                character.transform.position = new Vector3(teleportBlue.transform.position.x,
                                                           teleportBlue.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportBluePS.Stop();

                teleportBlue2.GetComponent<TeleportBlue2Level2Script>().teleport2 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportBluePS.Play();
    }
}
