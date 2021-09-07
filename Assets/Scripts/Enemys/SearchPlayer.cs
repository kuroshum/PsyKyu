using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchPlayer : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Vector3 direction;   // Rayを飛ばす方向
    //float distance = 10;    // Rayを飛ばす距離
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
        //メインカメラ上のマウスポインタのある位置からrayを飛ばす
        Ray ray = new Ray(transform.position, fwd);
        RaycastHit hit;
        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            //Rayが当たったオブジェクトのtagがPlayerだったら
            if (hit.collider.tag == "Player")
            {
                //Debug.Log("RayがPlayerに当たった");
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