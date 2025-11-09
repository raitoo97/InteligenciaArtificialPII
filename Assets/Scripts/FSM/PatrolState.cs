using System.Collections.Generic;
using UnityEngine;
public class PatrolState : IState
{
    private List<Transform> _wayPoints = new List<Transform>();
    private Transform _currentTarget;
    private float _nearDistance;
    private Enemy _enemy;
    private FSM _fsm;
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
    }
    public void OnUpdate()
    {
        _enemy.GetArriveForce(_currentTarget.position);
        var dir = _currentTarget.transform.position - _enemy.transform.position;
        var distance = dir.magnitude;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(dir);
            _enemy.transform.rotation = Quaternion.RotateTowards(_enemy.transform.rotation, rotation, _enemy.RotateDegrees * Time.deltaTime);
        }
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
        Debug.Log("Exit Patrol");
    }
}
