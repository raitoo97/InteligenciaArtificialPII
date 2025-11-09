using System.Collections.Generic;
using UnityEngine;
public class Enemy : Agent
{
    private FSM _fsm;
    [Header("Patrol")]
    [SerializeField]private List<Transform> _wayPoints = new List<Transform>();
    [SerializeField]private float nearDistance;
    [SerializeField]private int index;
    [SerializeField]private float rotateDegrees;
    private void OnEnable()
    {
        _fsm = new FSM();
        _fsm.AddState(FSM.State.patrol, new PatrolState(_wayPoints, nearDistance, this, _fsm));

        _fsm.ChangeState(FSM.State.patrol);
    }
    private void Start()
    {
        index = 0;
    }
    protected override void Update()
    {
        _fsm.OnUpdateState();
        base.Update();
    }
    private void OnDisable()
    {
        _fsm = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _radiusArrive);
    }
    public int Index { get => index; set => index = value; }
    public float RotateDegrees { get => rotateDegrees; }
}
