using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UkkonenSuffixTree
{
    class SuffixTree
    {
        private int nodeNumber = 0;
        private Node root;
        private int currentPosition; //As a symbol it would be #
        private string suffixString;
        private Triple activePoint;
        private int remainder;
        private int startCharacterIndexForInsertInString; //index of character in string from which new edge starts
        /// <summary>
        /// For example, in string abcabxabcd
        /// We have root->ab->cabx
        /// Now, let's say that index above has value 6(third a from beginning)
        /// We read a, b, read c and now have to change activePoint. 
        /// Here, index above remains same, but index bellow is set to index above+length of prevoius active node
        /// </summary>
        private int startCharacterIndexOfActiveNode;
        private Node previousInsertedNode; //node previously inserted during same step

        public SuffixTree(string _suffixString)
        {
            this.root = new Node(nodeNumber);
            nodeNumber++; 
            this.currentPosition = -1;
            this.suffixString = _suffixString;
            this.remainder = 0;
            this.startCharacterIndexForInsertInString = 0;
            this.startCharacterIndexOfActiveNode = 0;
            previousInsertedNode = null;

            this.activePoint = new Triple(root, null, 0);
        }

        public void CreateSuffixTree()
        {
            
            foreach (Char c in this.suffixString)
            {
                previousInsertedNode = null;
                currentPosition++; //Update the current position
                remainder++; //increment remainder

                //Add suffix to all existing edges
                AddSuffixToExistingEdges(root);

                bool exit = false;
                while (!exit)
                {
                    //If, after adding suffix, we see that active node length is larger
                    //that text at that
                    //if (this.activePoint.activeEdge!=null &&
                    //    (this.activePoint.activeEdge.endCharacterIndex+this.activePoint.activeEdge.startCharacterIndex-1 == activePoint.length))
                    //{
                    //    SetActivePoint(c);
                    //}
                    bool insertNewEdge = InsertNewEdge();                  

                    if (insertNewEdge)
                    {
                        if (remainder == 1)
                        {
                            //Create new edge from active node
                            InsertCharacter(ref activePoint.activeNode, startCharacterIndexForInsertInString, startCharacterIndexForInsertInString);

                            this.startCharacterIndexForInsertInString++;
                            this.remainder--;
                            SetActivePoint(false);
                            exit = true;
                        }
                        else
                        {
                            if (root.Equals(activePoint.activeNode))
                            {
                                //deside where to split old edge
                                //int splitIndex = activePoint.activeEdge.startCharacterIndex + activePoint.length - 1;
                                //int splitIndex = startCharacterIndexForNewEdge;
                                int splitIndex = activePoint.length + activePoint.activeEdge.startCharacterIndex;

                                InsertCharacter(ref activePoint.activeEdge.endNode, splitIndex, currentPosition);
                                InsertCharacter(ref activePoint.activeEdge.endNode, currentPosition, currentPosition);
                                if (previousInsertedNode == null)
                                {
                                    previousInsertedNode = activePoint.activeEdge.endNode;
                                }
                                else
                                {
                                    SuffixEdge suffixEdge = new SuffixEdge(previousInsertedNode, activePoint.activeEdge.endNode);
                                    previousInsertedNode.AddEdge(suffixEdge);
                                    previousInsertedNode=activePoint.activeEdge.endNode;
                                }
                                this.startCharacterIndexForInsertInString++;
                                this.remainder--;

                                activePoint.activeEdge.endCharacterIndex = splitIndex-1;
                                
                                activePoint.activeEdge.text = suffixString.Substring(activePoint.activeEdge.startCharacterIndex, 
                                    activePoint.activeEdge.endCharacterIndex - activePoint.activeEdge.startCharacterIndex+1); //
                                activePoint = new Triple(this.activePoint.activeNode, ChooseActiveEdge(), activePoint.length - 1);

                            }
                        }
                    }
                    else
                    {
                        if (activePoint.activeEdge == null)
                        {
                            activePoint.activeEdge = ChooseActiveEdge();
                        }
                        activePoint.IncrementLength();
                        exit = true;
                        //throw new NotImplementedException("TODO: AKO SUFFIX POSTOJI");
                    }
                }
            }
        }

        private void SetActivePoint(bool edgeWasSplitted)
        {
            if (edgeWasSplitted)
            {
                throw new NotImplementedException();
            }
            else 
            {
                
            }
        }

        #region CHOOSE EDGE WHEN WE "PROPAGATE" 

        /// <summary>
        /// This function is called when the active point is at the end of an edge.
        /// We need to reset active node and choose new active edge from that node.
        /// We choose that node that starts with given parameter. 
        /// Otherwise we set active edge to null
        /// </summary>
        /// <param name="firstCharacterInEdge">Character that new edge should start with</param>
        private void SetActivePoint(char firstCharacterInEdge)
        {
            if (activePoint.activeEdge.endNode == null)
            {
                throw new InvalidProgramException("At this point, edge should have end node that is not null");
            }
            else
            {
                this.startCharacterIndexOfActiveNode = this.startCharacterIndexForInsertInString + activePoint.length;
                activePoint.activeNode = activePoint.activeEdge.endNode;
                activePoint.activeEdge = ChooseActiveEdge(firstCharacterInEdge);
                if (activePoint.activeEdge != null)
                {
                    activePoint.length = 1;
                }
            }
        }

        private NormalEdge ChooseActiveEdge(char firstCharacterOfEdge)
        {
            foreach (NormalEdge edge in activePoint.activeNode.GetAllNormalEdges())
            {
                if (this.suffixString.ElementAt(edge.startCharacterIndex) == firstCharacterOfEdge)
                {
                    return edge;
                }
            }
            return null;
        }

        #endregion

        private NormalEdge ChooseActiveEdge()
        {
            foreach (NormalEdge edge in activePoint.activeNode.GetAllNormalEdges())
            {
                string s1 = this.suffixString.Substring(edge.startCharacterIndex, 1);
                string s2 = this.suffixString.Substring(this.startCharacterIndexOfActiveNode, 1);
                if (s1.Equals(s2))
                {
                    return edge;
                }
            }
            return null;
        }


        
        private bool InsertNewEdge()
        {
            if (activePoint.activeEdge == null /* && activePoint==root*/)
            {
                //foreach (NormalEdge edge in activePoint.activeNode.GetAllNormalEdges())
                //{
                //    string s1 = this.suffixString.Substring(edge.startCharacterIndex, remainder);
                //    string s2 = suffixString.Substring(this.startCharacterIndexForNewEdge, remainder);
                //    if (this.suffixString.Substring(edge.startCharacterIndex, remainder).
                //        Equals(suffixString.Substring(this.startCharacterIndexForNewEdge, remainder)))
                //    {
                //        return false;
                //    }
                //}
                return true; //only when inserting first edge
            }
            else
            {
                string s1 = this.suffixString.Substring(this.activePoint.activeEdge.startCharacterIndex, this.activePoint.length);
                string s2 = this.suffixString.Substring(this.startCharacterIndexForInsertInString, remainder);
                if (s1.Equals(s2))
                {
                    return false;
                }
            }
            return true;
        }

        private void AddSuffixToExistingEdges(Node startNode)
        {
            foreach (NormalEdge edge in startNode.GetAllNormalEdges())
            {
                if (edge.EndNode() == null)
                {
                    edge.IncrementEndCharacterIndex();
                    edge.text = suffixString.Substring(edge.startCharacterIndex, edge.endCharacterIndex - edge.startCharacterIndex + 1);
                }
                else
                {
                    AddSuffixToExistingEdges(edge.EndNode());
                }
            }
        }

        private void InsertCharacter(ref Node startNode, int startIndex, int endIndex)
        {
            if (startNode != null)
            {
                Node leaf = null;
                NormalEdge edge = new NormalEdge(startIndex, endIndex, startNode, leaf);
                edge.text = suffixString.Substring(edge.startCharacterIndex, edge.endCharacterIndex - edge.startCharacterIndex+1);
                startNode.AddEdge(edge);
            }
            else
            {
                startNode = new Node(nodeNumber);
                nodeNumber++;
                Node leaf = null;
                NormalEdge edge = new NormalEdge(startIndex, endIndex, startNode, leaf);
                edge.text = suffixString.Substring(edge.startCharacterIndex, edge.endCharacterIndex - edge.startCharacterIndex+1);
                startNode.AddEdge(edge);
            }
            //this is true only at the very first beginning, at the time the after first edge was inserted
            if (activePoint.activeEdge == null && startNode == root) 
            {
                activePoint.activeEdge = startNode.GetAllNormalEdges()[0];
                activePoint.length = 1;
            }
        }

        #region VIZUALIZACIJA
        public void DrawTree()
        {
            string javascript = "window.onload = function() {\n"+
                                "var g = new Graph();\n"+
                                "var width = $(document).width() - 20;\n"+
                                "var height = $(document).height() - 60;\n";

            javascript += DraculaGraphLibraryDraw(root);
            javascript+="var layouter = new Graph.Layout.Spring(g);\n"+
                        "layouter.layout();\n"+
                        "var renderer = new Graph.Renderer.Raphael('canvas', g, width, height);\n"+
                        "renderer.draw();}\n";
            FileStream fs = new FileStream("result.js", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(javascript);
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// Create part of js script for dracula graph visualization
        /// </summary>
        /// <param name="node"></param>
        /// <param name="edge">This is edge that comes into Node</param>
        /// <returns></returns>
        private string DraculaGraphLibraryDraw(Node node)
        {
            string s = "";
            if (node != null)
            {
                List<Edge> edges = node.GetAllEdges().ToList();
                foreach (Edge edge in edges)
                {
                    if (edge.GetType() == typeof(NormalEdge))
                    {
                        NormalEdge nedge = edge as NormalEdge;
                        string edgeName = suffixString.Substring(nedge.startCharacterIndex, nedge.endCharacterIndex - nedge.startCharacterIndex + 1);
                        string start = nedge.StartNode().number.ToString();
                        string end = "";
                        if (nedge.EndNode() != null)
                        {
                            end = nedge.EndNode().number.ToString();
                        }
                        else
                        {
                            end = "#" + nedge.startNode.number;
                        }
                        s += "g.addEdge(\"" + start + "\", \"" + end + "\", {label: \"" + edgeName + "\", directed:true});\n";
                        s += DraculaGraphLibraryDraw(nedge.EndNode());
                    }
                    else
                    {
                        string start = edge.startNode.number.ToString();
                        string end = edge.endNode.number.ToString();
                        s += "g.addEdge(\"" + start + "\", \"" + end + "\", {directed:true, stroke: \"#FF0000\"});\n";
                    }
                }
            }
            else
            {
                return "";
            }
            return s;
        }

        //For TreeView GUI control
        private void TreeViewDraw(TreeView treeView, Node node, TreeNode parentNode)
        {
            if (node != null)
            {
                List<Edge> edges = node.GetAllEdges().ToList();
                foreach (Edge edge in edges)
                {
                    if (edge.GetType() == typeof(NormalEdge))
                    {
                        NormalEdge nedge = edge as NormalEdge;
                        TreeNode tn = new TreeNode(suffixString.Substring(nedge.startCharacterIndex, nedge.endCharacterIndex - nedge.startCharacterIndex + 1));
                        parentNode.Nodes.Add(tn);
                        TreeViewDraw(treeView, nedge.EndNode(), tn);
                    }
                }
            }
            else
            {
                return;
            }

        }

        #endregion
    }
}
