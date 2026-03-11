namespace Control_de_Facturas
{
    public partial class FormularioCargaManual : Form
    {
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

        List<string> tiposCodAut = new List<string>()
        {
            "CESP",
            "CAE",
            "NA",
            "CAI",
        };

        public FormularioCargaManual()
        {
            InitializeComponent();
        }

        private void FormularioCargaManual_Load(object sender, EventArgs e)
        {
            cBoxTipoServicio.DataSource = tiposDeServicios;
            cBoxTiposCodAut.DataSource = tiposCodAut;
            cBoxTiposComprobante.SelectedIndex = 0;

        }

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
                }
                else
                {
                    MessageBox.Show("No se encontró una empresa con ese CUIT.");
                }

            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}