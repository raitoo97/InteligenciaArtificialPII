using System.Collections.Generic;
using UnityEngine;
public class PatrolState : IState
{
    private List<Transform> _wayPoints = new List<Transform>();
    private Transform _currentTarget;
    private float _nearDistance;
    private Enemy _enemy;
    private FSM _fsm;
    private List<Vector3> _currentPath = new List<Vector3>();
    private Player _player;
    public PatrolState(Player player,List<Transform> wayPoints,float nearDistance,Enemy enemy, FSM fsm)
    {
        _wayPoints = wayPoints;
        _nearDistance = nearDistance;
        _enemy = enemy;
        _fsm = fsm;
        _player = player;
    }
    public void Onstart()
    {
        Debug.Log("Enter Patrol");
        _currentTarget = _wayPoints[_enemy.Index];
        if (!LineOfSight.IsOnSight(_enemy.transform.position, _currentTarget.position))
        {
            _enemy.CalculatePath(_currentTarget.position,_currentPath);
        }
    }
    public void OnUpdate()
    {
        if (FOV.InFOV(_player.transform, _enemy.transform, _enemy.ViewRadius, _enemy.ViewAngle))
        {
            EnemyManager.instance.AlertAllEnemies(_enemy, _player.transform.position);
            return;
        }
        if (_currentPath.Count > 0)
        {
            _enemy.TraveledThePath(_currentPath);
            return;
        }
        var dir = _currentTarget.transform.position - _enemy.transform.position;
        var distance = dir.magnitude;
        _enemy.RotateTo(dir);
        _enemy.GetArriveForce(_currentTarget.position);
        if (distance < _nearDistance) 
        {
            _enemy.Index++;
            if (_enemy.Index < _wayPoints.Count)
            {
                _currentTarget = _wayPoints[_enemy.Index];
            }
            else
            {
                _enemy.Index = 0;
                _currentTarget = _wayPoints[_enemy.Index];
            }
        }
    }
    public void OnExit()
    {
        Debug.Log("Exit Patrol");
    }
}