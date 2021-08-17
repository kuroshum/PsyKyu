using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchBall : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Vector3 direction;   // Rayを飛ばす方向
    float distance = 10;    // Rayを飛ばす距離

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
        //メインカメラ上のマウスポインタのある位置からrayを飛ばす
        Ray ray = new Ray(transform.position, fwd);
        RaycastHit hit;
        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            //Rayが当たったオブジェクトのtagがPlayerだったら
            if (hit.collider.tag == "Ball")
            {
                Debug.Log("RayがBallに当たった");
                return true;
            }
        }
        return false;
    }
}
