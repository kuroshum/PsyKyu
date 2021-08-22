using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // ロックオンしているボール
    protected GameObject lockOnBall;
    public void SetLockOnBall(GameObject lockOnBall) { this.lockOnBall = lockOnBall; }
    public GameObject GetLockOnBall() { return lockOnBall; }


    protected PickUpBall pub;
    protected IdleBall ib;
    protected CharacterDefence cd;
    protected CharacterJump cj;


    // ボールを持ってくるかどうかのフラグ
    protected bool isPickUpBall;
    public void SetIsPickUpBall(bool isPickUpBall) { this.isPickUpBall = isPickUpBall; }
    
    // ボールを持っているかどうかのフラグ
    protected bool isIdleBall;
    public void SetIsIdleBall(bool isIdleBall) { this.isIdleBall = isIdleBall; }

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
        isPickUpBall = false;
        isCatchBall = false;
        isTouchGround = false;
        isGround = false;
        isJumped = false;
        isJumped = false;
        canJump = true;
    }

}
