namespace Control_de_Facturas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            string path = "";
            folderBrowserDialog1.Description = "Seleccione la carpeta que contiene las facturas";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCarpeta.Text = folderBrowserDialog1.SelectedPath;
                path = folderBrowserDialog1.SelectedPath;
            }


        }
    }
}
