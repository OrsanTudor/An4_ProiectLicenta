using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;



public class BreakPlatformLevel2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    private LogicManagerLevelTwo logicManagerLevel2;
    public AudioSource breakPlatformBlockSoundEnter;
    public AudioSource breakPlatformBlockSoundLeave;
    private Animator animator;
    public GameObject invisibleWall;

    //Functii predefinite:
    
    //Start:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        logicManagerLevel2 = GameObject.FindGameObjectWithTag("LogicManagerLevelTwo")
            .GetComponent<LogicManagerLevelTwo>();

        animator = gameObject.GetComponent<Animator>();
    }

    //Update:
    void Update()
    {
    }

    //Functii noi:

    //Pentru distrugerea platformei, verifici daca esti sau nu in putere:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && 
            (character.canIcon1 == false
            || character.canIcon2 == false
            || character.canIcon3 == false)
            )
        {
            StartCoroutine(MakePlatformDisappear(0.6f)); //1f;
        }
        else if(collision.gameObject.CompareTag("Character"))
        {
            breakPlatformBlockSoundEnter.Play();
        }
    }

    //Particule pentru exit:
    private void OnCollisionExit2D(Collision2D collision)
    {
        character.CreateParticles();
    }

    //Functia pentru distrugerea platformei:
    private IEnumerator MakePlatformDisappear(float timeToWait) 
    {
        yield return new WaitForSeconds(timeToWait);

        breakPlatformBlockSoundLeave.Play();

        animator.SetTrigger("entry");

        BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;

        if (gameObject.tag == "BreakPlatform3")
        {
            invisibleWall.GetComponent<BoxCollider2D>().enabled = false;

        }

        Destroy(gameObject, 0.6f); //1f;
    }
}
