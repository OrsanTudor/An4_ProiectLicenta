using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovingBallScript : MonoBehaviour
{
    //Variabile:
    private CharacterScriptLevel3 character;
    public LogicManagerLevelThree logicManagerLevelThree;
    public float xLimitDown;
    public float xLimitUp;
    public float yLimitDown;
    public float yLimitUp;
    public float sizeDown;
    public float sizeUp;
    private List<IStrategyScript> strategyScripts = new List<IStrategyScript>();

    //Nu ai obiect in scena, deci dai add la scripts la componente,
    //pe care le creezi folosind scripturile existente;
    //Nu am nevoie de obiect nou, atasez scripturile de acesta:
    //private GameObject strategyUsed; //Obiectul nu trebuie instantiat, nu il folosesc, doar scripturile;

    //Functii noi:
    
    //Functia de apel a strategiei:
    public void ApplyStrategy(IStrategyScript strategyScript)
    {
        strategyScript.MovingStrategy(this); //Dai scriptul;
    }

    //Daca bila atinge caracterul, caracterul moare:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            character.CharacterDeath();
        }
    }

    //Functii predefinite:
    void Start()
    {
        //Instantiere obiect:
        //strategyUsed =
        //    GameObject.CreatePrimitive(PrimitiveType.Cube);
        //strategyUsed.AddComponent<MovingBallScript>();

        character = GameObject.FindGameObjectWithTag("Character")
            .GetComponent<CharacterScriptLevel3>();

        logicManagerLevelThree = GameObject.FindGameObjectWithTag("LogicManagerLevelThree")
            .GetComponent<LogicManagerLevelThree>();

        //Setate doar prima data:
        float xCoeficient = 40f;
        float yCoeficient = 40f;
        float sizeCoeficient = 2f;
        xLimitDown = transform.position.x - xCoeficient;
        xLimitUp = transform.position.x + xCoeficient;
        yLimitDown = transform.position.y - yCoeficient;
        yLimitUp = transform.position.y + yCoeficient;
        sizeDown = transform.localScale.x - sizeCoeficient; //Nu se poate mai mic;
        sizeUp = transform.localScale.y + sizeCoeficient;

        //Add instante: la obiectul ce foloseste strategia:
        //strategyScripts.Add(strategyUsed.AddComponent<Strategy1Script>());
        //strategyScripts.Add(strategyUsed.AddComponent<Strategy2Script>());
        //strategyScripts.Add(strategyUsed.AddComponent<Strategy3Script>());

        //Adaug scripturile direct la acest obiect:
        //Puteam ori lista pentru a le pastra, ori direct cu get component:
        strategyScripts.Add(gameObject.AddComponent<Strategy2Script>());
        strategyScripts.Add(gameObject.AddComponent<Strategy1Script>());
        strategyScripts.Add(gameObject.AddComponent<Strategy3Script>());
    }

    void Update()
    {
        //Apply strategy 1:
        if (
            character.canIcon1 == false
            //&& character.charState == true
            && logicManagerLevelThree.gameIsPaused == false
            )
        {
            //gameObject.GetComponent<MovingBallScript>().ApplyStrategy(strategyScripts[0]);
            ApplyStrategy(strategyScripts[0]);
        }
        //Apply strategy 2:
        else if (
            character.canIcon2 == false
            //&& character.charState == true
            && logicManagerLevelThree.gameIsPaused == false
            )
        {
            //gameObject.GetComponent<MovingBallScript>().ApplyStrategy(strategyScripts[1]);
            ApplyStrategy(strategyScripts[1]);
        }
        //Apply strategy 3:
        else if (
            character.canIcon3 == false
            //&& character.charState == true
            && logicManagerLevelThree.gameIsPaused == false
            )
        {
            //gameObject.GetComponent<MovingBallScript>().ApplyStrategy(strategyScripts[2]);
            ApplyStrategy(strategyScripts[2]);
        }
        //Apply no strategy:
        else
        {
            //Nothing;
        }
    }
}
