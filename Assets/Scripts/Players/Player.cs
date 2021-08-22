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

        cd = GetComponent<CharacterDefence>();
        cd.SetParent(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Isinvinsible)
        {

        }

        // ���C���΂�����ɂ���{�[�����擾
        if (isIdleBall == false) { pub.getPlayerLookAtBoal(mainCamera); }

        // �{�[�����擾�����ꍇ�Ƀ{�[���s�b�N�A�b�v�̃t���O�𗧂Ă�
        if (lockOnBall != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
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
            pub.pickUp(ballIdleSpace, lockOnBall, ib, magicCircle);
        }

        // �{�[����idle����
        if (isIdleBall == true)
        {
            ib.Idle(ballIdleSpace, lockOnBall);
        }

        // �{�[�����L���b�`����
        if (Input.GetKeyDown(KeyCode.Q) && isCatchBall == false)
        {
            cd.CatchBall(forwardSpace, magicCircle);
        }


    }

    private void Invinsible()
    {
        
    }
}
