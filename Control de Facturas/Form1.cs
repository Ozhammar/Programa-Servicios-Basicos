using DocumentFormat.OpenXml.Wordprocessing;

namespace Control_de_Facturas
{
    public partial class Form1 : Form
    {
        //VARIABLES GLOBALES
        string path = "";
        private GestorArchivos gestorArchivos;
        private ControladorFacturas controladorFacturas;


        public Form1()
        {
            InitializeComponent();
            gestorArchivos = new GestorArchivos();
            controladorFacturas = new ControladorFacturas();
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
            progressBar1.Value = 0;
            labelPorcentaje.Text = "0%";
            dataGridView1.DataSource = null;
        }

        private void btnPruebas_Click(object sender, EventArgs e)
        {
            if (path != "")
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
                using (StreamWriter sw = new StreamWriter(path_archivo_prueba, true))
                {
                    sw.WriteLine(texto);
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna carpeta.");

            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Deshabilitar botón durante procesamiento
            button1.Enabled = false;
            btnSeleccionarCarpeta.Enabled = false;

            dataGridView1.Rows.Clear();

            //DATOS BASICOS PARA EL PROGRESS BAR
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            
            int totalPDFS_Porcentual = gestorArchivos.ObtenerPDF(path).Count();
            var progreso = new Progress<int>(actual =>
            {
                int porcentaje = (actual*100)/ totalPDFS_Porcentual;
                progressBar1.Value = porcentaje;
                labelPorcentaje.Text = $"{porcentaje}%";
                labelPorcentaje.Refresh();
            });


            List<Factura> facturas = await controladorFacturas.ProcesarFacturasEnCarpeta(path, progreso);

            dataGridView1.DataSource = facturas;

            // Rehabilitar botones
            button1.Enabled = true;
            btnSeleccionarCarpeta.Enabled = true;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            {
                DataGridView dgv = sender as DataGridView;

                string numero = (e.RowIndex + 1).ToString();

                using (SolidBrush brush = new SolidBrush(dgv.RowHeadersDefaultCellStyle.ForeColor))
                {
                    e.Graphics.DrawString(
                        numero,
                        dgv.RowHeadersDefaultCellStyle.Font,
                        brush,
                        e.RowBounds.Left + 10,
                        e.RowBounds.Top + 4
                    );
                }
            }
        }


    }
}
