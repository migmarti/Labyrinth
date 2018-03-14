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
        List<Point> directions;
        int i = 0;
        bool newMaze = false;
        System.Timers.Timer aTimer;

        public Form1() {
            InitializeComponent();
            panel1.Paint += new PaintEventHandler(panel1_Paint);
        }

        private void buttonGenerate_Click(object sender, EventArgs e) {
            maze = new Maze(Convert.ToInt32(nupWidth.Value), Convert.ToInt32(nupHeight.Value));
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
                if (newMaze) {
                    agent = new Agent(maze, maze.startCell.position.X, maze.startCell.position.Y);
                    newMaze = false;
                }
                maze.draw(g, panel1.Width, panel1.Height);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e) {
            if (maze != null && agent != null) {
                i = 0;
                directions = agent.buildPath(maze);
                aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 
                    ((Convert.ToInt32(nupWidth.Value) + Convert.ToInt32(nupHeight.Value)) / 2) * 10;
                aTimer.Enabled = true;
                buttonRefresh.Enabled = false;
                
            }
        }

        
        private void OnTimedEvent(object source, ElapsedEventArgs e) {
            if (i < directions.Count) {
                agent.moveTo(maze, directions[i]);
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
