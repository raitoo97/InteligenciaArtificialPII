using UnityEngine;
public class ChaseState : IState
{
    Enemy _enemy;
    FSM _fsm;
    Player _player;
    public ChaseState(Player player,Enemy enemy, FSM fsm)
    {
        _enemy = enemy;
        _fsm = fsm;
        _player = player;
    }
    public void Onstart()
    {
        Debug.Log("Enter Chase");
        _enemy.CleanForce();
    }
    public void OnUpdate()
    {
        _enemy.ModififyStamina();
        _enemy.GetSeekForce(_player.transform.position);
    }
    public void OnExit()
    {
        Debug.Log("Exit Chase");
    }
}
