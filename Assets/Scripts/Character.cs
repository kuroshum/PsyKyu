using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // ロックオンしているボール
    protected GameObject lockOnBall;
    public void SetLockOnBall(GameObject lockOnBall) { this.lockOnBall = lockOnBall; }
    public GameObject GetLockOnBall() { return lockOnBall; }


    // 
    protected PickUpBall pub;
    //
    protected IdleBall ib;


    // ボールを持ってくるかどうかのフラグ
    protected bool isPickUpBall;
    public void SetIsPickUpBall(bool isPickUpBall) { this.isPickUpBall = isPickUpBall; }
    
    // ボールを持っているかどうかのフラグ
    protected bool isIdleBall;
    public void SetIsIdleBall(bool isIdleBall) { this.isIdleBall = isIdleBall; }


    // Start is called before the first frame update
    void Awake()
    {
        isIdleBall = false;
        isPickUpBall = false;

    }

}
