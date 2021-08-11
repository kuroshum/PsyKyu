using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCollider : MonoBehaviour
{
    float pi = 3.1415f;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Cos(pi * t) * 5;
        float y = Mathf.Sin(pi * t) * 5;
        transform.position = new Vector3(x, y + 10, 30);
        t += Time.deltaTime;
    }
}
