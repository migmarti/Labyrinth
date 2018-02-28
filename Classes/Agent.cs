﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Classes {
    class Agent {
        public int pos1 { get; set; }
        public int pos2 { get; set; }
        private int rows;
        private int columns;

        public Agent(Maze maze, int pos1, int pos2) {
            this.pos1 = pos1;
            this.pos2 = pos2;
            maze.cells[pos1, pos2].hasAgent = true;
        }

        public List<Point> buildPath(Maze maze) {
            List<Point> directions = new List<Point>();
            Graph<Cell> graph = maze.cellGraph;
           
            NodeList<Cell> dlsNodes = graph.DLS(maze.startCell, maze.endCell, maze.height * maze.width);

            foreach (Node<Cell> node in dlsNodes) {
                Cell cell = node.Value;
                directions.Add(new Point(cell.position.X, cell.position.Y));
            }

            return directions;
        }

        public void moveTo(Maze maze, Point p) {
            Point currentPoint = new Point(this.pos1, this.pos2);
            maze.cells[currentPoint.X, currentPoint.Y].hasAgent = false;
            maze.cells[p.X, p.Y].hasAgent = true;
            this.pos1 = p.X;
            this.pos2 = p.Y;
        }
    }
}
