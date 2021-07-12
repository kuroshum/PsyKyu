using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBall : MonoBehaviour
{
    // ���C���J����
    [SerializeField]
    private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
    }

    private GameObject getLookAtBoal()
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
                return hit.collider.gameObject.transform.parent.gameObject;
            }
        }
        
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        // ���C���΂�����ɂ���{�[�����擾
        GameObject ball = getLookAtBoal();
        if (ball != null)
        {
            Debug.Log(ball);
        }
    }
}
