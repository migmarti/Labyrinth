using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Labyrinth.Classes;
using System.Drawing;

namespace Labyrinth.Classes {
    public class Graph<T> : IEnumerable<T> {
        private NodeList<T> nodeSet;

        public Graph() : this(null) { }
        public Graph(NodeList<T> nodeSet) {
            if (nodeSet == null)
                this.nodeSet = new NodeList<T>();
            else
                this.nodeSet = nodeSet;
        }

        public void AddNode(Node<T> node) {
            // adds a node to the graph
            nodeSet.Add(node);
        }

        public void AddNode(T value) {
            // adds a node to the graph
            nodeSet.Add(new Node<T>(value));
        }

        public void AddDirectedEdge(T from, T to) {
            Node<T> fromNode = Get(from);
            Node<T> toNode = Get(to);
            fromNode.Neighbors.Add(toNode);
        }

        public void AddDirectedEdge(Node<T> from, Node<T> to) {
            from.Neighbors.Add(to);
            to.Neighbors.Add(from);
        }

        public void AddUndirectedEdge(Node<T> from, Node<T> to) {
            from.Neighbors.Add(to);
            to.Neighbors.Add(from);
        }

        public bool Contains(T value) {
            return nodeSet.FindByValue(value) != null;
        }

        public Node<T> Get(T value) {
            return (Node<T>)nodeSet.FindByValue(value);
        }

        public Node<T> Get(Node<T> value) {
            return nodeSet.FindByNode(value);
        }

        public bool Remove(T value) {
            // first remove the node from the nodeset
            Node<T> nodeToRemove = (Node<T>)nodeSet.FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (Node<T> gnode in nodeSet) {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1) {
                    // remove the reference to the node and associated cost
                    gnode.Neighbors.RemoveAt(index);
                }
            }

            return true;
        }

        public NodeList<T> Nodes {
            get {
                return nodeSet;
            }
        }

        public int Count {
            get { return nodeSet.Count; }
        }

        public NodeList<T> DLS(T start, T goal, int depthLimit) {
            NodeList<T> DLSNodes = new NodeList<T>();
            Node<T> startNode = Get(start);
            Node<T> goalNode = Get(goal);
            NodeList<T> neighbors = new NodeList<T>();
            Stack<Node<T>> stack = new Stack<Node<T>>();
            stack.Push(startNode);
            int depth = 0;

            Console.WriteLine();
            while (stack.Count != 0) {
                Node<T> currentNode = stack.Pop();
                DLSNodes.Add(currentNode);
                currentNode.visited = true;
                if (currentNode == goalNode) {
                    Console.WriteLine("\nSolved!");
                    break;
                }
                if (depth == depthLimit) {
                    continue;
                }
                else {
                    Console.Write(currentNode.Neighbors.Count + " ");
                    for (int i = 0; i < currentNode.Neighbors.Count; i++) {

                        Node<T> neighbor = Get(currentNode.Neighbors[i].Value);
                        if (!neighbor.visited) {
                            stack.Push(neighbor);
                            neighbor.visited = true;
                        } 
                    }
                }
                depth++;
            }
            return DLSNodes;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < Count; i++)
                yield return default(T);
        }
    }
}
