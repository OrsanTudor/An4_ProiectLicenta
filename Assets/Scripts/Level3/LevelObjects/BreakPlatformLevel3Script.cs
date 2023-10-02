using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;



public class BreakPlatformLevel3Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel3 character;
    private LogicManagerLevelThree logicManagerLevel3;
    public AudioSource breakPlatformBlockSoundEnter;
    public AudioSource breakPlatformBlockSoundLeave;
    private Animator animator;
    public bool touchedByPlayer;
    public bool onlyOneStayTouch;

    private float horizontalPosition;
    private float verticalPosition;
    private Rigidbody2D myRigidbody;
    private float movementSpeed;

    private bool playSoundEffectDuringMovementX;
    private bool playSoundEffectDuringMovementY;
    public bool destroyStart;
    private bool startScriptX;
    private bool startScriptY;
    private bool noDragCooldown;


    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel3>();

        logicManagerLevel3 = GameObject.FindGameObjectWithTag("LogicManagerLevelThree")
            .GetComponent<LogicManagerLevelThree>();

        animator = gameObject.GetComponent<Animator>();

        touchedByPlayer = false;
        onlyOneStayTouch = false;

        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        movementSpeed = 60;

        playSoundEffectDuringMovementX = false;
        playSoundEffectDuringMovementY = false;
        destroyStart = false;

        startScriptX = false;
        startScriptY = false;
    }

    //Update:
    void Update()
    {
        if (character.canIcon1 == false
            && character.touchedPlatform == true
            && destroyStart == false 
            )
        {
            horizontalPosition = Input.GetAxisRaw("Horizontal");

            if (horizontalPosition == 0)
            {
                if (playSoundEffectDuringMovementX == true)
                {
                    playSoundEffectDuringMovementX = false;
                    character.icon1SE.Stop();
                }
            }
            else 
            {
                if(myRigidbody.velocity.x == 0)
                {
                    if(startScriptX == true)
                    {
                        //DragCharacter();
                    }
                    else
                    {
                        StartCoroutine(MakeStartScriptXTrue(0.1f));
                    }
                }
                else
                {
                    NoDragCharacter();
                }

                if (playSoundEffectDuringMovementX == false
                    &&logicManagerLevel3.gameIsPaused == false 
                    )
                {
                    playSoundEffectDuringMovementX = true;
                    character.icon1SE.Play();
                }
            }
        }
        else if (character.canIcon2 == false
            && character.touchedPlatform == true
            && destroyStart == false
            )
        {
            verticalPosition = Input.GetAxisRaw("Vertical");

            if (verticalPosition == 0)
            {
                if (playSoundEffectDuringMovementY == true)
                {
                    playSoundEffectDuringMovementY = false;
                    character.icon3SE.Stop();
                }
            }
            else
            {
                if (myRigidbody.velocity.y == 0) 
                {
                    if (startScriptY == true)
                    {
                        //DragCharacter();
                    }
                    else
                    {
                        StartCoroutine(MakeStartScriptYTrue(0.1f));
                    }
                }
                else
                {
                    NoDragCharacter();
                }

                if (playSoundEffectDuringMovementY == false
                    && logicManagerLevel3.gameIsPaused == false 
                    )
                {
                    playSoundEffectDuringMovementY = true;
                    if(true) 
                    {
                        character.icon3SE.Play();
                    }
                }
            }
        }
        else if (character.canIcon3 == false
            && character.touchedPlatform == true
            && destroyStart == false
            )
        {
            horizontalPosition = Input.GetAxisRaw("Horizontal");
            verticalPosition = Input.GetAxisRaw("Vertical");

            if (horizontalPosition == 0)
            {
                if (playSoundEffectDuringMovementX == true)
                {
                    playSoundEffectDuringMovementX = false;
                    character.icon1SE.Stop();
                }
            }
            else 
            {
                if (myRigidbody.velocity.x == 0)
                {
                    if (startScriptX == true)
                    {
                        //DragCharacter();
                    }
                    else
                    {
                        StartCoroutine(MakeStartScriptXTrue(0.1f));
                    }
                }
                else
                {
                    NoDragCharacter();
                }

                if (playSoundEffectDuringMovementX == false
                    && logicManagerLevel3.gameIsPaused == false
                    )
                {
                    playSoundEffectDuringMovementX = true;
                    character.icon1SE.Play();
                }
            }

            if (verticalPosition == 0)
            {
                if (playSoundEffectDuringMovementY == true)
                {
                    playSoundEffectDuringMovementY = false;
                    character.icon3SE.Stop();
                }
            }
            else
            {
                if (myRigidbody.velocity.y == 0)
                {
                    if (startScriptY == true)
                    {
                        //DragCharacter();
                    }
                    else
                    {
                        StartCoroutine(MakeStartScriptYTrue(0.1f));
                    }
                }
                else
                {
                    NoDragCharacter();
                }

                if (playSoundEffectDuringMovementY == false 
                    && logicManagerLevel3.gameIsPaused == false
                    )
                {
                    playSoundEffectDuringMovementY = true;
                    character.icon3SE.Play();
                }
            }
        }
    }

    //Functii noi:

    //Pentru collision, sa se distruga platforma in conditiile corecte:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && touchedByPlayer == false
            && character.canIcon1 == true
            && character.canIcon2 == true
            && character.canIcon3 == true
            && destroyStart == false 
            && TestIfAbove()
            )
        {
            breakPlatformBlockSoundEnter.Play();

            touchedByPlayer = true;

            if(destroyStart == false)
            {
                StartCoroutine(MakePlatformDisappear(1f));
            }
        }
        else if (collision.gameObject.CompareTag("Character")
            && touchedByPlayer == false
            && TestIfAbove()
            )
        {
            breakPlatformBlockSoundEnter.Play();

            touchedByPlayer = true;
        }
        else if (collision.gameObject.CompareTag("Character"))
        {
        }
        else
        {
            noDragCooldown = false;
        }
    }


    //Daca am iesit din puteri, distruge:
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && character.canIcon1 == true
            && character.canIcon2 == true
            && character.canIcon3 == true
            && onlyOneStayTouch == false
            && destroyStart == false 
            && TestIfAbove()
            )
        {
            if (destroyStart == false)
            {
                StartCoroutine(MakePlatformDisappear(1f));
            }

            onlyOneStayTouch = true;
        }
        else if (collision.gameObject.CompareTag("Character"))
        {
        }
        else
        {
            if(character.dragOrNoDrag == true)
            {
                DragCharacter();
            }
        }
    }

    //Mor daca sunt inauntrul obiectului: Un trigger mai mic:
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && touchedByPlayer == false
            )
        {
            character.CharacterDeath();
        }
    }

    //Pentru exit, particule:
    private void OnCollisionExit2D(Collision2D collision)
    {
        character.CreateParticles();

        if (collision.gameObject.CompareTag("Character"))
        {
        }
    }

    //La distrugere refac movement player:
    public IEnumerator MakePlatformDisappear(float timeToWait)
    {
        destroyStart = true;

        yield return new WaitForSeconds(timeToWait);

        if (
            (character.canIcon1 == false
            || character.canIcon2 == false
            || character.canIcon3 == false)
            )
        {
            if (character.canIcon1 == false)
            {
                StartCoroutine(character.Icon1Reset());
            }
            else
            if (character.canIcon2 == false)
            {
                StartCoroutine(character.Icon2Reset());
            }
            else
            if (character.canIcon3 == false)
            {
                StartCoroutine(character.Icon3Reset());
            }
        }

        breakPlatformBlockSoundLeave.Play();

        animator.SetTrigger("entry");

        BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;

        if (gameObject.tag == "BreakPlatform")
        {
            logicManagerLevel3.noPlatform1 = true;
        }
        else if (gameObject.tag == "BreakPlatform1")
        {
            logicManagerLevel3.noPlatform2 = true;
        }
        else if (gameObject.tag == "BreakPlatform2")
        {
            logicManagerLevel3.noPlatform3 = true;
        }
        else if (gameObject.tag == "BreakPlatform3")
        {
            logicManagerLevel3.noPlatform4 = true;
        }
        else if (gameObject.tag == "BreakPlatform4")
        {
            logicManagerLevel3.noPlatform5 = true;
        }
        else if (gameObject.tag == "BreakPlatform5")
        {
            logicManagerLevel3.noPlatform6 = true;
        }
        else if (gameObject.tag == "BreakPlatform6")
        {
            logicManagerLevel3.noPlatform7 = true;
        }
        else if (gameObject.tag == "BreakPlatform7")
        {
            logicManagerLevel3.noPlatform8 = true;
        }
        else if (gameObject.tag == "BreakPlatform8")
        {
            logicManagerLevel3.noPlatform9 = true;
        }
        else if (gameObject.tag == "BreakPlatform9")
        {
            logicManagerLevel3.noPlatform10 = true;
        }
        else if (gameObject.tag == "BreakPlatform10")
        {
            logicManagerLevel3.noPlatform11 = true;
        }
        else if (gameObject.tag == "BreakPlatform11")
        {
            logicManagerLevel3.noPlatform12 = true;
        }
        else
        {
            Debug.Log("Not found!");
        }

        character.myRigidbody.drag = 0;
        character.myRigidbody.gravityScale = 16; 

        Destroy(gameObject, 1f);
    }

    //Doar daca sunt deasupra ei pot sa activez:
    public bool TestIfAbove()
    {
        if(character.transform.position.y > transform.position.y + 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Nu pot apela aceasta functie din character; Trebuie sa testez altcumva pozitiile lor:
    public bool TestIfInPlatform()
    {
        if (Mathf.Abs(character.transform.position.x - transform.position.x) < 10)
        {
            return true;
        }

        return false;
    }

    //Pentru miscarea platformei:
    private void FixedUpdate()
    {
        if (
            (character.canIcon1 == false
            || character.canIcon3 == false)
            && character.touchedPlatform == true
            && destroyStart == false
           )
        {
            //Caz pentru icon 1:
            if(character.canIcon1 == false)
            {
                myRigidbody.velocity = new Vector2(
                horizontalPosition * movementSpeed,
                myRigidbody.velocity.y);

                character.myRigidbody.velocity = myRigidbody.velocity;
            }
            //Caz pentru icon 3:
            else if (character.canIcon3 == false)
            {
                myRigidbody.velocity = new Vector2(
                horizontalPosition * (movementSpeed / 2),
                myRigidbody.velocity.y);

                character.myRigidbody.velocity = myRigidbody.velocity;
            }
        }
        else
        {
        }

        if (
            (character.canIcon2 == false
            || character.canIcon3 == false)
            && character.touchedPlatform == true
            && destroyStart == false
           )
        {
            //Caz pentru icon 2:
            if (character.canIcon2 == false)
            {
                myRigidbody.velocity = new Vector2(
                myRigidbody.velocity.x,
                verticalPosition * movementSpeed);

                character.myRigidbody.velocity = myRigidbody.velocity;
            }
            //Caz pentru icon 3:
            else if (character.canIcon3 == false)
            {
                myRigidbody.velocity = new Vector2(
                myRigidbody.velocity.x,
                verticalPosition * (movementSpeed / 2));

                character.myRigidbody.velocity = myRigidbody.velocity;
            }
        }
        else
        {
        }
    }

    //Freeze character position:
    private void FreezeCharacter()
    {
        character.myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX
                                            | RigidbodyConstraints2D.FreezePositionY
                                            | RigidbodyConstraints2D.FreezeRotation;
    }

    //Unfreeze positions:
    private void UnfreezeCharacter()
    {
        character.myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //No drag for character:
    private void NoDragCharacter()
    {
        character.myRigidbody.drag = 0;
    }

    //Drag for character:
    private void DragCharacter()
    {
        if (noDragCooldown == false)
        {
            //character.myRigidbody.drag = 5000; 
            character.myRigidbody.drag = 0;

            noDragCooldown = true;
            StartCoroutine(NoDragFromDragCharacter());
        }
    }

    //Aceeasi idee, cu delay:
    private IEnumerator NoDragFromDragCharacter()
    {
        yield return new WaitForSeconds(0.3f);
        character.myRigidbody.drag = 0;
    }

    //Aceeasi idee, cu delay:
    private IEnumerator MakeStartScriptXTrue(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        startScriptX = true;
    }

    private IEnumerator MakeStartScriptYTrue(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        startScriptY = true;
    }

    //Se reface velocity doar dupa un timp:
    private IEnumerator WaitForVelocty(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        character.myRigidbody.velocity = myRigidbody.velocity;
    }
}
