using System.Collections.Generic;
using UnityEngine;
public class ChaseState : IState
{
    private Enemy _enemy;
    private FSM _fsm;
    private Player _player;
    private List<Vector3> _currentPath = new List<Vector3>();
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
        if (_currentPath.Count > 0)
        {
            _enemy.TraveledThePath(_currentPath);
            return;
        }
        Vector3 dir = _player.transform.position - _enemy.transform.position;
        _enemy.RotateTo(dir);
        _enemy.GetSeekForce(_player.transform.position);
        if (!LineOfSight.IsOnSight(_enemy.transform.position, _player.transform.position))
        {
            _enemy.CalculatePath(_player.transform.position, _currentPath);
        }
    }
    public void OnExit()
    {
        Debug.Log("Exit Chase");
    }
}
