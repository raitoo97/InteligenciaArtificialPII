using System.Collections.Generic;
using UnityEngine;
public class SearchState : IState
{
    private Player _player;
    private List<Vector3> path = new List<Vector3>();
    private List<Node> nodePath = new List<Node>();
    private Enemy _enemy;
    private FSM _fsm;
    public SearchState(Player player, Enemy enemy, FSM fsm)
    {
        _player = player;
        _enemy = enemy;
        _fsm = fsm;
    }
    public void Onstart()
    {

    }
    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
    public void OnUpdate()
    {

    }
    private void CalculatePath(Vector3 target)
    {
        var start = NodeManager.GetClosetNode(_enemy.transform.position);
        var end = NodeManager.GetClosetNode(target);
        nodePath = Pathfinding.CalculateAStar(start, end);
        foreach (var node in nodePath) 
        {
            path.Add(node.transform.position);
        }

    }
}
