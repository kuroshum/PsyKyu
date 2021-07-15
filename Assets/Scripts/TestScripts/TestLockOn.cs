using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLockOn : MonoBehaviour
{
    private GameObject[] allenemy = new GameObject[10];
    public List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> Oncamera_enemys = new List<GameObject>();

    private TestCamera t1;

    // Start is called before the first frame update
    void Start()
    {
        t1 = GetComponent<TestCamera>();

        allenemy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject a in allenemy)
        {
            enemys.Add(a);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            
        }
    }

    void oncamera()
    {

    }
}
