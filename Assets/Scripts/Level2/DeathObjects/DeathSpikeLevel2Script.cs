using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DeathSpikeLevel2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer.isVisible)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
    }

    //Functii noi:

    //Moarte daca atingi triggerul:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }
}
