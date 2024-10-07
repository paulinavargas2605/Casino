namespace Casino
{
    partial class Form2
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
            groupBox1 = new GroupBox();
            btnRazonSocial = new Button();
            dataGridView1 = new DataGridView();
            txtFecha = new TextBox();
            txtNit = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtNit);
            groupBox1.Controls.Add(txtFecha);
            groupBox1.Controls.Add(btnRazonSocial);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(38, 33);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1038, 518);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // btnRazonSocial
            // 
            btnRazonSocial.Location = new Point(403, 27);
            btnRazonSocial.Name = "btnRazonSocial";
            btnRazonSocial.Size = new Size(277, 29);
            btnRazonSocial.TabIndex = 2;
            btnRazonSocial.Text = "Razon Social";
            btnRazonSocial.UseVisualStyleBackColor = true;
            btnRazonSocial.Click += btnRazonSocial_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(255, 137);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(568, 322);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // txtFecha
            // 
            txtFecha.Location = new Point(367, 77);
            txtFecha.Name = "txtFecha";
            txtFecha.Size = new Size(125, 27);
            txtFecha.TabIndex = 6;
            txtFecha.Text = "Fecha";
            txtFecha.TextAlign = HorizontalAlignment.Center;
            // 
            // txtNit
            // 
            txtNit.Location = new Point(572, 77);
            txtNit.Name = "txtNit";
            txtNit.Size = new Size(125, 27);
            txtNit.TabIndex = 7;
            txtNit.Text = "Nit";
            txtNit.TextAlign = HorizontalAlignment.Center;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1102, 563);
            Controls.Add(groupBox1);
            Name = "Form2";
            Text = "Form2";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Button btnRazonSocial;
        private TextBox txtFecha;
        private TextBox txtNit;
    }
}