using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportRedScript : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    public ParticleSystem teleportRedPS;
    public AudioSource teleportRedSound;
    public GameObject teleportRed2;
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
            teleportRedSound.Play();

            if (teleportRed2.GetComponent<TeleportRed2Script>().teleport2 == false)
            {
                teleportRedPS.Play();

                teleport1 = true;

                character.transform.position = new Vector3(teleportRed2.transform.position.x,
                                                           teleportRed2.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportRedPS.Stop();

                teleportRed2.GetComponent<TeleportRed2Script>().teleport2 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportRedPS.Play();
    }
}
