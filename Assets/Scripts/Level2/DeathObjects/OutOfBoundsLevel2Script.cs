using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OutOfBoundsLevel2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();
    }

    void Update()
    {
    }

    //Functii noi:

    //Moarte daca atingi trigger:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }
}

