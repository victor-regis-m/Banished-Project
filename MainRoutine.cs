using System;
using System.Drawing;
using System.Collections.Generic;

namespace Banished_Project
{
    class MainRoutine
    {
        static void Main(string[] args)
        {
            bool [,] mapSurface = new bool[6,6];
            int mapWidth = mapSurface.GetLength(0); 
            int mapHeight = mapSurface.GetLength(1);
            for(int i = 0; i<mapWidth; i++)
            {
                for(int j =0; j<mapHeight; j++)
                {
                    mapSurface[i,j]=true;
                }
            }
            //mapSurface[0,2]=false;
            //mapSurface[1,2]=false;
            //mapSurface[2,2]=false;
            //mapSurface[3,2]=false;
            //mapSurface[4,2]=false;
            //mapSurface[5,2]=false;
            //mapSurface[2,1]=false;
            //mapSurface[2,2]=false;
            //mapSurface[2,3]=false;
            //mapSurface[3,3]=false;

            List<List<Node>> nodes =  new List<List<Node>>();
            for(int i=0; i<mapWidth; i++)
            {
                List<Node> line = new List<Node>();
                for(int j=0; j<mapHeight; j++)
                    line.Add(new Node(new Point(i,j), mapSurface[i,j]));
                nodes.Add(line);
            }

            for(int i = 0; i<6; i++)
            {
                Console.WriteLine(mapSurface[0,i] + "," + mapSurface[1,i] + "," + mapSurface[2,i] + ","  + mapSurface[3,i] + ","  + mapSurface[4,i] + ","  + mapSurface[5,i] + ","  );
                Console.WriteLine("\n");
            }

            Node startingNode = nodes[0][0];
            Node endNode = nodes[2][4];
            Console.WriteLine(startingNode.IsWalkable);
            Console.WriteLine(endNode.IsWalkable);
            SearchParameters p = new SearchParameters(startingNode, endNode, nodes);
            AStarSolver A = new AStarSolver(p);
            List<Point> ans = A.FindPath();
            Console.WriteLine("Finished");
            Console.WriteLine(ans.Count);
            foreach(Point a in ans)
                Console.WriteLine("("+a.X+", "+a.Y+")");
        }
    }
}