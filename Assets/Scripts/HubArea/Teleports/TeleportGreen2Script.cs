using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportGreen2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    public ParticleSystem teleportGreenPS;
    public AudioSource teleportGreenSound;
    public GameObject teleportGreen1;
    public bool teleport2;

    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript4>();

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
        if (collision.gameObject.CompareTag("Character"))
        {
            teleportGreenSound.Play();

            if (teleportGreen1.GetComponent<TeleportGreenScript>().teleport1 == false)
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

                teleportGreen1.GetComponent<TeleportGreenScript>().teleport1 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportGreenPS.Play();
    }
}
