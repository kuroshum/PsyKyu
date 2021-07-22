using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraForController : MonoBehaviour
{
    [SerializeField]
    private Camera main;
    [SerializeField]
    private GameObject aimobj;
    [SerializeField]
    private Rigidbody player_rigidbody;

    private float pi = 3.141592f;
    private float harfpi;

    private Vector3 startpos;
    private float radiuscamera;

    private float inputHorizontal;
    private float inputVertical;

    private float playerspeed_front = 4.0f;
    private float playerspeed_back = 2.5f;
    private float playerspeed_side = 3.0f;
    private float aim_speed_bias = 0.6f;

    //累積
    private float mx = 0;
    private float my = 0;

    private float mousex;
    private float mousey;

    private float rotate_Xsensi = 15.0f;
    private float rotate_Ysensi = 5.0f;

    //振り向き速度
    private float player_rotspeed = 200.0f;

    private Vector3 cameranow_pos;

    public int Isaim = 0;
    private float aim_distance = 1.6f;
    private float aim_default;

    private bool touchground = false;
    private bool Isground = false;
    private bool Isjumped = false;
    private float jumpmove_speed_value = 0.6f;
    private float jumpforce = 5.0f;
    private float jumpforce_temp;
    private float jumpforce_xz = 1.5f;
    private float jumpforce_xz_temp;

    private float continuejumptime = 0.5f;
    private int continuejump_cnt = 0;

    private Vector3 parentrot;

    //チャタリング対策　あんま効果なかった
    private float timelag_push = 0.2f;
    private bool Ispush = false;

    //Debug
    private float test_aimradius = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        harfpi = pi / 2.0f;
        radiuscamera = 2.0f;
        aim_default = radiuscamera;

        jumpforce_temp = jumpforce;
        jumpforce_xz_temp = jumpforce_xz;

        mx = 0;
        my = 1.0f / 6.0f * pi;

        aimobj.transform.localPosition = new Vector3(-test_aimradius * Mathf.Cos(my) * Mathf.Sin(mx - harfpi), test_aimradius * Mathf.Sin(my), -test_aimradius * Mathf.Cos(my) * Mathf.Cos(mx - harfpi));
        startpos = new Vector3(-radiuscamera * Mathf.Sin(mx), radiuscamera * Mathf.Sin(my), -radiuscamera * Mathf.Cos(0));

        main.transform.localPosition = startpos;
        cameranow_pos = startpos;
    }

    private bool Isbetween(float value, float min, float max)
    {
        if (min > max)
        {
            return value <= min && value >= max;
        }

        return value <= min && value >= max;
    }

    void jumpChataring()
    {
        Ispush = false;
    }

    void jumpcontinue()
    {
        continuejump_cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxis("HorizontalLeft");
        inputVertical = Input.GetAxis("VerticalLeft");

        if(inputHorizontal < 0.1f)
        {
            inputHorizontal = 0;
        }

        if(inputVertical < 0.1f)
        {
            inputVertical = 0;
        }

        mousex = Input.GetAxis("HorizontalRight") / 20.0f;
        mousey = Input.GetAxis("VerticalRight") / 20.0f;

        if(mousex < 0.1f)
        {
            mousex = 0;
        }

        if(mousey < 0.1f)
        {
            mousey = 0;
        }

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            if (touchground)
            {
                Isground = true;
            }
        }
        else
        {
            Isground = false;
        }

        if (Input.GetButtonDown("lockon"))
        {
            Isaim = 1;
        }
        else
        {
            Isaim = -1;
        }

        if (continuejump_cnt == 0)
        {
            jumpforce = jumpforce_temp;
            jumpforce_xz = jumpforce_xz_temp;
        }
        else if (continuejump_cnt == 1)
        {
            jumpforce = jumpforce_temp * 0.7f;
            jumpforce_xz = jumpforce_xz_temp * 0.4f;
        }
        else if (continuejump_cnt >= 2)
        {
            jumpforce = jumpforce_temp * 0.4f;
            jumpforce_xz = jumpforce_xz_temp * 0.2f;
        }

        if (Isground == true && Ispush == false)
        {
            //Debug.Log(Isjumped);
            //Debug.Log(continuejump_cnt);
            if (Isjumped)
            {
                CancelInvoke("jumpcontinue");
                Invoke("jumpcontinue", continuejumptime);
                continuejump_cnt++;
            }

            Isjumped = false;
        }

        if (Isground)
        {
            var jumpvec = new Vector3(inputHorizontal, 0, inputVertical).normalized * jumpforce_xz;
            jumpvec += Vector3.up * jumpforce;

            if (Input.GetButtonDown("jump_button"))
            {
                player_rigidbody.AddForce(jumpvec, ForceMode.Impulse);
                Isjumped = true;
                Invoke("jumpChataring", timelag_push);
                Ispush = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if (Isaim == 1)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                var vecf = main.transform.forward * inputVertical * playerspeed_front;
                var vecr = main.transform.right * inputHorizontal * playerspeed_side;

                var vec = (vecf + vecr).normalized;

                if (Isjumped)
                    transform.position += vec * (playerspeed_front + playerspeed_side) / 2.0f * Time.fixedDeltaTime * jumpmove_speed_value * aim_speed_bias;
                else
                    transform.position += vec * (playerspeed_front + playerspeed_side) / 2.0f * Time.fixedDeltaTime * aim_speed_bias;

            }
            else if (inputHorizontal != 0)
            {
                var vec = main.transform.right;

                if (Isjumped)
                    transform.position += vec * playerspeed_side * Time.fixedDeltaTime * inputHorizontal * jumpmove_speed_value * aim_speed_bias;
                else
                    transform.position += vec * playerspeed_side * Time.fixedDeltaTime * inputHorizontal * aim_speed_bias;
            }
            else if (inputVertical != 0)
            {
                var vec = main.transform.forward;
                vec.y = 0;
                vec = vec.normalized;

                if (Isjumped)
                    transform.position += vec * playerspeed_front * Time.fixedDeltaTime * inputVertical * jumpmove_speed_value * aim_speed_bias;
                else
                    transform.position += vec * playerspeed_front * Time.fixedDeltaTime * inputVertical * aim_speed_bias;
            }
        }
        else
        {
            var vecf = main.transform.forward * inputVertical;
            vecf.y = 0;
            vecf = vecf.normalized;

            var vecr = main.transform.right * inputHorizontal;

            var vec = (vecf + vecr).normalized;

            transform.position += vec * playerspeed_front * Time.fixedDeltaTime;
        }


        mx += mousex * rotate_Xsensi * Time.fixedDeltaTime;
        my -= mousey * rotate_Ysensi * Time.fixedDeltaTime;

        my = Mathf.Clamp(my, -4.0f / 9.0f * pi, 4.0f / 9.0f * pi);

        if (Isaim == 1)
        {
            player_rotspeed = 500;
            var from = gameObject.transform.forward;
            var to = main.transform.forward;
            to.y = 0;
            to = to.normalized;

            var dis = to - from;
            var min = -0.02f;
            var max = 0.02f;

            if (Isbetween(dis.x, min, max) && Isbetween(dis.y, min, max) && Isbetween(dis.z, min, max))
            {

            }
            else
            {
                gameObject.transform.Rotate(0, player_rotspeed * Time.fixedDeltaTime, 0);
                aimobj.transform.Rotate(0, -player_rotspeed * Time.fixedDeltaTime, 0);
                mx -= player_rotspeed * Time.fixedDeltaTime * pi / 180.0f;
            }
        }
        else
        {
            player_rotspeed = 200;
            if (inputHorizontal != 0 || inputVertical != 0)
            {
                var vec = gameObject.transform.forward;

                var vecf = main.transform.forward * inputVertical;
                vecf.y = 0;
                vecf = vecf.normalized;

                var vecr = main.transform.right * inputHorizontal;

                var subject = (vecf + vecr).normalized;

                //if (Isjumped)
                //    transform.position += vec * playerspeed_side * Time.fixedDeltaTime * jumpmove_speed_value;
                //else
                //    transform.position += vec * playerspeed_side * Time.fixedDeltaTime;

                var from = gameObject.transform.forward;
                var dis = subject - from;

                var min = -0.02f;
                var max = 0.02f;


                if (Isbetween(dis.x, min, max) && Isbetween(dis.y, min, max) && Isbetween(dis.z, min, max))
                {

                }
                else
                {
                    gameObject.transform.Rotate(0, player_rotspeed * Time.fixedDeltaTime, 0);
                    aimobj.transform.Rotate(0, -player_rotspeed * Time.fixedDeltaTime, 0);
                    mx -= player_rotspeed * Time.fixedDeltaTime * pi / 180.0f;
                }
            }
        }


        //テスト用　使わない
        //if (Isaim == 1)
        //{
        //    main.transform.localPosition = new Vector3(0 , radiuscamera * Mathf.Sin(my), -radiuscamera * Mathf.Cos(my));
        //}
        //else
        //{
        //    main.transform.localPosition = new Vector3(- radiuscamera * Mathf.Cos(my) * Mathf.Sin(mx), radiuscamera * Mathf.Sin(my), -radiuscamera * Mathf.Cos(my) * Mathf.Cos(mx));
        //}

        var upvec = aimobj.transform.up;

        if (Isaim == 1)
        {
            var vec = aimobj.transform.position - main.transform.position;

            if (vec.sqrMagnitude >= aim_distance * aim_distance)
            {
                radiuscamera -= Time.deltaTime * 20;
            }
        }
        else
        {
            var vec = aimobj.transform.position - main.transform.position;

            if (vec.sqrMagnitude <= aim_default * aim_default)
            {
                radiuscamera += Time.deltaTime * 20;
            }

            //微妙にずれる差を補正するやつ　未実装
            //else if(vec.sqrMagnitude > radiuscamera * radiuscamera)
            //{
            //    main.transform.localPosition = ;
            //}

        }

        //aimobj.transform.localRotation = new Quaternion(upvec.x, upvec.y, upvec.z, - mx / pi);
        //aimobj.transform.Rotate(aimobj.transform.right, mousey * rotate_Ysensi * Time.deltaTime * 180.0f / pi);

        // カメラワーク実装
        main.transform.localPosition = new Vector3(0, radiuscamera * Mathf.Sin(my), -radiuscamera * Mathf.Cos(my));

        aimobj.transform.Rotate(aimobj.transform.up, mousex * rotate_Xsensi * Time.deltaTime * 180.0f / pi);

        aimobj.transform.localPosition = new Vector3(-test_aimradius * Mathf.Cos(my) * Mathf.Sin(mx - harfpi), test_aimradius * Mathf.Sin(my) + 0.7f, -test_aimradius * Mathf.Cos(my) * Mathf.Cos(mx - harfpi));

        //main.transform.localPosition = cameranow_pos;

        main.transform.LookAt(aimobj.transform);

    }

    private void LateUpdate()
    {

    }

    //TagによってGround判定を追加して
    //Ground箇所に新たにGround用のTriggerをおいてOnTriggerEnterにしたほうが良い
    private void OnCollisionEnter(Collision collision)
    {
        touchground = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        touchground = false;
    }
}
