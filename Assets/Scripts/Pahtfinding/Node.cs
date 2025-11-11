using System.Collections.Generic;
using UnityEngine;
public class Node : MonoBehaviour
{
    [SerializeField]private List <Node> _neighbords = new List <Node> ();
    [SerializeField]private int cost;
    private void Awake()
    {
        NodeManager.RegisterNode(this);
    }
    public void AddNeigbord(Node node)
    {
        if (_neighbords.Contains(node)) return;
        _neighbords.Add(node);
    }
    private void OnDestroy()
    {
        _neighbords.Clear();
        NodeManager.RemoveNode(this);
    }
    public List<Node> GetNeighbords { get => _neighbords; }
    public int GetCost { get => cost; }
}