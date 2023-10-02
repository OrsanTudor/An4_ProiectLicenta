using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PandaScript : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevelFinal character;
    public AudioSource pandaNoise1;
    public AudioSource pandaNoise2;
    public AudioSource pandaNoise3;
    public AudioSource pandaNoise4;
    public AudioSource pandaNoise5;
    public AudioSource pandaNoise6;
    public AudioSource pandaNoise7;
    public AudioSource pandaNoise8;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevelFinal>();
    }

    void Update()
    {
    }

    //Functii noi:

    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            if (gameObject.name == "FinalPanda1")
            {
               pandaNoise1.Play();
            }
            else if (gameObject.name == "FinalPanda2")
            {
                pandaNoise2.Play();
            }
            else if (gameObject.name == "FinalPanda3")
            {
                pandaNoise3.Play();
            }
            else if (gameObject.name == "FinalPanda4")
            {
                pandaNoise4.Play();
            }
            else if (gameObject.name == "FinalPanda5")
            {
                pandaNoise5.Play();
            }
            else if (gameObject.name == "FinalPanda6")
            {
                pandaNoise6.Play();
            }
            else if (gameObject.name == "FinalPanda7")
            {
                pandaNoise7.Play();
            }
            else if (gameObject.name == "FinalPanda8")
            {
                pandaNoise8.Play();
            }
        }
    }
}

