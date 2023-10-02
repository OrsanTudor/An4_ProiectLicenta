using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Strategy2Script : MonoBehaviour, IStrategyScript
{
    //Variabile:
    private bool oneTimeMovement;
    private bool goDown;
    private bool goUp;

    //Functii noi:

    //Pentru movement pe axa y:
    public void MovingStrategy(MovingBallScript movingBallScript)
    {
        if (oneTimeMovement == true)
        {
            if (movingBallScript.transform.position.y >= movingBallScript.yLimitUp && goUp == true)
            {
                StartCoroutine(MoveOnYAxisDownDelayed(movingBallScript));
                oneTimeMovement = false;
                goUp = false;
                goDown = true;
            }
            else if (movingBallScript.transform.position.y <= movingBallScript.yLimitDown && goDown == true)
            {
                StartCoroutine(MoveOnYAxisUpDelayed(movingBallScript));
                oneTimeMovement = false;
                goDown = false;
                goUp = true;
            }
            else
            {
                if (goUp == true)
                {
                    StartCoroutine(MoveOnYAxisUp(movingBallScript));
                    oneTimeMovement = false;
                }
                else if (goDown == true)
                {
                    StartCoroutine(MoveOnYAxisDown(movingBallScript));
                    oneTimeMovement = false;
                }
            }
        }
    }

    //Pentru miscare 1 singura data, la un anumit timp, axa y down:
    public IEnumerator MoveOnYAxisDown(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(0.025f);

        movingBallScript.transform.position = new Vector3
            (
            movingBallScript.transform.position.x,
            movingBallScript.transform.position.y - 1f,
            movingBallScript.transform.position.z
            );

        oneTimeMovement = true;
    }

    //Pentru miscare 1 singura data, la un anumit timp, axa y up:
    public IEnumerator MoveOnYAxisUp(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(0.025f);

        movingBallScript.transform.position = new Vector3
            (
            movingBallScript.transform.position.x,
            movingBallScript.transform.position.y + 1f,
            movingBallScript.transform.position.z
            );

        oneTimeMovement = true;
    }

    //Schimba directia de miscare dupa un timp:
    public IEnumerator MoveOnYAxisDownDelayed(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(1f);

        oneTimeMovement = true;
    }

    //Schimba directia de miscare dupa un timp:
    public IEnumerator MoveOnYAxisUpDelayed(MovingBallScript movingBallScript)
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
    }

    //void Update()
    //{
    //}
}
