using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AppearingDeathObjectScript : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public AudioSource spikesSE;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private bool spikesAreVisible;
    private bool oneTimeOnly;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        //Sau game object:
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        spikesAreVisible = false;
        oneTimeOnly = false;
    }

    void Update()
    {
        if(spikesAreVisible == false
            && oneTimeOnly == false
            )
        {
            oneTimeOnly = true;
            StartCoroutine(MakeVisible());
        }
        else if (spikesAreVisible == true
            && oneTimeOnly == false
            )
        {
            oneTimeOnly = true;
            StartCoroutine(MakeInvisible());
        }

        //Doar daca este vizibil sound effect:
        //if(spriteRenderer.isVisible == true)
        if (spriteRenderer.isVisible == false)
        {
            spikesSE.Play(); //Visible now;
        }
    }

    //Functii noi:

    //Moarte daca atingi triggerul: (stay, altfel prea greu cand chiar intri)
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }

    //Make spikes visible:
    private IEnumerator MakeVisible()
    {
        yield return new WaitForSeconds(0.8f);

        spriteRenderer.enabled = true;
        boxCollider2D.enabled = true;

        spikesAreVisible = true;
        oneTimeOnly = false;
    }

    //Make spikes invisible:
    private IEnumerator MakeInvisible()
    {
        yield return new WaitForSeconds(0.8f);

        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;

        spikesAreVisible = false;
        oneTimeOnly = false;
    }
}
