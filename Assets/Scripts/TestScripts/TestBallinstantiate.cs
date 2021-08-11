using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBallinstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject a = Instantiate(ball);
            a.transform.position = new Vector3(0, 10, 0);
        }
    }
}
