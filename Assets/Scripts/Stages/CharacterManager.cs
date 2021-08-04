using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //prefab
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    //キャラクターグループ
    public static List<GameObject> groupA = new List<GameObject>();
    public static List<GameObject> groupB = new List<GameObject>();
    //ゲーム開始時の位置
    [SerializeField]private Transform startPlaceA;
    [SerializeField]private Transform startPlaceB;
    //リスポーン位置候補
//    public static List<RespawnBeacon> resList = new List<RespawnBeacon>();
    public static List<Transform> resList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug用
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("groupA:\n" + groupA + "\ngroupA.Count:\n" + groupA.Count);
            Debug.Log("groupB:\n" + groupB + "\ngroupB.Count:\n" + groupB.Count);
            Debug.Log("resList:\n" + resList + "\nresList.Count:\n" + resList.Count);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            RespawnCharacter(groupA[0]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RespawnCharacter(groupB[0]);
        }
    }

    //ゲームスタート時
    //キャラクター生成
    private void Init()
    {
        if(player != null && enemy != null)
        {
            groupA.Add(Instantiate(player, startPlaceA.transform.position, new Quaternion()));
            groupB.Add(Instantiate(enemy, startPlaceB.transform.position, new Quaternion()));
        }
    }
    
    //リスポーン時
    //キャラクター側から呼び出されるメソッド
    //キャラクター側はthis.gameObjectを引数に渡す
    public static void RespawnCharacter(GameObject gameObject)
    {
        if (groupA.Contains(gameObject))
        {
            Debug.Log("A Group");
            Respawn(gameObject, groupB);
        }
        else if (groupB.Contains(gameObject))
        {
            Debug.Log("B Group");
            Respawn(gameObject, groupA);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }


    //SetActive(true,false),移動
    public static void Respawn(GameObject gameObject, List<GameObject> list)
    {
        //gameObjectがplayerenemy以外ならエラー
        gameObject.SetActive(false);
        gameObject.transform.position = SelectRespawnPlace(list).position;
        //秒待機
        gameObject.SetActive(true);
    }

    //引数は相手のグループ
    //相手のグループの中心
    public static Transform SelectRespawnPlace(List<GameObject> list)
    {
        Vector3 v3 = new Vector3(0, 0, 0);
        foreach(GameObject g in list)
        {
            v3 += g.transform.position;
        }
        v3 /= list.Count;
        //一番遠いとこととそのインデックス
        float max = 0;
        int maxIndex = 0;
        for (int i = 0;i<resList.Count;i++)
        {
            if(max< (resList[i].transform.position - v3).sqrMagnitude)
            {
                max = (resList[i].transform.position - v3).sqrMagnitude;
                maxIndex = i;
            }
        }
        return resList[maxIndex];
    }
}
