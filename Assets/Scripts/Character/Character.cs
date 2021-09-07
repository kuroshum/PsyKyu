using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // ロックオンしているボール
    protected GameObject lockOnBall;
    public void SetLockOnBall(GameObject lockOnBall) { this.lockOnBall = lockOnBall; }
    public GameObject GetLockOnBall() { return lockOnBall; }

    // キャッチしているボール
    protected GameObject catchedBall;
    public void SetCatchedBall(GameObject catchedBall) { this.catchedBall = catchedBall; }
    public GameObject GetCatchedBall() { return catchedBall; }

    // 自分に飛んでくるボール
    protected GameObject throwToMeBall;
    public void SetThrowToMeBall(GameObject throwToMeBall) { this.throwToMeBall = throwToMeBall; }
    public GameObject GetThrowToMeBall() { return throwToMeBall; }



    protected PickUpBall pub;
    protected IdleBall ib;
    protected CharacterDefence cd;
    protected CharacterJump cj;
    protected CharacterCatchSpaceManager ccpm;
    protected BallManager bm;


    // ボールを持ってくるかどうかのフラグ
    protected bool isPickUpBall;
    public void SetIsPickUpBall(bool isPickUpBall) { this.isPickUpBall = isPickUpBall; }
    
    // ボールを持っているかどうかのフラグ
    protected bool isIdleBall;
    public void SetIsIdleBall(bool isIdleBall) { this.isIdleBall = isIdleBall; }
    // 1フレーム前 : ボールを持っているかどうかのフラグ
    protected bool isPreIdleBall;

    // ボールをキャッチする状態になっているかどうか
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
