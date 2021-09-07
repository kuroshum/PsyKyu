using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchPlayer : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Vector3 direction;   // Ray���΂�����
    //float distance = 10;    // Ray���΂�����
    [SerializeField]
    private float searchAngle = 130f;
    private bool playerfind; 

    void Start()
    {
        playerfind = false;
    }
    
    public bool Playerhit()
    {
        /*
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        //���C���J������̃}�E�X�|�C���^�̂���ʒu����ray���΂�
        Ray ray = new Ray(transform.position, fwd);
        RaycastHit hit;
        //Ray�̉���    ��Ray�̌��_�@�@�@�@��Ray�̕����@�@�@�@�@�@�@�@�@��Ray�̐F
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            //Ray�����������I�u�W�F�N�g��tag��Player��������
            if (hit.collider.tag == "Player")
            {
                //Debug.Log("Ray��Player�ɓ�������");
                return true;
            }
        }
        return false;
        */
        return playerfind;
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {

            Debug.DrawLine(transform.position + Vector3.up, col.transform.position, Color.blue);
            var playerDirection = col.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //Physics.Raycast(transform.position + Vector3.up, playerDirection)
            if (angle <= searchAngle)
            {

                if (Physics.Linecast(transform.position + Vector3.up, col.transform.position, out hit))
                {
                    if (hit.collider.tag != col.tag)
                    {
                        playerfind = false;
                    }
                    else
                    {
                        playerfind = true;
                    }
                }
            }
        }
    }
}