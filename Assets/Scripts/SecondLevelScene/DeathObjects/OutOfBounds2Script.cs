using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class OutOfBounds2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript2 character;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript2>();
    }

    void Update()
    {
    }

    //Functii noi:

    //Cand face coliziune caracterul cu acest obiect, moare caracterul:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }
}

