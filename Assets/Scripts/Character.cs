using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // ���b�N�I�����Ă���{�[��
    protected GameObject lockOnBall;
    public void SetLockOnBall(GameObject lockOnBall) { this.lockOnBall = lockOnBall; }
    public GameObject GetLockOnBall() { return lockOnBall; }


    protected PickUpBall pub;
    protected IdleBall ib;
    protected CharacterDefence cd;


    // �{�[���������Ă��邩�ǂ����̃t���O
    protected bool isPickUpBall;
    public void SetIsPickUpBall(bool isPickUpBall) { this.isPickUpBall = isPickUpBall; }
    
    // �{�[���������Ă��邩�ǂ����̃t���O
    protected bool isIdleBall;
    public void SetIsIdleBall(bool isIdleBall) { this.isIdleBall = isIdleBall; }

    // �{�[�����L���b�`�����ԂɂȂ��Ă��邩�ǂ���
    protected bool isCatchBall;
    public void SetIsCatchBall(bool isCatchBall) { this.isCatchBall = isCatchBall; }


    // Start is called before the first frame update
    void Awake()
    {
        isIdleBall = false;
        isPickUpBall = false;
        isCatchBall = false;
    }

}
