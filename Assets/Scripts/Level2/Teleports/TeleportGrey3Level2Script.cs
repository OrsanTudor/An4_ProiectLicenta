using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportGrey3Level2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportGreyPS;
    public AudioSource teleportGreySound;
    public GameObject teleportGrey2;
    public GameObject teleportGrey;
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
            teleportGreySound.Play();

            if (teleportGrey2.GetComponent<TeleportGrey2Level2Script>().teleport2 == false)
            {
                teleportGreyPS.Play();

                teleport3 = true;

                character.transform.position = new Vector3(teleportGrey.transform.position.x,
                                                           teleportGrey.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportGreyPS.Stop();

                teleportGrey2.GetComponent<TeleportGrey2Level2Script>().teleport2 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportGreyPS.Play();
    }
}
