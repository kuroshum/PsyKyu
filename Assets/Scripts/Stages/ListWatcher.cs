using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListWatcher : MonoBehaviour
{
    [SerializeField] private List<Character> groupA = new List<Character>();
    [SerializeField] private List<Character> groupB = new List<Character>();
    [SerializeField] private List<Player> playerList = new List<Player>();
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    //リスポーン位置候補
    [SerializeField] private List<Transform> resList = new List<Transform>();
    [SerializeField] private List<BallSpawner> ballResList = new List<BallSpawner>();


    // Start is called before the first frame update
    void Start()
    {
        groupA = CharacterManager.groupA;
        groupB = CharacterManager.groupB;
        playerList = CharacterManager.playerList;
        enemyList = CharacterManager.enemyList;
        resList = CharacterManager.resList;
        ballResList = BallSpawnManager.ballResList;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            groupA = CharacterManager.groupA;
            groupB = CharacterManager.groupB;
            playerList = CharacterManager.playerList;
            enemyList = CharacterManager.enemyList;
            resList = CharacterManager.resList;
            ballResList = BallSpawnManager.ballResList;
        }
    }
}
