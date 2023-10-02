using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterScript3 : MonoBehaviour
{
    //Variabile:
    public Rigidbody2D myRigidbody;
    public Transform groundUnder;
    public LayerMask groundLayer;
    public LogicManagerLevel3 logicManagerLevel3;
    public FollowCamera3 followCamera3;
    private float deathStrength;
    public bool charState;
    private Tutorial3Script tutorial3Script;
    public Animator animator;
    public Animator icon1Animator;
    public Animator icon2Animator;
    public Animator icon3Animator;
    public ParticleSystem particleSystem;
    public SpriteRenderer jetpack;

    //Movement:
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
    public bool canDashRight;
    public int powerUp1;
    private bool characterDashingRight;
    private float dashRightStrength;
    private float dashRightTime;
    private float dashRightCooldown;
    public TrailRenderer trailRendererRight;
    private Text icon1Uses;
    private Image icon1Background;
    //Icon 2:
    public bool canGravity;
    public int powerUp2;
    private bool characterGravity;
    private float gravityTime;
    private float gravityCooldown;
    private float gravityStrenght;
    public TrailRenderer trailRendererGravity;
    private Text icon2Uses;
    private Image icon2Background;
    //Icon 3:
    public bool canDashLeft;
    public int powerUp3;
    private bool characterDashingLeft;
    private float dashLeftStrength;
    private float dashLeftTime;
    private float dashLeftCooldown;
    public TrailRenderer trailRendererLeft;
    private Text icon3Uses;
    private Image icon3Background;

    public float originalGravityJumpDown;
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
    public AudioSource noIcon2;
    public AudioSource noIcon3;

    public int lastCheckpointHit;
    public bool oneTimeRotate;

    //Functii predefinite:

    //Start:
    void Start()
    {
        charState = true;
        facingLeft = true;
        oneTimeRotate = true;

        logicManagerLevel3 = GameObject.FindGameObjectWithTag("LogicManagerLevel3")
            .GetComponent<LogicManagerLevel3>();

        lastCheckpointHit = PlayerPrefs.GetInt("LastCheckpoint");
        if (lastCheckpointHit == 0)
        {
            logicManagerLevel3.RestartPlayerLevel();

            oneTimeRotate = false;
        }
        else if (lastCheckpointHit == 1)
        {
            transform.position = new
            Vector3(
            PlayerPrefs.GetFloat("PozitieX_Checkpoint1"),
            PlayerPrefs.GetFloat("PozitieY_Checkpoint1"),
            transform.position.z
            );

            oneTimeRotate = false;
        }
        else if (lastCheckpointHit == 2)
        {
            transform.position = new
            Vector3(
            PlayerPrefs.GetFloat("PozitieX_Checkpoint2"),
            PlayerPrefs.GetFloat("PozitieY_Checkpoint2"),
            transform.position.z
            );

            transform.RotateAround(transform.position, new Vector3(0, 1, 0), 180);
        }

        animator.SetFloat("characterSpeed", 0);
        animator.SetBool("characterJumping", false);
        animator.SetBool("characterCrouching", false);
        animator.SetBool("characterDeath", false);

        jetpack.enabled = false;

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

        followCamera3 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCamera3>();

        tutorial3Script = GameObject.FindGameObjectWithTag("TutorialLevel1")
            .GetComponent<Tutorial3Script>();

        icon1Background = GameObject.FindGameObjectWithTag("UseIcon1Background").GetComponent<Image>();

        icon1Uses = GameObject.FindGameObjectWithTag("UseIcon1").GetComponent<Text>();

        if (lastCheckpointHit == 0)
        {
            powerUp1 = 2;
            powerUp2 = 1;
            powerUp3 = 2;
        }
        else if (lastCheckpointHit == 1)
        {
            powerUp1 = PlayerPrefs.GetInt("PowerUp1_Checkpoint1");
            powerUp2 = PlayerPrefs.GetInt("PowerUp2_Checkpoint1");
            powerUp3 = PlayerPrefs.GetInt("PowerUp3_Checkpoint1");
        }
        else if (lastCheckpointHit == 2)
        {
            powerUp1 = PlayerPrefs.GetInt("PowerUp1_Checkpoint2");
            powerUp2 = PlayerPrefs.GetInt("PowerUp2_Checkpoint2");
            powerUp3 = PlayerPrefs.GetInt("PowerUp3_Checkpoint2");
        }

        beginJump = false;
        endJump = false;

        canDashRight = true;
        icon1Uses.text = "Remaining: \n";
        
        for (int i = 0; i < powerUp1; i++)
        {
            icon1Uses.text = icon1Uses.text + " | ";
        }
        if (powerUp1 == 0)
        {
            icon1Uses.text = icon1Uses.text + " - ";
        }
        characterDashingRight = false;
        dashRightTime = 0.3f;
        dashRightCooldown = 0.15f;
        dashRightStrength = 15;

        icon2Background = GameObject.FindGameObjectWithTag("UseIcon2Background").GetComponent<Image>();

        icon2Uses = GameObject.FindGameObjectWithTag("UseIcon2").GetComponent<Text>();

        canGravity = true;
        icon2Uses.text = "Remaining: \n";
        
        for (int i = 0; i < powerUp2; i++)
        {
            icon2Uses.text = icon2Uses.text + " | ";
        }
        if (powerUp2 == 0)
        {
            icon2Uses.text = icon2Uses.text + " - ";
        }
        
        characterGravity = false;
        gravityTime = 5f;
        gravityCooldown = 0.25f;
        gravityStrenght = 5;

        icon3Background = GameObject.FindGameObjectWithTag("UseIcon3Background").GetComponent<Image>();

        icon3Uses = GameObject.FindGameObjectWithTag("UseIcon3").GetComponent<Text>();

        canDashLeft = true;
        icon3Uses.text = "Remaining: \n";
        
        for (int i = 0; i < powerUp3; i++)
        {
            icon3Uses.text = icon3Uses.text + " | ";
        }
        if (powerUp3 == 0)
        {
            icon3Uses.text = icon3Uses.text + " - ";
        }

        characterDashingLeft = false;
        dashLeftTime = 0.3f;
        dashLeftCooldown = 0.15f;
        dashLeftStrength = 15;
    }

    void Update()
    {
        if (canDashRight == false
            || canDashLeft == false
            || logicManagerLevel3.gameIsPaused == true
            || tutorial3Script.showingTutorial == true
            || logicManagerLevel3.startOfTransition == true
            || logicManagerLevel3.endOfTransition == true)
        {
            return;
        }

        if (lastCheckpointHit == 0 || lastCheckpointHit == 1)
        {
            horizontalPosition = Input.GetAxisRaw("Horizontal");
        }
        else if(lastCheckpointHit == 2)
        {
            horizontalPosition = Input.GetAxisRaw("Horizontal");
        }

        if (canGravity == false)
        {
            verticalPosition = Input.GetAxisRaw("Vertical");
        }

        animator.SetFloat("characterSpeed", Mathf.Abs(horizontalPosition));

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

            if (myRigidbody.gravityScale < maxGravity)
            {
                myRigidbody.gravityScale = myRigidbody.gravityScale + gravityDownU;
            }

            coyoteTimeCounter = 0;

            jumpStrength = originalJumpStrength;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)
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

        if (PlayerIsGrounded()
            && canGravity == true)
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
        else if (canGravity == true)
        {
            animator.SetBool("characterJumping", true);

            coyoteTimeCounter = coyoteTimeCounter - Time.deltaTime;
            movementSpeed = 65;

            if (myRigidbody.velocity.y < 0 && myRigidbody.gravityScale < maxGravity)
            {
                myRigidbody.gravityScale = myRigidbody.gravityScale + gravityDownD;
            }
        }

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) &&
            (powerUp1 > 0) && charState == true
            && facingLeft == true
            && canGravity == true)
        {
            CreateParticles();

            dashRL.Play();

            animator.SetBool("characterJumping", true);

            followCamera3.startShake = true;

            StartCoroutine(DashRight());
            powerUp1 = powerUp1 - 1;
            
            icon1Uses.text = "Remaining: " + "\n";
            for (int i = 0; i < powerUp1; i++)
            {
                icon1Uses.text = icon1Uses.text + " | ";
            }
            if (powerUp1 == 0)
            {
                icon1Uses.text = icon1Uses.text + " - ";
            }
        }
        else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) &&
            (powerUp1 == 0) && charState == true
            && facingLeft == true
            && canGravity == true)
        {
            icon1Background.enabled = false;
            noIcon.Play();
            StartCoroutine(WaitTime(1f));
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2))
            && (powerUp2 > 0) && charState == true)
        {
            jetpack.enabled = true;

            CreateParticles();

            noGravity.Play();

            animator.SetBool("characterJumping", true);

            followCamera3.startShake = true;

            StartCoroutine(NoGravity());
            powerUp2 = powerUp2 - 1;

            icon2Uses.text = "Remaining: " + "\n";
            for (int i = 0; i < powerUp2; i++)
            {
                icon2Uses.text = icon2Uses.text + " | ";
            }
            if (powerUp2 == 0)
            {
                icon2Uses.text = icon2Uses.text + " - ";
            }
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2)) &&
            (powerUp2 == 0) && charState == true)
        {
            icon2Background.enabled = false;
            noIcon2.Play();
            StartCoroutine(WaitTime2(1f));
        }

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3))
            && (powerUp3 > 0) && charState == true
            && facingLeft == false
            && canGravity == true)
        {
            CreateParticles();

            dashRL.Play();

            animator.SetBool("characterJumping", true);

            followCamera3.startShake = true;

            StartCoroutine(DashLeft());
            powerUp3 = powerUp3 - 1;

            icon3Uses.text = "Remaining: " + "\n";
            for (int i = 0; i < powerUp3; i++)
            {
                icon3Uses.text = icon3Uses.text + " | ";
            }
            if (powerUp3 == 0)
            {
                icon3Uses.text = icon3Uses.text + " - ";
            }
        }
        else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3)) &&
            (powerUp3 == 0) && charState == true
            && facingLeft == false
            && canGravity == true)
        {
            icon3Background.enabled = false;
            noIcon3.Play();
            StartCoroutine(WaitTime3(1f));
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
                                           (transform.localScale.y / 5) * dashRightStrength); 

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

        trailRendererGravity.emitting = true;
        icon2Animator.SetTrigger("playIcon2");

        yield return new WaitForSeconds(gravityTime);

        trailRendererGravity.emitting = false;
        myRigidbody.gravityScale = originalGravity;
        characterGravity = false;

        yield return new WaitForSeconds(gravityCooldown);

        canGravity = true;

        noGravity.Stop();

        jetpack.enabled = false;

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

        trailRendererLeft.emitting = true;
        icon3Animator.SetTrigger("playIcon3");

        yield return new WaitForSeconds(dashLeftTime);

        trailRendererLeft.emitting = false;
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

    //Pentru miscare pe orizontala si verticala:
    private void FixedUpdate()
    {
        if (charState == true
            && canDashRight == true
            && canDashLeft == true
            && tutorial3Script.showingTutorial == false
            && logicManagerLevel3.startOfTransition == false
            && logicManagerLevel3.endOfTransition == false)
        {
            myRigidbody.velocity = new Vector2(horizontalPosition
                * movementSpeed, myRigidbody.velocity.y);
        }

        if (charState == true && canGravity == false)
        {
            myRigidbody.velocity = new Vector2(
                myRigidbody.velocity.x,
                verticalPosition * movementSpeed);
        }
    }

    //Flip player:
    private void FlipPlayer()
    {
        if ((facingLeft == true && horizontalPosition < 0 && charState == true) ||
            (facingLeft == false && horizontalPosition > 0 && charState == true))
        {
            RotateOneTime();

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

    //Character death:
    public void CharacterDeath()
    {
        if (charState == true)
        {
            death.Play();
            logicManagerLevel3.level1Music.Stop();

            logicManagerLevel3.pause.Play();

            animator.SetBool("characterDeath", true);
        }

        charState = false;
        CharacterIsDead();

        logicManagerLevel3.RestartSceneCollision();
        logicManagerLevel3.SelectTryAgainLevel1Button();
        logicManagerLevel3.SelectExitGameOverLevel1Button();
    }

    //Moarte caracter:
    private void CharacterIsDead()
    {
        myRigidbody.velocity = new Vector2(0, 0);
    }

    //Wait for some time: (icon 1)
    private IEnumerator WaitTime(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon1Background.enabled = true;
    }

    //Wait for some time: (icon 2)
    private IEnumerator WaitTime2(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon2Background.enabled = true;
    }

    //Wait for some time: (icon 3)
    private IEnumerator WaitTime3(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon3Background.enabled = true;
    }

    //Rotate one time:
    private void RotateOneTime()
    {
        if (oneTimeRotate == true)
        {
            oneTimeRotate = false;

            transform.RotateAround(transform.position, new Vector3(0, 1, 0), 180);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
