using Microsoft.VisualBasic;

namespace Control_de_Facturas
{
    public partial class Form1 : Form
    {
        //VARIABLES GLOBALES
        string path = "E:\\Programa Facturas - Lucas\\AYSAEDESUR\\UNICA FACTURA"; //ELIMINAR RUTA PARA RELEASE
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

        #region Eventos de Carga y Configuración
        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            btnValidar.Enabled = false;
            btnEjecutar.Enabled = false;

            btnEjecutar.Enabled = true; //ELIMINAR CUANDO NO SE QUIERA PROBAR LA CARGA AUTOMÁTICA DE LA CARPETA PREDEFINIDA
        }
        #endregion

        #region Eventos de Selección y Limpieza
        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    path = folderBrowserDialog1.SelectedPath;
                }
                txtCarpeta.Text = path;

                if (Directory.Exists(path))
                {
                    label2.Text = $"Cantidad de archivos: {gestorArchivos.ObtenerPDF(path).Count()}.";
                }
                btnEjecutar.Enabled = true;
            }
            catch
            {
                MessageBox.Show("No se ha seleccionado ninguna carpeta.");
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

        private async Task comprobacionCache()
        {
            if (facturasCache == null || facturasCache.Count == 0)
            {
                await cargaFacturas();
            }
        }
        #endregion

        #region Procesamiento de Facturas
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
                // Deshabilitar controles durante procesamiento
                button1.Enabled = false;
                btnLimpiarPath.Enabled = false;
                btnSeleccionarCarpeta.Enabled = false;
                btnEjecutar.Enabled = false;

                dataGridView1.Rows.Clear();

                // Configurar progress bar
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;

                int totalPDFS = gestorArchivos.ObtenerPDF(path).Count();
                var progreso = new Progress<int>(actual =>
                {
                    int porcentaje = (actual * 100) / totalPDFS;
                    progressBar1.Value = porcentaje;
                    labelPorcentaje.Text = $"{porcentaje}%";
                    labelPorcentaje.Refresh();
                });

                // Procesar facturas
                facturasCache = await controladorFacturas.ProcesarFacturasEnCarpeta(path, progreso);
                dataGridView1.DataSource = facturasCache;
                FormatearColumnasDecimales();

                // Rehabilitar controles
                button1.Enabled = true;
                btnLimpiarPath.Enabled = true;
                btnEjecutar.Enabled = true;
                btnSeleccionarCarpeta.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante el procesamiento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Rehabilitar controles
                button1.Enabled = true;
                btnEjecutar.Enabled = true;
                btnLimpiarPath.Enabled = true;
                btnSeleccionarCarpeta.Enabled = true;
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = true;
        }
        #endregion

        #region Exportación de Liquidaciones 
        #region ELECTRICIDAD

        //LIQUIDACION EDESUR INDIVIDUAL
        private async void btnLiqIEdesur_Click(object sender, EventArgs e)
        {
            await comprobacionCache();

            // LÓGICA MOVIDA AL CONTROLADOR
            List<Factura> facturasEdesur = controladorFacturas.FiltrarPorEmpresa(facturasCache, "EDESUR");

            if (facturasEdesur.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de EDESUR", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            exportadorExcel.generarLiquidacionIndividual(facturasEdesur, "1.0.0.1.0");
        }


        //LIQUIDACION EDESUR UNIFICADA
        private async void btnLiqUEdesur_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            List<Factura> facturasEdesur = controladorFacturas.FiltrarPorEmpresa(facturasCache, "EDESUR");

            if (facturasEdesur.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de EDESUR", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exportadorExcel.generarLiquidacionUnificada(facturasEdesur, "1.0.0.1.0");
        }
        //INFORME EDESUR
        private void btnInformeEdesur_Click(object sender, EventArgs e)
        {

        }
        //INFORME EDENOR
        private void btnInformeEdenor_Click(object sender, EventArgs e)
        {

        }
        //LIQUIDACION EDENOR INDIVIDUAL
        private async void btnLiqIEdenor_Click(object sender, EventArgs e)
        {
            await comprobacionCache();

            // LÓGICA MOVIDA AL CONTROLADOR
            List<Factura> facturasEdenor = controladorFacturas.FiltrarPorEmpresa(facturasCache, "EDENOR");

            if (facturasEdenor.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de EDENOR", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            exportadorExcel.generarLiquidacionIndividual(facturasEdenor, "1.0.0.1.0");
        }
        //LIQUIDACION EDENOR UNIFICADA
        private async void btnLiqUEdenor_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            List<Factura> facturasEdenor = controladorFacturas.FiltrarPorEmpresa(facturasCache, "EDENOR");

            if (facturasEdenor.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de EDESUR", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exportadorExcel.generarLiquidacionUnificada(facturasEdenor, "1.0.0.1.0");
        }


        private void btnLiqIInterior_Click(object sender, EventArgs e)
        {

        }

        private void btnLiqUInterior_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region AGUA
        private async void btnLiqIAYSA_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            List<Factura> facturasAysa = controladorFacturas.FiltrarPorEmpresa(facturasCache, "AYSA");

            if (facturasAysa.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de AYSA", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exportadorExcel.generarLiquidacionIndividual(facturasAysa, "1.0.0.1.0");
        }

        private async void btnLiqUAYSA_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            List<Factura> facturasAysa = controladorFacturas.FiltrarPorEmpresa(facturasCache, "AYSA");

            if (facturasAysa.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de AYSA", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exportadorExcel.generarLiquidacionUnificada(facturasAysa, "1.0.0.1.0");
        }

        #endregion

        #region GAS
        private async void btnLiqIMetrogasG_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            List<Factura> facturasMetrogasG = controladorFacturas.FiltrarPorEmpresa(facturasCache, "METROGAS GRANDES CLIENTES");

            if (facturasMetrogasG.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de METROGAS GRANDES CLIENTES", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exportadorExcel.generarLiquidacionIndividual(facturasMetrogasG, "1.0.0.1.0");
        }

        private async void btnLiqUMetrogasG_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            List<Factura> facturasMetrogasG = controladorFacturas.FiltrarPorEmpresa(facturasCache, "METROGAS GRANDES CLIENTES");

            if (facturasMetrogasG.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de METROGAS GRANDES CLIENTES", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exportadorExcel.generarLiquidacionUnificada(facturasMetrogasG, "1.0.0.1.0");
        }

        private async void btnLiqIMetrogasC_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            List<Factura> facturasMetrogasP = controladorFacturas.FiltrarPorEmpresa(facturasCache, "METROGAS PEQUEÑOS CLIENTES");

            if (facturasMetrogasP.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de METROGAS PEQUEÑOS CLIENTES", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exportadorExcel.generarLiquidacionIndividual(facturasMetrogasP, "1.0.0.1.0");
        }

        private async void btnLiqUMetrogasC_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            List<Factura> facturasMetrogasP = controladorFacturas.FiltrarPorEmpresa(facturasCache, "METROGAS PEQUEÑOS CLIENTES");

            if (facturasMetrogasP.Count == 0)
            {
                MessageBox.Show("No se encontraron facturas de METROGAS PEQUEÑOS CLIENTES", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            exportadorExcel.generarLiquidacionUnificada(facturasMetrogasP, "1.0.0.1.0");
        }

        #endregion

        /// <summary>        /// /////
        #endregion

        #region Modificación de Datos
        private void modificarDatosFactura(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que no sea el header
            if (e.RowIndex < 0) return;

            int fila = e.RowIndex;
            int columna = e.ColumnIndex;

            var celdaActual = dataGridView1.Rows[fila].Cells[columna].Value;
            string atributo = dataGridView1.Columns[columna].DataPropertyName;

            // InputBox para obtener nuevo valor
            object valorNuevoStr = Interaction.InputBox(
                $"Modificando '{atributo}'\n\nValor actual: {celdaActual}\n\nIngrese el nuevo valor:",
                "Modificador de Datos",
                celdaActual?.ToString() ?? ""
            );

            // Si cancela o deja vacío, no hacer nada
            if (string.IsNullOrWhiteSpace(valorNuevoStr.ToString()))
            {
                return;
            }

            try
            {
                controladorFacturas.ModificarFactura(facturasCache, fila, atributo, valorNuevoStr);

                // Refrescar DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = facturasCache;
                FormatearColumnasDecimales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar la factura:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Utilidades UI


        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
        private void FormatearColumnasDecimales()
        {
            string[] columnasDecimales =
            {
        "ImportePrimerVencimiento",
        "ImporteSaldoAnterior",
        "ImporteAbonable"
    };

            foreach (string col in columnasDecimales)
            {
                if (dataGridView1.Columns.Contains(col))
                {
                    dataGridView1.Columns[col].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        //BOTON PRUEBAS EXCEL TESTING
        private async void button2_Click(object sender, EventArgs e)
        {
            await comprobacionCache();
            exportadorExcel.generarLiquidacionIndividual(facturasCache, "1.0.0.1.0");
        }
        #endregion









        private void button8_Click(object sender, EventArgs e)
        {

        }


    }
}