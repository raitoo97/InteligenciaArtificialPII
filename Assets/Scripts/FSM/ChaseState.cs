using UnityEngine;
public class ChaseState : IState
{
    private Enemy _enemy;
    private FSM _fsm;
    private Player _player;
    private float _lostSightTimer;
    private float _maxLostSightTime = 2f;
    public ChaseState(Player player,Enemy enemy, FSM fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
        _player = player;
    }
    public void Onstart()
    {
        Debug.Log("Enter Chase");
    }
    public void OnUpdate()
    {
        _enemy.ModififyStamina();
        if (LineOfSight.IsOnSight(_enemy.transform.position, _player.transform.position))
        {
            _lostSightTimer = 0f;
            Vector3 playerdir = _player.transform.position - _enemy.transform.position;
            _enemy.RotateTo(playerdir);
            _enemy.GetSeekForce(_player.transform.position);
            _enemy.UpdateLastKnownPosition(_player.transform.position);
            return;
        }
        else
        {
            _lostSightTimer += Time.deltaTime;
            if (_lostSightTimer >= _maxLostSightTime)
            {
                _fsm.ChangeState(FSM.State.search);
            }
        }
    }
    public void OnExit()
    {
        Debug.Log("Exit Chase");
    }
}