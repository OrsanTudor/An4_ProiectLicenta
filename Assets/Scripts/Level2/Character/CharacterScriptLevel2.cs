using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterScriptLevel2 : MonoBehaviour
{
    //Variabile:
    public Rigidbody2D myRigidbody;
    public Transform groundUnder;
    public LayerMask groundLayer;
    public LogicManagerLevelTwo logicManagerLevelTwo;
    public FollowCameraLevel2 followCameraLevel2;
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
    private float originalGravity;
    public AudioSource hitPlatformSE;
    private Vector2 position1;
    private Vector2 position2;
    private bool moving;
    private bool onlySomeTimes;
    private bool onlyOneAddForce;

    //Functii predefinite:

    //Start:
    void Start()
    {
        icon1BackgroundImage.enabled = false;
        icon1InUseText.enabled = false;
        icon2BackgroundImage.enabled = false;
        icon2InUseText.enabled = false;
        icon3BackgroundImage.enabled = false;
        icon3InUseText.enabled = false;

        moving = true;
        onlySomeTimes = false;
        onlyOneAddForce = false;

        charState = true;
        facingLeft = true;
        oneTimeRotate = true;

        logicManagerLevelTwo = GameObject.FindGameObjectWithTag("LogicManagerLevelTwo")
            .GetComponent<LogicManagerLevelTwo>();

        lastCheckpointHit = PlayerPrefs.GetInt("LastCheckpoint");

        if (lastCheckpointHit == 0)
        {
            logicManagerLevelTwo.RestartPlayerLevel();

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

        followCameraLevel2 = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<FollowCameraLevel2>();

        icon1Background = GameObject.FindGameObjectWithTag("UseIcon1Background").GetComponent<Image>();
        icon1Uses = GameObject.FindGameObjectWithTag("UseIcon1").GetComponent<Text>();

        if (lastCheckpointHit == 0)
        {
            powerUp1 = 4;
            powerUp2 = 6;
            powerUp3 = 4;
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
        icon1Cooldown = 0.4f;
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
        icon2Cooldown = 0.4f;
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
        icon3Time = 1f;
        icon3Cooldown = 0.4f;
        icon3Strength = 1f;
    }

    //Update:
    void Update()
    {
        if (
            canIcon1 == false
            || canIcon2 == false
            || canIcon3 == false
            || logicManagerLevelTwo.gameIsPaused == true
            || logicManagerLevelTwo.startOfTransition == true
            || logicManagerLevelTwo.endOfTransition == true
            )
        {
            if (
                 logicManagerLevelTwo.gameIsPaused == true
                 || logicManagerLevelTwo.startOfTransition == true
                 || logicManagerLevelTwo.endOfTransition == true
               )
            {
                return;
            }
            else if(
                canIcon1 == false
                || canIcon2 == false
                || canIcon3 == false
                )
            {
                if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) &&
                     charState == true && canIcon1 == false)
                {
                    StartCoroutine(Icon1Reset());
                }
                else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2)) &&
                     charState == true && canIcon2 == false)
                {
                    StartCoroutine(Icon2Reset());
                }
                else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3)) &&
                     charState == true && canIcon3 == false)
                {
                    StartCoroutine(Icon3Reset());
                }
                else
                {
                    return;
                }
            }
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

        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Alpha1)) &&
            (powerUp1 > 0) && charState == true
            && canIcon1 == true
            )
        {
            CreateParticles();

            icon1SE.Play();

            animator.SetBool("characterJumping", true);

            followCameraLevel2.startShake = true;

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
            )
        {
            icon1Background.enabled = false;
            noIcon.Play();
            StartCoroutine(WaitTime(1f));
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Alpha2))
            && (powerUp2 > 0) && charState == true
            && canIcon2 == true
            )
        {
            CreateParticles();

            icon2SE.Play();

            animator.SetBool("characterJumping", true);

            followCameraLevel2.startShake = true;

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
            && canIcon2 == true
            )
        {
            icon2Background.enabled = false;
            noIcon2.Play();
            StartCoroutine(WaitTime2(1f));
        }

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Alpha3))
            && (powerUp3 > 0) && charState == true
            && canIcon3 == true
            )
        {
            CreateParticles();

            icon3SE.Play();

            animator.SetBool("characterJumping", true);

            followCameraLevel2.startShake = true;

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

    //Delay pentru jump:
    private IEnumerator JumpStart()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("characterJumping", true);
    }

    //Puterea 1:
    private IEnumerator Icon1Routine()
    {
        canIcon1 = false;
        characterIcon1 = true;

        originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = new Vector2(0, 0);

        if(facingLeft == true)
        {
            myRigidbody.velocity = new Vector2(120, 0);
        }
        else if(facingLeft == false)
        {
            myRigidbody.velocity = new Vector2(-120, 0);
        }

        trailRenderer1.emitting = true;

        icon1BackgroundImage.enabled = true;
        icon1InUseText.enabled = true;

        characterIcon1 = false;

        yield return new WaitForSeconds(0.05f);

        Debug.Log("Icon 1 activated.");
    }

    //Puterea 2:
    private IEnumerator Icon2Routine()
    {
        canIcon2 = false;
        characterIcon2 = true;

        originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = new Vector2(0, 0);

        myRigidbody.velocity = new Vector2(0, 100);

        trailRenderer2.emitting = true;
        icon2BackgroundImage.enabled = true;
        icon2InUseText.enabled = true;

        characterIcon2 = false;

        yield return new WaitForSeconds(0.05f);

        Debug.Log("Icon 2 activated.");
    }

    //Puterea 3:
    private IEnumerator Icon3Routine()
    {
        canIcon3 = false;
        characterIcon3 = true;

        originalGravity = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = new Vector2(0, 0);

        myRigidbody.velocity = new Vector2(0, -100);

        trailRenderer3.emitting = true;
        icon3BackgroundImage.enabled = true;
        icon3InUseText.enabled = true;

        characterIcon3 = false;

        yield return new WaitForSeconds(0.05f);

        Debug.Log("Icon 3 activated.");
    }

    //Pentru cand este player-ul pe pamant:
    private bool PlayerIsGrounded()
    {
        return Physics2D.OverlapCircle(groundUnder.position, 0.33f, groundLayer);
    }

    //Pentru miscare stanga dreapta:
    private void FixedUpdate()
    {
        if (charState == true
            && canIcon1 == true
            && canIcon2 == true
            && canIcon3 == true
            && logicManagerLevelTwo.startOfTransition == false
            && logicManagerLevelTwo.endOfTransition == false
            )
        {
            myRigidbody.velocity = new Vector2(horizontalPosition
                * movementSpeed, myRigidbody.velocity.y);
        }
    }

    //Pentru flip sprite:
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

    //Moarte caracter:
    public void CharacterDeath()
    {
        if (charState == true)
        {
            death.Play();
            logicManagerLevelTwo.level2Music.Stop();
            logicManagerLevelTwo.pause.Play();
            animator.SetBool("characterDeath", true);
        }

        charState = false;
        CharacterIsDead();

        logicManagerLevelTwo.RestartSceneCollision();
        logicManagerLevelTwo.SelectTryAgainLevel1Button();
        logicManagerLevelTwo.SelectExitGameOverLevel1Button();
    }

    //No velocity on death:
    private void CharacterIsDead()
    {
        myRigidbody.velocity = new Vector2(0, 0);
    }

    //Wait for power:
    private IEnumerator WaitTime(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon1Background.enabled = true;
    }

    //Wait for power:
    private IEnumerator WaitTime2(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon2Background.enabled = true;
    }

    //Wait for power:
    private IEnumerator WaitTime3(float timeWaiting)
    {
        yield return new WaitForSeconds(timeWaiting);

        icon3Background.enabled = true;
    }

    //Rotate caracter la inceput checkpoint:
    private void RotateOneTime()
    {
        if (oneTimeRotate == true)
        {
            oneTimeRotate = false;

            transform.RotateAround(transform.position, new Vector3(0, 1, 0), 180);
        }
    }

    //Detectare daca se intampla colision:
    private void OnCollisionEnter2D(Collision2D collision)
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
           )
           && (canIcon1 == false
           || canIcon2 == false
           || canIcon3 == false))
        {
            hitPlatformSE.Play();
        }
        else if(
           (canIcon1 == false
           || canIcon2 == false
           || canIcon3 == false)
           )
        {
            hitPlatformSE.Play();
        }

        if (true)
        {
            if (canIcon1 == false && onlyOneAddForce == false)
            {
                onlyOneAddForce = true;

                if (facingLeft == true)
                {
                    myRigidbody.AddForce(Vector2.left * 300, ForceMode2D.Impulse);
                }
                else if (facingLeft == false)
                {
                    myRigidbody.AddForce(Vector2.right * 300, ForceMode2D.Impulse);
                }

                StartCoroutine(Icon1Reset());
            }
            else if (canIcon2 == false && onlyOneAddForce == false)
            {
                onlyOneAddForce = true;
                
                myRigidbody.AddForce(Vector2.down * 350, ForceMode2D.Impulse);

                StartCoroutine(Icon2Reset());
            }
            else if (canIcon3 == false && onlyOneAddForce == false)
            {
                onlyOneAddForce = true;

                myRigidbody.AddForce(Vector2.up * 350, ForceMode2D.Impulse);

                StartCoroutine(Icon3Reset());
            }
        }
    }

    //Daca sunt on stay collision, verific daca am velocity 0:
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(onlySomeTimes == false)
        {
            onlySomeTimes = true;
            StartCoroutine(NotMovingDetectorActivator(0.2f));
        }

        if (canIcon1 == false && moving == false)
        {
            StartCoroutine(Icon1Reset());
        }
        if (canIcon2 == false && moving == false)
        {
            StartCoroutine(Icon2Reset());
        }
        if (canIcon3 == false && moving == false)
        {
            StartCoroutine(Icon3Reset());
        }
    }

    //Reset pentru cand apesi iara puterea:
    private IEnumerator Icon1Reset()
    {
        yield return new WaitForSeconds(icon1Cooldown);

        canIcon1 = true;
        myRigidbody.gravityScale = originalGravity;
        myRigidbody.velocity = new Vector2(0, 0);
        trailRenderer1.emitting = false;
        icon1BackgroundImage.enabled = false;
        icon1SE.Stop();
        icon1InUseText.enabled = false;
        moving = true;
        onlyOneAddForce = false;
    }

    //Reset pentru cand apesi iara puterea:
    private IEnumerator Icon2Reset()
    {
        yield return new WaitForSeconds(icon2Cooldown);

        canIcon2 = true;
        myRigidbody.gravityScale = originalGravity;
        myRigidbody.velocity = new Vector2(0, 0);
        trailRenderer2.emitting = false;
        icon2BackgroundImage.enabled = false;
        icon2SE.Stop();
        icon2InUseText.enabled = false;
        moving = true;
        onlyOneAddForce = false;
    }

    //Reset pentru cand apesi iara puterea:
    private IEnumerator Icon3Reset()
    {
        yield return new WaitForSeconds(icon3Cooldown);

        canIcon3 = true;
        myRigidbody.gravityScale = originalGravity;
        myRigidbody.velocity = new Vector2(0, 0);
        trailRenderer3.emitting = false;
        icon3BackgroundImage.enabled = false;
        icon3SE.Stop();
        icon3InUseText.enabled = false;
        moving = true;
        onlyOneAddForce = false;
    }

    //Detecteaza daca esti sau nu blocat:
    private IEnumerator NotMovingDetectorActivator(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        NotMovingDetector();
    }

    //Detecteaza daca esti sau nu blocat:
    private void NotMovingDetector()
    {
        position1 = new Vector2(transform.position.x, transform.position.y);
        
        StartCoroutine(FindPosition2(0.2f));
    }

    //Gaseste noua pozitie:
    private IEnumerator FindPosition2(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        position2 = new Vector2(transform.position.x, transform.position.y);

        if(canIcon1 == false && (Mathf.Abs(position1.x - position2.x) < 1))
        {
            moving = false;
        }
        else if ((canIcon2 == false || canIcon3 == false) 
              && (Mathf.Abs(position1.y - position2.y) < 1))
        {
            moving = false;
        }
        else
        {
            moving = true;
        }

        onlySomeTimes = false;
    }
}
