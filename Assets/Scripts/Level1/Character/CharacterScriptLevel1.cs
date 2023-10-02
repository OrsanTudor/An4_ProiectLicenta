using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class CharacterScriptLevel1 : MonoBehaviour
{
    //Variabile:
    public Rigidbody2D myRigidbody;
    public Transform groundUnder;
    public LayerMask groundLayer;
    public LogicManagerLevelOne logicManagerLevelOne;
    public FollowCameraLevel1 followCameraLevel1;
    private float deathStrength;
    public bool charState;
    public Animator animator;
    public Animator icon1Animator;
    public Animator icon2Animator;
    public Animator icon3Animator;
    public ParticleSystem particleSystem;
    //Gravity blocks:
    private NoGravityBlockLevel1Script noGravityBlock1;
    private NoGravityBlockLevel1Script2 noGravityBlock2;
    private NoGravityBlockLevel1Script3 noGravityBlock3;
    private NoGravityBlockLevel1Script4 noGravityBlock4;
    private NoGravityBlockLevel1Script5 noGravityBlock5;
    private NoGravityBlockLevel1Script6 noGravityBlock6;
    private NoGravityBlockLevel1Script7 noGravityBlock7;
    private NoGravityBlockLevel1Script8 noGravityBlock8;
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
    public bool canIcon1;
    public int powerUp1;
    private bool characterIcon1;
    private float icon1Strength;
    private float icon1Time;
    private float icon1Cooldown;
    public TrailRenderer trailRenderer1;
    private Text icon1Uses;
    private Image icon1Background;
    //Icon 2:
    public bool canIcon2;
    public int powerUp2;
    private bool characterIcon2;
    private float icon2Strength;
    private float icon2Time;
    private float icon2Cooldown;
    public TrailRenderer trailRenderer2;
    private Text icon2Uses;
    private Image icon2Background;
    //Icon 3:
    public bool canIcon3;
    public int powerUp3;
    private bool characterIcon3;
    private float icon3Strength;
    private float icon3Time;
    private float icon3Cooldown;
    public TrailRenderer trailRenderer3;
    private Text icon3Uses;
    private Image icon3Background;

    public float originalGravityJumpDown;
    private float gravityDownD;
    private float gravityDownU;
    private float gravityDownArrow;
    private float maxGravity;
    //Sound effects:
    public AudioSource jump;
    public AudioSource death;
    public AudioSource icon1SE;
    public AudioSource icon2SE;
    public AudioSource icon3SE;
    public AudioSource noIcon;
    public AudioSource noIcon2;
    public AudioSource noIcon3;

    public int lastCheckpointHit;
    public bool oneTimeRotate;
    public GameObject touchPlatform;
    private PlatformTileScript touchPlatformScript;

    float velocityXActual;
    float velocityYActual;
    float velocityXMax;
    float velocityYMax;

    //Functii predefinite:

    //Start:
    void Start()
    {
        velocityXActual = 50;
        velocityYActual = 40;
        velocityXMax = 400;
        velocityYMax = 350;

        charState = true;
        facingLeft = true;
        oneTimeRotate = true;

        logicManagerLevelOne = GameObject.FindGameObjectWithTag("LogicManagerLevelOne")
            .GetComponent<LogicManagerLevelOne>();

        //Gravity Blocks:
        noGravityBlock1 = GameObject.FindGameObjectWithTag("NoGravityPlatform")
            .GetComponent<NoGravityBlockLevel1Script>();
        noGravityBlock2 = GameObject.FindGameObjectWithTag("NoGravityPlatform1")
            .GetComponent<NoGravityBlockLevel1Script2>();
        noGravityBlock3 = GameObject.FindGameObjectWithTag("NoGravityPlatform2")
            .GetComponent<NoGravityBlockLevel1Script3>();
        noGravityBlock4 = GameObject.FindGameObjectWithTag("NoGravityPlatform3")
            .GetComponent<NoGravityBlockLevel1Script4>();
        noGravityBlock5 = GameObject.FindGameObjectWithTag("NoGravityPlatform4")
            .GetComponent<NoGravityBlockLevel1Script5>();
        noGravityBlock6 = GameObject.FindGameObjectWithTag("NoGravityPlatform5")
            .GetComponent<NoGravityBlockLevel1Script6>();
        noGravityBlock7 = GameObject.FindGameObjectWithTag("NoGravityPlatform6")
            .GetComponent<NoGravityBlockLevel1Script7>();
        noGravityBlock8 = GameObject.FindGameObjectWithTag("NoGravityPlatform7")
            .GetComponent<NoGravityBlockLevel1Script8>();

        lastCheckpointHit = PlayerPrefs.GetInt("LastCheckpoint"); 

        if (lastCheckpointHit == 0)
        {
            logicManagerLevelOne.RestartPlayerLevel();

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
        }

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
        deathStrength = 150;
        movementSpeed = 90;
        charState = true;

        followCameraLevel1 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCameraLevel1>();

        //Icon 1:
        icon1Background = GameObject.FindGameObjectWithTag("UseIcon1Background").GetComponent<Image>();
        icon1Uses = GameObject.FindGameObjectWithTag("UseIcon1").GetComponent<Text>();

        if (lastCheckpointHit == 0)
        {
            powerUp1 = 8;
            powerUp2 = 2;
            powerUp3 = 7; 
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

        canIcon1 = true;
        icon1Uses.text = "Remaining: \n";

        for (int i = 0; i < powerUp1; i++)
        {
            icon1Uses.text = icon1Uses.text + " | ";
        }
        if (powerUp1 == 0)
        {
            icon1Uses.text = icon1Uses.text + " - ";
        }

        characterIcon1 = false;

        icon1Time = 0.1f;
        icon1Cooldown = 0.2f;
        icon1Strength = 1;

        //Icon 2:
        icon2Background = GameObject.FindGameObjectWithTag("UseIcon2Background").GetComponent<Image>();
        icon2Uses = GameObject.FindGameObjectWithTag("UseIcon2").GetComponent<Text>();

        canIcon2 = true;
        icon2Uses.text = "Remaining: \n";
        
        for (int i = 0; i < powerUp2; i++)
        {
            icon2Uses.text = icon2Uses.text + " | ";
        }
        if (powerUp2 == 0)
        {
            icon2Uses.text = icon2Uses.text + " - ";
        }

        characterIcon2 = false;

        icon2Time = 0.1f;
        icon2Cooldown = 0.2f;
        icon2Strength = 1;

        //Icon 3:
        icon3Background = GameObject.FindGameObjectWithTag("UseIcon3Background").GetComponent<Image>();
        icon3Uses = GameObject.FindGameObjectWithTag("UseIcon3").GetComponent<Text>();

        canIcon3 = true;
        icon3Uses.text = "Remaining: \n";
        
        for (int i = 0; i < powerUp3; i++)
        {
            icon3Uses.text = icon3Uses.text + " | ";
        }
        if (powerUp3 == 0)
        {
            icon3Uses.text = icon3Uses.text + " - ";
        }

        characterIcon3 = false;

        icon3Time = 0.1f;
        icon3Cooldown = 0.2f;
        icon3Strength = 1;

        touchPlatformScript = touchPlatform.GetComponent<PlatformTileScript>();
    }

    //Update:
    void Update()
    {
        if (
            canIcon1 == false
            || canIcon2 == false
            || canIcon3 == false
            || logicManagerLevelOne.gameIsPaused == true
            || logicManagerLevelOne.startOfTransition == true
            || logicManagerLevelOne.endOfTransition == true
            )
        {
            return;
        }

        if (lastCheckpointHit == 0 || lastCheckpointHit == 1)
        {
            horizontalPosition = Input.GetAxisRaw("Horizontal");
        }
        else if (lastCheckpointHit == 2)
        {
            horizontalPosition = Input.GetAxisRaw("Horizontal");
        }

        if (
            (noGravityBlock1.canGravityBlock == false
            || noGravityBlock2.canGravityBlock == false
            || noGravityBlock3.canGravityBlock == false
            || noGravityBlock4.canGravityBlock == false
            || noGravityBlock5.canGravityBlock == false
            || noGravityBlock6.canGravityBlock == false
            || noGravityBlock7.canGravityBlock == false
            || noGravityBlock8.canGravityBlock == false
            )
            )
        {
            verticalPosition = Input.GetAxisRaw("Vertical");
        }

        animator.SetFloat("characterSpeed", Mathf.Abs(horizontalPosition));

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0
            && charState == true
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
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
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
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
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
        {
            animator.SetBool("characterCrouching", true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)
            && charState == true
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
        {
            animator.SetBool("characterCrouching", false);
        }

        if (PlayerIsGrounded()
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
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
        else if (
            (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
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
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            ) 
        {
            CreateParticles();

            icon1SE.Play();

            animator.SetBool("characterJumping", true);

            followCameraLevel1.startShake = true;

            StartCoroutine(Icon1Routine());
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
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
        {
            icon1Background.enabled = false;
            noIcon.Play();
            StartCoroutine(WaitTime(1f));
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2))
            && (powerUp2 > 0) 
            && charState == true
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
        {
            CreateParticles();

            icon2SE.Play();

            animator.SetBool("characterJumping", true);

            followCameraLevel1.startShake = true;

            StartCoroutine(Icon2Routine());
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
            (powerUp2 == 0) 
            && charState == true
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
        {
            icon2Background.enabled = false;
            noIcon2.Play();
            StartCoroutine(WaitTime2(1f));
        }

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3))
            && (powerUp3 > 0) && charState == true
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            ) 
        {
            CreateParticles();

            icon3SE.Play();

            animator.SetBool("characterJumping", true);

            followCameraLevel1.startShake = true;

            StartCoroutine(Icon3Routine());
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
            && (noGravityBlock1.canGravityBlock == true
            && noGravityBlock2.canGravityBlock == true
            && noGravityBlock3.canGravityBlock == true
            && noGravityBlock4.canGravityBlock == true
            && noGravityBlock5.canGravityBlock == true
            && noGravityBlock6.canGravityBlock == true
            && noGravityBlock7.canGravityBlock == true
            && noGravityBlock8.canGravityBlock == true
            )
            )
        {
            icon3Background.enabled = false;
            noIcon3.Play();
            StartCoroutine(WaitTime3(1f));
        }

        FlipPlayer();
    }

    //Functii noi:

    //Pentru jump delayed:
    private IEnumerator JumpStart()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("characterJumping", true);
    }

    //Prima putere:
    private IEnumerator Icon1Routine()
    {
        canIcon1 = false;
        characterIcon1 = true;

        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = new Vector2(0, 0);

        int distanceTravelOnX = 50;
        int distanceTravelOnY = 0;

        Vector3 oldPosition = transform.position;

        if (facingLeft)
        {
            Vector3 newPosition = new Vector3(transform.position.x + distanceTravelOnX,
                                         transform.position.y + distanceTravelOnY,
                                         transform.position.z);

            DetectIfPastWallX(oldPosition, newPosition);
        }
        else if(!facingLeft)
        {
            Vector3 newPosition = new Vector3(transform.position.x - distanceTravelOnX,
                                         transform.position.y + distanceTravelOnY,
                                         transform.position.z);

            DetectIfPastWallX(oldPosition, newPosition);
        }

        trailRenderer1.emitting = true;
        icon1Animator.SetTrigger("playIcon1");

        yield return new WaitForSeconds(icon1Time);
        trailRenderer1.emitting = false;

        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(UnfreezeY(0.2f));

        myRigidbody.velocity = new Vector2(0, 0);
        myRigidbody.gravityScale = originalGravity;

        characterIcon1 = false;

        yield return new WaitForSeconds(icon1Cooldown);

        canIcon1 = true;

        Debug.Log("Icon 1 activated.");
    }

    //Puterea 2:
    private IEnumerator Icon2Routine()
    {
        canIcon2 = false;
        characterIcon2 = true;

        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = new Vector2(0, 0);

        int distanceTravelOnX = 0;
        int distanceTravelOnY = 40;

        Vector3 oldPosition = transform.position;
        Vector3 newPosition = new Vector3(transform.position.x + distanceTravelOnX,
                                         transform.position.y + distanceTravelOnY,
                                         transform.position.z);

        DetectIfPastWallY(oldPosition, newPosition);

        trailRenderer2.emitting = true;
        icon2Animator.SetTrigger("playIcon2");

        yield return new WaitForSeconds(icon2Time);
        trailRenderer2.emitting = false;

        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(UnfreezeY(0.2f));

        myRigidbody.velocity = new Vector2(0, 0);
        myRigidbody.gravityScale = originalGravity;

        characterIcon2 = false;

        yield return new WaitForSeconds(icon2Cooldown);

        canIcon2 = true;

        Debug.Log("Icon 2 activated.");
    }

    //Puterea 3:
    private IEnumerator Icon3Routine()
    {
        canIcon3 = false;
        characterIcon3 = true;

        float originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = new Vector2(0, 0);

        int distanceTravelOnX = 0;
        int distanceTravelOnY = 40;

        Vector3 oldPosition = transform.position;
        Vector3 newPosition = new Vector3(transform.position.x + distanceTravelOnX,
                                         transform.position.y - distanceTravelOnY,
                                         transform.position.z);

        DetectIfPastWallY(oldPosition, newPosition);

        trailRenderer3.emitting = true;
        icon3Animator.SetTrigger("playIcon3");

        yield return new WaitForSeconds(icon3Time);
        trailRenderer3.emitting = false;

        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(UnfreezeY(0.2f));

        myRigidbody.velocity = new Vector2(0, 0);
        myRigidbody.gravityScale = originalGravity;

        characterIcon3 = false;

        yield return new WaitForSeconds(icon3Cooldown);

        canIcon3 = true;

        Debug.Log("Icon 3 activated.");
    }

    //Pentru miscare:
    private bool PlayerIsGrounded()
    {
        return Physics2D.OverlapCircle(groundUnder.position, 0.33f, groundLayer);
    }

    private void FixedUpdate()
    {
        if (charState == true
            && canIcon1 == true
            && canIcon2 == true
            && canIcon3 == true
            && logicManagerLevelOne.startOfTransition == false
            && logicManagerLevelOne.endOfTransition == false
            )
        {
            if (noGravityBlock5.canGravityBlock == false)
            {
                if(noGravityBlock5.transform.position.y - transform.position.y > 0)
                {
                    transform.position = new Vector3(
                        transform.position.x,
                        transform.position.y + (noGravityBlock5.transform.position.y - transform.position.y) / 8,
                        transform.position.z);
                }
                else if(noGravityBlock5.transform.position.y - transform.position.y < 0)
                {
                    transform.position = new Vector3(
                        transform.position.x,
                        transform.position.y - (transform.position.y - noGravityBlock5.transform.position.y) / 8,
                        transform.position.z);
                }

                myRigidbody.velocity = new Vector2(horizontalPosition
                                                   * movementSpeed, 
                                                   myRigidbody.velocity.y);
            }
            else
            {
                velocityXActual = 50;

                myRigidbody.velocity = new Vector2(horizontalPosition
                * movementSpeed, myRigidbody.velocity.y);
            }
        }

        if (charState == true
            && (noGravityBlock1.canGravityBlock == false
            || noGravityBlock2.canGravityBlock == false
            || noGravityBlock3.canGravityBlock == false
            || noGravityBlock4.canGravityBlock == false
            || noGravityBlock5.canGravityBlock == false
            || noGravityBlock6.canGravityBlock == false
            || noGravityBlock7.canGravityBlock == false
            || noGravityBlock8.canGravityBlock == false
            )
            )
        {
            if (noGravityBlock5.canGravityBlock == false)
            {
                myRigidbody.velocity = new Vector2(
                myRigidbody.velocity.x,
                verticalPosition * (movementSpeed + Mathf.Abs(noGravityBlock5.transform.position.y - transform.position.y) * 7.5f));
            }
            else
            {
                velocityYActual = 40;

                myRigidbody.velocity = new Vector2(
                myRigidbody.velocity.x,
                verticalPosition * movementSpeed);
            }
        }
    }

    //Flip player:
    private void FlipPlayer()
    {
        if ((facingLeft == true && horizontalPosition < 0 && charState == true) ||
            (facingLeft == false && horizontalPosition > 0 && charState == true))
        {
            facingLeft = !facingLeft;

            Vector3 local = transform.localScale;
            local.x = local.x * (-1);
            transform.localScale = local;
        }
    }

    //Particles:
    public void CreateParticles()
    {
        particleSystem.Play();
    }

    //Death:
    public void CharacterDeath()
    {
        if (charState == true)
        {
            death.Play();
            logicManagerLevelOne.level1Music.Stop();

            logicManagerLevelOne.pause.Play();

            animator.SetBool("characterDeath", true);
        }

        charState = false;
        CharacterIsDead();

        logicManagerLevelOne.RestartSceneCollision();
        logicManagerLevelOne.SelectTryAgainLevel1Button();
        logicManagerLevelOne.SelectExitGameOverLevel1Button();
    }

    private void CharacterIsDead()
    {
        myRigidbody.velocity = new Vector2(0, 0);
    }

    //Wait pentru icon 1, 2, 3:
    private IEnumerator WaitTime(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon1Background.enabled = true;
    }
    private IEnumerator WaitTime2(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon2Background.enabled = true;
    }
    private IEnumerator WaitTime3(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon3Background.enabled = true;
    }

    //Rotate character:
    private void RotateOneTime()
    {
        if (oneTimeRotate == true)
        {
            oneTimeRotate = false;

            transform.RotateAround(transform.position, new Vector3(0, 1, 0), 180);
        }
    }

    //Detect if you are past a wall:
    private void DetectIfPastWallX(Vector3 oldPosition, Vector3 newPosition)
    {
        float oldX = oldPosition.x;
        float newX = newPosition.x;

        if(oldX < newX)
        {
            StartCoroutine(TeleportRight(oldX, newX, 0f));
        }
        else if(oldX >= newX)
        {
            StartCoroutine(TeleportLeft(oldX, newX, 0f));
        }
    }

    //Te duci catre dreapta if facing right:
    private IEnumerator TeleportRight(float oldX, float newX, float timeWaiting)
    {
        float firstOldX = oldX;

        while (oldX < newX)
        {
            myRigidbody.velocity = new Vector2(0, 0);

            yield return new WaitForSeconds(timeWaiting);

            transform.position = new Vector3(transform.position.x + 1,
                                     transform.position.y + 0,
                                     transform.position.z);
            
            oldX++;
        }
    }

    //Te duci catre stanga if facing left:
    private IEnumerator TeleportLeft(float oldX, float newX, float timeWaiting)
    {
        float firstOldX = oldX;

        while (oldX >= newX)
        {
            myRigidbody.velocity = new Vector2(0, 0);

            yield return new WaitForSeconds(timeWaiting);

            transform.position = new Vector3(transform.position.x - 1,
                                     transform.position.y + 0,
                                     transform.position.z);

            newX++;
        }
    }

    //Analog pentru y:
    private void DetectIfPastWallY(Vector3 oldPosition, Vector3 newPosition)
    {
        float oldY = oldPosition.y;
        float newY = newPosition.y;

        if (oldY < newY)
        {
            StartCoroutine(TeleportUp(oldY, newY, 0f)); 
        }
        else if (oldY >= newY)
        {
            StartCoroutine(TeleportDown(oldY, newY, 0f)); 
        }
    }

    //Up:
    private IEnumerator TeleportUp(float oldY, float newY, float timeWaiting)
    {
        while (oldY < newY)
        {
            myRigidbody.velocity = new Vector2(0, 0);

            yield return new WaitForSeconds(timeWaiting);

            transform.position = new Vector3(transform.position.x + 0,
                                     transform.position.y + 1,
                                     transform.position.z);

            oldY++;
        }
    }

    //Down:
    private IEnumerator TeleportDown(float oldY, float newY, float timeWaiting)
    {
        while (oldY >= newY)
        {
            myRigidbody.velocity = new Vector2(0, 0);

            yield return new WaitForSeconds(timeWaiting);

            transform.position = new Vector3(transform.position.x + 0,
                                     transform.position.y - 1,
                                     transform.position.z);

            newY++;
        }
    }

    //Unfreeze dupa teleport:
    private IEnumerator UnfreezeY( float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }
}
