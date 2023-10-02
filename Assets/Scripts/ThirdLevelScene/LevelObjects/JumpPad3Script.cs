using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class JumpPad3Script : MonoBehaviour
{
    //Variabile:
    private float jumpPadStrength;
    private CharacterScript3 character;
    public Animator animator;
    public AudioSource jumpPadSound;

    //Functii predefinite:

    void Start()
    {
        jumpPadStrength = 400;

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript3>();
    }

    //Update:
    void Update()
    {
    }

    //Functii noi:

    //Pentru cand intru in contact cu jump pad-ul:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             && character.canDashLeft == true
             && character.canDashRight == true
             && character.canGravity == true
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

    //Pentru cand sunt deja in contact cu colliderul:
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             && character.canDashLeft == true
             && character.canDashRight == true
             && character.canGravity == true
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

    //Daca nu ma aflu sub jump pad, return false:
    private bool CharacterNotUnderJumpPad()
    {
        Debug.Log("Character: " + character.transform.position.y);
        Debug.Log("Jump Pad: " + transform.position.y);
        
        int distanceFromJumpPad = +2;

        if(character.transform.position.y < transform.position.y + distanceFromJumpPad)
        {
            return false;
        }

        return true;
    }
}
