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
            
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);
            foreach (var location in nextLocations)
            {
                int x = location.X;
                int y = location.Y;
        
                // Stay within the grid's boundaries
                if (x < 0 || x >= Parameters.Map.Count || y < 0 || y >= Parameters.Map[0].Count)
                    continue;
        
                Node node = Parameters.Map[x][y];
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
        IEnumerable<Point> GetAdjacentLocations(Point loc)
        {
            List<Point> pointList = new List<Point>(){new Point(loc.X, loc.Y-1), new Point(loc.X, loc.Y+1),
            new Point(loc.X+1, loc.Y), new Point(loc.X-1, loc.Y)
            ,new Point(loc.X-1, loc.Y-1),new Point(loc.X+1, loc.Y-1), new Point(loc.X-1, loc.Y+1), new Point(loc.X+1, loc.Y+1)
            };
            return pointList;
        }
        private bool Search(Node currentNode)
        {   
            for(int i=0; i<Parameters.Map.Count; i++)
                for(int j=0; j<Parameters.Map[0].Count; j++)
                    Parameters.Map[i][j].SetGAndH(currentNode, Parameters.EndLocation);
            currentNode.State = NodeState.Closed;
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