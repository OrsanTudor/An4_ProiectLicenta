using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformTileScript : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel1 character;

    //Functii predefinite:
    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel1>();
    }

    //Update:
    void Update()
    {
    }

    //Functii noi:

    //Enter in tiles:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             && (character.canIcon1 == false
             || character.canIcon2 == false
             || character.canIcon3 == false)
             )
        {
            //Nothing
        }
    }

    //Stay in tiles:
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && (character.canIcon1 == false
             || character.canIcon2 == false
             || character.canIcon3 == false
           )
            && character.transform.position.y > transform.position.y + 3f
           )
        {
            //Nothing
        }
    }
}
