using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportYellow2Level2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportYellowPS;
    public AudioSource teleportYellowSound;
    public GameObject teleportYellow1;
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
            teleportYellowSound.Play();

            if (teleportYellow1.GetComponent<TeleportYellowLevel2Script>().teleport1 == false)
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

                teleportYellow1.GetComponent<TeleportYellowLevel2Script>().teleport1 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportYellowPS.Play();
    }
}
