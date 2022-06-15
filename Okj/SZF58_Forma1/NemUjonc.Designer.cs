
namespace SZF58_Forma1
{
    partial class NemUjonc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.rajtszam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vnev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nemzetiseg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csnev = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(804, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "A nem újonc versenyzők listája";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rajtszam,
            this.vnev,
            this.nemzetiseg,
            this.csnev});
            this.dataGridView1.Location = new System.Drawing.Point(3, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(804, 437);
            this.dataGridView1.TabIndex = 1;
            // 
            // rajtszam
            // 
            this.rajtszam.HeaderText = "Rajtszém";
            this.rajtszam.Name = "rajtszam";
            this.rajtszam.ReadOnly = true;
            // 
            // vnev
            // 
            this.vnev.HeaderText = "Versenyző";
            this.vnev.Name = "vnev";
            this.vnev.ReadOnly = true;
            // 
            // nemzetiseg
            // 
            this.nemzetiseg.HeaderText = "Nemzetiség";
            this.nemzetiseg.Name = "nemzetiseg";
            this.nemzetiseg.ReadOnly = true;
            // 
            // csnev
            // 
            this.csnev.HeaderText = "Csapat";
            this.csnev.Name = "csnev";
            this.csnev.ReadOnly = true;
            // 
            // NemUjonc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "NemUjonc";
            this.Size = new System.Drawing.Size(810, 499);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rajtszam;
        private System.Windows.Forms.DataGridViewTextBoxColumn vnev;
        private System.Windows.Forms.DataGridViewTextBoxColumn nemzetiseg;
        private System.Windows.Forms.DataGridViewTextBoxColumn csnev;
    }
}
