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

        // ���C���΂�����ɂ���{�[�����擾
        if (isIdleBall == false) { pub.getPlayerLookAtBoal(mainCamera); }

        // �{�[�����擾�����ꍇ�Ƀ{�[���s�b�N�A�b�v�̃t���O�𗧂Ă�
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

        // �{�[���������Ă���
        if (isPickUpBall == true)
        {
            pub.pickUp(ballIdleSpace, lockOnBall, ib);
        }

        // �{�[����idle����
        if (isIdleBall == true)
        {
            ib.Idle(ballIdleSpace, lockOnBall);
        }


    }

    private void Invinsible()
    {
        
    }
}
