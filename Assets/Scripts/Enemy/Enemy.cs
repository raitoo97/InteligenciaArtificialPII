using System.Collections.Generic;
using UnityEngine;
public class Enemy : Agent
{
    private FSM _fsm;
    [Header("Patrol")]
    [SerializeField]private List<Transform> _wayPoints = new List<Transform>();
    [SerializeField]private float _nearDistance;
    [SerializeField]private int _index;
    [SerializeField]private float _rotateDegrees;
    private void OnEnable()
    {
        var player = GameManager.instance.player;
        _fsm = new FSM();
        _fsm.AddState(FSM.State.patrol, new PatrolState(_wayPoints, _nearDistance, this, _fsm));
        _fsm.AddState(FSM.State.search, new SearchState(player, _nearDistance, this, _fsm));
        _fsm.ChangeState(FSM.State.patrol);
    }
    private void Start()
    {
        _index = 0;
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
    public void RotateTo(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotation, _rotateDegrees * Time.deltaTime);
        }
    }
    public void CalculatePath(Vector3 target,List<Vector3> path)
    {
        path.Clear();
        var start = NodeManager.GetClosetNode(this.transform.position);
        var end = NodeManager.GetClosetNode(target);
        var nodePath = Pathfinding.CalculateAStar(start, end);
        foreach (var node in nodePath)
        {
            path.Add(node.transform.position);
        }
        path.Add(target);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _radiusArrive);
    }
    public int Index { get => _index; set => _index = value; }
    public float RotateDegrees { get => _rotateDegrees; }
}
