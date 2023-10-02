using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportRed2Level2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportRedPS;
    public AudioSource teleportRedSound;
    public GameObject teleportRed1;
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

    //Teleport catre celalalt teleport:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && character.canIcon1
            && character.canIcon2
            && character.canIcon3
            )
        {
            teleportRedSound.Play();

            if (teleportRed1.GetComponent<TeleportRedLevel2Script>().teleport1 == false)
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

                teleportRed1.GetComponent<TeleportRedLevel2Script>().teleport1 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportRedPS.Play();
    }
}
