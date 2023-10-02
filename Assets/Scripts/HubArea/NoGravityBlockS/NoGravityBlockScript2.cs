using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class NoGravityBlockScript2 : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
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
            .GetComponent<CharacterScript4>();

        soundOn = false;
        distanceFromObject_x = +120;
        distanceFromObject_y = +30;

        canGravityBlock = true;
    }

    //Update:
    void Update()
    {
        if (character.transform.position.x - transform.position.x < distanceFromObject_x && character.transform.position.x - transform.position.x > -5
            && Math.Abs(character.transform.position.y - transform.position.y) < distanceFromObject_y)
        {
            //Play when near:
            if (soundOn == false)
            {
                noGravityBlockSound.Play();
            }

            //Pentru activare 1 data;
            soundOn = true;
        }
        else
        {
            noGravityBlockSound.Stop();
            soundOn = false;
        }
    }

    //Functii noi:

    //Cand caracterul intra in contact cu no gravity block, il face sa nu aiba gravity:
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             )
        {
            //Velocity 0:
            character.myRigidbody.velocity = new Vector2(0, 0);
            //Gravitatia 0:
            character.myRigidbody.gravityScale = 0;

            //Daca este gravitatia dezactivata:
            canGravityBlock = false;

            //Animatie no gravity: Este cea de jump;
            character.animator.SetBool("characterJumping", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        character.myRigidbody.gravityScale = character.originalGravityJumpDown;
        canGravityBlock = true;

        character.CreateParticles();
    }


    //Comparare pozitie caracter cu no gravity block:
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
