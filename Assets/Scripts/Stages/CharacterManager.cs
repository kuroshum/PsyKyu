using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //prefab
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    //キャラクターグループ
//    public static List<GameObject> groupA = new List<GameObject>();
//    public static List<GameObject> groupB = new List<GameObject>();
    public static List<Character> groupA = new List<Character>();
    public static List<Character> groupB = new List<Character>();
    public static List<Player> playerList = new List<Player>();
    public static List<Enemy> enemyList = new List<Enemy>();
    //ゲーム開始時の位置
    [SerializeField]private Transform startPlaceA;
    [SerializeField]private Transform startPlaceB;
    //リスポーン位置候補
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
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("groupA:\n" + groupA + "\ngroupA.Count:\n" + groupA.Count);
            Debug.Log("groupB:\n" + groupB + "\ngroupB.Count:\n" + groupB.Count);
            Debug.Log("resList:\n" + resList + "\nresList.Count:\n" + resList.Count);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            RespawnCharacter(groupA[0]);
        }
        if (Input.GetKeyDown(KeyCode.M))
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
            //            groupA.Add(Instantiate(player, startPlaceA.transform.position, new Quaternion()));
            //            groupB.Add(Instantiate(enemy, startPlaceB.transform.position, new Quaternion()));
            GameObject g = Instantiate(player, startPlaceA.transform.position, new Quaternion());
            Player p = g.GetComponent<Player>();
            groupA.Add(p);
            playerList.Add(p);

            g = Instantiate(enemy, startPlaceB.transform.position, new Quaternion());
            Enemy e = g.GetComponent<Enemy>();
            groupB.Add(e);
            enemyList.Add(e);
        }
    }

    //リスポーン時
    //キャラクター側から呼び出されるメソッド
    //キャラクター側はthisを引数に渡す
    public static void RespawnCharacter(Character gameObject)
    {
        if (groupA.Contains(gameObject))
        {
            Debug.Log("A Group");
            Respawn(gameObject.gameObject, groupB);
        }
        else if (groupB.Contains(gameObject))
        {
            Debug.Log("B Group");
            Respawn(gameObject.gameObject, groupA);
        }
        else
        {
            Debug.Log("ERROR");
        }
    }
    /*
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
    }*/


    //SetActive(true,false),移動
    public static void Respawn(GameObject gameObject, List<Character> list)
    {
        //gameObjectがplayerenemy以外ならエラー
        gameObject.SetActive(false);
        gameObject.transform.position = SelectRespawnPlace(list).position;
        //秒待機
        gameObject.SetActive(true);
    }
    /*
    public static void Respawn(GameObject gameObject, List<GameObject> list)
    {
        //gameObjectがplayerenemy以外ならエラー
        gameObject.SetActive(false);
        gameObject.transform.position = SelectRespawnPlace(list).position;
        //秒待機
        gameObject.SetActive(true);
    }*/

    //引数は相手のグループ
    //相手のグループの中心
    public static Transform SelectRespawnPlace(List<Character> list)
    {
        Vector3 v3 = new Vector3(0, 0, 0);
        foreach(Character g in list)
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
