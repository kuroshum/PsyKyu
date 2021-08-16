using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public bool Onhit;
    public bool DodgeobjHit;

    private void Awake()
    {
        Onhit = false;
        DodgeobjHit = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dodge")
        {
            DodgeobjHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Dodge")
        {
            DodgeobjHit = false;
        }
    }
}
