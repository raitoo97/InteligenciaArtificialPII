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
        if(_currentPath.Count > 0)
            _currentPath.Clear();
        Debug.Log("Enter Search");
        if (FOV.InFOV(_player.transform,_enemy.transform,_enemy.ViewRadius,_enemy.ViewAngle))
        {
            _fsm.ChangeState(FSM.State.chase);
        }
        _enemy.CalculatePath(_player.transform.position, _currentPath);
    }
    public void OnUpdate()
    {
        if (FOV.InFOV(_player.transform, _enemy.transform, _enemy.ViewRadius, _enemy.ViewAngle))
        {
            _fsm.ChangeState(FSM.State.chase);
            return;
        }
        if (_currentPath.Count > 0)
        {
            var currentTarget = _currentPath[0];
            _enemy.RotateTo(currentTarget - _enemy.transform.position);
            _enemy.GetSeekForce(currentTarget);
            var distance = (currentTarget - _enemy.transform.position).magnitude;
            if (distance < _nearDistance)
            {
                _currentPath.RemoveAt(0);
            }
            return;
        }
        _fsm.ChangeState(FSM.State.patrol);
    }
    public void OnExit()
    {
        Debug.Log("Exit Search");
        _currentPath.Clear();
    }
}
