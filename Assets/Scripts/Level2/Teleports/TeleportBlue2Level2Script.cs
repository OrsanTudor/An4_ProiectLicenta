using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportBlue2Level2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportBluePS;
    public AudioSource teleportBlueSound;
    public GameObject teleportBlue;
    public GameObject teleportBlue3;
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
            teleportBlueSound.Play();

            if (teleportBlue.GetComponent<TeleportBlueLevel2Script>().teleport1 == false)
            {
                teleportBluePS.Play();

                teleport2 = true;

                character.transform.position = new Vector3(teleportBlue3.transform.position.x,
                                                           teleportBlue3.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportBluePS.Stop();

                teleportBlue.GetComponent<TeleportBlueLevel2Script>().teleport1 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportBluePS.Play();
    }
}
