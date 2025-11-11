using System.Collections.Generic;
using UnityEngine;
public class SearchState : IState
{
    private Player _player;
    private Enemy _enemy;
    private FSM _fsm;
    private List<Vector3> _currentPath = new List<Vector3>();
    private Vector3 _lastSearchTarget;
    private float _recalculateDistance = 2f;
    public SearchState(Player player, Enemy enemy, FSM fsm)
    {
        _player = player;
        _enemy = enemy;
        _fsm = fsm;
    }
    public void Onstart()
    {
        Debug.Log("Enter Search");
        _lastSearchTarget = _enemy.GetLastKnownPlayerPosition;
        _enemy.CalculatePath(_lastSearchTarget, _currentPath);
    }
    public void OnUpdate()
    {
        if (FOV.InFOV(_player.transform, _enemy.transform, _enemy.ViewRadius, _enemy.ViewAngle))
        {
            EnemyManager.instance.AlertAllEnemies(_enemy, _player.transform.position);
            return;
        }
        if ((_lastSearchTarget - _enemy.GetLastKnownPlayerPosition).magnitude > _recalculateDistance)
        {
            _lastSearchTarget = _enemy.GetLastKnownPlayerPosition;
            _enemy.CalculatePath(_lastSearchTarget, _currentPath);
        }
        if (_currentPath.Count > 0)
        {
            _enemy.TraveledThePath(_currentPath);
            return;
        }
        _fsm.ChangeState(FSM.State.patrol);
    }
    public void OnExit()
    {
        _currentPath.Clear();
        Debug.Log("Exit Search");
    }
}