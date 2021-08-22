using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    // ���̃N���X���Ăяo�����N���X(Player or Enemy)
    private Character parent;
    public void SetParent(Character parent) { this.parent = parent; }

    // �W�����v����
    public void Jump(float jumpForce, ref Rigidbody rbody)
    {
        // �W�����v��������̃x�N�g��
        var jumpvec = Vector3.up * jumpForce;

        parent.SetCanJump(false);
        parent.SetIsJumped(true);

        rbody.AddForce(jumpvec, ForceMode.Impulse);
    }


    // �n�ʂɐڒn���Ă��邩�̔���
    public void CheckIsGround(bool isTouchGround, float checkIsGroundShphereRadius, Transform characterTransform, ref Rigidbody rbody)
    {
        // ���C���΂�
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
    
    // �W�����v���Ă��邩�̔���
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
