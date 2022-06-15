
namespace SZF58_Forma1
{
    partial class UjVersenyzo
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
            this.label2 = new System.Windows.Forms.Label();
            this.InputVNev = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.InputVRajt = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.InputCsapat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.InputUjonc = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.InputDatum = new System.Windows.Forms.DateTimePicker();
            this.Insert = new System.Windows.Forms.Button();
            this.InsertResult = new System.Windows.Forms.Label();
            this.ErrorNev = new System.Windows.Forms.Label();
            this.ErrorRajtszam = new System.Windows.Forms.Label();
            this.ErrorDatum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.InputVRajt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(681, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "Új versenyző felvétele";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Versenyző neve:";
            // 
            // InputVNev
            // 
            this.InputVNev.Location = new System.Drawing.Point(228, 94);
            this.InputVNev.Name = "InputVNev";
            this.InputVNev.Size = new System.Drawing.Size(191, 29);
            this.InputVNev.TabIndex = 2;
            this.InputVNev.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Versenyző rajtszáma:";
            // 
            // InputVRajt
            // 
            this.InputVRajt.Location = new System.Drawing.Point(228, 136);
            this.InputVRajt.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.InputVRajt.Name = "InputVRajt";
            this.InputVRajt.Size = new System.Drawing.Size(191, 29);
            this.InputVRajt.TabIndex = 4;
            this.InputVRajt.ValueChanged += new System.EventHandler(this.InputVRajt_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Csapata:";
            // 
            // InputCsapat
            // 
            this.InputCsapat.FormattingEnabled = true;
            this.InputCsapat.Location = new System.Drawing.Point(228, 176);
            this.InputCsapat.Name = "InputCsapat";
            this.InputCsapat.Size = new System.Drawing.Size(191, 32);
            this.InputCsapat.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "Űjonc:";
            // 
            // InputUjonc
            // 
            this.InputUjonc.AutoSize = true;
            this.InputUjonc.Location = new System.Drawing.Point(228, 225);
            this.InputUjonc.Name = "InputUjonc";
            this.InputUjonc.Size = new System.Drawing.Size(15, 14);
            this.InputUjonc.TabIndex = 8;
            this.InputUjonc.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 24);
            this.label6.TabIndex = 9;
            this.label6.Text = "Belépés dátuma:";
            // 
            // InputDatum
            // 
            this.InputDatum.Location = new System.Drawing.Point(228, 250);
            this.InputDatum.Name = "InputDatum";
            this.InputDatum.Size = new System.Drawing.Size(200, 29);
            this.InputDatum.TabIndex = 10;
            this.InputDatum.ValueChanged += new System.EventHandler(this.InputDatum_ValueChanged);
            // 
            // Insert
            // 
            this.Insert.Enabled = false;
            this.Insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Insert.Location = new System.Drawing.Point(204, 311);
            this.Insert.Name = "Insert";
            this.Insert.Size = new System.Drawing.Size(156, 53);
            this.Insert.TabIndex = 11;
            this.Insert.Text = "Felvétel";
            this.Insert.UseVisualStyleBackColor = true;
            this.Insert.Click += new System.EventHandler(this.Insert_Click);
            // 
            // InsertResult
            // 
            this.InsertResult.AutoSize = true;
            this.InsertResult.Location = new System.Drawing.Point(258, 380);
            this.InsertResult.Name = "InsertResult";
            this.InsertResult.Size = new System.Drawing.Size(0, 24);
            this.InsertResult.TabIndex = 12;
            this.InsertResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ErrorNev
            // 
            this.ErrorNev.AutoSize = true;
            this.ErrorNev.ForeColor = System.Drawing.Color.Red;
            this.ErrorNev.Location = new System.Drawing.Point(434, 97);
            this.ErrorNev.Name = "ErrorNev";
            this.ErrorNev.Size = new System.Drawing.Size(0, 24);
            this.ErrorNev.TabIndex = 13;
            // 
            // ErrorRajtszam
            // 
            this.ErrorRajtszam.AutoSize = true;
            this.ErrorRajtszam.ForeColor = System.Drawing.Color.Red;
            this.ErrorRajtszam.Location = new System.Drawing.Point(434, 141);
            this.ErrorRajtszam.Name = "ErrorRajtszam";
            this.ErrorRajtszam.Size = new System.Drawing.Size(0, 24);
            this.ErrorRajtszam.TabIndex = 14;
            // 
            // ErrorDatum
            // 
            this.ErrorDatum.AutoSize = true;
            this.ErrorDatum.ForeColor = System.Drawing.Color.Red;
            this.ErrorDatum.Location = new System.Drawing.Point(434, 254);
            this.ErrorDatum.Name = "ErrorDatum";
            this.ErrorDatum.Size = new System.Drawing.Size(0, 24);
            this.ErrorDatum.TabIndex = 15;
            // 
            // UjVersenyzo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ErrorDatum);
            this.Controls.Add(this.ErrorRajtszam);
            this.Controls.Add(this.ErrorNev);
            this.Controls.Add(this.InsertResult);
            this.Controls.Add(this.Insert);
            this.Controls.Add(this.InputDatum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.InputUjonc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.InputCsapat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InputVRajt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.InputVNev);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "UjVersenyzo";
            this.Size = new System.Drawing.Size(684, 424);
            this.Load += new System.EventHandler(this.UjVersenyzo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InputVRajt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox InputVNev;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown InputVRajt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox InputCsapat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox InputUjonc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker InputDatum;
        private System.Windows.Forms.Button Insert;
        private System.Windows.Forms.Label InsertResult;
        private System.Windows.Forms.Label ErrorNev;
        private System.Windows.Forms.Label ErrorRajtszam;
        private System.Windows.Forms.Label ErrorDatum;
    }
}
