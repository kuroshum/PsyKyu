using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckRenderOnCamera : MonoBehaviour
{
    public int player_number;

    private List<GameObject> cursor_list = new List<GameObject>();
    [SerializeField]
    private List<GameObject> Oncamera_enemys = new List<GameObject>();

    [SerializeField]
    private Camera mycam;
    [SerializeField]
    private GameObject cursor;

    private int num = 0;
    private TestLockOn tl;

    Rect rect = new Rect(0, 0, 1, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        tl = GetComponent<TestLockOn>();

        for(int i = 0;i < 10;i++)
        {
            Instantiate(cursor);
            cursor.SetActive(false);
        }

        //ここで自分のカメラをコンポネントしておくと良いかもSerialize fieldにぶち込んでprefab化しても良いかも
    }

    // Update is called once per frame
    void Update()
    {
        var viewport = mycam.WorldToViewportPoint(tl.enemys[num % 6].transform.position);


        if(rect.Contains(viewport))
        {
            //Debug
            //Debug.Log("GameObject:" + tl.enemys[num%6].ToString() + "\nViewPort : " + viewport.ToString());

            //Ray ray = new Ray(gameObject.transform.position, tl.enemys[num % 6].transform.position);
            //Ray rightray = new Ray(gameObject.transform.position, tl.enemys[num % 6].transform.position + gameObject.transform.right * 0.45f);
            //Ray leftray = new Ray(gameObject.transform.position, tl.enemys[num % 6].transform.position + gameObject.transform.right * - 0.45f);
            //RaycastHit hit;

            //Debug.DrawRay(gameObject.transform.position, tl.enemys[num % 6].transform.position, new Color(1,0,0));

            //if (Physics.Raycast(ray, out hit, 20))
            //{
            //    // enemyを変える必要あり
            //    if (hit.collider.tag == "Enemy")
            //    {
            //        if (!tl.Oncamera_enemys.Contains(tl.enemys[num % 6]))
            //        {
            //            tl.Oncamera_enemys.Add(tl.enemys[num % 6]);
            //        }
            //    }
            //    else if (Physics.Raycast(rightray, out hit, 20))
            //    {
            //        // enemyを変える必要あり
            //        if (hit.collider.tag == "Enemy")
            //        {
            //            if (!tl.Oncamera_enemys.Contains(tl.enemys[num % 6]))
            //            {
            //                tl.Oncamera_enemys.Add(tl.enemys[num % 6]);
            //            }
            //        }
            //        else if (Physics.Raycast(leftray, out hit, 20))
            //        {
            //            if (hit.collider.tag == "Enemy")
            //            {
            //                if (!tl.Oncamera_enemys.Contains(tl.enemys[num % 6]))
            //                {
            //                    tl.Oncamera_enemys.Add(tl.enemys[num % 6]);
            //                }
            //            }
            //        }
            //    }
            //}

            if (viewport.z >= 0)
            {
                //Ray ray = new Ray(mycam.transform.position, (tl.enemys[num%6].transform.position - mycam.transform.position).normalized);
                Ray ray = new Ray(mycam.transform.position, (tl.enemys[num % 6].transform.position - mycam.transform.position).normalized);

                //Ray rightray = new Ray(gameObject.transform.position, tl.enemys[num % 6].transform.position + gameObject.transform.right * 0.45f);
                //Ray leftray = new Ray(gameObject.transform.position, tl.enemys[num % 6].transform.position + gameObject.transform.right * -0.45f);
                RaycastHit hit;

                Debug.DrawRay(ray.origin, ray.direction * 10, new Color(1, 0, 0), 1.0f);

                if (Physics.Raycast(ray, out hit, 10))
                {
                    // enemyを変える必要あり
                    if (hit.collider.tag == "Enemy")
                    {
                        if (!Oncamera_enemys.Contains(tl.enemys[num % 6]))
                        {
                            Oncamera_enemys.Add(tl.enemys[num % 6]);
                        }
                    }
                }
            }

        }
        else
        {
            if(Oncamera_enemys.Contains(tl.enemys[num%6]))
            {
                Oncamera_enemys.Remove(tl.enemys[num%6]);
            }
        }

        num++;
    }

    private void FixedUpdate()
    {
        //if(tl.Oncamera_enemys.Count > 0)
        //{
        //    foreach( GameObject a in tl.Oncamera_enemys)
        //    {
        //        var screenport = mycam.WorldToScreenPoint(a.transform.position);

        //        Image img = GetCursor();

        //        if (img != null)
        //        {
        //            img.rectTransform.anchoredPosition = screenport;
        //        }
        //    }
        //}
    }

    private Image GetCursor()
    {
        foreach(GameObject c in cursor_list)
        {
            if(!c.activeInHierarchy)
            {
                c.SetActive(true);
                var img = c.GetComponent<Image>();
                return img;
            }
        }

        return null;
    }

}
