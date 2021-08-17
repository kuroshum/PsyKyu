using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchBall : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Vector3 direction;   // Ray���΂�����
    float distance = 10;    // Ray���΂�����

    private Camera enemyCamera;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Ballhit()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        //���C���J������̃}�E�X�|�C���^�̂���ʒu����ray���΂�
        Ray ray = new Ray(transform.position, fwd);
        RaycastHit hit;
        //Ray�̉���    ��Ray�̌��_�@�@�@�@��Ray�̕����@�@�@�@�@�@�@�@�@��Ray�̐F
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            //Ray�����������I�u�W�F�N�g��tag��Player��������
            if (hit.collider.tag == "Ball")
            {
                Debug.Log("Ray��Ball�ɓ�������");
                return true;
            }
        }
        return false;
    }
}
