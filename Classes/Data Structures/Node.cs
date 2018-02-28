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

        public Node(T data) {
            this.neighbors = new NodeList<T>();
            this.data = data;
            visited = false;
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
