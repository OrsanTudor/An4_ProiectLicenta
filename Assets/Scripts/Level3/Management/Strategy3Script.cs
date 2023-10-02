using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Strategy3Script : MonoBehaviour, IStrategyScript
{
    //Variabile:
    private bool oneTimeSize;
    private bool goDown;
    private bool goUp;

    //Functii noi:

    //Pentru marire obiect:
    public void MovingStrategy(MovingBallScript movingBallScript)
    {
        //Compar cu x deoarece toate sunt la fel:
        if (oneTimeSize == true)
        {
            if (movingBallScript.transform.localScale.x >= movingBallScript.sizeUp && goUp == true)
            {
                StartCoroutine(SizeDownDelayed(movingBallScript));
                oneTimeSize = false;
                goUp = false;
                goDown = true;
            }
            else if (movingBallScript.transform.localScale.x <= movingBallScript.sizeDown && goDown == true)
            {
                StartCoroutine(SizeUpDelayed(movingBallScript));
                oneTimeSize = false;
                goDown = false;
                goUp = true;
            }
            else
            {
                if (goUp == true)
                {
                    StartCoroutine(SizeUp(movingBallScript));
                    oneTimeSize = false;
                }
                else if (goDown == true)
                {
                    StartCoroutine(SizeDown(movingBallScript));
                    oneTimeSize = false;
                }
            }
        }
    }

    //Pentru miscare 1 singura data, la un anumit timp, axa y down:
    public IEnumerator SizeDown(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(0.010f);

        movingBallScript.transform.localScale = new Vector3(
             movingBallScript.transform.localScale.x - 0.1f,
             movingBallScript.transform.localScale.y - 0.1f,
             movingBallScript.transform.localScale.z
             );

        oneTimeSize = true;
    }

    //Pentru miscare 1 singura data, la un anumit timp, axa y up:
    public IEnumerator SizeUp(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(0.010f);

        movingBallScript.transform.localScale = new Vector3(
             movingBallScript.transform.localScale.x + 0.1f,
             movingBallScript.transform.localScale.y + 0.1f,
             movingBallScript.transform.localScale.z
             );

        oneTimeSize = true;
    }

    //Schimba directia de miscare dupa un timp:
    public IEnumerator SizeDownDelayed(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(1f);

        oneTimeSize = true;
    }

    //Schimba directia de miscare dupa un timp:
    public IEnumerator SizeUpDelayed(MovingBallScript movingBallScript)
    {
        yield return new WaitForSeconds(1f);

        oneTimeSize = true;
    }

    //Functii predefinite:
    void Start()
    {
        oneTimeSize = true;
        goDown = false;
        goUp = true;
    }

    //void Update()
    //{
    //}
}