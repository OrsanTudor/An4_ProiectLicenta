using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CharacterScriptLevelFinal : MonoBehaviour
{
    //Variabile:
    public Rigidbody2D myRigidbody;
    public Transform groundUnder;
    public LayerMask groundLayer;
    public LogicManagerLevelFinal logicManagerLevelFinal;
    public FollowCameraLevelFinal followCameraLevelFinal;
    private float deathStrength;
    public bool charState;
    public Animator animator;
    public Animator icon1Animator;
    public Animator icon2Animator;
    public Animator icon3Animator;
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
    //public bool canIcon1;
    //public int powerUp1;
    //private bool characterIcon1;
    //private float icon1Strength;
    //private float icon1Time;
    //private float icon1Cooldown;
    //public TrailRenderer trailRenderer1;
    //private Text icon1Uses;
    //private Image icon1Background;
    //Icon 2:
    //public bool canIcon2;
    //public int powerUp2;
    //private bool characterIcon2;
    //private float icon2Strength;
    //private float icon2Time;
    //private float icon2Cooldown;
    //public TrailRenderer trailRenderer2;
    //private Text icon2Uses;
    //private Image icon2Background;
    //Icon 3:
    //public bool canIcon3;
    //public int powerUp3;
    //private bool characterIcon3;
    //private float icon3Strength;
    //private float icon3Time;
    //private float icon3Cooldown;
    //public TrailRenderer trailRenderer3;
    //private Text icon3Uses;
    //private Image icon3Background;

    public float originalGravityJumpDown;
    private float gravityDownD;
    private float gravityDownU;
    private float gravityDownArrow;
    private float maxGravity;

    public AudioSource jump;
    public AudioSource death;
    //public AudioSource icon1SE;
    //public AudioSource icon2SE;
    //public AudioSource icon3SE;
    //public AudioSource noIcon;
    //public AudioSource noIcon2;
    //public AudioSource noIcon3;
    public int lastCheckpointHit;
    public bool oneTimeRotate;

    //Functii predefinite:

    //Start:
    void Start()
    {
        charState = true;
        facingLeft = true;
        oneTimeRotate = true;

        logicManagerLevelFinal = GameObject.FindGameObjectWithTag("LogicManagerLevelFinal")
            .GetComponent<LogicManagerLevelFinal>();

        lastCheckpointHit = PlayerPrefs.GetInt("LastCheckpoint");

        if (lastCheckpointHit == 0)
        {
            //logicManagerLevelFinal.RestartPlayerLevel();

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

        followCameraLevelFinal = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCameraLevelFinal>();

        //Icons:
        //icon1Background = GameObject.FindGameObjectWithTag("UseIcon1Background").GetComponent<Image>();
        //icon1Uses = GameObject.FindGameObjectWithTag("UseIcon1").GetComponent<Text>();

        //if (lastCheckpointHit == 0)
        //{
        //    powerUp1 = 10;
        //    powerUp2 = 10;
        //    powerUp3 = 10;
        //}
        //else if (lastCheckpointHit == 1)
        //{
        //    powerUp1 = PlayerPrefs.GetInt("PowerUp1_Checkpoint1");
        //    powerUp2 = PlayerPrefs.GetInt("PowerUp2_Checkpoint1");
        //    powerUp3 = PlayerPrefs.GetInt("PowerUp3_Checkpoint1");
        //}
        //else if (lastCheckpointHit == 2)
        //{
        //    powerUp1 = PlayerPrefs.GetInt("PowerUp1_Checkpoint2");
        //    powerUp2 = PlayerPrefs.GetInt("PowerUp2_Checkpoint2");
        //    powerUp3 = PlayerPrefs.GetInt("PowerUp3_Checkpoint2");
        //}

        beginJump = false;
        endJump = false;

        //Icon 1:
        //canIcon1 = true;
        //icon1Uses.text = "Remaining: \n";

        //for (int i = 0; i < powerUp1; i++)
        //{
        //    icon1Uses.text = icon1Uses.text + " | ";
        //}
        //if (powerUp1 == 0)
        //{
        //    icon1Uses.text = icon1Uses.text + " - ";
        //}

        //characterIcon1 = false;
        //icon1Time = 0.3f;
        //icon1Cooldown = 0.15f;
        //icon1Strength = 15;

        //Icon 2:
        //icon2Background = GameObject.FindGameObjectWithTag("UseIcon2Background").GetComponent<Image>();
        //icon2Uses = GameObject.FindGameObjectWithTag("UseIcon2").GetComponent<Text>();

        //canIcon2 = true;
        //icon2Uses.text = "Remaining: \n";

        //for (int i = 0; i < powerUp2; i++)
        //{
        //    icon2Uses.text = icon2Uses.text + " | ";
        //}
        //if (powerUp2 == 0)
        //{
        //    icon2Uses.text = icon2Uses.text + " - ";
        //}

        //characterIcon2 = false;
        //icon2Time = 2f;
        //icon2Cooldown = 0.25f;
        //icon2Strength = 5;

        //Icon 3:
        //icon3Background = GameObject.FindGameObjectWithTag("UseIcon3Background").GetComponent<Image>();
        //icon3Uses = GameObject.FindGameObjectWithTag("UseIcon3").GetComponent<Text>();

        //canIcon3 = true;
        //icon3Uses.text = "Remaining: \n";

        //for (int i = 0; i < powerUp3; i++)
        //{
        //    icon3Uses.text = icon3Uses.text + " | ";
        //}
        //if (powerUp3 == 0)
        //{
        //    icon3Uses.text = icon3Uses.text + " - ";
        //}

        //characterIcon3 = false;
        //icon3Time = 0.3f;
        //icon3Cooldown = 0.15f;
        //icon3Strength = 15;
    }

    //Update:
    void Update()
    {
        if (
            //canIcon1 == false
            //|| canIcon2 == false
            //|| canIcon3 == false
            logicManagerLevelFinal.gameIsPaused == true
            || logicManagerLevelFinal.startOfTransition == true
            || logicManagerLevelFinal.endOfTransition == true
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

        animator.SetFloat("characterSpeed", Mathf.Abs(horizontalPosition));

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0
            && charState == true
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

        //if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) &&
        //    (powerUp1 > 0) 
        //    && charState == true
        //    )
        //{
        //    CreateParticles();

        //    icon1SE.Play();

        //    animator.SetBool("characterJumping", true);

        //    followCameraLevelFinal.startShake = true;

        //    StartCoroutine(Icon1Routine());
        //    powerUp1 = powerUp1 - 1;

        //    icon1Uses.text = "Remaining: " + "\n";
        //    for (int i = 0; i < powerUp1; i++)
        //    {
        //        icon1Uses.text = icon1Uses.text + " | ";
        //    }
        //    if (powerUp1 == 0)
        //    {
        //        icon1Uses.text = icon1Uses.text + " - ";
        //    }
        //}
        //else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) &&
        //    (powerUp1 == 0) 
        //    && charState == true
        //    )
        //{
        //    icon1Background.enabled = false;
        //    noIcon.Play();
        //    StartCoroutine(WaitTime(1f));
        //}

        //if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2))
        //    && (powerUp2 > 0) 
        //    && charState == true)
        //{
        //    CreateParticles();

        //    icon2SE.Play();

        //    animator.SetBool("characterJumping", true);

        //    followCameraLevelFinal.startShake = true;

        //    StartCoroutine(Icon2Routine());
        //    powerUp2 = powerUp2 - 1;

        //    icon2Uses.text = "Remaining: " + "\n";
        //    for (int i = 0; i < powerUp2; i++)
        //    {
        //        icon2Uses.text = icon2Uses.text + " | ";
        //    }
        //    if (powerUp2 == 0)
        //    {
        //        icon2Uses.text = icon2Uses.text + " - ";
        //    }
        //}
        //else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2)) &&
        //    (powerUp2 == 0) 
        //    && charState == true)
        //{
        //    icon2Background.enabled = false;
        //    noIcon2.Play();
        //    StartCoroutine(WaitTime2(1f));
        //}

        //if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3))
        //    && (powerUp3 > 0) 
        //    && charState == true
        //    )
        //{
        //    CreateParticles();

        //    icon3SE.Play();

        //    animator.SetBool("characterJumping", true);

        //    followCameraLevelFinal.startShake = true;

        //    StartCoroutine(Icon3Routine());
        //    powerUp3 = powerUp3 - 1;

        //    icon3Uses.text = "Remaining: " + "\n";
        //    for (int i = 0; i < powerUp3; i++)
        //    {
        //        icon3Uses.text = icon3Uses.text + " | ";
        //    }
        //    if (powerUp3 == 0)
        //    {
        //        icon3Uses.text = icon3Uses.text + " - ";
        //    }
        //}
        //else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3)) &&
        //    (powerUp3 == 0) 
        //    && charState == true
        //    )
        //{
        //    icon3Background.enabled = false;
        //    noIcon3.Play();
        //    StartCoroutine(WaitTime3(1f));
        //}

        FlipPlayer();
    }

    //Functii noi:

    //Pentru inceput jump:
    private IEnumerator JumpStart()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("characterJumping", true);
    }

    //Putere 1:
    //private IEnumerator Icon1Routine()
    //{
    //    canIcon1 = false;
    //    characterIcon1 = true;

    //    float originalGravity = myRigidbody.gravityScale;
    //    myRigidbody.gravityScale = 0;
    //    myRigidbody.velocity = new Vector2((transform.localScale.x / 2) * icon1Strength,
    //                                       (transform.localScale.y / 5) * icon1Strength);

    //    trailRenderer1.emitting = true;
    //    icon1Animator.SetTrigger("playIcon1");

    //    yield return new WaitForSeconds(icon1Time);
    //    trailRenderer1.emitting = false;

    //    myRigidbody.gravityScale = originalGravity;

    //    characterIcon1 = false;

    //    yield return new WaitForSeconds(icon1Cooldown);

    //    canIcon1 = true;

    //    Debug.Log("Icon 1 activated.");
    //}

    //Putere 2:
    //private IEnumerator Icon2Routine()
    //{
    //    canIcon2 = false;
    //    characterIcon2 = true;

    //    float originalGravity = myRigidbody.gravityScale;
    //    myRigidbody.gravityScale = 0;
    //    myRigidbody.velocity = new Vector2(0, 0);

    //    trailRenderer2.emitting = true;
    //    icon2Animator.SetTrigger("playIcon2");

    //    yield return new WaitForSeconds(icon2Time);
    //    trailRenderer2.emitting = false;

    //    myRigidbody.gravityScale = originalGravity;

    //    characterIcon2 = false;

    //    yield return new WaitForSeconds(icon2Cooldown);

    //    canIcon2 = true;

    //    Debug.Log("Icon 2 activated.");
    //}

    //Putere 3:
    //private IEnumerator Icon3Routine()
    //{
    //    canIcon3 = false;
    //    characterIcon3 = true;

    //    float originalGravity = myRigidbody.gravityScale;
    //    myRigidbody.gravityScale = 0;
    //    myRigidbody.velocity = new Vector2((transform.localScale.x / 2) * icon3Strength,
    //                                       (transform.localScale.y / 5) * icon3Strength);
 
    //    trailRenderer3.emitting = true;
    //    icon3Animator.SetTrigger("playIcon3");

    //    yield return new WaitForSeconds(icon3Time);
    //    trailRenderer3.emitting = false;

    //    myRigidbody.gravityScale = originalGravity;

    //    characterIcon3 = false;

    //    yield return new WaitForSeconds(icon3Cooldown);

    //    canIcon3 = true;

    //    Debug.Log("Icon 3 activated.");
    //}

    //Atingere ground:
    private bool PlayerIsGrounded()
    {
        return Physics2D.OverlapCircle(groundUnder.position, 0.33f, groundLayer);
    }

    //Miscare stanga si dreapta:
    private void FixedUpdate()
    {
        if (charState == true
            //&& canIcon1 == true
            //&& canIcon2 == true
            //&& canIcon3 == true
            && logicManagerLevelFinal.startOfTransition == false
            && logicManagerLevelFinal.endOfTransition == false
            )
        {
            myRigidbody.velocity = new Vector2(horizontalPosition
                * movementSpeed, myRigidbody.velocity.y);
        }
    }

    //Pentru flip srpite player:
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

    //Moartea caracterului: Nu se intampla in level final!
    public void CharacterDeath()
    {
        if (charState == true)
        {
            death.Play();
            logicManagerLevelFinal.levelFinalMusic.Stop();
            logicManagerLevelFinal.pause.Play();
            animator.SetBool("characterDeath", true);
        }

        charState = false;
        CharacterIsDead();

        logicManagerLevelFinal.RestartSceneCollision();
        logicManagerLevelFinal.SelectTryAgainLevel1Button();
        logicManagerLevelFinal.SelectExitGameOverLevel1Button();
    }

    //No velocity on death:
    private void CharacterIsDead()
    {
        myRigidbody.velocity = new Vector2(0, 0);
    }

    //Pentru prima putere:
    //private IEnumerator WaitTime(float timeWaiting)
    //{
    //    yield return new WaitForSeconds(timeWaiting);

    //    icon1Background.enabled = true;
    //}

    //Pentru a 2-a putere:
    //private IEnumerator WaitTime2(float timeWaiting)
    //{
    //    yield return new WaitForSeconds(timeWaiting);

    //    icon2Background.enabled = true;
    //}

    //Pentru a 3-a putere:
    //private IEnumerator WaitTime3(float timeWaiting)
    //{
    //    yield return new WaitForSeconds(timeWaiting);

    //    icon3Background.enabled = true;
    //}

    //Rotatie la checkpoint:
    private void RotateOneTime()
    {
        if (oneTimeRotate == true)
        {
            oneTimeRotate = false;

            transform.RotateAround(transform.position, new Vector3(0, 1, 0), 180);
        }
    }
}
