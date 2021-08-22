using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PickUpBall : MonoBehaviour
{
    // フィールド上に存在するボールのリスト
    [SerializeField]
    private List<GameObject> onFieldBallList;

    // カメラに映っているボールのリスト
    [SerializeField]
    private List<GameObject> onCameraBallList;

    private Character parent;
    public void SetParent(Character parent) { this.parent = parent; }
    
    private LockOn lo;

    // ボールを持ってくる時のスピード
    [SerializeField]
    private float ballSpeed;

    // Start is called before the first frame update
    void Start()
    {

        lo = GetComponent<LockOn>();
        onCameraBallList = new List<GameObject>();
        onFieldBallList = new List<GameObject>();

        GameObject[] allBall = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject a in allBall)
        {
            onFieldBallList.Add(a);
        }

    }



    public void getPlayerLookAtBoal(Camera camera)
    {
        // カメラに映っているボールを取得
        lo.AddListOnCameraTarget(camera, onFieldBallList, onCameraBallList, "Ball");

        // 持ってくるボールを選択
        // 取り敢えずインデックス０のものを取得
        if (onCameraBallList.Count != 0)
        {
            parent.SetLockOnBall(onCameraBallList[0]);
        }
        else
        {
            parent.SetLockOnBall(null);
        }

    }

    public Coroutine pickUp(GameObject ballIdleSpace, GameObject lockOnBall, IdleBall ib, ParticleSystem magicCircle)
    {
        Vector3 targetVec = ballIdleSpace.transform.position - lockOnBall.transform.position;

        if (targetVec.magnitude > 0.1f)
        {
            lockOnBall.transform.position += targetVec.normalized * ballSpeed * Time.deltaTime;
        }
        else
        {
            parent.SetIsPickUpBall(false);
            parent.SetIsIdleBall(true);
            return StartCoroutine(ib.playMagicCircle(ballIdleSpace, magicCircle, 1.8f));
        }
        return null;
    }

    public void StopPlayIdleCircleCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
