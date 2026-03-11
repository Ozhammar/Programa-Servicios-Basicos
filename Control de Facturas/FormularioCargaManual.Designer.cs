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
            labelCUIT = new Label();
            txtCUIT = new TextBox();
            labelRazonSocial = new Label();
            txtRazonSocial = new TextBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // cBoxTipoServicio
            // 
            cBoxTipoServicio.FormattingEnabled = true;
            cBoxTipoServicio.Location = new Point(626, 373);
            cBoxTipoServicio.Name = "cBoxTipoServicio";
            cBoxTipoServicio.Size = new Size(169, 23);
            cBoxTipoServicio.TabIndex = 0;
            // 
            // cBoxTiposCodAut
            // 
            cBoxTiposCodAut.FormattingEnabled = true;
            cBoxTiposCodAut.Location = new Point(801, 373);
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
            // FormularioCargaManual
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 509);
            Controls.Add(groupBox1);
            Controls.Add(cBoxTiposCodAut);
            Controls.Add(cBoxTipoServicio);
            Name = "FormularioCargaManual";
            Text = "FormularioCargaManual";
            Load += FormularioCargaManual_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
    }
}