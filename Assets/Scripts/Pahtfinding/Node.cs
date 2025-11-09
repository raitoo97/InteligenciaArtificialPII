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
    private void Start()
    {
        NodeManager.CompleteNeighbords();
    }
    public void AddNeigbord(Node node)
    {
        if (_neighbords.Contains(node)) return;
        _neighbords.Add(node);
    }
    private void OnDestroy()
    {
        _neighbords.Clear();
        NodeManager.RegisterNode(this);
    }
}
