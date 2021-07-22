using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouseOrController : MonoBehaviour
{
    private int debugmode;
    static private int maxnum = 2;

    TestCamera TC;
    TestCameraForController TCFC;

    private float mousex;
    private float mousey;

    private void Awake()
    {
        debugmode = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        TC = GetComponent<TestCamera>();
        TCFC = GetComponent<TestCameraForController>();

        switchplay();
    }

    // Update is called once per frame
    void Update()
    {
        mousex = Input.GetAxis("Mouse X");
        mousey = Input.GetAxis("Mouse Y");

        if (Input.anyKeyDown || mousex != 0 || mousey != 0)
        {
            if (debugmode != 1)
            {
                debugmode = 1;
                switchplay();
            }

        }
        else if(Input.GetButtonDown("checkcont"))
        {
            if (debugmode != 2)
            {
                debugmode = 2;
                switchplay();
            }

        }
    }

    private void FixedUpdate()
    {
        
    }

    private void switchplay()
    {
        switch (debugmode)
        {
            case 1:
                TC.enabled = true;
                TCFC.enabled = false;
                break;
            case 2:
                TC.enabled = false;
                TCFC.enabled = true;
                break;
        }
    }
}
