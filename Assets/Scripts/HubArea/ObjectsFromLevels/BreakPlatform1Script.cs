using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;



public class BreakPlatform1Script : MonoBehaviour
{
    //Variabile:
    private CharacterScript4 character;
    private LogicManagerHubArea logicManagerHubArea;
    public AudioSource breakPlatformBlockSoundEnter;
    public AudioSource breakPlatformBlockSoundLeave;
    private Animator animator;
    public bool touchedByPlayer;

    //Functii predefinite:

    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScript4>();

        logicManagerHubArea = GameObject.FindGameObjectWithTag("LogicManagerHubArea")
            .GetComponent<LogicManagerHubArea>();

        animator = gameObject.GetComponent<Animator>();

        touchedByPlayer = false;
    }

    //Update:
    void Update()
    {
    }

    //Functii noi:

    //Pentru a distruge platforma la atingere:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && touchedByPlayer == false)
        {
            breakPlatformBlockSoundEnter.Play();

            touchedByPlayer = true;

            StartCoroutine(MakePlatformDisappear(1f));
        }
    }

    //Mor daca sunt inauntrul obiectului: Un trigger mai mic:
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            //&& touchedByPlayer == false
            )
        {
            //Nu merge cum doresc, asa ca nu fac nimic aici:
            //Debug.Log("Restart the scene!");
            //character.CharacterDeath();
            //logicManagerHubArea.RestartScene();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        character.CreateParticles();
    }

    //Functia ce distruge platforma, apelata mai sus: In functie de tag;
    private IEnumerator MakePlatformDisappear(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        breakPlatformBlockSoundLeave.Play();

        animator.SetTrigger("entry");

        BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;

        if (gameObject.tag == "BreakPlatform")
        {
            logicManagerHubArea.noPlatform1 = true;
        }
        else if (gameObject.tag == "BreakPlatform1")
        {
            logicManagerHubArea.noPlatform2 = true;
        }
        else if (gameObject.tag == "BreakPlatform2")
        {
            logicManagerHubArea.noPlatform3 = true;
        }
        else if (gameObject.tag == "BreakPlatform3")
        {
            logicManagerHubArea.noPlatform4 = true;
        }
        else if (gameObject.tag == "BreakPlatform4")
        {
            logicManagerHubArea.noPlatform5 = true;
        }
        else
        {
            Debug.Log("Not found!");
        }

        Destroy(gameObject, 1f);
    }
}
