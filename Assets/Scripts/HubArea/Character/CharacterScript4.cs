using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterScript4 : MonoBehaviour
{
    //Variabile:
    public Rigidbody2D myRigidbody;
    public Transform groundUnder;
    public LayerMask groundLayer;
    public LogicManagerHubArea logicManagerHubArea;
    public FollowCamera4 followCamera4;
    public bool charState;
    public Animator animator;
    public ParticleSystem particleSystem;

    //Movement:
    private float horizontalPosition;
    private float verticalPosition;
    private float movementSpeed;
    private float originalJumpStrength;
    private float jumpStrength;
    private bool facingLeft;
    private bool beginJump;
    private bool endJump;

    //Cat poti sta in aer dupa ce scapi de coliziune:
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    public float originalGravityJumpDown;
    private float gravityDownD;
    private float gravityDownU;
    private float gravityDownArrow;
    private float maxGravity;
    public AudioSource jump;
    private NoGravityBlockScript noGravityBlockScript;
    private NoGravityBlockScript2 noGravityBlockScript2;
    private NoGravityBlockScript3 noGravityBlockScript3;

    //Functii predefinite:

    //Start:
    void Start()
    {
        charState = true;
        facingLeft = true;

        transform.position = new Vector3(PlayerPrefs.GetFloat("CharacterPositionX"),
                                         PlayerPrefs.GetFloat("CharacterPositionY"),
                                         transform.position.z);

        logicManagerHubArea = GameObject.FindGameObjectWithTag("LogicManagerHubArea")
            .GetComponent<LogicManagerHubArea>();

        noGravityBlockScript = GameObject.FindGameObjectWithTag("NoGravityPlatform")
            .GetComponent<NoGravityBlockScript>();
        noGravityBlockScript2 = GameObject.FindGameObjectWithTag("NoGravityPlatform1")
            .GetComponent<NoGravityBlockScript2>();
        noGravityBlockScript3 = GameObject.FindGameObjectWithTag("NoGravityPlatform2")
            .GetComponent<NoGravityBlockScript3>();

        logicManagerHubArea.RestartPlayerLevel();

        animator.SetFloat("characterSpeed", 0);
        animator.SetBool("characterJumping", false);
        animator.SetBool("characterCrouching", false);
        animator.SetBool("characterDeath", false);

        originalGravityJumpDown = myRigidbody.gravityScale;
        gravityDownD = 0.06f;
        gravityDownU = 0.8f;
        gravityDownArrow = 1f;
        maxGravity = 40;
        originalJumpStrength = 100;
        jumpStrength = 100; 
        movementSpeed = 90;
        charState = true;

        followCamera4 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCamera4>();

        beginJump = false;
        endJump = false;
    }

    //Update:
    void Update()
    {
        if (logicManagerHubArea.gameIsPaused == true
            || logicManagerHubArea.startOfTransition == true
            || logicManagerHubArea.endOfTransition == true)
        {
            return;
        }

        horizontalPosition = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("characterSpeed", Mathf.Abs(horizontalPosition));

        if (noGravityBlockScript.canGravityBlock == false
            || noGravityBlockScript2.canGravityBlock == false
            || noGravityBlockScript3.canGravityBlock == false)
        {
            verticalPosition = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0
            && charState == true
            && (noGravityBlockScript.canGravityBlock == true
               && noGravityBlockScript2.canGravityBlock == true
               && noGravityBlockScript3.canGravityBlock == true))
        {
            CreateParticles();

            StartCoroutine(JumpStart());
            beginJump = true;

            jump.Play();

            myRigidbody.velocity = new Vector2(transform.localScale.x, jumpStrength);
        }

        //Jump:
        if (Input.GetKeyUp(KeyCode.Space)
            && myRigidbody.velocity.y > 0
            && charState == true
            && (noGravityBlockScript.canGravityBlock == true
               && noGravityBlockScript2.canGravityBlock == true
               && noGravityBlockScript3.canGravityBlock == true))
        {
            myRigidbody.velocity = new Vector2(transform.localScale.x, myRigidbody.velocity.y * 0.3f); 

            if (myRigidbody.gravityScale < maxGravity)
            {
                myRigidbody.gravityScale = myRigidbody.gravityScale + gravityDownU;
            }

            coyoteTimeCounter = 0;

            jumpStrength = originalJumpStrength;
        }

        //Crouch and not Crouched:
        if (Input.GetKeyDown(KeyCode.DownArrow)
            && charState == true
            && (noGravityBlockScript.canGravityBlock == true
               && noGravityBlockScript2.canGravityBlock == true
               && noGravityBlockScript3.canGravityBlock == true))
        {
            //Animation crounch:
            animator.SetBool("characterCrouching", true);
        }

        //Crouch animation:
        if (Input.GetKeyUp(KeyCode.DownArrow)
            && charState == true
            && (noGravityBlockScript.canGravityBlock == true
               && noGravityBlockScript2.canGravityBlock == true
               && noGravityBlockScript3.canGravityBlock == true))
        {
            //Animation crounch:
            animator.SetBool("characterCrouching", false);
        }

        //Incepe de la grounded 0.2 si scade de acolo daca nu esti grounded:
        if (PlayerIsGrounded()
            && (noGravityBlockScript.canGravityBlock == true
               && noGravityBlockScript2.canGravityBlock == true
               && noGravityBlockScript3.canGravityBlock == true))
        {
            if (animator.GetFloat("characterSpeed") > 0.01)
            {
                CreateParticles();
            }

            if (beginJump == false)
            {
                animator.SetBool("characterJumping", false);
            }

            if (beginJump == true)
            {
                beginJump = false;
            }

            myRigidbody.gravityScale = originalGravityJumpDown;
            coyoteTimeCounter = coyoteTime;
            movementSpeed = 90;
        }
        else if((noGravityBlockScript.canGravityBlock == true
               && noGravityBlockScript2.canGravityBlock == true
               && noGravityBlockScript3.canGravityBlock == true))
        {
            animator.SetBool("characterJumping", true);

            coyoteTimeCounter = coyoteTimeCounter - Time.deltaTime;
            
            movementSpeed = 65;

            if (myRigidbody.velocity.y < 0 && myRigidbody.gravityScale < maxGravity)
            {
                myRigidbody.gravityScale = myRigidbody.gravityScale + gravityDownD;
            }
        }

        FlipPlayer();
    }

    //Functii noi:

    //Asteptare dupa jump:
    private IEnumerator JumpStart()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("characterJumping", true);
    }

    //Pentru grounded: You are allowed to jump if touched ground:
    private bool PlayerIsGrounded()
    {
        return Physics2D.OverlapCircle(groundUnder.position, 0.33f, groundLayer);
    }

    //Pentru miscare caracter:
    private void FixedUpdate()
    {
        if (charState == true
            && logicManagerHubArea.startOfTransition == false
            && logicManagerHubArea.endOfTransition == false)
        {
            myRigidbody.velocity = new Vector2(horizontalPosition
                * movementSpeed, myRigidbody.velocity.y);
        }

        if (charState == true && (noGravityBlockScript.canGravityBlock == false
                                  || noGravityBlockScript2.canGravityBlock == false
                                  || noGravityBlockScript3.canGravityBlock == false))
        {
            myRigidbody.velocity = new Vector2(
                myRigidbody.velocity.x,
                verticalPosition * movementSpeed);
        }
    }

    //Flip player:
    private void FlipPlayer()
    {
        //Conditii pentru flip:
        if ((facingLeft == true && horizontalPosition < 0 && charState == true) 
            || (facingLeft == false && horizontalPosition > 0 && charState == true))
        {
            facingLeft = !facingLeft;

            Vector3 local = transform.localScale;
            local.x = local.x * (-1);
            transform.localScale = local;
        }
    }

    //Pentru creare particles:
    public void CreateParticles()
    {
        particleSystem.Play();
    }
}
