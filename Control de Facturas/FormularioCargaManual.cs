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
        }
            


    }
}
