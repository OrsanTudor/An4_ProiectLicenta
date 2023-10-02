using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportRed2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    public ParticleSystem teleportRedPS;
    public AudioSource teleportRedSound;
    public GameObject teleportRed1;
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
            teleportRedSound.Play();

            if(teleportRed1.GetComponent<TeleportRedScript>().teleport1 == false)
            {
                teleportRedPS.Play();

                teleport2 = true;

                character.transform.position = new Vector3(teleportRed1.transform.position.x,
                                                           teleportRed1.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportRedPS.Stop();

                teleportRed1.GetComponent<TeleportRedScript>().teleport1 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportRedPS.Play();
    }
}
