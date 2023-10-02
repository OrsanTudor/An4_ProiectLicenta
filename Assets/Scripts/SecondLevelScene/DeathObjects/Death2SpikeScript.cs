using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Death2SpikeScript : MonoBehaviour
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }
}
