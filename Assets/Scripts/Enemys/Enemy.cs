using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    StateMachineDemo stateMachineDemo;
    SearchBall searchBall;
    SearchPlayer searchPlayer;
    EnemyMeshController emc;
    // Start is called before the first frame update
    void Awake()
    {
        stateMachineDemo = GetComponent<StateMachineDemo>();
        searchBall = GetComponent<SearchBall>();
        searchPlayer = GetComponent<SearchPlayer>();
        emc = GetComponent<EnemyMeshController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (searchBall.Ballhit()) stateMachineDemo.SearchedBall();
        if (searchPlayer.Playerhit()) stateMachineDemo.SearchedPlayer();

        if("Attack" == stateMachineDemo.getState().ToString())
        {
            emc.PlayerFound();
        }
        else
        {
            emc.Finding();
        }
        

    }
}
