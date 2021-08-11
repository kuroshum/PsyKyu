using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [System.NonSerialized]
    public bool isBall;
    // Start is called before the first frame update
    void Start()
    {
        isBall = false;
        BallSpawnManager.ballResList.Add(this);
        //BallSpawnManager.ballResList.Add(this.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ball")
        {
            isBall = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ball")
        {
            isBall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ball")
        {
            isBall = false;
        }

    }
}
