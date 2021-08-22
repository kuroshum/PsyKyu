using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    private Character parent;
    public void SetParent(Character parent) { this.parent = parent; }


    // �v���C���[�̈ړ�
    public void Move(bool isAim, float inputHorizontal, float inputVertical, float frontMoveSpeed, float rotateSpeed, Camera mainCamera)
    {
        if (isAim == false)
        {
            // �������͒l������ꍇ�̓v���C���[�𓮂���
            if (inputHorizontal != 0 || inputVertical != 0)
            {
                // ���͒l���琅���E���������̈ړ��x�N�g�������߂�
                var vecf = mainCamera.transform.forward * inputVertical;
                var vecr = mainCamera.transform.right * inputHorizontal;

                // �����E���������̃x�N�g���𓝍�
                var move_vec = (vecf + vecr);
                move_vec.y = 0;
                move_vec = move_vec.normalized;

                // �ړ���������Ƀv���C���[����]����
                RotatePlayerDirection(move_vec, rotateSpeed);

                // �v���C���[�̈ړ����x���ړ��x�N�g������v�Z
                move_vec *= frontMoveSpeed;

                // �v���C���[���ړ�
                transform.position += move_vec * Time.deltaTime;
            }

        }
        else
        {
            var vecf = mainCamera.transform.forward * inputVertical;
            vecf.y = 0;
            vecf = vecf.normalized;

            var vecr = mainCamera.transform.right * inputHorizontal;

            var vec = (vecf + vecr).normalized;

            transform.position += vec * frontMoveSpeed * Time.deltaTime;
        }
    }

    // �v���C���[�̈ړ�����ς�悤�ɉ�]
    private void RotatePlayerDirection(Vector3 refvec, float rotateSpeed)
    {
        var vec = transform.forward;

        float costheta = Vector3.Dot(refvec, vec) / (refvec.magnitude * vec.magnitude);


        float degrees = 0;
        float tmp = 0;
        Vector3 outer = Vector3.zero;

        if (costheta == 1)
        {
            degrees = 0;
            outer = transform.up;
        }
        else if (costheta == -1)
        {
            degrees = 180;
            outer = transform.up;
        }
        else
        {
            float theta = Mathf.Acos(costheta);
            degrees = theta * 180.0f / Mathf.PI;
            outer = Vector3.Cross(refvec, vec);
        }

        var rot_deg = -Time.deltaTime * rotateSpeed;
        tmp = rot_deg * Mathf.PI / 180.00f;

        if (degrees < rot_deg)
        {
            rot_deg = degrees;
        }

        if (degrees >= 3)
        {
            Quaternion rot = Quaternion.AngleAxis(rot_deg, outer);
            Quaternion q = transform.rotation;
            transform.rotation = q * rot;
        }

    }

}
