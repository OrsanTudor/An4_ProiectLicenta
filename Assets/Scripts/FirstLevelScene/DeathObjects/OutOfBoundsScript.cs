using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class OutOfBoundsScript : MonoBehaviour
{
    //Variabile:
    private CharacterScript character;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript>();
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

