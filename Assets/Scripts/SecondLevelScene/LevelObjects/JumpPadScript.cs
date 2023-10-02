using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JumpPadScript : MonoBehaviour
{
    //Variabile:
    private float jumpPadStrength;
    private CharacterScript2 character;
    public Animator animator;
    public AudioSource jumpPadSound;

    //Functii predefinite:

    //Start:
    void Start()
    {
        jumpPadStrength = 620;

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript2>();
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
             && character.canDashLeft == true
             && character.canDashRight == true
             && character.canGravity == true)
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

    //Pentru cand stau pe jump pad, sa se activeze in conditii corecte:
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             && character.canDashLeft == true
             && character.canDashRight == true
             && character.canGravity == true)
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
}
