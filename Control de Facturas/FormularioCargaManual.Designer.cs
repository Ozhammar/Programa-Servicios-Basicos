namespace Control_de_Facturas
{
    partial class FormularioCargaManual
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
            cBoxTipoServicio = new ComboBox();
            cBoxTiposCodAut = new ComboBox();
            groupBox1 = new GroupBox();
            labelRazonSocial = new Label();
            txtRazonSocial = new TextBox();
            labelCUIT = new Label();
            txtCUIT = new TextBox();
            groupBox2 = new GroupBox();
            labelTipoServicio = new Label();
            labelImporte = new Label();
            txtImporte = new TextBox();
            groupBox4 = new GroupBox();
            dateVencimientoCodAut = new DateTimePicker();
            label1 = new Label();
            labelTipo = new Label();
            txtNumCodAut = new TextBox();
            labelNumeroCodigo = new Label();
            datePeriodo = new DateTimePicker();
            dateVencimientoFC = new DateTimePicker();
            labelPeriodo = new Label();
            dateEmision = new DateTimePicker();
            labelFechaVencimiento = new Label();
            labelFechaEmision = new Label();
            groupBox3 = new GroupBox();
            label5 = new Label();
            label4 = new Label();
            cBoxTiposComprobante = new ComboBox();
            txtNumeroFactura = new TextBox();
            txtPuntoVenta = new TextBox();
            labelCliente = new Label();
            txtCliente = new TextBox();
            btnLimpiar = new Button();
            btnCargar = new Button();
            btnSalir = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // cBoxTipoServicio
            // 
            cBoxTipoServicio.FormattingEnabled = true;
            cBoxTipoServicio.Location = new Point(8, 232);
            cBoxTipoServicio.Name = "cBoxTipoServicio";
            cBoxTipoServicio.Size = new Size(169, 23);
            cBoxTipoServicio.TabIndex = 0;
            // 
            // cBoxTiposCodAut
            // 
            cBoxTiposCodAut.FormattingEnabled = true;
            cBoxTiposCodAut.Location = new Point(13, 37);
            cBoxTiposCodAut.Name = "cBoxTiposCodAut";
            cBoxTiposCodAut.Size = new Size(66, 23);
            cBoxTiposCodAut.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelRazonSocial);
            groupBox1.Controls.Add(txtRazonSocial);
            groupBox1.Controls.Add(labelCUIT);
            groupBox1.Controls.Add(txtCUIT);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(387, 75);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del Proveedor";
            // 
            // labelRazonSocial
            // 
            labelRazonSocial.AutoSize = true;
            labelRazonSocial.Location = new Point(139, 18);
            labelRazonSocial.Name = "labelRazonSocial";
            labelRazonSocial.Size = new Size(73, 15);
            labelRazonSocial.TabIndex = 3;
            labelRazonSocial.Text = "Razón Social";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.Location = new Point(139, 36);
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.PlaceholderText = "Razón social del proveedor";
            txtRazonSocial.Size = new Size(241, 23);
            txtRazonSocial.TabIndex = 2;
            txtRazonSocial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCUIT
            // 
            labelCUIT.AutoSize = true;
            labelCUIT.Location = new Point(6, 18);
            labelCUIT.Name = "labelCUIT";
            labelCUIT.Size = new Size(32, 15);
            labelCUIT.TabIndex = 1;
            labelCUIT.Text = "CUIT";
            // 
            // txtCUIT
            // 
            txtCUIT.Location = new Point(6, 36);
            txtCUIT.Name = "txtCUIT";
            txtCUIT.PlaceholderText = "CUIT del Proveedor";
            txtCUIT.Size = new Size(111, 23);
            txtCUIT.TabIndex = 0;
            txtCUIT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            txtCUIT.Leave += buscarEmpresa;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(labelTipoServicio);
            groupBox2.Controls.Add(labelImporte);
            groupBox2.Controls.Add(txtImporte);
            groupBox2.Controls.Add(cBoxTipoServicio);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(datePeriodo);
            groupBox2.Controls.Add(dateVencimientoFC);
            groupBox2.Controls.Add(labelPeriodo);
            groupBox2.Controls.Add(dateEmision);
            groupBox2.Controls.Add(labelFechaVencimiento);
            groupBox2.Controls.Add(labelFechaEmision);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(labelCliente);
            groupBox2.Controls.Add(txtCliente);
            groupBox2.Location = new Point(12, 93);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(387, 262);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Datos del Comprobante";
            // 
            // labelTipoServicio
            // 
            labelTipoServicio.AutoSize = true;
            labelTipoServicio.Location = new Point(6, 214);
            labelTipoServicio.Name = "labelTipoServicio";
            labelTipoServicio.Size = new Size(90, 15);
            labelTipoServicio.TabIndex = 23;
            labelTipoServicio.Text = "Tipo de Servicio";
            // 
            // labelImporte
            // 
            labelImporte.AutoSize = true;
            labelImporte.Font = new Font("Segoe UI", 12F);
            labelImporte.Location = new Point(229, 202);
            labelImporte.Name = "labelImporte";
            labelImporte.Size = new Size(65, 21);
            labelImporte.TabIndex = 22;
            labelImporte.Text = "Importe";
            // 
            // txtImporte
            // 
            txtImporte.Font = new Font("Segoe UI", 12F);
            txtImporte.Location = new Point(229, 226);
            txtImporte.Name = "txtImporte";
            txtImporte.Size = new Size(151, 29);
            txtImporte.TabIndex = 21;
            txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(dateVencimientoCodAut);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(labelTipo);
            groupBox4.Controls.Add(txtNumCodAut);
            groupBox4.Controls.Add(labelNumeroCodigo);
            groupBox4.Controls.Add(cBoxTiposCodAut);
            groupBox4.Location = new Point(6, 135);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(375, 68);
            groupBox4.TabIndex = 20;
            groupBox4.TabStop = false;
            groupBox4.Text = "Código de Autorización";
            // 
            // dateVencimientoCodAut
            // 
            dateVencimientoCodAut.CustomFormat = "dd/MM/yyyy";
            dateVencimientoCodAut.Format = DateTimePickerFormat.Custom;
            dateVencimientoCodAut.Location = new Point(259, 37);
            dateVencimientoCodAut.Name = "dateVencimientoCodAut";
            dateVencimientoCodAut.Size = new Size(95, 23);
            dateVencimientoCodAut.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(245, 19);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 18;
            label1.Text = "Fecha de Vencimiento";
            // 
            // labelTipo
            // 
            labelTipo.AutoSize = true;
            labelTipo.Location = new Point(13, 19);
            labelTipo.Name = "labelTipo";
            labelTipo.Size = new Size(30, 15);
            labelTipo.TabIndex = 20;
            labelTipo.Text = "Tipo";
            // 
            // txtNumCodAut
            // 
            txtNumCodAut.Location = new Point(85, 37);
            txtNumCodAut.Name = "txtNumCodAut";
            txtNumCodAut.Size = new Size(157, 23);
            txtNumCodAut.TabIndex = 20;
            txtNumCodAut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelNumeroCodigo
            // 
            labelNumeroCodigo.AutoSize = true;
            labelNumeroCodigo.Location = new Point(85, 19);
            labelNumeroCodigo.Name = "labelNumeroCodigo";
            labelNumeroCodigo.Size = new Size(51, 15);
            labelNumeroCodigo.TabIndex = 21;
            labelNumeroCodigo.Text = "Número";
            // 
            // datePeriodo
            // 
            datePeriodo.CustomFormat = "MM/yyyy";
            datePeriodo.Format = DateTimePickerFormat.Custom;
            datePeriodo.Location = new Point(278, 106);
            datePeriodo.Name = "datePeriodo";
            datePeriodo.Size = new Size(95, 23);
            datePeriodo.TabIndex = 17;
            // 
            // dateVencimientoFC
            // 
            dateVencimientoFC.CustomFormat = "dd/MM/yyyy";
            dateVencimientoFC.Format = DateTimePickerFormat.Custom;
            dateVencimientoFC.Location = new Point(146, 106);
            dateVencimientoFC.Name = "dateVencimientoFC";
            dateVencimientoFC.Size = new Size(95, 23);
            dateVencimientoFC.TabIndex = 15;
            // 
            // labelPeriodo
            // 
            labelPeriodo.AutoSize = true;
            labelPeriodo.Location = new Point(301, 88);
            labelPeriodo.Name = "labelPeriodo";
            labelPeriodo.Size = new Size(48, 15);
            labelPeriodo.TabIndex = 16;
            labelPeriodo.Text = "Período";
            // 
            // dateEmision
            // 
            dateEmision.CustomFormat = "dd/MM/yyyy";
            dateEmision.Format = DateTimePickerFormat.Custom;
            dateEmision.Location = new Point(6, 106);
            dateEmision.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            dateEmision.Name = "dateEmision";
            dateEmision.Size = new Size(103, 23);
            dateEmision.TabIndex = 4;
            dateEmision.Value = new DateTime(2026, 3, 11, 0, 0, 0, 0);
            // 
            // labelFechaVencimiento
            // 
            labelFechaVencimiento.AutoSize = true;
            labelFechaVencimiento.Location = new Point(132, 88);
            labelFechaVencimiento.Name = "labelFechaVencimiento";
            labelFechaVencimiento.Size = new Size(123, 15);
            labelFechaVencimiento.TabIndex = 14;
            labelFechaVencimiento.Text = "Fecha de Vencimiento";
            // 
            // labelFechaEmision
            // 
            labelFechaEmision.AutoSize = true;
            labelFechaEmision.Location = new Point(8, 88);
            labelFechaEmision.Name = "labelFechaEmision";
            labelFechaEmision.Size = new Size(99, 15);
            labelFechaEmision.TabIndex = 13;
            labelFechaEmision.Text = "Fecha de Emisión";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(cBoxTiposComprobante);
            groupBox3.Controls.Add(txtNumeroFactura);
            groupBox3.Controls.Add(txtPuntoVenta);
            groupBox3.Location = new Point(142, 22);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(239, 58);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Factura";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(105, 25);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(16, 21);
            label5.TabIndex = 13;
            label5.Text = "-";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(44, 25);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(16, 21);
            label4.TabIndex = 12;
            label4.Text = "-";
            // 
            // cBoxTiposComprobante
            // 
            cBoxTiposComprobante.FormattingEnabled = true;
            cBoxTiposComprobante.Items.AddRange(new object[] { "B", "C" });
            cBoxTiposComprobante.Location = new Point(8, 24);
            cBoxTiposComprobante.Name = "cBoxTiposComprobante";
            cBoxTiposComprobante.Size = new Size(36, 23);
            cBoxTiposComprobante.TabIndex = 4;
            // 
            // txtNumeroFactura
            // 
            txtNumeroFactura.Location = new Point(121, 24);
            txtNumeroFactura.Margin = new Padding(0);
            txtNumeroFactura.Name = "txtNumeroFactura";
            txtNumeroFactura.Size = new Size(111, 23);
            txtNumeroFactura.TabIndex = 8;
            txtNumeroFactura.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPuntoVenta
            // 
            txtPuntoVenta.Location = new Point(60, 24);
            txtPuntoVenta.Margin = new Padding(0);
            txtPuntoVenta.Name = "txtPuntoVenta";
            txtPuntoVenta.Size = new Size(45, 23);
            txtPuntoVenta.TabIndex = 6;
            txtPuntoVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCliente
            // 
            labelCliente.AutoSize = true;
            labelCliente.Location = new Point(6, 28);
            labelCliente.Name = "labelCliente";
            labelCliente.Size = new Size(44, 15);
            labelCliente.TabIndex = 5;
            labelCliente.Text = "Cliente";
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(6, 46);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(111, 23);
            txtCliente.TabIndex = 4;
            txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(241, 361);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnCargar
            // 
            btnCargar.Location = new Point(324, 361);
            btnCargar.Name = "btnCargar";
            btnCargar.Size = new Size(75, 23);
            btnCargar.TabIndex = 5;
            btnCargar.Text = "Cargar";
            btnCargar.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(12, 361);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(75, 23);
            btnSalir.TabIndex = 6;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // FormularioCargaManual
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 395);
            Controls.Add(btnSalir);
            Controls.Add(btnCargar);
            Controls.Add(btnLimpiar);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormularioCargaManual";
            Text = "FormularioCargaManual";
            Load += FormularioCargaManual_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cBoxTipoServicio;
        private ComboBox cBoxTiposCodAut;
        private GroupBox groupBox1;
        private TextBox txtCUIT;
        private Label labelCUIT;
        private Label labelRazonSocial;
        private TextBox txtRazonSocial;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label labelCliente;
        private TextBox txtCliente;
        private TextBox txtPuntoVenta;
        private TextBox txtNumeroFactura;
        private ComboBox cBoxTiposComprobante;
        private Label label5;
        private Label label4;
        private Label labelFechaVencimiento;
        private Label labelFechaEmision;
        private DateTimePicker dateEmision;
        private DateTimePicker dateVencimientoFC;
        private DateTimePicker datePeriodo;
        private Label labelPeriodo;
        private DateTimePicker dateVencimientoCodAut;
        private Label label1;
        private GroupBox groupBox4;
        private Label labelTipo;
        private TextBox txtNumCodAut;
        private Label labelNumeroCodigo;
        private Label labelImporte;
        private TextBox txtImporte;
        private Label labelTipoServicio;
        private Button btnLimpiar;
        private Button btnCargar;
        private Button btnSalir;
    }
}