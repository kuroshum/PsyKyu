using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    // このクラスを呼び出したクラス(Player or Enemy)
    private Character parent;
    public void SetParent(Character parent) { this.parent = parent; }

    // ジャンプ処理
    public void Jump(float jumpForce, ref Rigidbody rbody)
    {
        // ジャンプする方向のベクトル
        var jumpvec = Vector3.up * jumpForce;

        parent.SetCanJump(false);
        parent.SetIsJumped(true);

        rbody.AddForce(jumpvec, ForceMode.Impulse);
    }


    // 地面に接地しているかの判定
    public void CheckIsGround(bool isTouchGround, float checkIsGroundShphereRadius, Transform characterTransform, ref Rigidbody rbody)
    {
        // レイを飛ばす
        Ray ray = new Ray(characterTransform.position, -characterTransform.up);
        RaycastHit hit;

        // 
        if (Physics.SphereCast(ray, checkIsGroundShphereRadius, out hit, 1.0f))
        {
            if (isTouchGround)
            {
                parent.SetIsGround(true);
            }
        }
        else
        {
            rbody.velocity -= new Vector3(0, 0.05f, 0);
            parent.SetIsGround(false);
        }
    }


    IEnumerator Jump_Time_Delay(float jumpContinueTime)
    {
        yield return new WaitForSeconds(jumpContinueTime);
        parent.SetCanJump(true);
    }
    
    // ジャンプしているかの判定
    public void CheckIsJumped(float jumpContinueTime, bool isGround, bool isJumped)
    {
        if (isGround == true)
        {
            if (isJumped == true)
            {
                StartCoroutine(Jump_Time_Delay(jumpContinueTime));
                parent.SetIsJumped(false);
            }

        }
    }
}
