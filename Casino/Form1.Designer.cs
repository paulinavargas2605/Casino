namespace Casino
{
    partial class frmExportarx18
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            casino = new GroupBox();
            groupBox1 = new GroupBox();
            txtNit = new TextBox();
            txtFecha = new TextBox();
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
            btnImportar = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // casino
            // 
            casino.Location = new Point(0, 0);
            casino.Name = "casino";
            casino.Size = new Size(200, 100);
            casino.TabIndex = 0;
            casino.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.GradientInactiveCaption;
            groupBox1.Controls.Add(txtNit);
            groupBox1.Controls.Add(txtFecha);
            groupBox1.Controls.Add(dataGridView2);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Controls.Add(btnImportar);
            groupBox1.Location = new Point(24, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1073, 753);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // txtNit
            // 
            txtNit.Location = new Point(900, 69);
            txtNit.Name = "txtNit";
            txtNit.Size = new Size(125, 27);
            txtNit.TabIndex = 6;
            txtNit.Text = "Nit";
            txtNit.TextAlign = HorizontalAlignment.Center;
            // 
            // txtFecha
            // 
            txtFecha.Location = new Point(754, 69);
            txtFecha.Name = "txtFecha";
            txtFecha.Size = new Size(125, 27);
            txtFecha.TabIndex = 5;
            txtFecha.Text = "Fecha";
            txtFecha.TextAlign = HorizontalAlignment.Center;
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = SystemColors.InactiveCaption;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(33, 442);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(1012, 284);
            dataGridView2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.InactiveCaption;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(33, 142);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1012, 270);
            dataGridView1.TabIndex = 2;
            // 
            // btnImportar
            // 
            btnImportar.Location = new Point(200, 47);
            btnImportar.Name = "btnImportar";
            btnImportar.Size = new Size(147, 71);
            btnImportar.TabIndex = 0;
            btnImportar.Text = "Importar";
            btnImportar.UseVisualStyleBackColor = true;
            btnImportar.Click += btnImportar_Click;
            // 
            // frmExportarx18
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1123, 789);
            Controls.Add(groupBox1);
            Name = "frmExportarx18";
            Text = "Exportacion X18";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox casino;
        private GroupBox groupBox1;
        private Button btnImportar;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private TextBox txtFecha;
        private TextBox txtNit;
    }
}
