using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachineDemo : MonoBehaviour
{
    public enum StateType
    {
        BallFind,
        PlayerFind,
        Attack,
        Escape,
        Catch,
    }
    public enum TriggerType
    {
        KeyDownB,
        KeyDownP,
        KeyDownA,
        KeyDownE,
        KeyDownC,
    }

    private StateMachine<StateType, TriggerType> _stateMachine;

    private void Start()
    {
        // StateMachineを生成
        _stateMachine = new StateMachine<StateType, TriggerType>(this, StateType.BallFind);

        // 遷移情報を登録
        _stateMachine.AddTransition(StateType.BallFind, StateType.PlayerFind, TriggerType.KeyDownP);
        _stateMachine.AddTransition(StateType.BallFind, StateType.Escape, TriggerType.KeyDownE);
        _stateMachine.AddTransition(StateType.PlayerFind, StateType.Attack, TriggerType.KeyDownA);
        _stateMachine.AddTransition(StateType.PlayerFind, StateType.Catch, TriggerType.KeyDownC);
        _stateMachine.AddTransition(StateType.PlayerFind, StateType.Escape, TriggerType.KeyDownE);
        _stateMachine.AddTransition(StateType.Attack, StateType.Catch, TriggerType.KeyDownC);
        _stateMachine.AddTransition(StateType.Attack, StateType.Escape, TriggerType.KeyDownE);
        _stateMachine.AddTransition(StateType.Escape, StateType.Attack, TriggerType.KeyDownA);
        _stateMachine.AddTransition(StateType.Escape, StateType.BallFind, TriggerType.KeyDownB);
        _stateMachine.AddTransition(StateType.Catch, StateType.Attack, TriggerType.KeyDownA);
        _stateMachine.AddTransition(StateType.Catch, StateType.Escape, TriggerType.KeyDownE);

        // Stateごとのふるまいを登録
        _stateMachine.SetupState(StateType.BallFind, () => Debug.Log("OnEnter: BallFind"), () => EnterRoutine(StateType.BallFind), () => Debug.Log("OnExit ; BallFind"), () => ExitRoutine(StateType.BallFind));
        _stateMachine.SetupState(StateType.PlayerFind, () => Debug.Log("OnEnter: PlayerFind"), () => EnterRoutine(StateType.PlayerFind), () => Debug.Log("OnExit ; PlayerFind"), () => ExitRoutine(StateType.PlayerFind));
        _stateMachine.SetupState(StateType.Attack, () => Debug.Log("OnEnter: Attack"), () => EnterRoutine(StateType.Attack), () => Debug.Log("OnExit ; Attack"), () => ExitRoutine(StateType.Attack));
        _stateMachine.SetupState(StateType.Escape, () => Debug.Log("OnEnter: Escape"), () => EnterRoutine(StateType.Escape), () => Debug.Log("OnExit ; Escape"), () => ExitRoutine(StateType.Escape));
        _stateMachine.SetupState(StateType.Catch, () => Debug.Log("OnEnter: Catch"), () => EnterRoutine(StateType.Catch), () => Debug.Log("OnExit ; Catch"), () => ExitRoutine(StateType.Catch));

    }


    private void Update()
    {
        // トリガーを呼ぶ
        if (Input.GetKeyDown(KeyCode.P)) _stateMachine.ExecuteTrigger(TriggerType.KeyDownP);
        if (Input.GetKeyDown(KeyCode.E)) _stateMachine.ExecuteTrigger(TriggerType.KeyDownE);
        if (Input.GetKeyDown(KeyCode.A)) _stateMachine.ExecuteTrigger(TriggerType.KeyDownA);
        if (Input.GetKeyDown(KeyCode.B)) _stateMachine.ExecuteTrigger(TriggerType.KeyDownB);
        if (Input.GetKeyDown(KeyCode.C)) _stateMachine.ExecuteTrigger(TriggerType.KeyDownC);
        // ステートマシンを更新
        _stateMachine.Update(Time.deltaTime);
 
    }
    private IEnumerator EnterRoutine(StateType stateType)
    {
        Debug.Log(stateType + " : Enter routine start.");
        yield return new WaitForSeconds(1);
        //Debug.Log(stateType + " : Enter routine end.");
        /*
         * ここに状態が変化したときの設定(enemyのAI変更などを入れるはず)
         */
    }
    private IEnumerator ExitRoutine(StateType stateType)
    {
        //Debug.Log(stateType + " : Exit routine start.");
        yield return new WaitForSeconds(1);
        //Debug.Log(stateType + " : Exit routine end.");
    }
    public void SearchedPlayer()
    {
        _stateMachine.ExecuteTrigger(TriggerType.KeyDownA);

    }
    public void SearchedBall()
    {
        _stateMachine.ExecuteTrigger(TriggerType.KeyDownP);
    }
    public StateType getState()
    {
        return _stateMachine.getState();
        //return _stateMachine.getState().ToString();
    }
}

