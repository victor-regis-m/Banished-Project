using System;
using System.Drawing;

namespace Banished_Project
{
    public class Node
    {
        public Node(Point P, bool walkable)
        {Location = P; IsWalkable = walkable; State = NodeState.Open;}
        public Point Location;
        public bool IsWalkable;
        public float G;
        public float H;
        public float F { get { return this.G + this.H; } }
        public NodeState State;
        public Node ParentNode;

        static public float GetTraversalCost(Node NA, Node NB)
        {
            if(NB!=null)
            {
                Point A = NA.Location;
                Point B = NB.Location;
                return (float)Math.Sqrt(Math.Pow((A.X - B.X),2) + Math.Pow((A.Y - B.Y),2));                
            }
            else
                return 0;
        }

        public void SetGAndH(Node StartNode, Node EndNode)
        {
            this.G = (float)Math.Sqrt(Math.Pow((StartNode.Location.X - this.Location.X),2) + Math.Pow((StartNode.Location.Y - this.Location.Y),2));
            this.H = (float)Math.Sqrt(Math.Pow((EndNode.Location.X - this.Location.X),2) + Math.Pow((EndNode.Location.Y - this.Location.Y),2));
        }
    }
    public enum NodeState {Untested, Closed, Open}
}