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
            btnLiqUInterior_Luz = new Button();
            btnLiqIInterior_Luz = new Button();
            btnInformeInterior_Luz = new Button();
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
            groupBox7 = new GroupBox();
            label7 = new Label();
            btnLiqUInterior_Agua = new Button();
            btnLiqIInterior_Agua = new Button();
            btnInformeInterior_Agua = new Button();
            groupBox6 = new GroupBox();
            label6 = new Label();
            btnLiqUAYSA = new Button();
            btnLiqIAYSA = new Button();
            btnInformeAYSA = new Button();
            tabPage1 = new TabPage();
            groupBox10 = new GroupBox();
            label10 = new Label();
            button15 = new Button();
            button16 = new Button();
            button17 = new Button();
            groupBox9 = new GroupBox();
            label9 = new Label();
            button12 = new Button();
            button13 = new Button();
            button14 = new Button();
            groupBox8 = new GroupBox();
            label8 = new Label();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
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
            groupBox7.SuspendLayout();
            groupBox6.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox8.SuspendLayout();
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
            groupBox4.Controls.Add(btnLiqUInterior_Luz);
            groupBox4.Controls.Add(btnLiqIInterior_Luz);
            groupBox4.Controls.Add(btnInformeInterior_Luz);
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
            // btnLiqUInterior_Luz
            // 
            btnLiqUInterior_Luz.Location = new Point(89, 71);
            btnLiqUInterior_Luz.Name = "btnLiqUInterior_Luz";
            btnLiqUInterior_Luz.Size = new Size(75, 23);
            btnLiqUInterior_Luz.TabIndex = 11;
            btnLiqUInterior_Luz.Text = "Unificada";
            btnLiqUInterior_Luz.UseVisualStyleBackColor = true;
            btnLiqUInterior_Luz.Click += btnLiqUInterior_Click;
            // 
            // btnLiqIInterior_Luz
            // 
            btnLiqIInterior_Luz.Location = new Point(6, 71);
            btnLiqIInterior_Luz.Name = "btnLiqIInterior_Luz";
            btnLiqIInterior_Luz.Size = new Size(75, 23);
            btnLiqIInterior_Luz.TabIndex = 10;
            btnLiqIInterior_Luz.Text = "Individual";
            btnLiqIInterior_Luz.UseVisualStyleBackColor = true;
            btnLiqIInterior_Luz.Click += btnLiqIInterior_Click;
            // 
            // btnInformeInterior_Luz
            // 
            btnInformeInterior_Luz.Location = new Point(6, 22);
            btnInformeInterior_Luz.Name = "btnInformeInterior_Luz";
            btnInformeInterior_Luz.Size = new Size(75, 23);
            btnInformeInterior_Luz.TabIndex = 9;
            btnInformeInterior_Luz.Text = "Informe";
            btnInformeInterior_Luz.UseVisualStyleBackColor = true;
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
            btnLiqUEdenor.Click += btnLiqUEdenor_Click;
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
            btnInformeEdenor.Click += btnInformeEdenor_Click;
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
            btnInformeEdesur.Click += btnInformeEdesur_Click;
            // 
            // tabEdenor
            // 
            tabEdenor.Controls.Add(groupBox7);
            tabEdenor.Controls.Add(groupBox6);
            tabEdenor.Location = new Point(4, 24);
            tabEdenor.Name = "tabEdenor";
            tabEdenor.Size = new Size(536, 130);
            tabEdenor.TabIndex = 1;
            tabEdenor.Text = "Agua";
            tabEdenor.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(label7);
            groupBox7.Controls.Add(btnLiqUInterior_Agua);
            groupBox7.Controls.Add(btnLiqIInterior_Agua);
            groupBox7.Controls.Add(btnInformeInterior_Agua);
            groupBox7.Location = new Point(182, 6);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(170, 118);
            groupBox7.TabIndex = 12;
            groupBox7.TabStop = false;
            groupBox7.Text = "Interior";
            groupBox7.UseCompatibleTextRendering = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 53);
            label7.Name = "label7";
            label7.Size = new Size(80, 15);
            label7.TabIndex = 10;
            label7.Text = "Liquidaciones";
            // 
            // btnLiqUInterior_Agua
            // 
            btnLiqUInterior_Agua.Location = new Point(89, 71);
            btnLiqUInterior_Agua.Name = "btnLiqUInterior_Agua";
            btnLiqUInterior_Agua.Size = new Size(75, 23);
            btnLiqUInterior_Agua.TabIndex = 11;
            btnLiqUInterior_Agua.Text = "Unificada";
            btnLiqUInterior_Agua.UseVisualStyleBackColor = true;
            // 
            // btnLiqIInterior_Agua
            // 
            btnLiqIInterior_Agua.Location = new Point(6, 71);
            btnLiqIInterior_Agua.Name = "btnLiqIInterior_Agua";
            btnLiqIInterior_Agua.Size = new Size(75, 23);
            btnLiqIInterior_Agua.TabIndex = 10;
            btnLiqIInterior_Agua.Text = "Individual";
            btnLiqIInterior_Agua.UseVisualStyleBackColor = true;
            // 
            // btnInformeInterior_Agua
            // 
            btnInformeInterior_Agua.Location = new Point(6, 22);
            btnInformeInterior_Agua.Name = "btnInformeInterior_Agua";
            btnInformeInterior_Agua.Size = new Size(75, 23);
            btnInformeInterior_Agua.TabIndex = 9;
            btnInformeInterior_Agua.Text = "Informe";
            btnInformeInterior_Agua.UseVisualStyleBackColor = true;
            btnInformeInterior_Agua.Click += button8_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(label6);
            groupBox6.Controls.Add(btnLiqUAYSA);
            groupBox6.Controls.Add(btnLiqIAYSA);
            groupBox6.Controls.Add(btnInformeAYSA);
            groupBox6.Location = new Point(6, 6);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(170, 118);
            groupBox6.TabIndex = 10;
            groupBox6.TabStop = false;
            groupBox6.Text = "AySA";
            groupBox6.UseCompatibleTextRendering = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 53);
            label6.Name = "label6";
            label6.Size = new Size(80, 15);
            label6.TabIndex = 10;
            label6.Text = "Liquidaciones";
            // 
            // btnLiqUAYSA
            // 
            btnLiqUAYSA.Location = new Point(89, 71);
            btnLiqUAYSA.Name = "btnLiqUAYSA";
            btnLiqUAYSA.Size = new Size(75, 23);
            btnLiqUAYSA.TabIndex = 11;
            btnLiqUAYSA.Text = "Unificada";
            btnLiqUAYSA.UseVisualStyleBackColor = true;
            btnLiqUAYSA.Click += btnLiqUAYSA_Click;
            // 
            // btnLiqIAYSA
            // 
            btnLiqIAYSA.Location = new Point(6, 71);
            btnLiqIAYSA.Name = "btnLiqIAYSA";
            btnLiqIAYSA.Size = new Size(75, 23);
            btnLiqIAYSA.TabIndex = 10;
            btnLiqIAYSA.Text = "Individual";
            btnLiqIAYSA.UseVisualStyleBackColor = true;
            btnLiqIAYSA.Click += btnLiqIAYSA_Click;
            // 
            // btnInformeAYSA
            // 
            btnInformeAYSA.Location = new Point(6, 22);
            btnInformeAYSA.Name = "btnInformeAYSA";
            btnInformeAYSA.Size = new Size(75, 23);
            btnInformeAYSA.TabIndex = 9;
            btnInformeAYSA.Text = "Informe";
            btnInformeAYSA.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox10);
            tabPage1.Controls.Add(groupBox9);
            tabPage1.Controls.Add(groupBox8);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(536, 130);
            tabPage1.TabIndex = 3;
            tabPage1.Text = "Gas";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(label10);
            groupBox10.Controls.Add(button15);
            groupBox10.Controls.Add(button16);
            groupBox10.Controls.Add(button17);
            groupBox10.Location = new Point(358, 6);
            groupBox10.Name = "groupBox10";
            groupBox10.Size = new Size(170, 118);
            groupBox10.TabIndex = 12;
            groupBox10.TabStop = false;
            groupBox10.Text = "Interior";
            groupBox10.UseCompatibleTextRendering = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 53);
            label10.Name = "label10";
            label10.Size = new Size(80, 15);
            label10.TabIndex = 10;
            label10.Text = "Liquidaciones";
            // 
            // button15
            // 
            button15.Location = new Point(89, 71);
            button15.Name = "button15";
            button15.Size = new Size(75, 23);
            button15.TabIndex = 11;
            button15.Text = "Unificada";
            button15.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            button16.Location = new Point(6, 71);
            button16.Name = "button16";
            button16.Size = new Size(75, 23);
            button16.TabIndex = 10;
            button16.Text = "Individual";
            button16.UseVisualStyleBackColor = true;
            // 
            // button17
            // 
            button17.Location = new Point(6, 22);
            button17.Name = "button17";
            button17.Size = new Size(75, 23);
            button17.TabIndex = 9;
            button17.Text = "Informe";
            button17.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(label9);
            groupBox9.Controls.Add(button12);
            groupBox9.Controls.Add(button13);
            groupBox9.Controls.Add(button14);
            groupBox9.Location = new Point(182, 6);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(170, 118);
            groupBox9.TabIndex = 12;
            groupBox9.TabStop = false;
            groupBox9.Text = "Metrogas Grandes";
            groupBox9.UseCompatibleTextRendering = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 53);
            label9.Name = "label9";
            label9.Size = new Size(80, 15);
            label9.TabIndex = 10;
            label9.Text = "Liquidaciones";
            // 
            // button12
            // 
            button12.Location = new Point(89, 71);
            button12.Name = "button12";
            button12.Size = new Size(75, 23);
            button12.TabIndex = 11;
            button12.Text = "Unificada";
            button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            button13.Location = new Point(6, 71);
            button13.Name = "button13";
            button13.Size = new Size(75, 23);
            button13.TabIndex = 10;
            button13.Text = "Individual";
            button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            button14.Location = new Point(6, 22);
            button14.Name = "button14";
            button14.Size = new Size(75, 23);
            button14.TabIndex = 9;
            button14.Text = "Informe";
            button14.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(label8);
            groupBox8.Controls.Add(button9);
            groupBox8.Controls.Add(button10);
            groupBox8.Controls.Add(button11);
            groupBox8.Location = new Point(6, 6);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(170, 118);
            groupBox8.TabIndex = 11;
            groupBox8.TabStop = false;
            groupBox8.Text = "Metrogas Pequeños";
            groupBox8.UseCompatibleTextRendering = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 53);
            label8.Name = "label8";
            label8.Size = new Size(80, 15);
            label8.TabIndex = 10;
            label8.Text = "Liquidaciones";
            // 
            // button9
            // 
            button9.Location = new Point(89, 71);
            button9.Name = "button9";
            button9.Size = new Size(75, 23);
            button9.TabIndex = 11;
            button9.Text = "Unificada";
            button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.Location = new Point(6, 71);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 10;
            button10.Text = "Individual";
            button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            button11.Location = new Point(6, 22);
            button11.Name = "button11";
            button11.Size = new Size(75, 23);
            button11.TabIndex = 9;
            button11.Text = "Informe";
            button11.UseVisualStyleBackColor = true;
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
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            tabPage1.ResumeLayout(false);
            groupBox10.ResumeLayout(false);
            groupBox10.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
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
        private Button btnEjecutar;
        private TabPage tabTest;
        private Button btnLiqIEdesur;
        private GroupBox groupBox2;
        private GroupBox groupBox4;
        private Label label5;
        private Button btnLiqUInterior_Luz;
        private Button btnLiqIInterior_Luz;
        private Button btnInformeInterior_Luz;
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
        private GroupBox groupBox6;
        private Label label6;
        private Button btnLiqUAYSA;
        private Button btnLiqIAYSA;
        private Button btnInformeAYSA;
        private GroupBox groupBox7;
        private Label label7;
        private Button btnLiqUInterior_Agua;
        private Button btnLiqIInterior_Agua;
        private Button btnInformeInterior_Agua;
        private GroupBox groupBox10;
        private Label label10;
        private Button button15;
        private Button button16;
        private Button button17;
        private GroupBox groupBox9;
        private Label label9;
        private Button button12;
        private Button button13;
        private Button button14;
        private GroupBox groupBox8;
        private Label label8;
        private Button button9;
        private Button button10;
        private Button button11;
    }
}
