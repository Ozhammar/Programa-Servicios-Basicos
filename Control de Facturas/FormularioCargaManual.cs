namespace Control_de_Facturas
{
    public partial class FormularioCargaManual : Form
    {
        #region Declaraciones Globales

        ProcesadorCargaManual cargaManual;

        string bufferFecha = "";
        public List<Factura> facturasCargadas { get; private set; }
        // Listas para cargar los tipos de servicios y tipos de códigos de autorización en los ComboBox del formulario
        List<string> tiposDeServicios = new List<string>()
        {
            "ELECTRICIDAD",
            "ELECTRICIDAD INTERIOR",
            "AGUA",
            "AGUA INTERIOR",
            "GAS",
            "GAS INTERIOR",
            "INTERNET",
            "TELEFONÍA",
            "MUNICIPAL"
        };
        // Lista de tipos de códigos de autorización, incluyendo "NA" para casos donde no se requiere código de autorización    
        List<string> tiposCodAut = new List<string>()
        {
            "CESP",
            "CAE",
            "NA",
            "CAI",
        };
        decimal valorReal = 0; // valor real de la factura sin formato
        #endregion

        public FormularioCargaManual()
        {
            InitializeComponent();
            cargaManual = new ProcesadorCargaManual();
            facturasCargadas = new List<Factura>();
        }

        private void FormularioCargaManual_Load(object sender, EventArgs e)
        {
            cBoxTipoServicio.DataSource = tiposDeServicios;
            cBoxTiposCodAut.DataSource = tiposCodAut;
            cBoxTiposComprobante.SelectedIndex = 0;

        }
        // Método para cargar la factura manualmente (aún no implementado)
        private void btnCargar_Click(object sender, EventArgs e)
        {
            facturasCargadas.Add(cargaManual.ProcesarFactura(Cargar()));

            DialogResult = DialogResult.OK;
            Close();

        }
        private DatosFactura Cargar()
        {
            DatosFactura nuevaFactura = new DatosFactura
                (
                    txtRazonSocial.Text,
                    txtCliente.Text,
                    cBoxTiposComprobante.SelectedItem.ToString(),
                    txtPuntoVenta.Text,
                    txtNumeroFactura.Text,
                    dateEmision.Value.Date,
                    dateVencimientoFC.Value.Date,
                    datePeriodo.Value.ToString("MM/yy"),
                    valorReal,
                    long.Parse(txtCUIT.Text),
                    cBoxTiposCodAut.SelectedItem.ToString(),
                    txtNumCodAut.Text,
                    dateVencimientoCodAut.Value.Date,
                    cBoxTipoServicio.SelectedItem.ToString()
                );

            return nuevaFactura;


        }
        // Método para buscar la empresa por CUIT y cargar su razón social en el formulario
        private void buscarEmpresa(object sender, EventArgs e)
        {
            BuscadorCUIT buscador = new BuscadorCUIT();

            string CUIT = txtCUIT.Text;

            if (CUIT != "")
            {
                Empresas empresa = buscador.BuscarEmpresa(CUIT);
                if (empresa != null)
                {
                    txtRazonSocial.Text = empresa.Denominacion;

                    if (empresa.CodAuth == "si")
                    {
                        cBoxTiposCodAut.SelectedIndex = 2;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró una empresa con ese CUIT.");
                }

            }
        }
        // Método para validar que la fecha de emisión no sea mayor a la fecha actual
        private void checkFechaMaximaEmision(object sender, EventArgs e)
        {
            DateTime fechaMaximaEmision = DateTime.Now;

            if (dateEmision.Value > fechaMaximaEmision)
            {
                MessageBox.Show("La fecha de emisión no puede ser mayor a la fecha actual.");
                dateEmision.Value = fechaMaximaEmision;
                dateEmision.Focus();
            }

            bufferFecha = "";
        }
        // Método para cerrar el formulario
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtImporte.Text.Contains("."))
                {
                    string valorConComa = txtImporte.Text.Replace(".", ",");
                    txtImporte.Text = valorConComa;
                }
                decimal valor = txtImporte.Text != "" ? decimal.Parse(txtImporte.Text) : 0;
                valorReal = valor;
                txtImporte.Text = $"$ {valor}";
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un valor numérico válido para el importe.");
                txtImporte.Text = "";
                valorReal = 0;

            }
        }
        private void txtPuntoVtaCheck(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPuntoVenta.Text))
                return;

            int size = txtPuntoVenta.Text.Length;

            if (size > 5)
            {
                MessageBox.Show(
                    "El punto de venta no puede tener más de 5 dígitos.",
                    "Validación de Punto de Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPuntoVenta.Focus();
                return;
            }

            if (!int.TryParse(txtPuntoVenta.Text, out _))
            {
                MessageBox.Show(
                    "El punto de venta debe contener solo números.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtPuntoVenta.Focus();
                return;
            }
        }

        private void txtNumeroFacturaCheck(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroFactura.Text))
                return;

            int size = txtNumeroFactura.Text.Length;

            if (size > 8)
            {
                MessageBox.Show(
                    "El número de factura no puede tener más de 8 dígitos.",
                    "Validación de Número de Factura",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtNumeroFactura.Focus();
                return;
            }

            if (!long.TryParse(txtNumeroFactura.Text, out _))
            {
                MessageBox.Show(
                    "El número de factura debe contener solo números.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtNumeroFactura.Focus();
                return;
            }
        }

      
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCUIT.Text = "";
            txtRazonSocial.Text = "";
            txtCliente.Text = "";
            txtPuntoVenta.Text = "";
            txtNumeroFactura.Text = "";
            txtImporte.Text = "";
            txtNumCodAut.Text = "";
            cBoxTiposComprobante.SelectedIndex = 0;
            cBoxTiposCodAut.SelectedIndex = 0;
            cBoxTipoServicio.SelectedIndex = 0;
            dateEmision.Value = DateTime.Now;
            dateVencimientoFC.Value = DateTime.Now;
            datePeriodo.Value = DateTime.Now;
            dateVencimientoCodAut.Value = DateTime.Now;
        }

      
    }
}