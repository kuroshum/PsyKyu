using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallScriptThrow : MonoBehaviour
{
    public GameObject cube;

    private Vector3 arrivepos;
    private float lefttime;
    private Vector3 vec;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnBecameVisible()
    {
        lefttime = 0.4f;
        pos = transform.position;
        cube = GameObject.Find("Cube (3)");
        //vec = cube.transform.position - pos;
        vec = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (lefttime < - 0.5f)
        {

        }
        else
        {
            Vector3 dis = cube.transform.position - transform.position;

            var acc = Vector3.zero;

            acc += 2 * (dis - vec * lefttime) / (lefttime * lefttime);

            lefttime -= Time.deltaTime;

            vec += acc * Time.deltaTime;

            GetComponent<Rigidbody>().velocity = vec;
            //transform.position += vec * Time.deltaTime;
        }
    }
}
