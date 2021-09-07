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
    private GameObject catchSpace;

    // ボールをキャッチ・Idleするときに表示する魔法陣
    [SerializeField]
    private ParticleSystem magicCircle;

    // エイム
    [SerializeField]
    private GameObject aimObj;

    // プレイヤーにアタッチされているリギッドボディ
    [SerializeField]
    private Rigidbody rigidBody;

    // 前方移動の速度
    [SerializeField]
    private float frontMoveSpeed;

    // 向き変更の回転の速度
    [SerializeField]
    private float rotateSpeed;

    // ジャンプ力
    [SerializeField]
    private float jumpForce;
    
    // ジャンプの継続時間
    [SerializeField]
    private float jumpContinueTime;

    // 
    [SerializeField]
    private float cameraRadius;

    //
    [SerializeField]
    private float aimRadius;

    [SerializeField]
    private float aimDistance;

    [SerializeField]
    private float rotateXSensi;

    [SerializeField]
    private float rotateYSensi;

    [SerializeField]
    private float checkIsGroundShphereRadius;

    private PlayerMover pm;
    private PlayerCameraWork pcw;
    

    // 平行方向の移動入力値
    private float inputHorizontal;
    // 垂直方向の移動入力値
    private float inputVertical;

    // マウスのx軸（平行方向）の移動値
    private float mouseX;
    // マウスのy軸（垂直方向）の移動値
    private float mouseY;

    // mouseXの累積和
    private float sumMouseX;
    // mouseXの累積和
    private float sumMouseY;

    private bool isAim;

    // ボールをIdle時に再生するコルーチン
    // キャッチするときに止める必要があるので変数を用意してる
    private Coroutine playIdleCircleCoroutine;


    //
    public bool Isinvinsible = false;
    //
    public bool Canplay = false;




    // Start is called before the first frame update
    void Awake()
    {
        jumpForce *= rigidBody.mass;

        sumMouseX = 0.0f;
        sumMouseY = 1.0f / 6.0f * Mathf.PI;

        isAim = false;
    }

    void Start()
    {
        pub = GetComponent<PickUpBall>();
        pub.SetParent(this);

        ib = GetComponent<IdleBall>();

        cd = GetComponent<CharacterDefence>();
        cd.SetParent(this);

        pm = GetComponent<PlayerMover>();
        pm.SetParent(this);

        cj = GetComponent<CharacterJump>();
        cj.SetParent(this);

        pcw = GetComponent<PlayerCameraWork>();

        ccpm = catchSpace.GetComponent<CharacterCatchSpaceManager>();
        ccpm.SetParent(this);

        // フラグの初期化
        InitCharacterFlags();

        SetLockOnBall(null);
        SetCatchedBall(null);
        SetThrowToMeBall(null);

        // 
        aimObj.transform.localPosition = new Vector3(aimRadius * Mathf.Cos(mouseY) * Mathf.Sin(mouseX), aimRadius * Mathf.Sin(mouseY) + 0.1f, aimRadius * Mathf.Cos(mouseY) * Mathf.Cos(mouseX)) + transform.position;
        // メインカメラの位置
        Vector3 startPos = aimObj.transform.position - Vector3.forward * cameraRadius;
        mainCamera.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(Isinvinsible)
        {

        }

        // プレイヤーの移動値
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        // プレイヤーの視点値
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


        // 地面に接地しているか
        cj.CheckIsGround(isTouchGround, checkIsGroundShphereRadius, this.transform, ref rigidBody);

        // ジャンプしているか
        cj.CheckIsJumped(jumpContinueTime, isGround, isJumped);

        // ジャンプ処理
        if (isGround == true && canJump == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cj.Jump(jumpForce, ref rigidBody);
            }
        }

        // 移動処理
        pm.Move(inputHorizontal, inputVertical, frontMoveSpeed, rotateSpeed, mainCamera);

        // レイを飛ばした先にあるボールを取得
        if (isIdleBall == false) { pub.getPlayerLookAtBoal(mainCamera); }

        // ボールを取得した場合にボールピックアップのフラグを立てる
        if (lockOnBall != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && isIdleBall == false)
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
            pub.pickUp(ballIdleSpace, lockOnBall);
            //playIdleCircleCoroutine = StartCoroutine(ib.playMagicCircle(ballIdleSpace, magicCircle, 1.8f));
            
            // ロックオンしているボールをキャッチしているボールに変更する
            if (lockOnBall != null)
            {
                SetCatchedBall(lockOnBall);
                SetLockOnBall(null);
            }
        }

        // ボールをidleする
        if (isIdleBall == true)
        {
            // 
            if (catchedBall != null)
            {
                if (isPreIdleBall == false)
                {
                    bm = catchedBall.GetComponent<BallManager>();
                    bm.isCatched = true;
                    playIdleCircleCoroutine = StartCoroutine(ib.playMagicCircle(ballIdleSpace, magicCircle, 1.8f));
                }
                ib.Idle(ballIdleSpace, catchedBall);
            }
        }
        // ボールをIdleしてないときはボールのキャッチフラグを折る
        else
        {
            if (bm != null)
            {
                bm.isCatched = false;
                bm = null;
            }
        }


        // ===========================================================
        // ボールをキャッチする
        
        // ボールをキャッチする構えに入る
        if (Input.GetKeyDown(KeyCode.Q) && isCatchBall == true)
        {
            // ボールをIdleしているときはボールを離す
            SetIsIdleBall(false);
            
            // 魔法陣を表示している場合は解除する
            if (playIdleCircleCoroutine != null)
            {
                pub.StopPlayIdleCircleCoroutine(playIdleCircleCoroutine);
                playIdleCircleCoroutine = null;
            }
            // キャッチスペースに魔法陣を表示する
            // キャッチの構えに入る
            cd.PlayCatchMagicCircle(catchSpace, magicCircle, 0.5f);
        }

        // catchSpaceにボールがEnterした or ボールをキャッチする構えに入っている場合
        if (ccpm.GetOnBallHit() == true && isCatchBall == false)
        {
            // ボールをキャッチする
            cd.CatchBall(catchSpace, throwToMeBall);
        }
        // ===========================================================


        isPreIdleBall = isIdleBall;

    }

    void FixedUpdate()
    {
        sumMouseX += pcw.UpdateSumMouseX(mouseX, rotateXSensi);
        sumMouseY = pcw.UpdateSumMouseY(sumMouseY, mouseY, rotateXSensi);
    }

    void LateUpdate()
    {
        pcw.CameraWork(sumMouseX, sumMouseY, aimDistance, ref aimObj, ref mainCamera);
    }

    /*
     * ========================================================
     * 当たり判定
     */
    private void OnCollisionEnter(Collision collision)
    {
        isTouchGround = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isJumped)
        {
            var zero = Vector3.zero;
            zero.y = rigidBody.velocity.y;
            rigidBody.velocity = zero;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isTouchGround = false;
    }

    /*
     * =======================================================
     */

    private void Invinsible()
    {
        
    }
}
