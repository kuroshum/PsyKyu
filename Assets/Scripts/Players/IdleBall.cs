using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBall : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem magicCircle;

    private float pauseMagicCircleSeconds;

    void Start()
    {
        pauseMagicCircleSeconds = 1.8f;
    }

    IEnumerator playMagicCircle()
    {
        magicCircle.Play();
        yield return new WaitForSeconds(pauseMagicCircleSeconds);
        magicCircle.Pause();
    }



    public void Init()
    {
        StartCoroutine(playMagicCircle());
    }

    public void Idle(GameObject ballIdleSpace, GameObject lockOnBall)
    {
        Vector3 f = new Vector3(0, Mathf.Sin(Time.time * Mathf.PI) / 5, 0);
        lockOnBall.transform.position = ballIdleSpace.transform.position + f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
