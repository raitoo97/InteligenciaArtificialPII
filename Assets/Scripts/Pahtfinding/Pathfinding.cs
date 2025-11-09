using System.Collections.Generic;
public static class Pathfinding
{
    public static List<Node> CalculateAStar(Node start,Node goal)
    {
        var frontier = new PriorityQueue<Node>();
        var cameFrom = new Dictionary<Node,Node>();
        var costSoFar = new Dictionary<Node,float>();
        frontier.Enqueue(start,0);
        costSoFar.Add(start, 0);
        cameFrom.Add(start, null);
        while (frontier.Count > 0)
        {
            Node current = frontier.Dequeue();
            if (current == goal)
            {
                var path = new List<Node>();
                while (current != null) 
                {
                    path.Add(current);
                    current = cameFrom[current];
                }
                path.Reverse();
                return path;
            }
            foreach(var node in current.GetNeighbords)
            {
                var newCost = costSoFar[current] + node.GetCost;
                var distance = (goal.transform.position - node.transform.position).magnitude;
                var totalCost = newCost + distance;
                if (!cameFrom.ContainsKey(node))
                {
                    frontier.Enqueue(node, totalCost);
                    cameFrom.Add(node, current);
                    costSoFar.Add(node, newCost);
                }else if (costSoFar[node]> newCost)
                {
                    frontier.Enqueue(node, totalCost);
                    cameFrom[node] = current;
                    costSoFar[node] = newCost;
                }
            }
        }
        return new List<Node>();
    }
}
