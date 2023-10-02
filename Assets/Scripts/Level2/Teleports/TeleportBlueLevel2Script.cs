using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportBlueLevel2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportBluePS;
    public AudioSource teleportBlueSound;
    public GameObject teleportBlue3;
    public GameObject teleportBlue2;
    public bool teleport1;

    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        teleport1 = false;
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

            if (teleportBlue3.GetComponent<TeleportBlue3Level2Script>().teleport3 == false)
            {
                teleportBluePS.Play();

                teleport1 = true;

                character.transform.position = new Vector3(teleportBlue2.transform.position.x,
                                                           teleportBlue2.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportBluePS.Stop();

                teleportBlue3.GetComponent<TeleportBlue3Level2Script>().teleport3 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportBluePS.Play();
    }
}
