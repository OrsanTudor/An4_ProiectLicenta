using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DeathSpikeLevel3Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel3 character;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel3>();

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

    //Mori cand atingi:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }
}
