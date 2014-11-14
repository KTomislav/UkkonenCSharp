using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkkonenSuffixTree
{
    public class Node
    {
        private List<Edge> edges; //Edges that goes FROM this node. FROM, not into.
        public int number { get; private set; } //Represents order in which this node was created. I.e 3 means is is thired node crated (root is not counted)



        public Node(int _number)
        {
            this.number = _number;
            this.edges = new List<Edge>();
        }

        public void AddEdge(Edge e)
        {
            this.edges.Add(e);
        }

        public List<NormalEdge> GetAllNormalEdges()
        {
            List<NormalEdge> result = new List<NormalEdge>();
            foreach (Edge edge in this.edges)
            {
                if (edge.GetType() == typeof(NormalEdge))
                {
                    result.Add(edge as NormalEdge);
                }
            }
            return result;
        }

        public List<Edge> GetAllEdges()
        {
            return this.edges;
        }

        public  bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Node node = obj as Node;
            if ((System.Object)node == null)
            {
                return false;
            }

            return this.number.Equals(node.number);
        }

    }
}
