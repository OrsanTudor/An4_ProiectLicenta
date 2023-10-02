using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterScript : MonoBehaviour
{
    //Variabile:
    public Rigidbody2D myRigidbody;
    public Transform groundUnder;
    public LayerMask groundLayer;
    public LogicManagerLevel1 logicManagerLevel1;
    public FollowCamera followCamera;
    private float deathStrength;
    public bool charState;
    private TutorialScript tutorialScript;
    public Animator animator;
    public Animator icon1Animator;
    public ParticleSystem particleSystem;

    private float horizontalPosition;
    private float verticalPosition;
    private float movementSpeed;
    private float originalJumpStrength;
    private float jumpStrength;
    private bool facingLeft;
    private bool beginJump;
    private bool endJump;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    //Icon 1:
    private bool canDashRight;
    private int powerUp1;
    private bool characterDashingRight;
    private float dashRightStrength;
    private float dashRightTime;
    private float dashRightCooldown;
    public TrailRenderer trailRendererRight;
    private Text icon1Uses;
    private Image icon1Background;

    //Icon 2:
    private bool canGravity;
    private int powerUp2;
    private bool characterGravity;
    private float gravityTime;
    private float gravityCooldown;
    private float gravityStrenght;

    //Icon 3:
    private bool canDashLeft;
    private int powerUp3;
    private bool characterDashingLeft;
    private float dashLeftStrength;
    private float dashLeftTime;
    private float dashLeftCooldown;

    private float originalGravityJumpDown;
    private float gravityDownD;
    private float gravityDownU;
    private float gravityDownArrow;
    private float maxGravity;

    //Sound effects:
    public AudioSource dashRL;
    public AudioSource death;
    public AudioSource jump;
    public AudioSource noGravity;
    public AudioSource noIcon;


    //Functii predefinite:
    //Start:
    void Start()
    {
        animator.SetFloat("characterSpeed", 0);
        animator.SetBool("characterJumping", false);
        animator.SetBool("characterCrouching", false);
        animator.SetBool("characterDeath", false);

        charState = true;
        facingLeft = true;

        originalGravityJumpDown = myRigidbody.gravityScale;
        gravityDownD = 0.06f;
        gravityDownU = 0.8f;
        gravityDownArrow = 1f;
        maxGravity = 40;

        originalJumpStrength = 100;
        jumpStrength = 100; 
        deathStrength = 150;
        movementSpeed = 90;
        charState = true;

        logicManagerLevel1 = GameObject.FindGameObjectWithTag("LogicManagerLevel1")
            .GetComponent<LogicManagerLevel1>();

        followCamera = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCamera>();

        tutorialScript = GameObject.FindGameObjectWithTag("TutorialLevel1")
            .GetComponent<TutorialScript>();

        icon1Background = GameObject.FindGameObjectWithTag("UseIcon1Background").GetComponent<Image>();

        icon1Uses = GameObject.FindGameObjectWithTag("UseIcon1").GetComponent<Text>();

        beginJump = false;
        endJump = false;

        //Pentru icons:
        canDashRight = true;
        icon1Uses.text = "Remaining: \n | |";
        powerUp1 = 2; 
        characterDashingRight = false;
        dashRightTime = 0.3f;
        dashRightCooldown = 0.15f;
        dashRightStrength = 15;

        canGravity = true;
        powerUp2 = 0; 
        characterGravity = false;
        gravityTime = 3f;
        gravityCooldown = 0.25f;
        gravityStrenght = 5;

        canDashLeft = true;
        powerUp3 = 0;
        characterDashingLeft = false;
        dashLeftTime = 0.3f;
        dashLeftCooldown = 0.15f;
        dashLeftStrength = 15;
    }

    //Update:
    void Update()
    {
        //Daca nu se pot executa miscarile uzuale ale caracterului:
        if(canDashRight == false 
            || canDashLeft == false
            || logicManagerLevel1.gameIsPaused == true
            || tutorialScript.showingTutorial == true
            || logicManagerLevel1.startOfTransition == true
            || logicManagerLevel1.endOfTransition == true)
        {
            return;
        }

        horizontalPosition = Input.GetAxisRaw("Horizontal");

        if (canGravity == false)
        {
            verticalPosition = Input.GetAxisRaw("Vertical");
        }

        animator.SetFloat("characterSpeed", Mathf.Abs(horizontalPosition));

        //Jump:
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0 
            && charState == true
            && canGravity == true)
        {
            CreateParticles();

            StartCoroutine(JumpStart());
            beginJump = true;

            jump.Play();

            myRigidbody.velocity = new Vector2(transform.localScale.x, jumpStrength);
        }
        if (Input.GetKeyUp(KeyCode.Space) 
            && myRigidbody.velocity.y > 0
            && charState == true
            && canGravity == true)
        {
            myRigidbody.velocity = new Vector2(transform.localScale.x, myRigidbody.velocity.y * 0.3f); 

            if(myRigidbody.gravityScale < maxGravity)
            {
                myRigidbody.gravityScale = myRigidbody.gravityScale + gravityDownU;
            }

            coyoteTimeCounter = 0;

            jumpStrength = originalJumpStrength;
        }

        //Crouch and not Crouched:
        if(Input.GetKeyDown(KeyCode.DownArrow)
            && charState == true
            && canGravity == true)
        {
            animator.SetBool("characterCrouching", true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)
            && charState == true
            && canGravity == true)
        {
            animator.SetBool("characterCrouching", false);
        }

        //Incepe de la grounded 0.2 si scade de acolo daca nu esti grounded:
        if (PlayerIsGrounded()
            && canGravity == true)
        {
            if(animator.GetFloat("characterSpeed") > 0.01)
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
        else if (canGravity == true)
        {
            animator.SetBool("characterJumping", true);

            coyoteTimeCounter = coyoteTimeCounter - Time.deltaTime;
            movementSpeed = 65;

            if(myRigidbody.velocity.y < 0 && myRigidbody.gravityScale < maxGravity)
            {
                myRigidbody.gravityScale = myRigidbody.gravityScale + gravityDownD;
            }
        }

        //Puterea 1:
        if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) && 
            (powerUp1 > 0) && charState == true
            && facingLeft == true
            && canGravity == true) 
        {
            CreateParticles();

            dashRL.Play();

            animator.SetBool("characterJumping", true);

            followCamera.startShake = true;

            StartCoroutine(DashRight());
            powerUp1 = powerUp1 - 1;
            icon1Uses.text = "Remaining: " + "\n";
            for(int i = 0; i < powerUp1; i++)
            {
                icon1Uses.text = icon1Uses.text + " | ";
            }
            if(powerUp1 == 0)
            {
                icon1Uses.text = icon1Uses.text + " - ";
            }
        }
        else if((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) &&
            (powerUp1 == 0) && charState == true
            && facingLeft == true
            && canGravity == true)
        {
            icon1Background.enabled = false;
            noIcon.Play();
            StartCoroutine(WaitTime(1f));
        }

        //Puterea 2:
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2))
            && (powerUp2 > 0) && charState == true)
        {
            CreateParticles();

            noGravity.Play();

            animator.SetBool("characterJumping", true);

            followCamera.startShake = true;

            StartCoroutine(NoGravity());
            powerUp2 = powerUp2 - 1;

            noGravity.Stop();
        }

        //Puterea 3:
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3))
            && (powerUp3 > 0) && charState == true
            && facingLeft == false
            && canGravity == true) 
        {
            CreateParticles();

            dashRL.Play();

            animator.SetBool("characterJumping", true);

            followCamera.startShake = true;

            StartCoroutine(DashLeft());
            powerUp3 = powerUp3 - 1;
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

    //Pentru cand faci dash right:
    private IEnumerator DashRight()
    {
        canDashRight = false;
        characterDashingRight = true;

        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;

        myRigidbody.velocity = new Vector2((transform.localScale.x / 2) * dashRightStrength,
                                           (transform.localScale.y / 5)  * dashRightStrength); 

        trailRendererRight.emitting = true;
        icon1Animator.SetTrigger("playIcon1");

        yield return new WaitForSeconds(dashRightTime);

        trailRendererRight.emitting = false;
        myRigidbody.gravityScale = originalGravity;
        characterDashingRight = false;

        yield return new WaitForSeconds(dashRightCooldown);

        canDashRight = true;

        Debug.Log("Dash right.");
    }

    //Pentru cand faci no gravity:
    private IEnumerator NoGravity()
    {
        canGravity = false;
        characterGravity = true;

        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0; 

        myRigidbody.velocity = new Vector2(0, 0); 

        yield return new WaitForSeconds(gravityTime);

        myRigidbody.gravityScale = originalGravity;
        characterGravity = false;

        yield return new WaitForSeconds(gravityCooldown);

        canGravity = true;

        Debug.Log("No gravity.");
    }

    //Pentru cand faci dash left:
    private IEnumerator DashLeft()
    {
        canDashLeft = false;
        characterDashingLeft = true;

        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;

        myRigidbody.velocity = new Vector2((transform.localScale.x / 2) * dashLeftStrength,
                                           (transform.localScale.y / 5) * dashLeftStrength);

        yield return new WaitForSeconds(dashLeftTime);

        myRigidbody.gravityScale = originalGravity;
        characterDashingLeft = false;

        yield return new WaitForSeconds(dashLeftCooldown);

        canDashLeft = true;

        Debug.Log("Dash left.");
    }

    //Pentru grounded: You are allowed to jump if touched ground:
    private bool PlayerIsGrounded()
    {
        return Physics2D.OverlapCircle(groundUnder.position, 0.33f, groundLayer);
    }

    //Pentru miscare stanga / dreapta / sus / jos;
    private void FixedUpdate()
    {
        if(charState == true 
            && canDashRight == true 
            && canDashLeft == true
            && tutorialScript.showingTutorial == false
            && logicManagerLevel1.startOfTransition == false
            && logicManagerLevel1.endOfTransition == false)
        {
            myRigidbody.velocity = new Vector2(horizontalPosition
                * movementSpeed, myRigidbody.velocity.y);
        }

        if(charState == true && canGravity == false)
        {
            myRigidbody.velocity = new Vector2(
                myRigidbody.velocity.x,
                verticalPosition * movementSpeed);
        }
    }

    //Flip player:
    private void FlipPlayer()
    {
        if((facingLeft == true && horizontalPosition < 0 && charState == true) || 
            (facingLeft == false && horizontalPosition > 0 && charState == true))
        {
            facingLeft = !facingLeft;

            Vector3 local = transform.localScale;
            local.x = local.x * (-1);
            transform.localScale = local;
        }
    }

    //Pentru creare particles:
    private void CreateParticles()
    {
        particleSystem.Play();
    }

    //Pentru moarte caracter:
    public void CharacterDeath()
    {
        if(charState == true)
        {
            death.Play();
            logicManagerLevel1.level1Music.Stop();

            logicManagerLevel1.pause.Play();

            animator.SetBool("characterDeath", true);
        }

        charState = false;
        CharacterIsDead();

        logicManagerLevel1.RestartSceneCollision();
        logicManagerLevel1.SelectTryAgainLevel1Button();
        logicManagerLevel1.SelectExitGameOverLevel1Button();
    }
    private void CharacterIsDead()
    {
        myRigidbody.velocity = new Vector2(0, 0);
    }

    //Wait for some time:
    private IEnumerator WaitTime(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon1Background.enabled = true;
    }

    //Coliziune 2D:
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
