using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportGreenScript : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    public ParticleSystem teleportGreenPS;
    public AudioSource teleportGreenSound;
    public GameObject teleportGreen2;
    public bool teleport1;

    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript4>();

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
        if (collision.gameObject.CompareTag("Character"))
        {
            teleportGreenSound.Play();

            if (teleportGreen2.GetComponent<TeleportGreen2Script>().teleport2 == false)
            {
                teleportGreenPS.Play();

                teleport1 = true;

                character.transform.position = new Vector3(teleportGreen2.transform.position.x,
                                                           teleportGreen2.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportGreenPS.Stop();

                teleportGreen2.GetComponent<TeleportGreen2Script>().teleport2 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportGreenPS.Play();
    }
}
