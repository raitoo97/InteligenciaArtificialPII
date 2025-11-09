using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    Enemy _enemy;
    FSM _fsm;
    public ChaseState(Enemy enemy, FSM fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
    }
    public void Onstart()
    {
        Debug.Log("Enter Chase");
        _enemy.CleanForce();
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {
        Debug.Log("Exit Chase");
    }

}
