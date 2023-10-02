using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class MovingPlatformScript : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel3 character;
    //private LayerMask layerMask;
    private Animator animator;
    private float theXPosition;
    private float driftX;
    //Pune 0 cateodata pentru ca nu apuca sa dea load la scriptul de character:
    //private float originalJumpStrength;
    //private float currentJumpStrength;

    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel3>();

        animator = GetComponent<Animator>();
        //animator.enabled = false; //Test;

        //originalJumpStrength = character.jumpStrength;
        //currentJumpStrength = character.jumpStrength; // * 5;
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
            && AbovePlatform()
            //&& character.detectJumping == false
            )
        {
            character.myRigidbody.velocity = new Vector2(0, 0);
            character.detectJumping = false; //Pentru inceput; Daca ai sarit inainte;

            driftX = character.transform.position.x - transform.position.x;

            int groundLayer = LayerMask.NameToLayer("GroundLayer");
            //layerMask = groundLayer;
            gameObject.layer = groundLayer;
        }
    }

    //Make movement faster:
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && AbovePlatform()
            )
        {
            //Nu se executa aceasta functie la sarit: Se schimba in true cand vrei sa sari;
            //Daca sari, automat se face false deoarece ai dat exit;
            if(character.detectJumping == true)
            {
                //Y:
                //character.transform.SetParent(transform);
                //character.jumpStrength = currentJumpStrength;

                return;
            }

            Vector2 vectorZero = new Vector2(1, 1);

            //X: Daca nu doresc sa sar, atunci miscare as usual pe x:
            if (
                ((character.facingLeft == true && character.myRigidbody.velocity.x < vectorZero.x)      
                || (character.facingLeft == false && character.myRigidbody.velocity.x > -vectorZero.x))
                && character.detectJumping == false
                )
            {
                //StartCoroutine(ChangePositionOfCharacter());
                theXPosition = transform.position.x;

                character.transform.position =
                    new Vector3(
                        theXPosition + driftX,
                        //transform.position.y + 10.3f,
                        character.transform.position.y,
                        character.transform.position.z
                        );
            }
            else
            {
                if((character.transform.position.x - transform.position.x != driftX)
                    && character.detectJumping == false)
                {
                    driftX = character.transform.position.x - transform.position.x;
                }
            }
            
            //Daca doresc sa sar, prima data sar, dupa care se va reface false:
            //if(character.detectJumping == true)
            //{
            //    character.detectJumping = false;
            //}
        }
    }

    //Particule la iesire:
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
             )
        {
            character.CreateParticles();
            //character.jumpStrength = originalJumpStrength;
            character.detectJumping = false;
        }
    }

    //Daca esti deasupra platformei:
    private bool AbovePlatform()
    {
        if(character.transform.position.y > transform.position.y + 10)
        {
            return true;
        }

        return false;
    }

    //Oprire si pornire animator:
    //Enable:
    public void EnableAnimator()
    {
        animator.enabled = true;
    }

    //Disable:
    public void DisableAnimator()
    {
        animator.enabled = false;
    }

    //Rutina pentru schimbat pozitie caracter:
    //private IEnumerator ChangePositionOfCharacter()
    //{
    //    yield return new WaitForSeconds(0.02f);

    //    theXPosition = transform.position.x;

    //    character.transform.position =
    //        new Vector3(
    //            theXPosition + driftX,
    //            transform.position.y + 10f,
    //            character.transform.position.z
    //            );
    //}
}
