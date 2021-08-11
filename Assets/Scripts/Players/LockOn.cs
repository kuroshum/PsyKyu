using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    private int num = 0;

    Rect rect = new Rect(0, 0, 1, 1);

    
    private void Awake()
    {
        //ここで自分のカメラをコンポネントしておくと良いかもSerialize fieldにぶち込んでprefab化しても良いかも
    }

    public void AddListOnCameraTarget(Camera mycam, List<GameObject> targetList, List<GameObject> onCameraTargetList, string targetTagName)
    {
        var viewport = mycam.WorldToViewportPoint(targetList[num % targetList.Count].transform.position);


        if (rect.Contains(viewport))
        {
            if (viewport.z >= 0)
            {
                Ray ray = new Ray(mycam.transform.position, (targetList[num % targetList.Count].transform.position - mycam.transform.position).normalized);

                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 10, new Color(1, 0, 0), 1.0f);

                if (Physics.Raycast(ray, out hit, 10))
                {
                    // enemyを変える必要あり
                    if (hit.collider.tag == targetTagName)
                    {
                        if (!onCameraTargetList.Contains(targetList[num % targetList.Count]))
                        {
                            onCameraTargetList.Add(targetList[num % targetList.Count]);
                        }
                    }
                }
            }

        }
        else
        {
            if (onCameraTargetList.Contains(targetList[num % targetList.Count]))
            {
                onCameraTargetList.Remove(targetList[num % targetList.Count]);
            }
        }

        num++;
    }

}
