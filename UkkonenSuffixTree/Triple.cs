using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkkonenSuffixTree
{
    public class Triple
    {
        public Node activeNode;
        public NormalEdge activeEdge;
        public int length;

        public Triple(Node _activeNode, NormalEdge _activeEdge, int _length)
        {
            this.activeNode = _activeNode;
            this.activeEdge = _activeEdge;
            this.length = _length;
        }

        public void IncrementLength()
        {
            this.length++;
        }


    }
}
