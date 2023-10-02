using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportGrey2Level2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportGreyPS;
    public AudioSource teleportGreySound;
    public GameObject teleportGrey;
    public GameObject teleportGrey3;
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
            teleportGreySound.Play();

            if (teleportGrey.GetComponent<TeleportGreyLevel2Script>().teleport1 == false)
            {
                teleportGreyPS.Play();

                teleport2 = true;

                character.transform.position = new Vector3(teleportGrey3.transform.position.x,
                                                           teleportGrey3.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportGreyPS.Stop();

                teleportGrey.GetComponent<TeleportGreyLevel2Script>().teleport1 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportGreyPS.Play();
    }
}
