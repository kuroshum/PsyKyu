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
    [SerializeField]
    private GameObject lockonBall;

    // �t�B�[���h��ɑ��݂���{�[���̃��X�g
    [SerializeField]
    private List<GameObject> onFieldBallList;

    // �J�����ɉf���Ă���{�[���̃��X�g
    [SerializeField]
    private List<GameObject> onCameraBallList;

    [SerializeField]
    private ParticleSystem magicCircle;


    private LockOn lo;


    // Start is called before the first frame update
    void Start()
    {
        isPickUpBall = false;
        isIdleBall = false;

        lo = GetComponent<LockOn>();
        onCameraBallList = new List<GameObject>();
        onFieldBallList = new List<GameObject>();

        GameObject[] allBall = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject a in allBall)
        {
            onFieldBallList.Add(a);
        }

    }

    private void getLookAtBoal()
    {
        // �J�����ɉf���Ă���{�[�����擾
        lo.AddListOnCameraTarget(mainCamera, onFieldBallList, onCameraBallList, "Ball");
        // �����Ă���{�[����I��
        // ��芸�����C���f�b�N�X�O�̂��̂��擾
        if (onCameraBallList.Count != 0)
        {
            lockonBall = onCameraBallList[0];
        }
        else
        {
            lockonBall = null;
        }

        /*
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
        */
    }

    IEnumerator playMagicCircle()
    {
        magicCircle.Play();
        yield return new WaitForSeconds(1f);
        magicCircle.Pause();
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
                StartCoroutine(playMagicCircle());
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
