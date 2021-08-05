using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBall : MonoBehaviour
{
    // ���C���J����
    [SerializeField]
    private Camera mainCamera;

    // �{�[���������Ă���ꍇ�ɒu���X�y�[�X
    [SerializeField]
    private GameObject ballIdleSpace;

    // �{�[���������Ă��邩�ǂ����̃t���O
    private bool isPickUpBall;

    // �{�[���������Ă��邩�ǂ����̃t���O
    private bool isIdleBall;

    // �{�[���������Ă��鎞�̃X�s�[�h
    [SerializeField]
    private float ballSpeed;

    // ���b�N�I�����Ă���{�[��
    private GameObject lockonBall;


    // Start is called before the first frame update
    void Start()
    {
        isPickUpBall = false;
        isIdleBall = false;
    }

    private void getLookAtBoal()
    {
        // ���C�ƏՓ˂����I�u�W�F�N�g
        RaycastHit hit;

        // ���C���J���������΂����C
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        // ���C�ƏՓ˂����I�u�W�F�N�g���uFindBallArea�v�̏ꍇ
        // ���̃I�u�W�F�N�g�̐e�I�u�W�F�N�g�i�{�[���j���擾
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "FindBallArea")
            {
                // ���b�N�I�������{�[���ɕω��Ȃ��ꍇ�͍X�V���Ȃ�
                if (lockonBall != hit.collider.transform.parent.gameObject)
                {
                    lockonBall = hit.collider.transform.parent.gameObject;
                }
            }
            else
            {
                // ���C�ɃI�u�W�F�N�g���Փ˂��Ȃ��ꍇ��null
                lockonBall = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���C���΂�����ɂ���{�[�����擾
        if(isIdleBall == false)
        {
            getLookAtBoal();
        }
        // �{�[�����擾�����ꍇ�Ƀ{�[���s�b�N�A�b�v�̃t���O�𗧂Ă�
        if (lockonBall != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
               isPickUpBall = true;
            }
        }
        else
        {
            isPickUpBall = false;
        }

        // �{�[�����s�b�N�A�b�v���鏈��
        if (isPickUpBall == true)
        {
            Vector3 targetVec = ballIdleSpace.transform.position - lockonBall.transform.position;

            if (targetVec.magnitude > 0.1f)
            {
                lockonBall.transform.position += targetVec.normalized * ballSpeed * Time.deltaTime;
            }
            else
            {
                isPickUpBall = false;
                isIdleBall = true;
            }
        }

        // �{�[���������Ă���Ƃ��̏���
        if(isIdleBall == true)
        {
            Vector3 f = new Vector3(0, Mathf.Sin(Time.time * Mathf.PI) / 5, 0);
            lockonBall.transform.position = ballIdleSpace.transform.position + f;
        }
    }
}
