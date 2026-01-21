using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.VisualBasic;

namespace Control_de_Facturas
{
    public partial class Form1 : Form
    {
        //VARIABLES GLOBALES
        string path = "";
        private GestorArchivos gestorArchivos;
        private ControladorFacturas controladorFacturas;
        private ExportadorExcel exportadorExcel;

        private List<Factura> facturasCache = null;

        public Form1()
        {
            InitializeComponent();
            gestorArchivos = new GestorArchivos();
            controladorFacturas = new ControladorFacturas();
            exportadorExcel = new ExportadorExcel();
        }

        #region Eventos y Métodos de la Interfaz de Usuario
        //FUNCION PARA PONER EL NUMERO DE ROW EN EL DATAGRID
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
        private void btnLimpiarPath_Click(object sender, EventArgs e)
        {
            path = "";
            txtCarpeta.Text = path;
            label2.Text = "Cantidad de archivos: 0.";
            progressBar1.Value = 0;
            labelPorcentaje.Text = "0%";
            dataGridView1.DataSource = null;
            facturasCache = null;
            progressBar1.Visible = false;
            labelPorcentaje.Visible = false;
            btnValidar.Enabled = false;
            tabControl1.Enabled = false;
            btnEjecutar.Enabled = false;
            btnSeleccionarCarpeta.Enabled = true;
        }
        //FUNCION PARA EXTRACCION DE TEXTO DEL PRIMER PDF DE LA CARPETA SELECCIONADA
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
        private async void button2_Click(object sender, EventArgs e)
        {
            if (facturasCache == null || facturasCache.Count == 0)
            {
                await cargaFacturas();
            }
            exportadorExcel.generarLiquidacionGeneral(facturasCache);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            btnValidar.Enabled = false;
            btnEjecutar.Enabled = false;
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            try
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
                btnEjecutar.Enabled = true;
            }
            catch
            {
                MessageBox.Show("No se ha seleccionado ninguna carpeta.");
            }
        }

        private async void btnEjecutar_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            labelPorcentaje.Visible = true;

            await cargaFacturas();

            btnEjecutar.Enabled = false;
            btnSeleccionarCarpeta.Enabled = false;
            btnValidar.Enabled = true;
        }

        private async Task cargaFacturas()
        {
            try
            {
                // Deshabilitar botón durante procesamiento
                button1.Enabled = false;
                btnLimpiarPath.Enabled = false;
                btnSeleccionarCarpeta.Enabled = false;
                btnEjecutar.Enabled = false;

                dataGridView1.Rows.Clear();

                //DATOS BASICOS PARA EL PROGRESS BAR
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;

                int totalPDFS_Porcentual = gestorArchivos.ObtenerPDF(path).Count();
                var progreso = new Progress<int>(actual =>
                {
                    int porcentaje = (actual * 100) / totalPDFS_Porcentual;
                    progressBar1.Value = porcentaje;
                    labelPorcentaje.Text = $"{porcentaje}%";
                    labelPorcentaje.Refresh();
                });

                facturasCache = await controladorFacturas.ProcesarFacturasEnCarpeta(path, progreso);
                dataGridView1.DataSource = facturasCache;

                // Rehabilitar botones
                button1.Enabled = true;
                btnLimpiarPath.Enabled = true;
                btnEjecutar.Enabled = true;
                btnSeleccionarCarpeta.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante el procesamiento: {ex.Message}");
                // Rehabilitar botones
                button1.Enabled = true;
                btnEjecutar.Enabled = true;
                btnLimpiarPath.Enabled = true;
                btnSeleccionarCarpeta.Enabled = true;
            }
        }

        private async void btnLiqIEdesur_Click(object sender, EventArgs e)
        {
            
            if (facturasCache == null || facturasCache.Count == 0)
            {
                await cargaFacturas();
            }

            List<Factura> facturasAuxEdesur = new List<Factura>();
            foreach (Factura factura in facturasCache)
            {
                if(factura.Empresa == "EDESUR")
                {

                    facturasAuxEdesur.Add(factura);
                }
            }
            facturasAuxEdesur.OrderBy(f => f.NumeroCliente);
            exportadorExcel.generarLiquidacionGeneral(facturasAuxEdesur);
            
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = true;
        }

        private void modificarDatosFactura(object sender, DataGridViewCellEventArgs e)
        {
            int fila = e.RowIndex; //INDIVIDUALIZA ROW
            var celda = dataGridView1.Rows[fila].Cells[e.ColumnIndex].Value;//CONTENIDO DE LA COLUMNA
            string atributo = dataGridView1.Columns[e.ColumnIndex].DataPropertyName; //NOMBRE DE LA COLUMNA
            var celda_vieja = dataGridView1.Rows[fila].Cells[e.ColumnIndex].Value;

            object valorNuevo = Interaction.InputBox($"Modificado {atributo}: {celda}. \n\n Ingrese el nuevo valor. ","Modificador de Datos",$"{ celda}");
    
            if (valorNuevo == "") { 
                valorNuevo = celda_vieja;
                dataGridView1.DataSource = facturasCache; 
            }
            try
            {
                controladorFacturas.ModificarFactura(facturasCache, fila, atributo, valorNuevo);
                dataGridView1.DataSource = facturasCache;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la factura: {ex.Message}");
            }
        }
    }
}