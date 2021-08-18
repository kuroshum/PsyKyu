using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBall : MonoBehaviour
{
    public void Idle(GameObject ballIdleSpace, GameObject lockOnBall)
    {
        Vector3 f = new Vector3(0, Mathf.Sin(Time.time * Mathf.PI) / 5, 0);
        lockOnBall.transform.position = ballIdleSpace.transform.position + f;
    }

}
