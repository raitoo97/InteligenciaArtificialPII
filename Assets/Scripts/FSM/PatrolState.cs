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
    public PatrolState(List<Transform> wayPoints,float nearDistance,Enemy enemy, FSM fsm)
    {
        _wayPoints = wayPoints;
        _nearDistance = nearDistance;
        _enemy = enemy;
        _fsm = fsm;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _fsm.ChangeState(FSM.State.search);
            return;
        }
        if (_currentPath.Count > 0)
        {
            var currentTarget = _currentPath[0];
            _enemy.GetSeekForce(currentTarget);
            _enemy.RotateTo(currentTarget - _enemy.transform.position);
            var dis = (currentTarget - _enemy.transform.position).magnitude;
            if (dis < _nearDistance)
            {
                _currentPath.RemoveAt(0);
            }
            return;
        }
        _enemy.GetArriveForce(_currentTarget.position);
        var dir = _currentTarget.transform.position - _enemy.transform.position;
        var distance = dir.magnitude;
        _enemy.RotateTo(dir);
        if (distance < _nearDistance) 
        {
            _enemy.CleanForce();
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
        _currentPath.Clear();
    }
}
