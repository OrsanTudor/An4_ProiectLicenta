using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportGreyLevel2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportGreyPS;
    public AudioSource teleportGreySound;
    public GameObject teleportGrey3;
    public GameObject teleportGrey2;
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
            teleportGreySound.Play();

            if (teleportGrey3.GetComponent<TeleportGrey3Level2Script>().teleport3 == false)
            {
                teleportGreyPS.Play();

                teleport1 = true;

                character.transform.position = new Vector3(teleportGrey2.transform.position.x,
                                                           teleportGrey2.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportGreyPS.Stop();

                teleportGrey3.GetComponent<TeleportGrey3Level2Script>().teleport3 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportGreyPS.Play();
    }
}
