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
            bool [,] mapPavement = new bool[6,6];
            mapPavement[0,0] = true;
            mapPavement[1,0] = true;
            mapPavement[2,0] = true;
            mapPavement[3,0] = true;
            mapPavement[4,1] = true;
            mapPavement[4,2] = true;
            mapPavement[4,3] = true;
            mapPavement[3,4] = true;
            mapPavement[2,4] = true;
            mapPavement[1,4] = true;
            mapPavement[0,4] = true;
            mapSurface[1,1]=false;
            mapSurface[1,2]=false;
            mapSurface[1,3]=false;
            mapSurface[2,1]=false;
            mapSurface[2,2]=false;
            mapSurface[2,3]=false;
            mapSurface[3,1]=false;
            mapSurface[3,2]=false;
            mapSurface[3,3]=false;
            //mapSurface[3,3]=false;

            List<List<Node>> nodes =  new List<List<Node>>();
            for(int i=0; i<mapWidth; i++)
            {
                List<Node> line = new List<Node>();
                for(int j=0; j<mapHeight; j++)
                    line.Add(new Node(new Point(i,j), mapSurface[i,j], mapPavement[i,j]));
                nodes.Add(line);
            }

            for(int i = 0; i<6; i++)
            {
                Console.WriteLine(mapSurface[0,i] + "," + mapSurface[1,i] + "," + mapSurface[2,i] + ","  + mapSurface[3,i] + ","  + mapSurface[4,i] + ","  + mapSurface[5,i] + ","  );
                Console.WriteLine("\n");
            }

            Node startingNode = nodes[0][0];
            Node endNode = nodes[0][4];
            float paveSpeed = 2f;
            Console.WriteLine(startingNode.IsWalkable);
            Console.WriteLine(endNode.IsWalkable);
            SearchParameters p = new SearchParameters(startingNode, endNode, nodes, paveSpeed);
            AStarSolver A = new AStarSolver(p);
            List<Point> ans = A.FindPath();
            Console.WriteLine("Finished");
            Console.WriteLine(ans.Count);
            foreach(Point a in ans)
                Console.WriteLine("("+a.X+", "+a.Y+")");

        }
    }
}