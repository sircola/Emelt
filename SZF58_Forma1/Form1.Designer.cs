
namespace SZF58_Forma1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.kezdőlapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nemÚjoncokListájaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.újVersenyzőFelvételToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ujVersenyzo1 = new SZF58_Forma1.UjVersenyzo();
            this.nemUjonc1 = new SZF58_Forma1.NemUjonc();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kezdőlapToolStripMenuItem,
            this.nemÚjoncokListájaToolStripMenuItem,
            this.újVersenyzőFelvételToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(874, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // kezdőlapToolStripMenuItem
            // 
            this.kezdőlapToolStripMenuItem.Name = "kezdőlapToolStripMenuItem";
            this.kezdőlapToolStripMenuItem.Size = new System.Drawing.Size(102, 29);
            this.kezdőlapToolStripMenuItem.Text = "Kezdőlap";
            this.kezdőlapToolStripMenuItem.Click += new System.EventHandler(this.kezdőlapToolStripMenuItem_Click);
            // 
            // nemÚjoncokListájaToolStripMenuItem
            // 
            this.nemÚjoncokListájaToolStripMenuItem.Name = "nemÚjoncokListájaToolStripMenuItem";
            this.nemÚjoncokListájaToolStripMenuItem.Size = new System.Drawing.Size(190, 29);
            this.nemÚjoncokListájaToolStripMenuItem.Text = "Nem újoncok listája";
            this.nemÚjoncokListájaToolStripMenuItem.Click += new System.EventHandler(this.nemÚjoncokListájaToolStripMenuItem_Click);
            // 
            // újVersenyzőFelvételToolStripMenuItem
            // 
            this.újVersenyzőFelvételToolStripMenuItem.Name = "újVersenyzőFelvételToolStripMenuItem";
            this.újVersenyzőFelvételToolStripMenuItem.Size = new System.Drawing.Size(207, 29);
            this.újVersenyzőFelvételToolStripMenuItem.Text = "Új versenyző felvétele";
            this.újVersenyzőFelvételToolStripMenuItem.Click += new System.EventHandler(this.újVersenyzőFelvételToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SZF58_Forma1.Properties.Resources.formula1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(874, 502);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ujVersenyzo1
            // 
            this.ujVersenyzo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ujVersenyzo1.Location = new System.Drawing.Point(82, 66);
            this.ujVersenyzo1.Margin = new System.Windows.Forms.Padding(6);
            this.ujVersenyzo1.Name = "ujVersenyzo1";
            this.ujVersenyzo1.Size = new System.Drawing.Size(684, 464);
            this.ujVersenyzo1.TabIndex = 3;
            this.ujVersenyzo1.Visible = false;
            // 
            // nemUjonc1
            // 
            this.nemUjonc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nemUjonc1.Location = new System.Drawing.Point(33, 39);
            this.nemUjonc1.Margin = new System.Windows.Forms.Padding(6);
            this.nemUjonc1.Name = "nemUjonc1";
            this.nemUjonc1.Size = new System.Drawing.Size(810, 499);
            this.nemUjonc1.TabIndex = 2;
            this.nemUjonc1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 545);
            this.Controls.Add(this.ujVersenyzo1);
            this.Controls.Add(this.nemUjonc1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Formula1 2021";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kezdőlapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nemÚjoncokListájaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem újVersenyzőFelvételToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private NemUjonc nemUjonc1;
        private UjVersenyzo ujVersenyzo1;
    }
}

