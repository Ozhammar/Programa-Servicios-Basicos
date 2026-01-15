namespace Control_de_Facturas
{
    partial class Form1
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
            groupBox1 = new GroupBox();
            btnLimpiarPath = new Button();
            label2 = new Label();
            btnSeleccionarCarpeta = new Button();
            txtCarpeta = new TextBox();
            label1 = new Label();
            btnPruebas = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnLimpiarPath);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnSeleccionarCarpeta);
            groupBox1.Controls.Add(txtCarpeta);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(474, 131);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Selección de Archivos";
            // 
            // btnLimpiarPath
            // 
            btnLimpiarPath.Location = new Point(145, 22);
            btnLimpiarPath.Name = "btnLimpiarPath";
            btnLimpiarPath.Size = new Size(75, 23);
            btnLimpiarPath.TabIndex = 1;
            btnLimpiarPath.Text = "Limpiar";
            btnLimpiarPath.UseVisualStyleBackColor = true;
            btnLimpiarPath.Click += btnLimpiarPath_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 92);
            label2.Name = "label2";
            label2.Size = new Size(133, 15);
            label2.TabIndex = 3;
            label2.Text = "Cantidad de archivos: 0.";
            // 
            // btnSeleccionarCarpeta
            // 
            btnSeleccionarCarpeta.Location = new Point(6, 22);
            btnSeleccionarCarpeta.Name = "btnSeleccionarCarpeta";
            btnSeleccionarCarpeta.Size = new Size(133, 23);
            btnSeleccionarCarpeta.TabIndex = 2;
            btnSeleccionarCarpeta.Text = "Seleccionar Carpeta";
            btnSeleccionarCarpeta.UseVisualStyleBackColor = true;
            btnSeleccionarCarpeta.Click += btnSeleccionarCarpeta_Click;
            // 
            // txtCarpeta
            // 
            txtCarpeta.Location = new Point(6, 66);
            txtCarpeta.Name = "txtCarpeta";
            txtCarpeta.ReadOnly = true;
            txtCarpeta.Size = new Size(462, 23);
            txtCarpeta.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 48);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 0;
            label1.Text = "Carpeta";
            // 
            // btnPruebas
            // 
            btnPruebas.BackColor = Color.DarkGoldenrod;
            btnPruebas.ForeColor = SystemColors.ControlText;
            btnPruebas.Location = new Point(1016, 34);
            btnPruebas.Name = "btnPruebas";
            btnPruebas.Size = new Size(182, 23);
            btnPruebas.TabIndex = 4;
            btnPruebas.Text = "PRUEBA EXTRAER TEXTO";
            btnPruebas.UseVisualStyleBackColor = false;
            btnPruebas.Click += btnPruebas_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.DarkGoldenrod;
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(1016, 63);
            button1.Name = "button1";
            button1.Size = new Size(221, 23);
            button1.TabIndex = 5;
            button1.Text = "PRUEBA FACTURAS A UN DATAGRID";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 182);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1260, 150);
            dataGridView1.TabIndex = 6;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1292, 465);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(btnPruebas);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnSeleccionarCarpeta;
        private TextBox txtCarpeta;
        private Label label1;
        private Label label2;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnLimpiarPath;
        private Button btnPruebas;
        private Button button1;
        private DataGridView dataGridView1;
    }
}
