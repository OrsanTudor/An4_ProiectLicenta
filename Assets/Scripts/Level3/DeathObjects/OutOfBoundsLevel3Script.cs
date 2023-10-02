using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OutOfBoundsLevel3Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel3 character;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel3>();
    }

    void Update()
    {
    }

    //Functii noi:

    //Mori atunci cand atingi:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }
}

