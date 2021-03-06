using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBall : MonoBehaviour
{
    public IEnumerator playMagicCircle(GameObject space, ParticleSystem magicCircle, float pauseMagicCircleSeconds)
    {
        magicCircle.transform.localPosition = space.transform.localPosition;
        magicCircle.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        magicCircle.Simulate(0.0f, true, true);
        magicCircle.Play();
        yield return new WaitForSeconds(pauseMagicCircleSeconds);
        magicCircle.Pause();
    }


    public void Idle(GameObject ballIdleSpace, GameObject lockOnBall)
    {
        Vector3 f = new Vector3(0, Mathf.Sin(Time.time * Mathf.PI) / 5, 0);
        lockOnBall.transform.position = ballIdleSpace.transform.position + f;
    }

}
