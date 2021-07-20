using System;
using System.Drawing;
using System.Collections.Generic;

namespace Banished_Project
{
    class MainRoutine
    {
        static void Main()
        {
            List<Point> startPoint;
            List<Point> endPoint;
            List<List<Node>> nodes = GetMapInfo("MapInput.txt", out startPoint, out endPoint);
            List<Node> startingNodes =new List<Node>();
            List<Node> endNodes = new List<Node>();
            foreach(Point sp in startPoint)
                startingNodes.Add(nodes[sp.Y][sp.X]);
            foreach(Point ep in endPoint)
                endNodes.Add(nodes[ep.Y][ep.X]);
            float paveSpeed = 2.2f;
            List<List<Point>> ans= new List<List<Point>>();
            foreach(Node sn in startingNodes)
            {
                foreach(Node en in endNodes)
                {  
                    ClearNodes(nodes);
                    SearchParameters p = new SearchParameters(sn, en, nodes, paveSpeed);
                    AStarSolver A = new AStarSolver(p); 
                    ans.Add(A.FindPath());
                }
            }
            Console.WriteLine("Finished");
            Console.WriteLine(ans.Count);
            foreach(List<Point> a in ans)
            {
                foreach(Point b in a)
                    Console.WriteLine("("+b.X+", "+b.Y+")");
                Console.WriteLine("\n");
            }
        }

        public static List<List<Node>> GetMapInfo(string filename, out List<Point> startLocation, out List<Point> endLocation)
        {
            string[] map = System.IO.File.ReadAllText(@filename).Split("\n");
            List<List<Node>> nodes =  new List<List<Node>>();
            startLocation=new List<Point>();                
            endLocation=new List<Point>();
            int i=0;
            foreach(string l in map)
            {
                Console.WriteLine(l);
                List<Node> line = new List<Node>();
                for(int j=0; j<l.Length; j++)
                {
                    Console.WriteLine(l[j]);
                    bool walkable=true;
                    bool paved=false;
                    if(l[j].ToString()=="*")
                        walkable=false;
                    if(l[j].ToString()=="#")
                        paved=true;
                    if(l[j].ToString()=="S")
                    {
                        paved=true;
                        startLocation.Add(new Point(i,j));
                    }
                    if(l[j].ToString()=="F")
                    {
                        paved=true;
                        endLocation.Add(new Point(i,j));
                    }
                    line.Add(new Node(new Point(i,j), walkable, paved));
                }
                nodes.Add(line);
                i++;
            }
            return nodes;
        }

        public static void ClearNodes(List<List<Node>> m)
        {
            foreach(List<Node> l in m)
            {
                foreach(Node n in l)
                {
                    n.ParentNode=null;
                    n.State=NodeState.Open;
                }
            }
        }
    }
}