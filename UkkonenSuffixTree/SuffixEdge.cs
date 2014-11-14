using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkkonenSuffixTree
{
    public class SuffixEdge : Edge
    {
        public SuffixEdge(Node _startNode, Node _endNode)
        {
            this.startNode = _startNode;
            this.endNode = _endNode;
        }
    }
}
