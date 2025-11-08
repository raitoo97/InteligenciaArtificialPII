using UnityEngine;
public class Enemy : Agent
{
    private FSM _fsm;
    protected override void Update()
    {
        base.Update();
    }
    private void OnEnable()
    {
        _fsm = new FSM();
    }
    private void OnDisable()
    {
        _fsm = null;
    }
}
