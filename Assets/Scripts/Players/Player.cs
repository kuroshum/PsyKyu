using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // ���C���J����
    [SerializeField]
    private Camera mainCamera;

    // �{�[���������Ă���ꍇ�ɒu���X�y�[�X
    [SerializeField]
    private GameObject ballIdleSpace;

    // �{�[�����L���b�`�E������Ƃ��ɖ��@�w��u���X�y�[�X
    [SerializeField]
    private GameObject catchSpace;

    // �{�[�����L���b�`�EIdle����Ƃ��ɕ\�����閂�@�w
    [SerializeField]
    private ParticleSystem magicCircle;

    // �G�C��
    [SerializeField]
    private GameObject aimObj;

    // �v���C���[�ɃA�^�b�`����Ă��郊�M�b�h�{�f�B
    [SerializeField]
    private Rigidbody rigidBody;

    // �O���ړ��̑��x
    [SerializeField]
    private float frontMoveSpeed;

    // �����ύX�̉�]�̑��x
    [SerializeField]
    private float rotateSpeed;

    // �W�����v��
    [SerializeField]
    private float jumpForce;
    
    // �W�����v�̌p������
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
    

    // ���s�����̈ړ����͒l
    private float inputHorizontal;
    // ���������̈ړ����͒l
    private float inputVertical;

    // �}�E�X��x���i���s�����j�̈ړ��l
    private float mouseX;
    // �}�E�X��y���i���������j�̈ړ��l
    private float mouseY;

    // mouseX�̗ݐϘa
    private float sumMouseX;
    // mouseX�̗ݐϘa
    private float sumMouseY;

    private bool isAim;

    // �{�[����Idle���ɍĐ�����R���[�`��
    // �L���b�`����Ƃ��Ɏ~�߂�K�v������̂ŕϐ���p�ӂ��Ă�
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

        // �t���O�̏�����
        InitCharacterFlags();

        // 
        aimObj.transform.localPosition = new Vector3(aimRadius * Mathf.Cos(mouseY) * Mathf.Sin(mouseX), aimRadius * Mathf.Sin(mouseY) + 0.1f, aimRadius * Mathf.Cos(mouseY) * Mathf.Cos(mouseX)) + transform.position;
        // ���C���J�����̈ʒu
        Vector3 startPos = aimObj.transform.position - Vector3.forward * cameraRadius;
        mainCamera.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(Isinvinsible)
        {

        }

        // �v���C���[�̈ړ��l
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        // �v���C���[�̎��_�l
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


        if (Input.GetMouseButton(1))
        {
            isAim = true;
        }
        else
        {
            isAim = false;
        }


        // �n�ʂɐڒn���Ă��邩
        cj.CheckIsGround(isTouchGround, checkIsGroundShphereRadius, this.transform, ref rigidBody);

        // �W�����v���Ă��邩
        cj.CheckIsJumped(jumpContinueTime, isGround, isJumped);

        // �W�����v����
        if (isGround == true && canJump == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cj.Jump(jumpForce, ref rigidBody);
            }
        }

        // �ړ�����
        pm.Move(isAim, inputHorizontal, inputVertical, frontMoveSpeed, rotateSpeed, mainCamera);

        // ���C���΂�����ɂ���{�[�����擾
        if (isIdleBall == false) { pub.getPlayerLookAtBoal(mainCamera); }

        // �{�[�����擾�����ꍇ�Ƀ{�[���s�b�N�A�b�v�̃t���O�𗧂Ă�
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

        // �{�[���������Ă���
        if (isPickUpBall == true)
        {
            playIdleCircleCoroutine = pub.pickUp(ballIdleSpace, lockOnBall, ib, magicCircle);
        }

        // �{�[����idle����
        if (isIdleBall == true)
        {
            ib.Idle(ballIdleSpace, lockOnBall);
        }

        // �{�[�����L���b�`����
        if (Input.GetKeyDown(KeyCode.Q) && isCatchBall == false)
        {
            isIdleBall = false;
            if (playIdleCircleCoroutine != null)
            {
                pub.StopPlayIdleCircleCoroutine(playIdleCircleCoroutine);
                playIdleCircleCoroutine = null;
            }
            cd.CatchBall(catchSpace, magicCircle);
        }


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
     * �����蔻��
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
