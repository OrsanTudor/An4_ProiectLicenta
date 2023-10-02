using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportYellow2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    public ParticleSystem teleportYellowPS;
    public AudioSource teleportYellowSound;
    public GameObject teleportYellow1;
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
            teleportYellowSound.Play();

            if (teleportYellow1.GetComponent<TeleportYellowScript>().teleport1 == false)
            {
                teleportYellowPS.Play();

                teleport2 = true;

                character.transform.position = new Vector3(teleportYellow1.transform.position.x,
                                                           teleportYellow1.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportYellowPS.Stop();

                teleportYellow1.GetComponent<TeleportYellowScript>().teleport1 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportYellowPS.Play();
    }
}
