using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // ���b�N�I�����Ă���{�[��
    protected GameObject lockOnBall;
    public void SetLockOnBall(GameObject lockOnBall) { this.lockOnBall = lockOnBall; }
    public GameObject GetLockOnBall() { return lockOnBall; }

    // �L���b�`���Ă���{�[��
    protected GameObject catchedBall;
    public void SetCatchedBall(GameObject catchedBall) { this.catchedBall = catchedBall; }
    public GameObject GetCatchedBall() { return catchedBall; }

    // �����ɔ��ł���{�[��
    protected GameObject throwToMeBall;
    public void SetThrowToMeBall(GameObject throwToMeBall) { this.throwToMeBall = throwToMeBall; }
    public GameObject GetThrowToMeBall() { return throwToMeBall; }



    protected PickUpBall pub;
    protected IdleBall ib;
    protected CharacterDefence cd;
    protected CharacterJump cj;
    protected CharacterCatchSpaceManager ccpm;
    protected BallManager bm;


    // �{�[���������Ă��邩�ǂ����̃t���O
    protected bool isPickUpBall;
    public void SetIsPickUpBall(bool isPickUpBall) { this.isPickUpBall = isPickUpBall; }
    
    // �{�[���������Ă��邩�ǂ����̃t���O
    protected bool isIdleBall;
    public void SetIsIdleBall(bool isIdleBall) { this.isIdleBall = isIdleBall; }
    // 1�t���[���O : �{�[���������Ă��邩�ǂ����̃t���O
    protected bool isPreIdleBall;

    // �{�[�����L���b�`�����ԂɂȂ��Ă��邩�ǂ���
    protected bool isCatchBall;
    public void SetIsCatchBall(bool isCatchBall) { this.isCatchBall = isCatchBall; }

    // 
    protected bool isTouchGround;
    public void SetIsTouchGround(bool isTouchGround) { this.isTouchGround = isTouchGround; }
    // 
    protected bool isGround;
    public void SetIsGround(bool isGround) { this.isGround = isGround; }
    //
    protected bool isJumped;
    public void SetIsJumped(bool isJumped) { this.isJumped = isJumped; }
    //
    protected bool canJump;
    public void SetCanJump(bool canJump) { this.canJump = canJump; }

    protected void InitCharacterFlags()
    {
        isIdleBall = false;
        isPreIdleBall = false;
        isPickUpBall = false;
        isCatchBall = true;
        isTouchGround = false;
        isGround = false;
        isJumped = false;
        isJumped = false;
        canJump = true;
    }

}
