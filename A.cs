using System;
using System.Drawing;
using System.Collections.Generic;

namespace Banished_Project
{
    public class AStarSolver
    {
        public AStarSolver(SearchParameters p)
        {Parameters = p;}

        SearchParameters Parameters;

        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Node> nextNodes = GetAdjacentLocations(fromNode.Location);                
            foreach (var node in nextNodes)
            {
                //Ignore current node
                if(node == fromNode)
                    continue;
                // Ignore non-walkable nodes
                if (!node.IsWalkable)
                    continue;
        
                // Ignore already-closed nodes
                if (node.State == NodeState.Closed)
                    continue;
        
                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == NodeState.Open)
                {
                    float traversalCost = Node.GetTraversalCost(node, fromNode);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp <= node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }
            }
        
            return walkableNodes;
        }
        IEnumerable<Node> GetAdjacentLocations(Point loc)
        {
            List<Node> nodeList = new List<Node>();
            for(int i=-1; i<=1; i++)
                for(int j=-1; j<=1; j++)
                {
                    try
                    {
                        nodeList.Add(Parameters.Map[loc.X + i][loc.Y + j]);
                    }
                    catch(Exception e){}
                }
            return nodeList;
        }
        private bool Search(Node currentNode)
        {   
            currentNode.State = NodeState.Closed;
            foreach(Node n in GetAdjacentLocations(currentNode.Location))
            {
                n.SetGAndH(currentNode, Parameters.EndLocation);
            }
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Location == Parameters.EndLocation.Location)
                {
                    return true;
                }
                else
                {
                    if (Search(nextNode)) // Note: Recurses back into Search(Node)
                        return true;
                }
            }
            return false;
        }
        public List<Point> FindPath()
        {
            List<Point> path = new List<Point>();
            bool success = Search(Parameters.StartLocation);
            if (success)
            {
                Node node = Parameters.EndLocation;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }
                path.Reverse();
            }
            return path;
        }
    }

    public class SearchParameters
    {
        public SearchParameters(Node Start, Node End, List<List<Node>> m)
        {StartLocation = Start; EndLocation = End; Map=m;}
        public Node StartLocation;
        public Node EndLocation;
        public List<List<Node>> Map {get;}
    }
}