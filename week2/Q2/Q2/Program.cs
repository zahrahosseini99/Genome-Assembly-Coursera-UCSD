using System;
using System.Collections.Generic;
using System.Linq;

namespace Q2
{
    public class graph
    {
        public int inEdge;
        public int outEdge;
        public List<int> neighbours;
        public bool cycle;
        public int cycleIndex = -1;
        public graph()
        {
            inEdge = 0;
            outEdge = 0;
            neighbours = new List<int>();
            cycle = false;
        }
    }
    class Program
    {
      
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int n = int.Parse(input.Split()[0]);
            int m = int.Parse(input.Split()[1]);
            // in out neigh
            graph[] adj = new graph[m];
            graph[] radj=new graph[m];
            for (int i = 0; i < m; i++)
            {
                adj[i] = new graph();
                adj[i].neighbours = new List<int>();
                radj[i] = new graph();
                radj[i].neighbours = new List<int>();
            }
            for (int i = 0; i < m; i++)
            {
                string str = Console.ReadLine();
                int a = int.Parse(str.Split()[0]);
                int b = int.Parse(str.Split()[1]);
                adj[a-1].outEdge++;
                adj[b-1].inEdge++;
                adj[a-1].neighbours.Add(b-1);
                if (a == b)
                {
                    adj[a - 1].cycle = true;
                    adj[a - 1].cycleIndex = adj[a - 1].neighbours.Count - 1;
                }
                   
                //reverse graph
                radj[a-1].inEdge++;
                radj[b - 1].outEdge++;
                radj[b - 1].neighbours.Add(a - 1);

            }
            if (!IsScc(adj,radj,n,m))
            {
                Console.WriteLine("0");
                return;
            }
            for (int i = 0; i < m; i++)
            {
                if(adj[i].inEdge!=adj[i].outEdge)
                {
                    Console.WriteLine("0");
                    return;
                }
            }
            Console.WriteLine("1");
            var res = findeurlianPath(adj, n, m);
            res.Reverse();
            string result = "";
            for (int i = 0; i < res.Count; i++)
            {
                result += res[i] + " ";
            }
            Console.WriteLine(result);
        }

        private static List<int>  findeurlianPath(graph[] adj, int n, int m)
        {
            List<int> path = new List<int>();
            Stack<int> gr = new Stack<int>();
            int curNode = 0;
     
            
            
            int counter = m;
         
            //gr.Push(curNode);
            while(true)
            {
                
                if (adj[curNode].neighbours.Count != 0)
                {

                    gr.Push(curNode);
                    if (adj[curNode].cycle)
                    {
                        int index = adj[curNode].neighbours.FindIndex(x => x == curNode);
                        adj[curNode].cycle = false;
                        int temp = adj[curNode].neighbours[index];
                        adj[curNode].neighbours.RemoveAt(index);
                        curNode = temp;
                    }
                    else
                    {
                        int temp = adj[curNode].neighbours[0];
                        adj[curNode].neighbours.RemoveAt(0);
                        curNode = temp;
                    }

                }
                else
                {
                    path.Add(curNode + 1);
                    curNode = gr.Pop();
                }


                if (adj[curNode].neighbours.Count == 0 && gr.Count == 0)
                    break;
            }
            return path;
        }

        private static bool IsScc(graph[] adj, graph[] radj, int n, int m)
        {
            bool[] visited = new bool[n];
            for (int i = 0; i < n; i++)
            {
                visited[i] = false;
            }
            dfs(0, visited, adj);
            for (int i = 0; i < n; i++)
            {
                if (visited[i] == false)
                    return false;
            }
            for (int i = 0; i < n; i++)
            {
                visited[i] = false;
            }
            dfs(0, visited, radj);
            for (int i = 0; i < n; i++)
            {
                if (visited[i] == false)
                    return false;
            }
            return true;
        }

        private static void dfs(int v, bool[] visited, graph[] adj)
        {
            visited[v] = true;
            foreach (var e in adj[v].neighbours)
            {
                if (!visited[e])
                    dfs(e, visited, adj);
            }

        }
    }
}
