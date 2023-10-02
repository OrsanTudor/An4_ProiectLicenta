using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DeathSpikeLevel1Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel1 character;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel1>();

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

    //Moare atunci cand atinge spikes:
    private void OnCollisionEnter2D(Collision2D collision)
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
