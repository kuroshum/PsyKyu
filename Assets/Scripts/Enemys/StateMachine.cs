using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

// ŠeState–ˆ‚Ìdelagate‚ğ“o˜^‚µ‚Ä‚¨‚­ƒNƒ‰ƒX
public class StateMapping
{
    public Action onEnter;
    public Func<IEnumerator> EnterRoutine;
    public Action onExit;
    public Func<IEnumerator> ExitRoutine;
    public Action<float> onUpdate;
}
public class Transition<TState, TTrigger>
{
    public TState To { get; set; }
    public TTrigger Trigger { get; set; }
}

public class StateMachine<TState, TTrigger>
    where TState : struct, IConvertible, IComparable
    where TTrigger : struct, IConvertible, IComparable
{
    private MonoBehaviour _monoBehaviour;
    private TState _stateType;
    private StateMapping _stateMapping;
    // ‘JˆÚ’†‚Å‚ ‚éê‡‚Ì‘JˆÚæ
    private TState? _destinationState;
    // ‘JˆÚ’†‚©
    private bool _inExitTransition;
    private bool _inEnterTransition;

    private Dictionary<object, StateMapping> _stateMappings = new Dictionary<object, StateMapping>();
    private Dictionary<TState, List<Transition<TState, TTrigger>>> _transitionLists = new Dictionary<TState, List<Transition<TState, TTrigger>>>();

    public StateMachine(MonoBehaviour monoBehaviour, TState initialState)
    {
        _monoBehaviour = monoBehaviour;

        // State‚©‚çStateMapping‚ğì¬
        var enumValues = Enum.GetValues(typeof(TState));
        for (int i = 0; i < enumValues.Length; i++)
        {
            var mapping = new StateMapping();
            _stateMappings.Add(enumValues.GetValue(i), mapping);
        }

        // Å‰‚ÌState‚É‘JˆÚ
        ChangeStateImmediately(initialState);
    }

    public TState getState()
    {
        return _stateType;
    }

    /// <summary>
    /// ƒgƒŠƒK[‚ğÀs‚·‚é
    /// </summary>
    public void ExecuteTrigger(TTrigger trigger)
    {
        var transitions = _transitionLists[_stateType];
        foreach (var transition in transitions)
        {
            if (transition.Trigger.Equals(trigger))
            {
                _monoBehaviour.StartCoroutine(ChangeState(transition.To));
                break;
            }
        }
    }

    /// <summary>
    /// ‘JˆÚî•ñ‚ğ“o˜^‚·‚é
    /// </summary>
    public void AddTransition(TState from, TState to, TTrigger trigger)
    {
        if (!_transitionLists.ContainsKey(from))
        {
            _transitionLists.Add(from, new List<Transition<TState, TTrigger>>());
        }
        var transitions = _transitionLists[from];
        var transition = transitions.FirstOrDefault(x => x.To.Equals(to));
        if (transition == null)
        {
            // V‹K“o˜^
            transitions.Add(new Transition<TState, TTrigger> { To = to, Trigger = trigger });
        }
        else
        {
            // XV
            transition.To = to;
            transition.Trigger = trigger;
        }
    }

    /// <summary>
    /// State‚ğ‰Šú‰»‚·‚é
    /// </summary>
    public void SetupState(TState state, Action onEnter = null, Func<IEnumerator> enterRoutine = null, Action onExit = null, Func<IEnumerator> exitRoutine = null, Action<float> onUpdate = null)
    {
        var stateMapping = _stateMappings[state];
        stateMapping.onEnter = onEnter;
        stateMapping.EnterRoutine = enterRoutine;
        stateMapping.onExit = onExit;
        stateMapping.ExitRoutine = exitRoutine;
        stateMapping.onUpdate = onUpdate;
    }

    /// <summary>
    /// XV‚·‚é
    /// </summary>
    public void Update(float deltaTime)
    {
        if (_inExitTransition || _inEnterTransition)
        {
            // ‘JˆÚ’†‚ÍXV‚µ‚È‚¢
            return;
        }
        if (_stateMapping != null && _stateMapping.onUpdate != null)
        {
            _stateMapping.onUpdate(deltaTime);
        }
    }

    /// <summary>
    /// State‚ğ‚½‚¾‚¿‚É•ÏX‚·‚é
    /// </summary>
    private void ChangeStateImmediately(TState to)
    {
        // Exit
        if (_stateMapping != null)
        {
            if (_stateMapping.onExit != null)
            {
                _stateMapping.onExit();
            }
        }

        // Enter
        _stateType = to;
        _stateMapping = _stateMappings[_stateType];
        if (_stateMapping.onEnter != null)
        {
            _stateMapping.onEnter();
        }
    }

    /// <summary>
    /// State‚ğ•ÏX‚·‚é
    /// </summary>
    private IEnumerator ChangeState(TState to)
    {
        if (_inEnterTransition)
        {
            // Enter‘JˆÚ’†‚¾‚Á‚½‚ç‰½‚à‚¹‚¸breakió‘Ô‘JˆÚ¸”sj
            yield break;
        }

        _destinationState = to;
        if (_inExitTransition)
        {
            // Exit‘JˆÚ’†‚¾‚Á‚½‚ç‘JˆÚæ‚ğã‘‚«‚µ‚Äbreak
            yield break;
        }


        // Exit
        _inExitTransition = true;
        if (_stateMapping != null)
        {
            if (_stateMapping.ExitRoutine != null)
            {
                yield return _monoBehaviour.StartCoroutine(_stateMapping.ExitRoutine());
            }
            if (_stateMapping.onExit != null)
            {
                _stateMapping.onExit();
            }
        }
        _inExitTransition = false;

        // Enter
        _inEnterTransition = true;
        var stateMapping = _stateMappings[_destinationState.Value];
        if (stateMapping.EnterRoutine != null)
        {
            yield return stateMapping.EnterRoutine();
        }
        if (stateMapping.onEnter != null)
        {
            stateMapping.onEnter();
        }
        _inEnterTransition = false;
        // State‚ğ‘‚«Š·‚¦
        _stateType = _destinationState.Value;
        _stateMapping = _stateMappings[_stateType];

        _destinationState = null;
    }
}
