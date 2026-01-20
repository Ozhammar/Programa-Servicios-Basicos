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
            labelPorcentaje = new Label();
            progressBar1 = new ProgressBar();
            btnLimpiarPath = new Button();
            label2 = new Label();
            btnSeleccionarCarpeta = new Button();
            txtCarpeta = new TextBox();
            label1 = new Label();
            btnPruebas = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            tabControl1 = new TabControl();
            EDESUR = new TabPage();
            button3 = new Button();
            EDENOR = new TabPage();
            button4 = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            EDESUR.SuspendLayout();
            EDENOR.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelPorcentaje);
            groupBox1.Controls.Add(progressBar1);
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
            // labelPorcentaje
            // 
            labelPorcentaje.AutoSize = true;
            labelPorcentaje.Location = new Point(418, 110);
            labelPorcentaje.Name = "labelPorcentaje";
            labelPorcentaje.Size = new Size(23, 15);
            labelPorcentaje.TabIndex = 8;
            labelPorcentaje.Text = "0%";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(6, 115);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(406, 10);
            progressBar1.TabIndex = 7;
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
            btnPruebas.Location = new Point(492, 12);
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
            button1.Location = new Point(492, 41);
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
            dataGridView1.Location = new Point(12, 187);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1309, 150);
            dataGridView1.TabIndex = 6;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGoldenrod;
            button2.ForeColor = SystemColors.ControlText;
            button2.Location = new Point(492, 70);
            button2.Name = "button2";
            button2.Size = new Size(221, 23);
            button2.TabIndex = 7;
            button2.Text = "PRUEBAS EXCEL";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(EDESUR);
            tabControl1.Controls.Add(EDENOR);
            tabControl1.Location = new Point(728, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(593, 158);
            tabControl1.TabIndex = 8;
            // 
            // EDESUR
            // 
            EDESUR.Controls.Add(button3);
            EDESUR.Location = new Point(4, 24);
            EDESUR.Name = "EDESUR";
            EDESUR.Padding = new Padding(3);
            EDESUR.Size = new Size(585, 130);
            EDESUR.TabIndex = 0;
            EDESUR.Text = "EDESUR";
            EDESUR.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(17, 24);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 9;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // EDENOR
            // 
            EDENOR.Controls.Add(button4);
            EDENOR.Location = new Point(4, 24);
            EDENOR.Name = "EDENOR";
            EDENOR.Size = new Size(585, 130);
            EDENOR.TabIndex = 1;
            EDENOR.Text = "EDENOR";
            EDENOR.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(12, 16);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 0;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1333, 465);
            Controls.Add(tabControl1);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(btnPruebas);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            EDESUR.ResumeLayout(false);
            EDENOR.ResumeLayout(false);
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
        private Label labelPorcentaje;
        private ProgressBar progressBar1;
        private Button button2;
        private TabControl tabControl1;
        private TabPage EDESUR;
        private TabPage EDENOR;
        private Button button3;
        private Button button4;
    }
}
