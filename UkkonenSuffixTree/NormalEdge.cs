using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkkonenSuffixTree
{
    public class NormalEdge: Edge
    {
        public int startCharacterIndex { get; set; }
        public int endCharacterIndex { get; set; }
        public string text = "";

        public NormalEdge(int _startCharacterIndex, int _endCharacterIndex, Node _startNode, Node _endNode)
        {
            this.startCharacterIndex = _startCharacterIndex;
            this.endCharacterIndex = _endCharacterIndex;
            this.startNode = _startNode;
            this.endNode = _endNode;
        }

        public void IncrementEndCharacterIndex()
        {
            this.endCharacterIndex += 1;
        }

        public Node EndNode()
        {
            return this.endNode;
        }

        public Node StartNode()
        {
            return this.startNode;
        }
    }
}
