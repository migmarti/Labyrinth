using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Classes {
    class Maze {
        public int width, height, rows, columns;
        public Cell[,] cells;
        private Random rnd = new Random();
        public Graph<Cell> cellGraph;
        public Cell startCell;
        public Cell endCell;

        public Maze(int width, int height) {
            this.width = width;
            this.height = height;
            this.cells = new Cell[height, width];
            this.rows = this.cells.GetLength(0);
            this.columns = this.cells.GetLength(1);
        }

        public void generate() {
            initializeCells();
            Stack stack = new Stack();
            Point currentSquare = getRandomStart();
            stack.Push(currentSquare);
            while (stack.Count > 0) {
                List<Point> neighbors = getNeighbors(currentSquare);
                if (neighbors.Count > 0) {
                    Point temp = neighbors[rnd.Next(0, neighbors.Count)];
                    knockWall(currentSquare, temp);
                    stack.Push(currentSquare);
                    currentSquare = temp;
                }
                else {
                    currentSquare = (Point)stack.Pop();
                }
            }
            designateEnd();
            this.cellGraph = buildGraph();
        }    

        void knockWall(Point currentSquare, Point temp) {
            int x1 = currentSquare.X;
            int y1 = currentSquare.Y;
            int x2 = temp.X;
            int y2 = temp.Y;

            if (y2 > y1) {
                this.cells[y1, x1].bottomWall = false;
                this.cells[y2, x2].topWall = false;
            }
            else if (y2 < y1) {
                this.cells[y1, x1].topWall = false;
                this.cells[y2, x2].bottomWall = false;
            }
            else if (x2 > x1) {
                this.cells[y1, x1].rightWall = false;
                this.cells[y2, x2].leftWall = false;
            }
            else {
                this.cells[y1, x1].leftWall = false;
                this.cells[y2, x2].rightWall = false;
            }
            this.cells[y1, x1].visited = true;
            this.cells[y2, x2].visited = true;
        }

        void initializeCells() {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    this.cells[i, j] = new Cell(new Point(i, j));
                }
            }
        }

        private void designateEnd() {
            bool breaker = false;
            for (int i = height - 1; i >= 0; i--) {
                for (int j = width - 1; j >= 0; j--) {
                    Cell cell = this.cells[i, j];
                    if (cell.isDeadEnd() && !cell.start) {
                        this.cells[i, j].end = true;
                        endCell = this.cells[i, j];
                        breaker = true;
                        break;
                    }
                }
                if (breaker) {
                    break;
                }
            }
        }

        private Point getRandomStart() {
            Point start = new Point();
            start.X = rnd.Next(0, this.width);
            start.Y = rnd.Next(0, this.height);
            this.cells[start.Y, start.X].start = true;
            startCell = this.cells[start.Y, start.X];
            return start;
        }

        private List<Point> getNeighbors(Point point) {
            List<Point> neighbors = new List<Point>();
            int x = point.X, y = point.Y;

            if (y + 1 < rows) {
                if (!this.cells[y + 1, x].visited) {
                    neighbors.Add(new Point(x, y + 1));
                }
            }
            if (y - 1 < rows && y - 1 >= 0) {
                if (!this.cells[y - 1, x].visited) {
                    neighbors.Add(new Point(x, y - 1));
                }
            }
            if (x + 1 < columns) {
                if (!this.cells[y, x + 1].visited) {
                    neighbors.Add(new Point(x + 1, y));
                }
            }
            if (x - 1 < columns && x - 1 >= 0) {
                if (!this.cells[y, x - 1].visited) {
                    neighbors.Add(new Point(x - 1, y));
                }
            }

            return neighbors;
        }

        private Graph<Cell> buildGraph() {
            Graph<Cell> paths = new Graph<Cell>();

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Node<Cell> cell1 = new Node<Cell>(this.cells[y, x]);
                    paths.AddNode(cell1);
                }
            }

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    Cell cell1 = this.cells[y, x];
                    if (y + 1 < rows) {
                        if (!this.cells[y + 1, x].topWall) {
                            Cell cell2 = this.cells[y + 1, x];
                            paths.AddDirectedEdge(cell1, cell2);
                        }
                    }
                    if (y - 1 < rows && y - 1 >= 0) {
                        if (!this.cells[y - 1, x].bottomWall) {
                            Cell cell2 = this.cells[y - 1, x];
                            paths.AddDirectedEdge(cell1, cell2);
                        }
                    }
                    if (x + 1 < columns) {
                        if (!this.cells[y, x + 1].leftWall) {
                            Cell cell2 = this.cells[y, x + 1];
                            paths.AddDirectedEdge(cell1, cell2);
                        }
                    }
                    if (x - 1 < columns && x - 1 >= 0) {
                        if (!this.cells[y, x - 1].rightWall) {
                            Cell cell2 = this.cells[y, x - 1];
                            paths.AddDirectedEdge(cell1, cell2);
                        }
                    }
                }
            }
            return paths;
        }
    }
}
