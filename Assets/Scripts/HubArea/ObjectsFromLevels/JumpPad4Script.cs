using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class JumpPad4Script : MonoBehaviour
{
    //Variabile:
    private float jumpPadStrength;
    private CharacterScript4 character;
    public Animator animator;
    public AudioSource jumpPadSound;

    //Functii predefinite:

    //Start:
    void Start()
    {
        jumpPadStrength = 400;

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript4>();
    }

    //Update:
    void Update()
    {
    }

    //Functii noi:

    //Cand caracterul intra in contact cu jump pad-ul, il arunca in sus:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             && CharacterNotUnderJumpPad() 
             )
        {
            animator.SetTrigger("entry");

            jumpPadSound.Play();

            character.myRigidbody.velocity = new Vector2(0, 0);
            character.myRigidbody.gravityScale = character.originalGravityJumpDown;

            collision.gameObject.GetComponent<Rigidbody2D>()
                .AddForce(Vector2.up * jumpPadStrength,
                ForceMode2D.Impulse);

            character.CreateParticles();
        }
    }

    //Verific pozitia caracterului fata de pozitia jump pad-ului;
    private bool CharacterNotUnderJumpPad()
    {
        int distanceFromJumpPad = +2;

        if (character.transform.position.y < transform.position.y + distanceFromJumpPad)
        {
            return false;
        }

        return true;
    }
}
