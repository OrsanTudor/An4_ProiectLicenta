using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class NoGravityBlockLevel1Script8 : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel1 character;
    public Rigidbody2D rigidbody2D;
    public BoxCollider2D boxCollider2D;
    public ParticleSystem noGravityBlockPS;
    public AudioSource noGravityBlockSound;
    private bool soundOn;
    float distanceFromObject_x;
    float distanceFromObject_y;
    public bool canGravityBlock;

    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel1>();

        soundOn = false;

        //Stanga -30  Dreapta +30  ;
        distanceFromObject_x = +30;
        //Jos    -120  Sus     +10 ;
        distanceFromObject_y = -305;

        canGravityBlock = true;
    }

    //Update:
    void Update()
    {
        if (character.transform.position.y - transform.position.y > distanceFromObject_y
         && character.transform.position.y - transform.position.y < 10
         && Math.Abs(character.transform.position.x - transform.position.x) < distanceFromObject_x)
        {
            if (soundOn == false)
            {
                noGravityBlockSound.Play();
            }

            soundOn = true;
        }
        else
        {
            noGravityBlockSound.Stop();
            soundOn = false;
        }
    }

    //Functii noi:

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             )
        {
            character.myRigidbody.velocity = new Vector2(0, 0);
        }
    }

    //Pentru cand sunt inauntrul blocului, am gravitatie 0;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             )
        {
            character.myRigidbody.gravityScale = 0;

            canGravityBlock = false;

            character.animator.SetBool("characterJumping", true);
        }
    }

    //Se reface gravitatia:
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.myRigidbody.gravityScale = character.originalGravityJumpDown;
            canGravityBlock = true;

            character.CreateParticles();
        }
    }

    //Nu sunt under block (atunci cand vine vorba de pozitie)
    private bool CharacterNotUnderNoGravityBlock()
    {
        int distanceFromNoGravityBlock = +5;

        if (character.transform.position.y < transform.position.y + distanceFromNoGravityBlock)
        {
            return false;
        }

        return true;
    }
}
