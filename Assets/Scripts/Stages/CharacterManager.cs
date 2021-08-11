using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //prefab
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    //�L�����N�^�[�O���[�v
//    public static List<GameObject> groupA = new List<GameObject>();
//    public static List<GameObject> groupB = new List<GameObject>();
    public static List<Character> groupA = new List<Character>();
    public static List<Character> groupB = new List<Character>();
    public static List<Player> playerList = new List<Player>();
    public static List<Enemy> enemyList = new List<Enemy>();
    //�Q�[���J�n���̈ʒu
    [SerializeField]private Transform startPlaceA;
    [SerializeField]private Transform startPlaceB;
    //���X�|�[���ʒu���
    public static List<Transform> resList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug�p
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

    //�Q�[���X�^�[�g��
    //�L�����N�^�[����
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

    //���X�|�[����
    //�L�����N�^�[������Ăяo����郁�\�b�h
    //�L�����N�^�[����this�������ɓn��
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
    //���X�|�[����
    //�L�����N�^�[������Ăяo����郁�\�b�h
    //�L�����N�^�[����this.gameObject�������ɓn��
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


    //SetActive(true,false),�ړ�
    public static void Respawn(GameObject gameObject, List<Character> list)
    {
        //gameObject��playerenemy�ȊO�Ȃ�G���[
        gameObject.SetActive(false);
        gameObject.transform.position = SelectRespawnPlace(list).position;
        //�b�ҋ@
        gameObject.SetActive(true);
    }
    /*
    public static void Respawn(GameObject gameObject, List<GameObject> list)
    {
        //gameObject��playerenemy�ȊO�Ȃ�G���[
        gameObject.SetActive(false);
        gameObject.transform.position = SelectRespawnPlace(list).position;
        //�b�ҋ@
        gameObject.SetActive(true);
    }*/

    //�����͑���̃O���[�v
    //����̃O���[�v�̒��S
    public static Transform SelectRespawnPlace(List<Character> list)
    {
        Vector3 v3 = new Vector3(0, 0, 0);
        foreach(Character g in list)
        {
            v3 += g.transform.position;
        }
        v3 /= list.Count;
        //��ԉ����Ƃ��ƂƂ��̃C���f�b�N�X
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
