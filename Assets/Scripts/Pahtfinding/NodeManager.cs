using System.Collections.Generic;
using UnityEngine;
public static class NodeManager
{
    [SerializeField]private static List<Node> _allNodes = new List<Node>();
    public static float _maxDistanceNeighbord = 25;
    public static void RegisterNode(Node node)
    {
        if(_allNodes.Contains(node)) return;
        _allNodes.Add(node);
    }
    public static void RemoveNode(Node node)
    {
        if (!_allNodes.Contains(node)) return;
        _allNodes.Remove(node);
    }
    public static void CompleteNeighbords()
    {
        foreach (Node node in _allNodes) 
        {
            foreach(Node neigbord in _allNodes)
            {
                if(node == neigbord)continue;
                if(LineOfSight.IsOnSight(node.transform.position, neigbord.transform.position))
                    if (Vector3.Distance(node.transform.position, neigbord.transform.position) < _maxDistanceNeighbord)
                        node.AddNeigbord(neigbord);
            }
        }
    }
    public static Node GetClosetNode(Vector3 pos)
    {
        Node minNode = null;
        float minDistance = Mathf.Infinity;
        foreach (Node node in _allNodes)
        {
            if (!LineOfSight.IsOnSight(node.transform.position, pos)) continue;
            var distance = pos - node.transform.position;
            if (distance.magnitude < minDistance)
            {
                minNode = node;
                minDistance = distance.magnitude;
            }
        }
        return minNode;
    }
}
