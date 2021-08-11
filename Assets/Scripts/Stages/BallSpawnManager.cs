using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class BallSpawnManager : MonoBehaviour
{
    //prefab
    [SerializeField] private GameObject ball;
    //���X�|�[���ʒu���
//    public static List<Transform> ballResList = new List<Transform>();
    public static List<BallSpawner> ballResList = new List<BallSpawner>();
    public static int cnt = 0;
    public static List<GameObject> ballsList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public static void RespawnBall(GameObject gameObject)
    {
        //gameObject��playerenemy�ȊO�Ȃ�G���[
        gameObject.SetActive(false);
//        gameObject.transform.position = SelectRespawnPlace(ballResList).position;
        gameObject.transform.position = SelectRespawnPlace().transform.position;
        //�b�ҋ@
        gameObject.SetActive(true);
    }

    void Init()
    {
        ballResList = ballResList.OrderBy(a => Guid.NewGuid()).ToList();
        //����
        //ballResList�ŃP�c���琶��
        //�v���C���[*2�̐������B
        int ballGenNum = 4;
        for(int i = ballResList.Count - 1; i >= (ballResList.Count - ballGenNum); i--)
        {
            GameObject g = Instantiate(ball, ballResList[i].transform.position, new Quaternion());
            ballResList[i].isBall = true;
            ballsList.Add(g);
        }
    }

    //    public static Transform SelectRespawnPlace(List<Transform> list)
    public static BallSpawner SelectRespawnPlace()
    {
        int v = cnt;
        cnt = (cnt + 1) % ballResList.Count;
        for(int i = 0; i < ballResList.Count; i++)
        {
            if (ballResList[(v + i) % ballResList.Count].isBall == false) 
            {
                ballResList[(v + i) % ballResList.Count].isBall = true;
                return ballResList[(v + i) % ballResList.Count];
            }
        }
        return ballResList[v];
    }
}
