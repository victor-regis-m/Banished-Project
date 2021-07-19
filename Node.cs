using System;
using System.Drawing;

namespace Banished_Project
{
    public class Node
    {
        public Node(Point P, bool walkable, bool paved)
        {Location = P; IsWalkable = walkable; State = NodeState.Open; isPaved = paved;}
        public Point Location;
        public bool IsWalkable;
        public float G;
        public float H;
        public float F { get { return this.G + this.H; } }
        public NodeState State;
        public Node ParentNode;
        public bool isPaved;

        static public float GetTraversalCost(Node NA, Node NB, float pavedSpeed)
        {
            if(NB!=null)
            {
                Point A = NA.Location;
                Point B = NB.Location; 
                float baseDistance = GetEuclidianDistance(A,B);
                if(NA.isPaved && NB.isPaved)
                    baseDistance = baseDistance/MathF.Pow(pavedSpeed,1/3f);
                return baseDistance;
            }
            else
                return 0;
        }

        public void SetGAndH(Node StartNode, Node EndNode, float pavedRatio, float speed)
        {
            this.G = GetTraversalCost(StartNode, this, speed);
            if(!this.isPaved)
                this.H = GetEuclidianDistance(EndNode.Location, this.Location);
            else
            {
                float hCorrection =  1/(MathF.Pow(((1+(speed-1)*MathF.Pow(pavedRatio, 1.5f))),1/1.5f));
                this.H = GetEuclidianDistance(EndNode.Location, this.Location)*hCorrection;
            }
        }

        public static float GetEuclidianDistance(Point A, Point B)
        {
            return MathF.Sqrt(MathF.Pow((A.X - B.X),2) + MathF.Pow((A.Y - B.Y),2));       
        } 
    }
    public enum NodeState {Untested, Closed, Open}
}