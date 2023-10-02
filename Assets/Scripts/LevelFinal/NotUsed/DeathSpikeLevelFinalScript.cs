using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DeathSpikeLevelFinalScript : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevelFinal character;

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

    //Mori cand atingi triggerul:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }
}
