using UnityEngine;
public class IdleState : IState
{
    private Enemy _enemy;
    private FSM _fsm;
    private float _timer;
    public IdleState(float timer,Enemy enemy, FSM fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
        _timer = timer;
    }
    public void Onstart()
    {
        Debug.Log("Enter Idle");
        _timer = _enemy.TimeToRecovery;
    }
    public void OnUpdate()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
            _fsm.ChangeState(FSM.State.patrol);
    }
    public void OnExit()
    {
        _enemy.SetMaxStamina();
    }
}
