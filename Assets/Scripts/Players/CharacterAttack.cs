using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private float lefttime = 1.2f;
    private float chargebias = 1.5f;

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

    public void ThrowBall(float chargetime, Vector3 enemypos, Vector3 FirstVec,GameObject ball)
    {
        lefttime = 1.2f;
        vec = FirstVec;
        StartCoroutine(Throw(chargetime, enemypos ,ball ));
    }

    public void Feint()
    {

    }

    IEnumerator Throw(float chargetime, Vector3 epos, GameObject ball)
    {
        Vector3 dis = arrivepos - ball.transform.position;

        var acc = Vector3.zero;

        acc += 2 * (dis - vec * lefttime) / lefttime * lefttime;

        lefttime -= Time.deltaTime;

        vec += acc * Time.deltaTime;

        ball.transform.position += vec * Time.deltaTime;

        yield return null;
    }
}
