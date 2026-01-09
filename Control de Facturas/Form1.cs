using DocumentFormat.OpenXml.Wordprocessing;

namespace Control_de_Facturas
{
    public partial class Form1 : Form
    {
        //VARIABLES GLOBALES
        string path = "";
        private GestorArchivos gestorArchivos;


        public Form1()
        {
            InitializeComponent();
            gestorArchivos = new GestorArchivos();
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {

            //folderBrowserDialog1.Description = "Seleccione la carpeta que contiene las facturas"; //AGREGA UNA DESCRIPCION EN LA VENTANA DE SELECCIÓN
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
            }
            txtCarpeta.Text = path;

            // Contar archivos PDF en la carpeta seleccionada
            if (Directory.Exists(path))
            {
                label2.Text = $"Cantidad de archivos: {(gestorArchivos.ObtenerPDF(path)).Count().ToString()}.";
            }
        }

        private void btnLimpiarPath_Click(object sender, EventArgs e)
        {
            path = "";
            txtCarpeta.Text = path;
            label2.Text = "Cantidad de archivos: 0.";
        }

        private void btnPruebas_Click(object sender, EventArgs e)
        {
            var pdfs = gestorArchivos.ObtenerPDF(path);

            string primerPdf = pdfs.FirstOrDefault();

            if (primerPdf == null)
            {
                MessageBox.Show("No se encontraron PDFs.");
                return;
            }

            string texto = gestorArchivos.LeerPDF(primerPdf);
            //MessageBox.Show(texto);

            string path_archivo_prueba = @"..\..\..\prueba.txt";
            using(StreamWriter sw = new StreamWriter(path_archivo_prueba, true))
            {
                sw.WriteLine(texto);
            }
        }
    }
}
