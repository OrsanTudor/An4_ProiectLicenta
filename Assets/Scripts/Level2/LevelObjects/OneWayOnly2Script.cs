using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OneWayOnly2Script : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel2 character;
    public BoxCollider2D boxCollider2D;
    private PlatformEffector2D platformEffector;
    private bool oneTimeEntry;

    //Functii predefinite:
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel2>();

        platformEffector = gameObject.GetComponent<PlatformEffector2D>();

        oneTimeEntry = false;
    }

    void Update()
    {
    }

    //Functii noi:

    //Se activeaza collider si one way only platform:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Character")
            && oneTimeEntry == false
            )
        {
            StartCoroutine(ActivateCollisions());

            oneTimeEntry = true;
        }
    }

    //Activare dupa putin timp:
    private IEnumerator ActivateCollisions()
    {
        yield return new WaitForSeconds(0.2f);

        boxCollider2D.enabled = true;
        platformEffector.enabled = true;
    }
}

