using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class TeleportYellowLevel2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public ParticleSystem teleportYellowPS;
    public AudioSource teleportYellowSound;
    public GameObject teleportYellow2;
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

            if (teleportYellow2.GetComponent<TeleportYellow2Level2Script>().teleport2 == false)
            {
                teleportYellowPS.Play();

                teleport1 = true;

                character.transform.position = new Vector3(teleportYellow2.transform.position.x,
                                                           teleportYellow2.transform.position.y,
                                                           character.transform.position.z);
            }
            else
            {
                teleportYellowPS.Stop();

                teleportYellow2.GetComponent<TeleportYellow2Level2Script>().teleport2 = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.CreateParticles();

        teleportYellowPS.Play();
    }
}
