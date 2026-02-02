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
            btnEjecutar = new Button();
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
            button2 = new Button();
            tabControl1 = new TabControl();
            tabCapital = new TabPage();
            groupBox4 = new GroupBox();
            label5 = new Label();
            btnLiqUInterior = new Button();
            btnLiqIInterior = new Button();
            btnInformeInterior = new Button();
            groupBox3 = new GroupBox();
            label4 = new Label();
            btnLiqUEdenor = new Button();
            btnLiqIEdenor = new Button();
            btnInformeEdenor = new Button();
            groupBox2 = new GroupBox();
            label3 = new Label();
            btnLiqUEdesur = new Button();
            btnLiqIEdesur = new Button();
            btnInformeEdesur = new Button();
            tabEdenor = new TabPage();
            button4 = new Button();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            tabTest = new TabPage();
            btnSalir = new Button();
            btnValidar = new Button();
            groupBox5 = new GroupBox();
            dataGridView1 = new DataGridView();
            groupBox1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabCapital.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            tabEdenor.SuspendLayout();
            tabTest.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnEjecutar);
            groupBox1.Controls.Add(labelPorcentaje);
            groupBox1.Controls.Add(progressBar1);
            groupBox1.Controls.Add(btnLimpiarPath);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnSeleccionarCarpeta);
            groupBox1.Controls.Add(txtCarpeta);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(474, 154);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Selección de Archivos";
            // 
            // btnEjecutar
            // 
            btnEjecutar.Location = new Point(337, 109);
            btnEjecutar.Name = "btnEjecutar";
            btnEjecutar.Size = new Size(75, 23);
            btnEjecutar.TabIndex = 9;
            btnEjecutar.Text = "Ejecutar";
            btnEjecutar.UseVisualStyleBackColor = true;
            btnEjecutar.Click += btnEjecutar_Click;
            // 
            // labelPorcentaje
            // 
            labelPorcentaje.AutoSize = true;
            labelPorcentaje.Location = new Point(418, 133);
            labelPorcentaje.Name = "labelPorcentaje";
            labelPorcentaje.Size = new Size(23, 15);
            labelPorcentaje.TabIndex = 8;
            labelPorcentaje.Text = "0%";
            labelPorcentaje.Visible = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(6, 138);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(406, 10);
            progressBar1.TabIndex = 7;
            progressBar1.Visible = false;
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
            btnPruebas.Location = new Point(6, 6);
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
            button1.Location = new Point(6, 35);
            button1.Name = "button1";
            button1.Size = new Size(221, 23);
            button1.TabIndex = 5;
            button1.Text = "PRUEBA FACTURAS A UN DATAGRID";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGoldenrod;
            button2.ForeColor = SystemColors.ControlText;
            button2.Location = new Point(6, 64);
            button2.Name = "button2";
            button2.Size = new Size(221, 23);
            button2.TabIndex = 7;
            button2.Text = "PRUEBAS EXCEL";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabCapital);
            tabControl1.Controls.Add(tabEdenor);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabTest);
            tabControl1.Location = new Point(495, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(544, 158);
            tabControl1.TabIndex = 8;
            // 
            // tabCapital
            // 
            tabCapital.Controls.Add(groupBox4);
            tabCapital.Controls.Add(groupBox3);
            tabCapital.Controls.Add(groupBox2);
            tabCapital.Location = new Point(4, 24);
            tabCapital.Name = "tabCapital";
            tabCapital.Padding = new Padding(3);
            tabCapital.Size = new Size(536, 130);
            tabCapital.TabIndex = 0;
            tabCapital.Text = "Electricidad";
            tabCapital.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(btnLiqUInterior);
            groupBox4.Controls.Add(btnLiqIInterior);
            groupBox4.Controls.Add(btnInformeInterior);
            groupBox4.Location = new Point(358, 6);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(170, 118);
            groupBox4.TabIndex = 12;
            groupBox4.TabStop = false;
            groupBox4.Text = "Interior";
            groupBox4.UseCompatibleTextRendering = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 53);
            label5.Name = "label5";
            label5.Size = new Size(80, 15);
            label5.TabIndex = 10;
            label5.Text = "Liquidaciones";
            // 
            // btnLiqUInterior
            // 
            btnLiqUInterior.Location = new Point(89, 71);
            btnLiqUInterior.Name = "btnLiqUInterior";
            btnLiqUInterior.Size = new Size(75, 23);
            btnLiqUInterior.TabIndex = 11;
            btnLiqUInterior.Text = "Unificada";
            btnLiqUInterior.UseVisualStyleBackColor = true;
            // 
            // btnLiqIInterior
            // 
            btnLiqIInterior.Location = new Point(6, 71);
            btnLiqIInterior.Name = "btnLiqIInterior";
            btnLiqIInterior.Size = new Size(75, 23);
            btnLiqIInterior.TabIndex = 10;
            btnLiqIInterior.Text = "Individual";
            btnLiqIInterior.UseVisualStyleBackColor = true;
            // 
            // btnInformeInterior
            // 
            btnInformeInterior.Location = new Point(6, 22);
            btnInformeInterior.Name = "btnInformeInterior";
            btnInformeInterior.Size = new Size(75, 23);
            btnInformeInterior.TabIndex = 9;
            btnInformeInterior.Text = "Informe";
            btnInformeInterior.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(btnLiqUEdenor);
            groupBox3.Controls.Add(btnLiqIEdenor);
            groupBox3.Controls.Add(btnInformeEdenor);
            groupBox3.Location = new Point(182, 6);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(170, 118);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Edenor";
            groupBox3.UseCompatibleTextRendering = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 53);
            label4.Name = "label4";
            label4.Size = new Size(80, 15);
            label4.TabIndex = 10;
            label4.Text = "Liquidaciones";
            // 
            // btnLiqUEdenor
            // 
            btnLiqUEdenor.Location = new Point(89, 71);
            btnLiqUEdenor.Name = "btnLiqUEdenor";
            btnLiqUEdenor.Size = new Size(75, 23);
            btnLiqUEdenor.TabIndex = 11;
            btnLiqUEdenor.Text = "Unificada";
            btnLiqUEdenor.UseVisualStyleBackColor = true;
            // 
            // btnLiqIEdenor
            // 
            btnLiqIEdenor.Location = new Point(6, 71);
            btnLiqIEdenor.Name = "btnLiqIEdenor";
            btnLiqIEdenor.Size = new Size(75, 23);
            btnLiqIEdenor.TabIndex = 10;
            btnLiqIEdenor.Text = "Individual";
            btnLiqIEdenor.UseVisualStyleBackColor = true;
            btnLiqIEdenor.Click += btnLiqIEdenor_Click;
            // 
            // btnInformeEdenor
            // 
            btnInformeEdenor.Location = new Point(6, 22);
            btnInformeEdenor.Name = "btnInformeEdenor";
            btnInformeEdenor.Size = new Size(75, 23);
            btnInformeEdenor.TabIndex = 9;
            btnInformeEdenor.Text = "Informe";
            btnInformeEdenor.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(btnLiqUEdesur);
            groupBox2.Controls.Add(btnLiqIEdesur);
            groupBox2.Controls.Add(btnInformeEdesur);
            groupBox2.Location = new Point(6, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(170, 118);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Edesur";
            groupBox2.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 53);
            label3.Name = "label3";
            label3.Size = new Size(80, 15);
            label3.TabIndex = 10;
            label3.Text = "Liquidaciones";
            // 
            // btnLiqUEdesur
            // 
            btnLiqUEdesur.Location = new Point(89, 71);
            btnLiqUEdesur.Name = "btnLiqUEdesur";
            btnLiqUEdesur.Size = new Size(75, 23);
            btnLiqUEdesur.TabIndex = 11;
            btnLiqUEdesur.Text = "Unificada";
            btnLiqUEdesur.UseVisualStyleBackColor = true;
            btnLiqUEdesur.Click += btnLiqUEdesur_Click;
            // 
            // btnLiqIEdesur
            // 
            btnLiqIEdesur.Location = new Point(6, 71);
            btnLiqIEdesur.Name = "btnLiqIEdesur";
            btnLiqIEdesur.Size = new Size(75, 23);
            btnLiqIEdesur.TabIndex = 10;
            btnLiqIEdesur.Text = "Individual";
            btnLiqIEdesur.UseVisualStyleBackColor = true;
            btnLiqIEdesur.Click += btnLiqIEdesur_Click;
            // 
            // btnInformeEdesur
            // 
            btnInformeEdesur.Location = new Point(6, 22);
            btnInformeEdesur.Name = "btnInformeEdesur";
            btnInformeEdesur.Size = new Size(75, 23);
            btnInformeEdesur.TabIndex = 9;
            btnInformeEdesur.Text = "Informe";
            btnInformeEdesur.UseVisualStyleBackColor = true;
            // 
            // tabEdenor
            // 
            tabEdenor.Controls.Add(button4);
            tabEdenor.Location = new Point(4, 24);
            tabEdenor.Name = "tabEdenor";
            tabEdenor.Size = new Size(536, 130);
            tabEdenor.TabIndex = 1;
            tabEdenor.Text = "Agua";
            tabEdenor.UseVisualStyleBackColor = true;
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
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(536, 130);
            tabPage1.TabIndex = 3;
            tabPage1.Text = "Gas";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(536, 130);
            tabPage2.TabIndex = 4;
            tabPage2.Text = "Internet";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(536, 130);
            tabPage3.TabIndex = 5;
            tabPage3.Text = "Telefonía";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(536, 130);
            tabPage4.TabIndex = 6;
            tabPage4.Text = "Municipal";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabTest
            // 
            tabTest.Controls.Add(btnPruebas);
            tabTest.Controls.Add(button2);
            tabTest.Controls.Add(button1);
            tabTest.Location = new Point(4, 24);
            tabTest.Name = "tabTest";
            tabTest.Padding = new Padding(3);
            tabTest.Size = new Size(536, 130);
            tabTest.TabIndex = 2;
            tabTest.Text = "TESTING";
            tabTest.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSalir.Location = new Point(964, 457);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnValidar
            // 
            btnValidar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnValidar.BackColor = SystemColors.Control;
            btnValidar.Location = new Point(940, 246);
            btnValidar.Name = "btnValidar";
            btnValidar.Size = new Size(75, 23);
            btnValidar.TabIndex = 10;
            btnValidar.Text = "Validar";
            btnValidar.UseVisualStyleBackColor = false;
            btnValidar.Click += btnValidar_Click;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(dataGridView1);
            groupBox5.Controls.Add(btnValidar);
            groupBox5.Location = new Point(12, 172);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(1023, 279);
            groupBox5.TabIndex = 11;
            groupBox5.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 14);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(1009, 220);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellContentClick += modificarDatosFactura;
            dataGridView1.RowPostPaint += dataGridView1_RowPostPaint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1046, 492);
            Controls.Add(groupBox5);
            Controls.Add(tabControl1);
            Controls.Add(btnSalir);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabCapital.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tabEdenor.ResumeLayout(false);
            tabTest.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
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
        private Label labelPorcentaje;
        private ProgressBar progressBar1;
        private Button button2;
        private TabControl tabControl1;
        private TabPage tabCapital;
        private TabPage tabEdenor;
        private Button button4;
        private Button btnEjecutar;
        private TabPage tabTest;
        private Button btnLiqIEdesur;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private Label label5;
        private Button btnLiqUInterior;
        private Button btnLiqIInterior;
        private Button btnInformeInterior;
        private GroupBox groupBox3;
        private Label label4;
        private Button btnLiqUEdenor;
        private Button btnLiqIEdenor;
        private Button btnInformeEdenor;
        private Label label3;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Button btnLiqUEdesur;
        private Button btnInformeEdesur;
        private Button btnSalir;
        private Button btnValidar;
        private GroupBox groupBox5;
        private DataGridView dataGridView1;
    }
}
