using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraWork : MonoBehaviour
{
    public float UpdateSumMouseX(float mouseX, float rotateXSensi)
    {
        return mouseX * rotateXSensi * Time.deltaTime;
    }

    public float UpdateSumMouseY(float sumMouseY, float mouseY, float rotateYSensi)
    {
        sumMouseY -= mouseY * rotateYSensi * Time.deltaTime;

        return Mathf.Clamp(sumMouseY, -4.0f / 9.0f * Mathf.PI, 4.0f / 9.0f * Mathf.PI);
    }

    // ÉJÉÅÉâÉèÅ[ÉNé¿ëï
    public void CameraWork(float sumMouseX, float sumMouseY, float aimDistance, ref GameObject aimObj, ref Camera mainCamera)
    {
        Quaternion p = Quaternion.AngleAxis(sumMouseX * 180.0f / Mathf.PI, Vector3.up);
        Quaternion q = Quaternion.AngleAxis(sumMouseY * 180.0f / Mathf.PI, Vector3.right);

        aimObj.transform.rotation = p * q;

        aimObj.transform.position = transform.position + aimObj.transform.right * aimDistance + Vector3.up * 0.3f;

        mainCamera.transform.LookAt(aimObj.transform.position);
    }
}
