using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Strategy1Script : MonoBehaviour, IStrategyScript
{
    //Variabile:
    private bool oneTimeMovement;
    private bool goDown;
    private bool goUp;
    //private bool startCycle;

    //Functii noi:

    //Pentru movement pe axa x:
    public void MovingStrategy(MovingBallScript movingBallScript)
    {
        //Daca sa se faca miscarea:
        if (oneTimeMovement == true)
        {
            //Daca a ajuns la limita de sus sau jos:
            if(movingBallScript.transform.position.x >= movingBallScript.xLimitUp && goUp == true)
            {
                StartCoroutine(MoveOnXAxisDownDelayed(movingBallScript));
                oneTimeMovement = false; //Nu lasa celelalte
                //Incepe cealalta miscare:
                goUp = false;
                goDown = true;
            }
            else if(movingBallScript.transform.position.x <= movingBallScript.xLimitDown && goDown == true)
            {
                StartCoroutine(MoveOnXAxisUpDelayed(movingBallScript));
                oneTimeMovement = false;
                goDown = false;
                goUp = true;
            }
            //Prima data merge in sus, la dreapta:
            else
            {
                //Daca nu a ajuns la limite, merge in sus sau jos:
                if(goUp == true)
                {
                    StartCoroutine(MoveOnXAxisUp(movingBallScript));
                    oneTimeMovement = false;
                }
                else if (goDown == true)
                {
                    StartCoroutine(MoveOnXAxisDown(movingBallScript));
                    oneTimeMovement = false;
                }
            }
        }
    }

    //Pentru miscare 1 singura data, la un anumit timp, axa x down:
    public IEnumerator MoveOnXAxisDown(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(0.025f);

        movingBallScript.transform.position = new Vector3
            (
            movingBallScript.transform.position.x - 1f,
            movingBallScript.transform.position.y,
            movingBallScript.transform.position.z
            );

        oneTimeMovement = true;
        //if (movingBallScript.transform.position.x > movingBallScript.xLimitDown)
        //{
        //    oneTimeMovement = true;
        //}
        //else
        //{
        //    //Asteptare tranzitie:
        //    yield return new WaitForSeconds(1f);
        //    oneTimeMovement = true;
        //}
    }

    //Pentru miscare 1 singura data, la un anumit timp, axa x up:
    public IEnumerator MoveOnXAxisUp(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(0.025f);

        movingBallScript.transform.position = new Vector3
            (
            movingBallScript.transform.position.x + 1f,
            movingBallScript.transform.position.y,
            movingBallScript.transform.position.z
            );

        oneTimeMovement = true;
        //if (movingBallScript.transform.position.x < movingBallScript.xLimitUp)
        //{
        //    oneTimeMovement = true;
        //}
        //else
        //{
        //    yield return new WaitForSeconds(1f);
        //    oneTimeMovement = true;
        //}
    }

    //Schimba directia de miscare dupa un timp:
    public IEnumerator MoveOnXAxisDownDelayed(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(1f);

        oneTimeMovement = true;
    }

    //Schimba directia de miscare dupa un timp:
    public IEnumerator MoveOnXAxisUpDelayed(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(1f);

        oneTimeMovement = true;
    }

    //Functii predefinite:
    void Start()
    {
        oneTimeMovement = true;
        goDown = false;
        goUp = true;
        //startCycle = true;
    }

    //void Update()
    //{
    //}
}
