using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public bool Onhit;

    private void Awake()
    {
        Onhit = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Onhit = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        Onhit = false;
    }
}
