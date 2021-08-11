using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private float lefttime = 1.2f;
    private float chargebias = 1.5f;

    private float Straight_vec_bias = 5.0f;

    public Vector3 arrivepos;
    public Vector3 vec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowStraightBall(float chargetime, GameObject enemy, Rigidbody ball)
    {
        var vec = (enemy.transform.position - ball.transform.position).normalized * Straight_vec_bias * chargetime;
        ball.AddForce(vec, ForceMode.Impulse);
    }

    public void ThrowCurveBall(float chargetime, GameObject enemy, Vector3 FirstVec, GameObject ball, BallManager bm)
    {
        lefttime = 0.6f;
        vec = FirstVec;
        StartCoroutine(CurveThrow(chargetime, enemy , ball.GetComponent<Rigidbody>(), bm));
    }

    public void Feint()
    {
        // animationÇÉLÉÉÉìÉZÉãÇ∑ÇÈèàóù
    }

    IEnumerator CurveThrow(float chargetime, GameObject enemy, Rigidbody ball, BallManager bm)
    {
        Vector3 dis = enemy.transform.position - ball.transform.position;

        var acc = Vector3.zero;

        acc += 2 * (dis - vec * lefttime) / (lefttime * lefttime);

        lefttime -= Time.deltaTime;

        vec += acc * Time.deltaTime;

        ball.velocity = vec;

        if(lefttime < 0)
        {
            StopCoroutine(CurveThrow(chargetime, enemy, ball, bm));
        }
        else if(bm.Onhit == true)
        {
            StopCoroutine(CurveThrow(chargetime, enemy, ball, bm));
        }

        yield return null;

        StartCoroutine(CurveThrow(chargetime, enemy, ball, bm));
    }
}
