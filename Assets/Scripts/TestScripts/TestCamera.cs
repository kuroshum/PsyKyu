﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCamera : MonoBehaviour
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
    private float player_rotspeed = 1000.0f;

    private Vector3 cameranow_pos;

    public int Isaim = 0;
    private float aim_distance = 1.6f;
    private float aim_default;

    private bool touchground = false;
    private bool Isground = false;
    private bool Isjumped = false;

    private bool CanJump = true;

    private float jumpmove_speed_value = 0.6f;
    private float jumpforce = 5.0f;
    private float jumpforce_temp;
    private float jumpforce_xz = 1.5f;
    private float jumpforce_xz_temp;

    private float continuejumptime = 0.6f;
    //private int continuejump_cnt = 0;

    private Vector3 parentrot;

    //チャタリング対策　あんま効果なかった
    private float timelag_push = 0.2f;
    private bool Ispush = false;

    //Debug
    private float test_aimradius = 0.4f;
    [SerializeField]
    private Text test;
    [SerializeField]
    private Text test2;
    private float radius = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        harfpi = pi / 2.0f;
        radiuscamera = 0.9f;
        aim_default = radiuscamera;

        jumpforce_temp = jumpforce;
        jumpforce_xz_temp = jumpforce_xz;

        mx = 0;
        my = 1.0f / 6.0f * pi;

        aimobj.transform.localPosition = new Vector3(-test_aimradius * Mathf.Cos(my) * Mathf.Sin(mx - harfpi), test_aimradius * Mathf.Sin(my), -test_aimradius * Mathf.Cos(my) * Mathf.Cos(mx - harfpi));
        startpos = new Vector3(- radiuscamera * Mathf.Sin(mx), radiuscamera * Mathf.Sin(my),  - radiuscamera * Mathf.Cos(0));

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

    void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Gizmos.color = Color.green;
        RaycastHit hit;
        var isHit = Physics.SphereCast(ray, radius, out hit, 0.5f);

        if (isHit)
        {
            Gizmos.DrawRay(transform.position, -transform.up * hit.distance);
            Gizmos.DrawWireSphere(transform.position - transform.up * (hit.distance), radius);
        }
        else
        {
            Gizmos.DrawRay(transform.position, -transform.up * 0.5f);
            Gizmos.DrawWireSphere(transform.position - transform.up * 0.5f, radius);
        }
    }

        // Update is called once per frame
    void Update()
    {
        //Debug.Log("hi");

        //WASD使えるみたい
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        mousex = Input.GetAxis("Mouse X");
        mousey = Input.GetAxis("Mouse Y");

        Ray ray = new Ray(transform.position, -transform.up);

        RaycastHit hit;

        if (Physics.SphereCast(ray, radius,out hit, 1.0f))
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

        if (Input.GetMouseButton(1))
        {
            Isaim = 1;
        }
        else
        {
            Isaim = -1;
        }


        //if(continuejump_cnt == 0)
        //{
        //    jumpforce = jumpforce_temp;
        //    jumpforce_xz = jumpforce_xz_temp;
        //}
        //else if(continuejump_cnt == 1)
        //{
        //    jumpforce = jumpforce_temp * 0.7f;
        //    jumpforce_xz = jumpforce_xz_temp * 0.4f;
        //}
        //else if(continuejump_cnt >= 2)
        //{
        //    jumpforce = jumpforce_temp * 0.4f;
        //    jumpforce_xz = jumpforce_xz_temp * 0.2f;
        //}

        if (Isground == true)
        {
            ////Debug.Log(Isjumped);
            ////Debug.Log(continuejump_cnt);
            //if (Isjumped)
            //{
            //    CancelInvoke("jumpcontinue");
            //    Invoke("jumpcontinue", continuejumptime);
            //    continuejump_cnt++;
            //}

            if(Isjumped)
            {
                StartCoroutine(Jump_Time_Delay());
                Isjumped = false;
            }

        }

        //jump
        if (Isground && CanJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump();
            }

            //var jumpvec = new Vector3(inputHorizontal, 0, inputVertical).normalized * jumpforce_xz;
            //jumpvec += Vector3.up * jumpforce;

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    player_rigidbody.AddForce(jumpvec, ForceMode.Impulse);
            //    Isjumped = true;
            //    Invoke("jumpChataring", timelag_push);
            //    Ispush = true;
            //}
        }

        //移動
        move();

        //camerawork
        //CameraWork();

        //test2.text = Isaim.ToString();
    }

    private void FixedUpdate()
    {
        mx += mousex * rotate_Xsensi * Time.deltaTime;
        my -= mousey * rotate_Ysensi * Time.deltaTime;

        my = Mathf.Clamp(my, - 4.0f / 9.0f * pi, 4.0f / 9.0f * pi);


        //if (Isaim == 1)
        //{
        //    player_rotspeed = 500;
        //    var from = gameObject.transform.forward;
        //    var to = main.transform.forward;
        //    to.y = 0;
        //    to = to.normalized;

        //    var dis = to - from;
        //    var min = -0.02f;
        //    var max = 0.02f;

        //    if (Isbetween(dis.x, min, max) && Isbetween(dis.y, min, max) && Isbetween(dis.z, min, max))
        //    {

        //    }
        //    else
        //    {
        //        gameObject.transform.Rotate(0, player_rotspeed * Time.deltaTime, 0);
        //        aimobj.transform.Rotate(0, -player_rotspeed * Time.deltaTime, 0);
        //        mx -= player_rotspeed * Time.deltaTime * pi / 180.0f;
        //    }
        //}
        //else
        //{
        //    player_rotspeed = 200;
        //    if (inputHorizontal != 0 || inputVertical != 0)
        //    {
        //        var vec = gameObject.transform.forward;

        //        var vecf = main.transform.forward * inputVertical;
        //        vecf.y = 0;
        //        vecf = vecf.normalized;

        //        var vecr = main.transform.right * inputHorizontal;

        //        var subject = (vecf + vecr).normalized;

        //        //if (Isjumped)
        //        //    transform.position += vec * playerspeed_side * Time.deltaTime * jumpmove_speed_value;
        //        //else
        //        //    transform.position += vec * playerspeed_side * Time.deltaTime;

        //        var from = gameObject.transform.forward;
        //        var dis = subject - from;

        //        var min = -0.02f;
        //        var max = 0.02f;


        //        if (Isbetween(dis.x, min, max) && Isbetween(dis.y, min, max) && Isbetween(dis.z, min, max))
        //        {

        //        }
        //        else
        //        {
        //            gameObject.transform.Rotate(0, player_rotspeed * Time.deltaTime, 0);
        //            aimobj.transform.Rotate(0, -player_rotspeed * Time.deltaTime, 0);
        //            mx -= player_rotspeed * Time.deltaTime * pi / 180.0f;
        //        }
        //    }
        //}

        //テスト用　使わない
        //if (Isaim == 1)
        //{
        //    main.transform.localPosition = new Vector3(0 , radiuscamera * Mathf.Sin(my), -radiuscamera * Mathf.Cos(my));
        //}
        //else
        //{
        //    main.transform.localPosition = new Vector3(- radiuscamera * Mathf.Cos(my) * Mathf.Sin(mx), radiuscamera * Mathf.Sin(my), -radiuscamera * Mathf.Cos(my) * Mathf.Cos(mx));
        //}

        //var upvec = aimobj.transform.up;

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

        //CameraWork();

    }

    private void CameraWork()
    {
        // カメラワーク実装
        main.transform.localPosition = new Vector3(0, radiuscamera * Mathf.Sin(my), -radiuscamera * Mathf.Cos(my));

        aimobj.transform.Rotate(aimobj.transform.up, mousex * rotate_Xsensi * Time.deltaTime * 180.0f / pi);
        //aimobj.transform.rotation = aimobj.transform.rotation * Quaternion.AngleAxis(mousex * rotate_Xsensi * Time.deltaTime * 180.0f / pi, aimobj.transform.up);

        var tmp = new Vector3(-test_aimradius * Mathf.Cos(my) * Mathf.Sin(mx - harfpi), test_aimradius * Mathf.Sin(my) + 0.7f, -test_aimradius * Mathf.Cos(my) * Mathf.Cos(mx - harfpi));
        aimobj.transform.localPosition = tmp + transform.position;

        //main.transform.localPosition = cameranow_pos;

        main.transform.LookAt(aimobj.transform);
    }

    private void move()
    {
        if (Isaim == -1)
        {
            //Debug.Log("hi");

            //ディレイがかかる
            //if (inputHorizontal != 0 || inputVertical != 0)
            //{
            //    var vecf = main.transform.forward * inputVertical * playerspeed_front;
            //    var vecr = main.transform.right * inputHorizontal * playerspeed_front;

            //    var vec = (vecf + vecr).normalized;

            //    if (Isjumped)
            //        transform.position += vec * (playerspeed_front + playerspeed_side) / 2.0f * Time.deltaTime * jumpmove_speed_value * aim_speed_bias;
            //    else
            //        transform.position += vec * (playerspeed_front + playerspeed_side) / 2.0f * Time.deltaTime * aim_speed_bias;

            //}

            if (inputHorizontal != 0 || inputVertical != 0)
            {
                var vecf = main.transform.forward * inputVertical;
                var vecr = main.transform.right * inputHorizontal;

                var move_vec = (vecf + vecr);
                move_vec.y = 0;
                move_vec = move_vec.normalized;

                RotatePlayerDirection(move_vec);

                move_vec *= playerspeed_front;

                transform.position += move_vec * Time.deltaTime;
            }
              

        }
        else
        {
            var vecf = main.transform.forward * inputVertical;
            vecf.y = 0;
            vecf = vecf.normalized;

            var vecr = main.transform.right * inputHorizontal;

            var vec = (vecf + vecr).normalized;

            transform.position += vec * playerspeed_front * Time.deltaTime;
        }
    }

    private void jump()
    {
        var jumpvec = Vector3.up * jumpforce;

        CanJump = false;
        Isjumped = true;
        player_rigidbody.AddForce(jumpvec, ForceMode.Impulse);
    }

    private void RotatePlayerDirection(Vector3 refvec)
    {
        var vec = transform.forward;

        float costheta = CalcInner(refvec, vec) / (refvec.magnitude * vec.magnitude);
        float theta = Mathf.Acos(costheta);


        float degrees = 0;
        Vector3 outer = Vector3.zero;

        if (costheta == 1)
        {
            //Debug.Log(costheta);
            degrees = 0;
            outer = transform.up;
        }
        else if(costheta == -1)
        {
            degrees = 180;
            outer = transform.up;
        }
        else
        {
            degrees = theta * 180.0f / pi;
            outer = CalcOuter(refvec, vec).normalized;
        }

        var rot_deg = - Time.deltaTime * player_rotspeed;
        test.text = rot_deg.ToString();
        test2.text = degrees.ToString();

        if(degrees < rot_deg)
        {
            rot_deg = degrees;
        }

        if (degrees >= 3)
        {
            Quaternion rot = Quaternion.AngleAxis(rot_deg, outer);
            Quaternion q = transform.rotation;
            transform.rotation = q * rot;
        }

        //if (degrees <= 1)
        //{

        //}
        //else
        //{
        //    //theta = theta * Time.deltaTime;
        //    //mx -= theta;

        //    var outer = CalcOuter(refvec, vec).normalized;

        //    test.text = rot_deg.ToString();

        //    transform.Rotate(outer, - rot_deg);

        //    //Quaternion rot = Quaternion.AngleAxis(rot_deg, outer);
        //    //Quaternion q = transform.rotation;
        //    //transform.rotation = q * rot;

        //    //aimobj.transform.Rotate(outer, - rad);

        //    //Quaternion p = aimobj.transform.rotation;

        //    //float r = Quaternion.Angle(q, rot);

        //    //test2.text = outer.ToString();

        //    //transform.Rotate(new Vector3(0, rad * Time.deltaTime, 0));
        //}
    }

    private float CalcInner(Vector3 vec1, Vector3 vec2)
    {
        float inn;
        inn = vec1.x * vec2.x + vec1.y * vec2.y + vec1.z * vec2.z;

        return inn;
    }

    private Vector3 CalcOuter(Vector3 vec1, Vector3 vec2)
    {
        Vector3 vec;
        float x = vec1.y * vec2.z - vec2.y * vec1.z;
        float y = vec1.z * vec2.x - vec2.z * vec1.x;
        float z = vec1.x * vec2.y - vec2.x * vec1.y;

        vec = new Vector3(x,y,z);

        return vec;
    }

    //使ってないです
    private void CheckWall()
    {
        Ray leftray = new Ray(transform.position, - transform.right);
        Ray rightray = new Ray(transform.position, transform.right);
        Ray frontray = new Ray(transform.position, transform.forward);
        Ray backray = new Ray(transform.position, - transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(leftray, out hit, 0.6f))
        {

        }

        if(Physics.Raycast(rightray, out hit, 0.6f))
        {

        }

        if (Physics.Raycast(frontray, out hit, 0.6f))
        {

        }

        if (Physics.Raycast(backray, out hit, 0.6f))
        {

        }
    }

    IEnumerator Jump_Time_Delay()
    {
        yield return new WaitForSeconds(continuejumptime);
        CanJump = true;
    }

    private void LateUpdate()
    {
        CameraWork();
    }

    //TagによってGround判定を追加して
    //Ground箇所に新たにGround用のTriggerをおいてOnTriggerEnterにしたほうが良い
    private void OnCollisionEnter(Collision collision)
    {
        touchground = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!Isjumped)
        {
            var zero = Vector3.zero;
            zero.y = player_rigidbody.velocity.y;
            player_rigidbody.velocity = zero;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        touchground = false;
    }
}
