using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Classes {
    class Cell {
        public Point position { get; set; }
        public bool rightWall { get; set; }
        public bool leftWall { get; set; }
        public bool topWall { get; set; }
        public bool bottomWall { get; set; }
        public bool visited { get; set; }
        public bool start { get; set; }
        public bool end { get; set; }
        public bool hasAgent { get; set; }

        public Cell(Point point) {
            this.position = point;
            this.rightWall = true;
            this.leftWall = true;
            this.topWall = true;
            this.bottomWall = true;
            this.visited = false;
            this.start = false;
            this.end = false;
            this.hasAgent = false;
        }

        public bool isDeadEnd() {
            int wallCount = 0;
            bool deadEnd = false;
            bool[] walls = new bool[4];
            walls[0] = this.bottomWall;
            walls[1] = this.leftWall;
            walls[2] = this.rightWall;
            walls[3] = this.topWall;
            for (int i = 0; i < walls.Length; i++) {
                if (walls[i]) {
                    wallCount++;
                }
                if (wallCount == 3) {
                    deadEnd = true;
                    break;
                }
            }
            return deadEnd;
        }

    }
}
