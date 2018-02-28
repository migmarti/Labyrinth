namespace Labyrinth {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelWidth = new System.Windows.Forms.Label();
            this.nupWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nupHeight = new System.Windows.Forms.NumericUpDown();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nupWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(136, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 500);
            this.panel1.TabIndex = 0;
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(13, 12);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(35, 13);
            this.labelWidth.TabIndex = 1;
            this.labelWidth.Text = "Width";
            // 
            // nupWidth
            // 
            this.nupWidth.Location = new System.Drawing.Point(12, 29);
            this.nupWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nupWidth.Name = "nupWidth";
            this.nupWidth.Size = new System.Drawing.Size(118, 20);
            this.nupWidth.TabIndex = 2;
            this.nupWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Height";
            // 
            // nupHeight
            // 
            this.nupHeight.Location = new System.Drawing.Point(12, 72);
            this.nupHeight.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nupHeight.Name = "nupHeight";
            this.nupHeight.Size = new System.Drawing.Size(118, 20);
            this.nupHeight.TabIndex = 4;
            this.nupHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(12, 109);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(118, 52);
            this.buttonGenerate.TabIndex = 5;
            this.buttonGenerate.Text = "Generate Maze";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(12, 194);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(118, 59);
            this.buttonRefresh.TabIndex = 6;
            this.buttonRefresh.Text = "Start Agent";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 521);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.nupHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nupWidth);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Labyrinth";
            ((System.ComponentModel.ISupportInitialize)(this.nupWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.NumericUpDown nupWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nupHeight;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonRefresh;
    }
}

