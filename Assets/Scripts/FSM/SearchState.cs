using System.Collections.Generic;
using UnityEngine;
public class SearchState : IState
{
    private Player _player;
    private float _nearDistance;
    private Enemy _enemy;
    private FSM _fsm;
    private List<Vector3> _currentPath = new List<Vector3>();
    public SearchState(Player player, float nearDistance, Enemy enemy, FSM fsm)
    {
        _player = player;
        _enemy = enemy;
        _nearDistance = nearDistance;
        _fsm = fsm;
    }
    public void Onstart()
    {
        if(LineOfSight.IsOnSight(_enemy.transform.position, _player.transform.position))
        {
            //return;
        }
        _enemy.CalculatePath(_player.transform.position,_currentPath);
    }
    public void OnUpdate()
    {
        if (_currentPath.Count > 0)
        {
            var currentTarget = _currentPath[0];
            _enemy.GetSeekForce(currentTarget);
            _enemy.RotateTo(currentTarget - _enemy.transform.position);
            var distance = (currentTarget - _enemy.transform.position).magnitude;
            if (distance < _nearDistance)
            {
                _currentPath.RemoveAt(0);
            }
            return;
        }
        _enemy.CleanForce();
        _fsm.ChangeState(FSM.State.patrol);
    }
    public void OnExit()
    {
        _currentPath.Clear();
    }
}
