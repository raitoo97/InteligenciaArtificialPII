using System.Collections.Generic;
public class FSM
{
    public enum State
    {
        patrol,
        chase,
        search,
        Idle
    }
    private IState _currentState;
    private State _currentKey;
    private Dictionary<State, IState> _allStates = new Dictionary<State, IState>();
    public void AddState(State key, IState value)
    {
        if (_allStates.ContainsKey(key)) return;
        _allStates.Add(key, value);
    }
    public void RemoveState(State key)
    {
        if (!_allStates.ContainsKey(key)) return;
        _allStates.Remove(key);
    }
    public void ChangeState(State key)
    {
        if (!_allStates.ContainsKey(key)) return;
        _currentState?.OnExit();
        _currentState = _allStates[key];
        _currentKey = key;
        _currentState?.Onstart();
    }
    public void OnUpdateState()
    {
        _currentState?.OnUpdate();
    }
    public State GetCurrentKey { get => _currentKey; }
}