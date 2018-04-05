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
            nodeSet.Add(node);
        }

        public void AddNode(T value) {
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
            Node<T> nodeToRemove = (Node<T>)nodeSet.FindByValue(value);
            if (nodeToRemove == null)
                return false;

            nodeSet.Remove(nodeToRemove);

            foreach (Node<T> gnode in nodeSet) {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1) {
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

        public void setDistancesFromGoal(T goal) {
            int distance = 1;
            bool addedNode = false;
            NodeList<T> neighbors = new NodeList<T>();
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> goalNode = Get(goal);
            goalNode.distanceFromGoal = 0;
            stack.Push(goalNode);
        
            while (stack.Count != 0) {
                Node<T> currentNode = stack.Pop();
                currentNode.distanceSet = true;
                addedNode = false;
                for (int i = 0; i < currentNode.Neighbors.Count; i++) {
                    Node<T> neighbor = Get(currentNode.Neighbors[i].Value);
                    if (!neighbor.distanceSet) {
                        neighbor.distanceFromGoal = distance;
                        addedNode = true;
                        stack.Push(neighbor);
                    }
                }
                if (addedNode) {
                    distance++;
                }
            }
        }

        public NodeList<T> AStar(T start, T goal) {
            Node<T> startNode = Get(start);
            Node<T> goalNode = Get(goal);
            NodeList<T> parents = new NodeList<T>();
            NodeList<T> closedSet = new NodeList<T>();
            NodeList<T> openSet = new NodeList<T>() { startNode };

            startNode.parent = null;
            startNode.gScore = 0;
            startNode.fScore = calculateDistance(start, goal);

            while (openSet.Count > 0) {
                Node<T> currentNode = getLowestNode(openSet);
                //Console.WriteLine(currentNode.gScore);
                if (currentNode == goalNode) {
                    return reconstructPath(currentNode);
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                for (int i = 0; i < currentNode.Neighbors.Count; i++) {
                    Node<T> neighbor = Get(currentNode.Neighbors[i]);
                    if (closedSet.Contains(neighbor)) {
                        continue;
                    }
                    if (!openSet.Contains(neighbor)) {
                        openSet.Add(neighbor);
                    }
                    double tentativeGScore = currentNode.gScore +
                        calculateDistance(currentNode.Value, neighbor.Value);
                    if (tentativeGScore >= neighbor.gScore) {
                        continue;
                    }
                    neighbor.parent = currentNode;
                    neighbor.gScore = tentativeGScore;
                    neighbor.fScore = neighbor.gScore +
                        calculateDistance(neighbor.Value, goal);
                }
            }
            return null;
        }

        private NodeList<T> reconstructPath(Node<T> currentNode) {
            NodeList<T> totalPath = new NodeList<T>() { currentNode };
            while (currentNode.parent != null) {
                currentNode = currentNode.parent;
                totalPath.Add(currentNode);
            }
            return totalPath;
        }

        private Node<T> getLowestNode(NodeList<T> openSet) {
            double lowest = 10000000.0;
            Node<T> returnNode = new Node<T>();
            foreach (Node<T> node in openSet) {
                if (node.fScore < lowest) {
                    lowest = node.fScore;
                    returnNode = node;
                }
            }
            return returnNode;
        } 


        private double calculateDistance(T t1, T t2) {
            Cell c1 = (Cell)(object)t1;
            Cell c2 = (Cell)(object)t2;

            int x1 = c1.position.Y;
            int y1 = c1.position.X;
            int x2 = c2.position.Y;
            int y2 = c2.position.X;

            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public NodeList<T> getBestPath(T start, T goal) {
            setDistancesFromGoal(goal);
            int minCost = int.MaxValue;
            bool addedNode = false;
            NodeList<T> nodesTraversed = new NodeList<T>();
            Node<T> startNode = Get(start);
            Node<T> goalNode = Get(goal);
            Node<T> bestNeighbor = new Node<T>();
            NodeList<T> neighbors = new NodeList<T>();
            Stack<Node<T>> stack = new Stack<Node<T>>();
            stack.Push(startNode);

            while (stack.Count != 0) {
                Node<T> currentNode = stack.Pop();
                nodesTraversed.Add(currentNode);
                addedNode = false;
                currentNode.visited = true;
                if (currentNode == goalNode) {
                    break;
                }
                else {
                    for (int i = 0; i < currentNode.Neighbors.Count; i++) {
                        Node<T> neighbor = Get(currentNode.Neighbors[i].Value);
                        int cost = neighbor.distanceFromGoal;
                        if (cost < minCost && !neighbor.visited) {
                            bestNeighbor = neighbor;
                            minCost = cost;
                            addedNode = true;
                        }
                    }
                    if (addedNode) {
                        stack.Push(bestNeighbor);
                    }
                }
            }
            return nodesTraversed;
        }

        /*
        public NodeList<T> DLS(T start, T goal, int depthLimit) {
            bool addedDepth = false;
            NodeList<T> DLSNodes = new NodeList<T>();
            Node<T> startNode = Get(start);
            Node<T> goalNode = Get(goal);
            NodeList<T> neighbors = new NodeList<T>();
            Stack<Node<T>> stack = new Stack<Node<T>>();
            stack.Push(startNode);
            int depth = 0;

            while (stack.Count != 0) {
                Node<T> currentNode = stack.Pop();
                DLSNodes.Add(currentNode);
                currentNode.visited = true;
                if (currentNode == goalNode) {
                    break;
                }
                if (depth == depthLimit) {
                    continue;
                }
                else {
                    for (int i = 0; i < currentNode.Neighbors.Count; i++) {

                        Node<T> neighbor = Get(currentNode.Neighbors[i].Value);
                        if (!neighbor.visited) {
                            stack.Push(neighbor);
                            neighbor.visited = true;
                            addedDepth = true;
                        }
                    }
                    if (addedDepth) {
                        depth++;
                    }
                }
            }
            return DLSNodes;
        }
        */

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            for (int i = 0; i < Count; i++)
                yield return default(T);
        }
    }
}
