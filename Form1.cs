using Labyrinth.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Labyrinth {
    public partial class Form1 : Form {

        Maze maze;
        Agent agent;
        Random rnd = new Random();
        List<Point> points;
        int i = 0;
        bool newMaze = false;
        System.Timers.Timer aTimer;

        public Form1() {
            InitializeComponent();
            panel1.Paint += new PaintEventHandler(panel1_Paint);
        }

        private void buttonGenerate_Click(object sender, EventArgs e) {
            maze = new Maze(Convert.ToInt32(nupWidth.Value), Convert.ToInt32(nupHeight.Value));
            maze.generate();
            newMaze = true;
            buttonRefresh.Enabled = true;
            if (aTimer != null) {
                aTimer.Stop();
            }
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            if (maze != null && maze.width > 0 && maze.height > 0) {
                var g = e.Graphics;
                int padding = 5;
                int w = maze.width;
                int h = maze.height;
                int xsize = panel1.Width / w, ysize = panel1.Height / h;
                Pen pen = new Pen(Color.Blue);
                SolidBrush startBrush = new SolidBrush(Color.Green);
                SolidBrush endBrush = new SolidBrush(Color.Red);
                SolidBrush agentBrush = new SolidBrush(Color.Yellow);
                Point startPoint = new Point(0, 0);
                pen.Width = 2;

                for (int i = 0; i < h; i++) {
                    for (int j = 0; j < w; j++) {
                        int x = startPoint.X;
                        int y = startPoint.Y;

                        if (maze.cells[i, j].start) {
                            g.FillRectangle(startBrush, new Rectangle(x + padding, y + padding, 
                                xsize - padding * 2, ysize - padding * 2));
                            if (newMaze) {
                                agent = new Agent(maze, i, j);
                                newMaze = false;
                            }
                        }
                        else if (maze.cells[i, j].end) {
                            g.FillRectangle(endBrush, new Rectangle(x + padding, y + padding,
                                xsize - padding * 2, ysize - padding * 2));
                        }

                        if (maze.cells[i, j].topWall) {
                            g.DrawLine(pen, x, y, x + xsize, y);
                        }
                        if (maze.cells[i, j].rightWall) {
                            g.DrawLine(pen, x + xsize, y, x + xsize, y + ysize);
                        }
                        if (maze.cells[i, j].leftWall) {
                            g.DrawLine(pen, x, y, x, y + ysize);
                        }
                        if (maze.cells[i, j].bottomWall) {
                            g.DrawLine(pen, x, y + ysize, x + xsize, y + ysize);
                        }

                        if (agent != null && maze.cells[i, j].hasAgent) {
                            g.FillEllipse(agentBrush, new Rectangle(x + 10, y + 10,
                                xsize - padding * 4, ysize - padding * 4));
                        }

                        startPoint = new Point(startPoint.X + xsize, startPoint.Y);
                    }
                    startPoint = new Point(0, startPoint.Y + ysize);
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e) {
            if (maze != null && agent != null) {
                i = 0;
                points = agent.buildPath(maze);
                aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 100;
                aTimer.Enabled = true;
                buttonRefresh.Enabled = false;
                
            }
        }

        
        private void OnTimedEvent(object source, ElapsedEventArgs e) {
            if (i < points.Count) {
                agent.moveTo(maze, points[i]);
                panel1.Invalidate();
                i++;
            }
            else {
                aTimer.Stop();
            }
        }

        //Eliminate flicker
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;

                return cp;
            }
        }
        
    }
}
