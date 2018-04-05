using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Classes {
    public class Node<T> {
        // Private member-variables
        private T data;
        private NodeList<T> neighbors;
        public bool visited { get; set; }
        public bool distanceSet { get; set; }
        public int distanceFromGoal { get; set; }
        public double fScore { get; set; }
        public double gScore { get; set; }
        public Node<T> parent { get; set; }


        public Node(T data) {
            this.neighbors = new NodeList<T>();
            this.data = data;
            this.visited = false;
            this.gScore = 1000000.0;
            this.fScore = 1000000.0;
            this.parent = null;
        }

        public Node() {
            this.neighbors = new NodeList<T>();
            this.visited = false;
        }

        public T Value {
            get {
                return data;
            }
            set {
                data = value;
            }
        }

        public NodeList<T> Neighbors {
            get {
                return neighbors;
            }
            set {
                neighbors = value;
            }
        }
    }
}
