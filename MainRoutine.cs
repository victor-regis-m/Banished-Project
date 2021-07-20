using System;
using System.Drawing;
using System.Collections.Generic;

namespace Banished_Project
{
    class MainRoutine
    {
        static void Main()
        {
            Point startPoint;
            Point endPoint;
            List<List<Node>> nodes = GetMapInfo("MapInput.txt", out startPoint, out endPoint);
            Node startingNode = nodes[startPoint.X][startPoint.Y];
            Node endNode = nodes[endPoint.X][endPoint.Y];
            float paveSpeed = 2.2f;
            SearchParameters p = new SearchParameters(startingNode, endNode, nodes, paveSpeed);
            AStarSolver A = new AStarSolver(p);
            List<Point> ans = A.FindPath();
            Console.WriteLine("Finished");
            Console.WriteLine(ans.Count);
            foreach(Point a in ans)
                Console.WriteLine("("+a.X+", "+a.Y+")");
        }

        public static List<List<Node>> GetMapInfo(string filename, out Point startLocation, out Point endLocation)
        {
            string[] map = System.IO.File.ReadAllText(@filename).Split("\n");
            List<List<Node>> nodes =  new List<List<Node>>();
            startLocation=new Point(0,0);
            endLocation=new Point(0,0);
            int i=0;
            foreach(string l in map)
            {
                List<Node> line = new List<Node>();
                for(int j=0; j<l.Length; j++)
                {
                    bool walkable=true;
                    bool paved=false;
                    if(l[j].ToString()=="*")
                        walkable=false;
                    if(l[j].ToString()=="#")
                        paved=true;
                    if(l[j].ToString()=="S")
                    {
                        paved=true;
                        startLocation=new Point(i,j);
                    }
                    if(l[j].ToString()=="F")
                    {
                        paved=true;
                        endLocation=new Point(i,j);
                    }
                    line.Add(new Node(new Point(i,j), walkable, paved));
                }
                nodes.Add(line);
                i++;
            }
            return nodes;
        }
    }
}