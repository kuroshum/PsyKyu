using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // メインカメラ
    [SerializeField]
    private Camera mainCamera;

    // ボールを持っている場合に置くスペース
    [SerializeField]
    private GameObject ballIdleSpace;

    // ボールをキャッチ・投げるときに魔法陣を置くスペース
    [SerializeField]
    private GameObject forwardSpace;

    [SerializeField]
    private ParticleSystem magicCircle;


    //
    public bool Isinvinsible = false;
    //
    public bool Canplay = false;


    // Start is called before the first frame update
    void Awake()
    {
        pub = GetComponent<PickUpBall>();
        pub.SetParent(this);

        ib = GetComponent<IdleBall>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Isinvinsible)
        {

        }

        // レイを飛ばした先にあるボールを取得
        if (isIdleBall == false) { pub.getPlayerLookAtBoal(mainCamera); }

        // ボールを取得した場合にボールピックアップのフラグを立てる
        if (lockOnBall != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                isPickUpBall = true;
            }
        }
        else
        {
            isPickUpBall = false;
        }

        // ボールを持ってくる
        if (isPickUpBall == true)
        {
            pub.pickUp(ballIdleSpace, lockOnBall, ib);
            StartCoroutine(playMagicCircle(ballIdleSpace, magicCircle, 1.8f));
        }

        // ボールをidleする
        if (isIdleBall == true)
        {
            ib.Idle(ballIdleSpace, lockOnBall);
        }

        // ボールをキャッチする
        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(playMagicCircle(forwardSpace, magicCircle, 1.8f));
            cd.CatchBall(forwardSpace);
        }


    }

    private void Invinsible()
    {
        
    }
}
