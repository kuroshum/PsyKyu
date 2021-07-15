using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBall : MonoBehaviour
{
    // メインカメラ
    [SerializeField]
    private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
    }

    private GameObject getLookAtBoal()
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
                return hit.collider.gameObject.transform.parent.gameObject;
            }
        }
        
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        // レイを飛ばした先にあるボールを取得
        GameObject ball = getLookAtBoal();
        if (ball != null)
        {
            Debug.Log(ball);
        }
    }
}
