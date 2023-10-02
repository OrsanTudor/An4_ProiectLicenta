using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;



public class CharacterScriptLevel3 : MonoBehaviour
{
    //Variabile:
    public Rigidbody2D myRigidbody;
    public Transform groundUnder;
    public LayerMask groundLayer;
    public LogicManagerLevelThree logicManagerLevelThree;
    public FollowCameraLevel3 followCameraLevel3;
    private float deathStrength;
    public bool charState;
    public Animator animator;
    public Animator icon1Animator;
    public Animator icon2Animator;
    public Animator icon3Animator;
    public Image icon1BackgroundImage;
    public Image icon2BackgroundImage;
    public Image icon3BackgroundImage;
    public Text icon1InUseText;
    public Text icon2InUseText;
    public Text icon3InUseText;
    public Text icon1FoundPlatformText;
    public Text icon2FoundPlatformText;
    public Text icon3FoundPlatformText;
    public ParticleSystem particleSystem;

    private float horizontalPosition;
    private float verticalPosition;
    private float movementSpeed;
    private float originalJumpStrength;
    public float jumpStrength;
    public bool facingLeft;
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
    public bool touchedPlatform;
    public bool onlyOneStayPlatform;
    private GameObject currentPlatform;
    public float currentGravity;
    private float timeForPower3;
    private bool power3IsDone;
    private bool oneCheckIfPower3IsDone;
    private float timeToWait;
    private bool afterWait;
    private float iconCooldowns;
    public bool dragOrNoDrag;

    private MovingPlatformScript movingPlatform1;
    private MovingPlatformScript movingPlatform2;
    private MovingPlatformScript movingPlatform3;
    public bool detectJumping;

    //Functii predefinite:

    //Start:
    void Start()
    {
        icon1BackgroundImage.enabled = false;
        icon1InUseText.enabled = false;
        icon1FoundPlatformText.enabled = false;
        icon2BackgroundImage.enabled = false;
        icon2InUseText.enabled = false;
        icon2FoundPlatformText.enabled = false;
        icon3BackgroundImage.enabled = false;
        icon3InUseText.enabled = false;
        icon3FoundPlatformText.enabled = false;
        detectJumping = false;

        touchedPlatform = false;
        onlyOneStayPlatform = false;

        timeForPower3 = 2f;
        power3IsDone = false;
        oneCheckIfPower3IsDone = false;
        timeToWait = 2f; 
        afterWait = true;
        iconCooldowns = 0.1f; 
        dragOrNoDrag = false;

        charState = true;
        facingLeft = true;
        oneTimeRotate = true;

        logicManagerLevelThree = GameObject.FindGameObjectWithTag("LogicManagerLevelThree")
            .GetComponent<LogicManagerLevelThree>();

        movingPlatform1 = GameObject.FindGameObjectWithTag("MovingPlatform1")
            .GetComponent<MovingPlatformScript>();
        movingPlatform2 = GameObject.FindGameObjectWithTag("MovingPlatform2")
            .GetComponent<MovingPlatformScript>();
        movingPlatform3 = GameObject.FindGameObjectWithTag("MovingPlatform3")
            .GetComponent<MovingPlatformScript>();

        lastCheckpointHit = PlayerPrefs.GetInt("LastCheckpoint");

        if (lastCheckpointHit == 0)
        {
            logicManagerLevelThree.RestartPlayerLevel();

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

        followCameraLevel3 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCameraLevel3>();

        icon1Background = GameObject.FindGameObjectWithTag("UseIcon1Background").GetComponent<Image>();
        icon1Uses = GameObject.FindGameObjectWithTag("UseIcon1").GetComponent<Text>();

        if (lastCheckpointHit == 0)
        {
            powerUp1 = 3;
            powerUp2 = 5;
            powerUp3 = 3;
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

        //Icon 1:
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
        icon1Time = 1f;
        icon1Cooldown = iconCooldowns;
        icon1Strength = 1f;

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
        icon2Time = 1f;
        icon2Cooldown = iconCooldowns;
        icon2Strength = 1f;

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
        icon3Time = 2f;
        icon3Cooldown = iconCooldowns;
        icon3Strength = 1f;
    }

    //Update:
    void Update()
    {
        if (
            canIcon1 == false
            || canIcon2 == false
            || canIcon3 == false
            || logicManagerLevelThree.gameIsPaused == true
            || logicManagerLevelThree.startOfTransition == true
            || logicManagerLevelThree.endOfTransition == true
            )
        {
            if (logicManagerLevelThree.gameIsPaused == true
               || logicManagerLevelThree.startOfTransition == true
               || logicManagerLevelThree.endOfTransition == true)
            {
                return;
            }
            else if (
                (canIcon1 == false
                || canIcon2 == false
                || canIcon3 == false)
                && touchedPlatform == true
                )
            {
                if (
                     (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) 
                     && charState == true 
                     && canIcon1 == false
                     && touchedPlatform == true
                     )
                {
                    dragOrNoDrag = false;
                    myRigidbody.drag = 0;

                    StartCoroutine(Icon1Reset());
                }
                else if (
                     (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2)) 
                     && charState == true 
                     && canIcon2 == false
                     && touchedPlatform == true
                     )
                {
                    dragOrNoDrag = false;
                    myRigidbody.drag = 0;

                    StartCoroutine(Icon2Reset());
                }
                //Nu apasare de buton, ci doar dupa un anumit timp:
                else if (
                     //(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3)) 
                     charState == true 
                     && canIcon3 == false
                     && touchedPlatform == true
                     && power3IsDone == true
                     )
                {
                    dragOrNoDrag = false;
                    myRigidbody.drag = 0;

                    StartCoroutine(Icon3Reset());

                    power3IsDone = false;
                }
                else
                {
                    return;
                }
            }
        }

        //Daca nu esti in putere si ai gravitatia 0:
        if(canIcon1 == true
           && canIcon2 == true
           && canIcon3 == true
           && myRigidbody.gravityScale == 0
           )
        {
            myRigidbody.gravityScale = 16;
        }

        //Daca nu esti in putere:
        if (canIcon1 == true
           && canIcon2 == true
           && canIcon3 == true)
        {
            power3IsDone = false;
        }

        if (lastCheckpointHit == 0 || lastCheckpointHit == 1)
        {
            horizontalPosition = Input.GetAxisRaw("Horizontal");
        }
        else if (lastCheckpointHit == 2)
        {
            horizontalPosition = Input.GetAxisRaw("Horizontal");
        }

        animator.SetFloat("characterSpeed", Mathf.Abs(horizontalPosition));

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0
            && charState == true
            )
        {
            CreateParticles();

            //Se opreste animatia inainte de a incerca jump:
            //movingPlatform1.DisableAnimator();
            //movingPlatform2.DisableAnimator();
            //movingPlatform3.DisableAnimator();
            //detectJumping = true;

            StartCoroutine(JumpStart());
            beginJump = true;

            jump.Play();

            //Debug.Log("Jumping started!");
            myRigidbody.velocity = new Vector2(transform.localScale.x, jumpStrength);
        }

        if (Input.GetKeyUp(KeyCode.Space)
            && myRigidbody.velocity.y > 0
            && charState == true
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
            )
        {
            animator.SetBool("characterCrouching", true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)
            && charState == true
            )
        {
            animator.SetBool("characterCrouching", false);
        }

        if (PlayerIsGrounded()
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
            //detectJumping = false;
        }
        else if (
            true
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
            && canIcon1 == true
            && canIcon2 == true
            && canIcon3 == true
            && afterWait == true
            )
        {
            afterWait = false;
            dragOrNoDrag = true;

            CreateParticles();

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
            && canIcon1 == true
            && canIcon2 == true
            && canIcon3 == true
            )
        {
            icon1Background.enabled = false;
            noIcon.Play();
            StartCoroutine(WaitTime(1f));
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2))
            && (powerUp2 > 0) && charState == true
            && canIcon1 == true
            && canIcon2 == true
            && canIcon3 == true
            && afterWait == true
            )
        {
            afterWait = false;
            dragOrNoDrag = true;

            CreateParticles();

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
            (powerUp2 == 0) && charState == true
            && canIcon1 == true
            && canIcon2 == true
            && canIcon3 == true
            )
        {
            icon2Background.enabled = false;
            noIcon2.Play();
            StartCoroutine(WaitTime2(1f));
        }

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3))
            && (powerUp3 > 0) && charState == true
            && canIcon1 == true
            && canIcon2 == true
            && canIcon3 == true
            && afterWait == true
            )
        {
            afterWait = false;
            dragOrNoDrag = true;

            CreateParticles();

            //Pentru inceput:
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
            && canIcon1 == true
            && canIcon2 == true
            && canIcon3 == true
            )
        {
            icon3Background.enabled = false;
            noIcon3.Play();
            StartCoroutine(WaitTime3(1f));
        }

        FlipPlayer();
    }

    //Functii noi:

    //Pentru inceput jump:
    private IEnumerator JumpStart()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("characterJumping", true);

        //yield return new WaitForSeconds(0.3f);
        ////Se reporneste animatia platformelor:
        //movingPlatform1.EnableAnimator();
        //movingPlatform2.EnableAnimator();
        //movingPlatform3.EnableAnimator();
        detectJumping = true;
    }

    //Putere 1:
    private IEnumerator Icon1Routine()
    {
        canIcon1 = false;
        characterIcon1 = true;

        icon1BackgroundImage.enabled = true;
        icon1InUseText.enabled = true;

        icon1SE.Play();

        yield return new WaitForSeconds(0.2f);
       
        icon1SE.Stop();

        Debug.Log("Icon 1 activated.");
    }

    //Putere 2:
    private IEnumerator Icon2Routine()
    {
        canIcon2 = false;
        characterIcon2 = true;

        icon2BackgroundImage.enabled = true;
        icon2InUseText.enabled = true;

        icon2SE.Play();
        yield return new WaitForSeconds(0.2f);
        icon2SE.Stop();

        Debug.Log("Icon 2 activated.");
    }

    //Putere 3:
    private IEnumerator Icon3Routine()
    {
        canIcon3 = false;
        characterIcon3 = true;
        //power3IsDone = false;

        icon3BackgroundImage.enabled = true;
        icon3InUseText.enabled = true;

        icon3SE.Play();
        yield return new WaitForSeconds(0.2f);
        icon3SE.Stop();

        Debug.Log("Icon 3 activated.");
    }

    //Player doest have legs on the ground:
    private bool PlayerIsGrounded()
    {
        return Physics2D.OverlapCircle(groundUnder.position, 0.33f, groundLayer);
    }

    //Miscare stanga si dreapta:
    private void FixedUpdate()
    {
        if (charState == true
            && touchedPlatform == false 
            && logicManagerLevelThree.startOfTransition == false
            && logicManagerLevelThree.endOfTransition == false
            )
        {
            myRigidbody.velocity = new Vector2(horizontalPosition
                * movementSpeed, myRigidbody.velocity.y);
        }
    }

    //Frlip sprite player:
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

    //Particule:
    public void CreateParticles()
    {
        particleSystem.Play();
    }

    //Moartea caracterului:
    public void CharacterDeath()
    {
        if (charState == true)
        {
            death.Play();
            logicManagerLevelThree.level3Music.Stop();
            logicManagerLevelThree.pause.Play();
            animator.SetBool("characterDeath", true);
        }

        charState = false;
        CharacterIsDead();

        logicManagerLevelThree.RestartSceneCollision();
        logicManagerLevelThree.SelectTryAgainLevel1Button();
        logicManagerLevelThree.SelectExitGameOverLevel1Button();
    }

    //No velocity:
    private void CharacterIsDead()
    {
        myRigidbody.velocity = new Vector2(0, 0);
    }

    //Pentru putere 1:
    private IEnumerator WaitTime(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon1Background.enabled = true;
    }

    //Pentru putere 2:
    private IEnumerator WaitTime2(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon2Background.enabled = true;
    }

    //Pentru putere 3:
    private IEnumerator WaitTime3(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon3Background.enabled = true;
    }

    //Pentru checkpoint rotation:
    private void RotateOneTime()
    {
        if (oneTimeRotate == true)
        {
            oneTimeRotate = false;

            transform.RotateAround(transform.position, new Vector3(0, 1, 0), 180);
        }
    }

    //Atunci cand stau pe coliziune cu platforma si sunt in icon:
    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("BreakPlatform")
           || collision.gameObject.CompareTag("BreakPlatform1")
           || collision.gameObject.CompareTag("BreakPlatform2")
           || collision.gameObject.CompareTag("BreakPlatform3")
           || collision.gameObject.CompareTag("BreakPlatform4")
           || collision.gameObject.CompareTag("BreakPlatform5")
           || collision.gameObject.CompareTag("BreakPlatform6")
           || collision.gameObject.CompareTag("BreakPlatform7")
           || collision.gameObject.CompareTag("BreakPlatform8")
           || collision.gameObject.CompareTag("BreakPlatform9")
           || collision.gameObject.CompareTag("BreakPlatform10")
           || collision.gameObject.CompareTag("BreakPlatform11"))
           && (canIcon1 == false
           || canIcon2 == false
           || canIcon3 == false)
           && onlyOneStayPlatform == false
           )
        {
            //Start reset:
            if (canIcon3 == false && oneCheckIfPower3IsDone == false)
            {
                //icon1BackgroundImage.enabled = false;
                //icon1InUseText.enabled = false;
                StartCoroutine(TimeForIcon3());
                oneCheckIfPower3IsDone = true;
            }

            GameObject platform = collision.gameObject;
            currentPlatform = platform;

            BreakPlatformLevel3Script platformScript = collision.gameObject.GetComponent<BreakPlatformLevel3Script>();

            onlyOneStayPlatform = true;

            if (currentPlatform != null)
            {
                if (platformScript.destroyStart == true)
                {
                    if(canIcon1 == false)
                    {
                        icon1BackgroundImage.enabled = false;
                        icon1InUseText.enabled = false;

                        StartCoroutine(Icon1Reset());
                    }
                    else if (canIcon2 == false)
                    {
                        icon2BackgroundImage.enabled = false;
                        icon2InUseText.enabled = false;

                        StartCoroutine(Icon2Reset());
                    }
                    else if (canIcon3 == false)
                    {
                        icon3BackgroundImage.enabled = false;
                        icon3InUseText.enabled = false;

                        StartCoroutine(Icon3Reset());
                    }

                    return;
                }

                if (
                    platformScript.TestIfInPlatform() == false
                    || platformScript.TestIfAbove() == false
                   )
                {
                    onlyOneStayPlatform = false;
                    return;
                }
            }

            touchedPlatform = true;

            myRigidbody.velocity = new Vector2(0, 0);
            currentGravity = myRigidbody.gravityScale;
            myRigidbody.gravityScale = 0;

            animator.SetFloat("characterSpeed", 0f);
            animator.SetBool("characterJumping", false);

            if (currentPlatform != null)
            {
                Rigidbody2D platformRigidBody = platform.GetComponent<Rigidbody2D>();

                platformRigidBody.mass = 3;
                platformRigidBody.drag = 0; 
                platformRigidBody.gravityScale = 0;

                if (canIcon1 == false)
                {
                    platformRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY
                                                    | RigidbodyConstraints2D.FreezeRotation;
                }
                else if (canIcon2 == false)
                {
                    platformRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX
                                                    | RigidbodyConstraints2D.FreezeRotation;
                }
                else if (canIcon3 == false)
                {
                    platformRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }

            if (canIcon1 == false)
            {
                icon1InUseText.enabled = false;
                icon1FoundPlatformText.enabled = true;
            }
            else if (canIcon2 == false)
            {
                icon2InUseText.enabled = false;
                icon2FoundPlatformText.enabled = true;
            }
            else if (canIcon3 == false)
            {
                icon3InUseText.enabled = false;
                icon3FoundPlatformText.enabled = true;
            }
        }
        else if ((collision.gameObject.CompareTag("BreakPlatform")
           || collision.gameObject.CompareTag("BreakPlatform1")
           || collision.gameObject.CompareTag("BreakPlatform2")
           || collision.gameObject.CompareTag("BreakPlatform3")
           || collision.gameObject.CompareTag("BreakPlatform4")
           || collision.gameObject.CompareTag("BreakPlatform5")
           || collision.gameObject.CompareTag("BreakPlatform6")
           || collision.gameObject.CompareTag("BreakPlatform7")
           || collision.gameObject.CompareTag("BreakPlatform8")
           || collision.gameObject.CompareTag("BreakPlatform9")
           || collision.gameObject.CompareTag("BreakPlatform10")
           || collision.gameObject.CompareTag("BreakPlatform11"))
           && (canIcon1 == false
           || canIcon2 == false
           || canIcon3 == false)
           && onlyOneStayPlatform == false
           )
        {
            if(currentPlatform != null)
            {
                if(currentPlatform.GetComponent<BreakPlatformLevel3Script>().destroyStart == true)
                {
                    myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX
                                              | RigidbodyConstraints2D.FreezePositionY
                                              | RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }

    //Atunci cand termin coliziunea cu platforma si sunt in putere:
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("BreakPlatform")
           || collision.gameObject.CompareTag("BreakPlatform1")
           || collision.gameObject.CompareTag("BreakPlatform2")
           || collision.gameObject.CompareTag("BreakPlatform3")
           || collision.gameObject.CompareTag("BreakPlatform4")
           || collision.gameObject.CompareTag("BreakPlatform5")
           || collision.gameObject.CompareTag("BreakPlatform6")
           || collision.gameObject.CompareTag("BreakPlatform7")
           || collision.gameObject.CompareTag("BreakPlatform8")
           || collision.gameObject.CompareTag("BreakPlatform9")
           || collision.gameObject.CompareTag("BreakPlatform10")
           || collision.gameObject.CompareTag("BreakPlatform11"))
           && (canIcon1 == false
           || canIcon2 == false
           || canIcon3 == false)
           && touchedPlatform == true
           )
        {
            Debug.Log("Started exit!");

            if (canIcon1 == false)
            {
                StartCoroutine(Icon1Reset());
            }
            else if (canIcon2 == false)
            {
                StartCoroutine(Icon2Reset());
            }
            else if (canIcon3 == false)
            {
                StartCoroutine(Icon3Reset());
            }

            StartCoroutine(currentPlatform.GetComponent<BreakPlatformLevel3Script>().MakePlatformDisappear(1f));
        }
    }

    //Reset puterea la apasare de buton:
    public IEnumerator Icon1Reset()
    {
        ResetCurrentPlatform();

        yield return new WaitForSeconds(icon1Cooldown);

        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        canIcon1 = true;
        myRigidbody.velocity = new Vector2(0, 0);
        myRigidbody.drag = 0;
        myRigidbody.gravityScale = currentGravity;
        icon1BackgroundImage.enabled = false;
        icon1FoundPlatformText.enabled = false;
        touchedPlatform = false;
        onlyOneStayPlatform = false;
        icon1SE.Stop();

        StartCoroutine(WaitBetweenPowerUps(timeToWait));
    }

    //Reset puterea la apasare de buton:
    public IEnumerator Icon2Reset()
    {
        ResetCurrentPlatform();

        yield return new WaitForSeconds(icon2Cooldown);

        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        canIcon2 = true;
        myRigidbody.velocity = new Vector2(0, 0);
        myRigidbody.drag = 0;
        myRigidbody.gravityScale = currentGravity;
        icon2BackgroundImage.enabled = false;
        icon2FoundPlatformText.enabled = false;
        touchedPlatform = false;
        onlyOneStayPlatform = false;
        icon3SE.Stop();

        StartCoroutine(WaitBetweenPowerUps(timeToWait));
    }

    //Timer reset pentru puterea 3:
    public IEnumerator TimeForIcon3()
    {
        yield return new WaitForSeconds(timeForPower3);

        power3IsDone = true;
        oneCheckIfPower3IsDone = false;
    }

    //Reset puterea la apasare de buton:
    public IEnumerator Icon3Reset()
    {
        ResetCurrentPlatform();

        yield return new WaitForSeconds(icon3Cooldown);

        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        canIcon3 = true;
        //power3IsDone = false;
        myRigidbody.velocity = new Vector2(0, 0);
        myRigidbody.drag = 0;
        myRigidbody.gravityScale = currentGravity;
        icon3BackgroundImage.enabled = false;
        icon3FoundPlatformText.enabled = false;
        touchedPlatform = false;
        onlyOneStayPlatform = false;

        icon1SE.Stop();
        icon3SE.Stop();

        StartCoroutine(WaitBetweenPowerUps(timeToWait));
    }

    //Reset platforma la a fi cum a fost inainte:
    private void ResetCurrentPlatform()
    {
        if(currentPlatform != null)
        {
            Rigidbody2D platformRigidBody = currentPlatform.GetComponent<Rigidbody2D>();

            platformRigidBody.mass = 3000;
            platformRigidBody.drag = 5000;
            platformRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX
                                            | RigidbodyConstraints2D.FreezePositionY
                                            | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    //Asteptarea intre apasarea pe icons:
    private IEnumerator WaitBetweenPowerUps(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        afterWait = true;
    }
}
