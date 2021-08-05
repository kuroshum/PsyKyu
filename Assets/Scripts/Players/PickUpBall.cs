using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBall : MonoBehaviour
{
    // メインカメラ
    [SerializeField]
    private Camera mainCamera;

    // ボールを持っている場合に置くスペース
    [SerializeField]
    private GameObject ballIdleSpace;

    // ボールを持ってくるかどうかのフラグ
    private bool isPickUpBall;

    // ボールを持っているかどうかのフラグ
    private bool isIdleBall;

    // ボールを持ってくる時のスピード
    [SerializeField]
    private float ballSpeed;

    // ロックオンしているボール
    private GameObject lockonBall;


    // Start is called before the first frame update
    void Start()
    {
        isPickUpBall = false;
        isIdleBall = false;
    }

    private void getLookAtBoal()
    {
        // レイと衝突したオブジェクト
        RaycastHit hit;

        // メインカメラから飛ばすレイ
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

        // レイと衝突したオブジェクトが「FindBallArea」の場合
        // そのオブジェクトの親オブジェクト（ボール）を取得
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "FindBallArea")
            {
                // ロックオンしたボールに変化ない場合は更新しない
                if (lockonBall != hit.collider.transform.parent.gameObject)
                {
                    lockonBall = hit.collider.transform.parent.gameObject;
                }
            }
            else
            {
                // レイにオブジェクトが衝突しない場合はnull
                lockonBall = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // レイを飛ばした先にあるボールを取得
        if(isIdleBall == false)
        {
            getLookAtBoal();
        }
        // ボールを取得した場合にボールピックアップのフラグを立てる
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

        // ボールをピックアップする処理
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

        // ボールを持っているときの処理
        if(isIdleBall == true)
        {
            Vector3 f = new Vector3(0, Mathf.Sin(Time.time * Mathf.PI) / 5, 0);
            lockonBall.transform.position = ballIdleSpace.transform.position + f;
        }
    }
}
