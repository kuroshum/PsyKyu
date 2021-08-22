using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    private Character parent;
    public void SetParent(Character parent) { this.parent = parent; }


    // プレイヤーの移動
    public void Move(bool isAim, float inputHorizontal, float inputVertical, float frontMoveSpeed, float rotateSpeed, Camera mainCamera)
    {
        if (isAim == false)
        {
            // 何か入力値がある場合はプレイヤーを動かす
            if (inputHorizontal != 0 || inputVertical != 0)
            {
                // 入力値から水平・垂直方向の移動ベクトルを求める
                var vecf = mainCamera.transform.forward * inputVertical;
                var vecr = mainCamera.transform.right * inputHorizontal;

                // 水平・垂直方向のベクトルを統合
                var move_vec = (vecf + vecr);
                move_vec.y = 0;
                move_vec = move_vec.normalized;

                // 移動する向きにプレイヤーを回転する
                RotatePlayerDirection(move_vec, rotateSpeed);

                // プレイヤーの移動速度を移動ベクトルから計算
                move_vec *= frontMoveSpeed;

                // プレイヤーを移動
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

    // プレイヤーの移動先を観るように回転
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
