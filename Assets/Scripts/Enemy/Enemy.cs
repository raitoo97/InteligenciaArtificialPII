using System.Collections.Generic;
using UnityEngine;
public class Enemy : Agent
{
    private FSM _fsm;
    [SerializeField]private float _maxStamina;
    [SerializeField]private float _timeToRecovery;
    private float _curentStamina;    
    [SerializeField]private List<Transform> _wayPoints = new List<Transform>();
    [SerializeField]private float _nearDistance;
    [SerializeField]private int _index;
    [SerializeField]private float _rotateDegrees;
    [SerializeField]private float _viewRadius;
    [SerializeField]private float _viewAngle;
    private Vector3 _lastKnownPlayerPosition;
    private void Awake()
    {
        EnemyManager.instance.RegisterEnemy(this);
    }
    private void OnEnable()
    {
        var player = GameManager.instance.player;
        _fsm = new FSM();
        _fsm.AddState(FSM.State.patrol, new PatrolState(player,_wayPoints, _nearDistance, this, _fsm));
        _fsm.AddState(FSM.State.search, new SearchState(player, this, _fsm));
        _fsm.AddState(FSM.State.chase, new ChaseState(player,this, _fsm));
        _fsm.AddState(FSM.State.Idle, new IdleState(_timeToRecovery,this, _fsm));
        _fsm.ChangeState(FSM.State.patrol);
    }
    private void Start()
    {
        ChangeMove(true);
        SetMaxStamina();
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
        if (start == null || end == null)
        {
            Debug.Log("No se encontraron nodos en pathfinding");
            return;
        }
        var nodePath = Pathfinding.CalculateAStar(start, end);
        foreach (var node in nodePath)
            path.Add(node.transform.position);
        path.Add(target);
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], Color.cyan, 2f);
        }
    }
    public void TraveledThePath(List<Vector3> path)
    {
        if (path.Count == 0) 
        {
            Debug.Log("El Camino esta vacio");
            return;
        }
        var currentTarget = path[0];
        RotateTo(currentTarget - this.transform.position);
        GetArriveForce(currentTarget);
        var dis = (currentTarget - this.transform.position).magnitude;
        if (dis < _nearDistance)
        {
            path.RemoveAt(0);
        }
    }
    public void ModififyStamina()
    {
        _curentStamina -= Time.deltaTime;
        if (_curentStamina <= 0)
            _fsm.ChangeState(FSM.State.Idle);
    }
    public void SetMaxStamina()
    {
        _curentStamina = _maxStamina;
    }
    public void UpdateLastKnownPosition(Vector3 pos)
    {
        _lastKnownPlayerPosition = pos;
    }
    public void OnAlerted(Vector3 lastKnownPosition, bool sawPlayer)
    {
        UpdateLastKnownPosition(lastKnownPosition);
        if (sawPlayer)
            _fsm.ChangeState(FSM.State.chase);
        else
            _fsm.ChangeState(FSM.State.search);
    }
    private void OnDestroy()
    {
        EnemyManager.instance.UnregisterEnemy(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, _radiusArrive);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
        Vector3 rightDir = Quaternion.Euler(0, _viewAngle * 0.5f, 0) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0, -_viewAngle * 0.5f, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * _viewRadius);
    }
    public int Index { get => _index; set => _index = value; }
    public float RotateDegrees { get => _rotateDegrees; }
    public float ViewRadius { get => _viewRadius; }
    public float ViewAngle { get => _viewAngle; }
    public float TimeToRecovery { get => _timeToRecovery; }
    public Vector3 GetLastKnownPlayerPosition { get => _lastKnownPlayerPosition; }
}