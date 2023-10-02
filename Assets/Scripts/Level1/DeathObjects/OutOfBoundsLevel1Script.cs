using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OutOfBoundsLevel1Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel1 character;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel1>();
    }

    void Update()
    {

    }

    //Functii noi:

    //Moare atunci cand sta out of bounds:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && character.canIcon1 == true
            && character.canIcon2 == true
            && character.canIcon3 == true
            )
        {
            character.CharacterDeath();
        }
    }

    //Moare cand intra, sau cand sta;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && character.canIcon1 == true
            && character.canIcon2 == true
            && character.canIcon3 == true
            )
        {
            character.CharacterDeath();
        }
    }
}

