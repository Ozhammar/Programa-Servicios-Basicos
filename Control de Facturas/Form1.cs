using DocumentFormat.OpenXml.Wordprocessing;

namespace Control_de_Facturas
{
    public partial class Form1 : Form
    {
        //VARIABLES GLOBALES
        string path = "";
        private GestorArchivos gestorArchivos;
        private ControladorFacturas controladorFacturas;
        private ExportadorExcel exportadorExcel;
        private ConfiguracionExcel _config;
        private DatosBasicosExcel datosBasicosExcel;


        public Form1()
        {
            InitializeComponent();
            gestorArchivos = new GestorArchivos();
            controladorFacturas = new ControladorFacturas();
            exportadorExcel = new ExportadorExcel();
  
            
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
                int porcentaje = (actual * 100) / totalPDFS_Porcentual;
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

        private async void button2_Click(object sender, EventArgs e)
        {
            XLWorkbook libro = exportadorExcel.abrirPlantilla();
            MessageBox.Show("Plantilla abierta correctamente");

            IXLWorksheet cabecera = libro.Worksheet("Cabecera-Cpte");
            IXLWorksheet detalle_cabecera = libro.Worksheet("Detalle Cpte FacGS");
            IXLWorksheet detalle_financiero = libro.Worksheet("Detalle Presupuestario  FACGS");

            string valor = cabecera.Cell("A5").GetString();
            MessageBox.Show(valor);

            string path_archivo_prueba = @"..\..\..\";

            if (path != "")
            {


                if (path == null)
                {
                    MessageBox.Show("No se encontraron PDFs.");
                    return;
                }

                List<Factura> facturas = await controladorFacturas.ProcesarFacturasEnCarpeta(path);
                int filaCabecera = 5;
                int filaDetalleCabecera = 4;
                int filaDetalleFinanciero = 4;


                foreach (Factura factura in facturas)
                {
                    //CABECERA
                    cabecera.Cell($"A{filaCabecera}").Value = 326;
                    cabecera.Cell($"B{filaCabecera}").Value = "FACGS";
                    cabecera.Cell($"C{filaCabecera}").Value = DateTime.Now.Year;
                    cabecera.Cell($"D{filaCabecera}").Value = "FSB";
                    cabecera.Cell($"E{filaCabecera}").Value = factura.TipoFactura;
                    cabecera.Cell($"F{filaCabecera}").Value = Convert.ToInt32(factura.PuntoVenta);
                    cabecera.Cell($"G{filaCabecera}").Value = Convert.ToInt32(factura.NumeroFactura);
                    cabecera.Cell($"K{filaCabecera}").Value = factura.TipoCodigoAutorizacion;
                    cabecera.Cell($"L{filaCabecera}").Value = factura.CodigoAutorizacion;
                    cabecera.Cell($"M{filaCabecera}").Value = factura.VencimientoCodigoAutorizacion.ToString("dd/MM/yyyy");
                    cabecera.Cell($"O{filaCabecera}").Value = "CUI";
                    cabecera.Cell($"P{filaCabecera}").Value = factura.CUIT;
                    cabecera.Cell($"X{filaCabecera}").Value = "ARP";
                    cabecera.Cell($"AA{filaCabecera}").Value = 1;
                    cabecera.Cell($"AB{filaCabecera}").Value = "RC";
                    cabecera.Cell($"AF{filaCabecera}").Value = factura.FechaEmision.ToString("dd/MM/yyyy");
                    cabecera.Cell($"AG{filaCabecera}").Value = factura.FechaEmision.AddDays(3).ToString("dd/MM/yyyy");
                    cabecera.Cell($"AI{filaCabecera}").Value = factura.ImporteAbonable;
                    cabecera.Cell($"AJ{filaCabecera}").Value = $"SERVICIO DE {factura.TipoServicio.ToUpper()} CORRESPONDIENTE A {factura.Empresa.ToUpper()}, CLIENTE: {factura.NumeroCliente} - FACTURA N°: {factura.NumeroFactura} - PERIODO: //-// - IMPORTE: $ {factura.ImporteAbonable}";

                    filaCabecera++;

                    //DETALLE CABECERA
                    detalle_cabecera.Cell($"A{filaDetalleCabecera}").Value = factura.TipoFactura;
                    detalle_cabecera.Cell($"B{filaDetalleCabecera}").Value = Convert.ToInt32(factura.PuntoVenta);
                    detalle_cabecera.Cell($"C{filaDetalleCabecera}").Value = Convert.ToInt32(factura.NumeroFactura);
                    detalle_cabecera.Cell($"D{filaDetalleCabecera}").Value = "CUI";
                    detalle_cabecera.Cell($"E{filaDetalleCabecera}").Value = factura.CUIT;
                    detalle_cabecera.Cell($"F{filaDetalleCabecera}").Value = factura.TipoCodigoAutorizacion;
                    detalle_cabecera.Cell($"G{filaDetalleCabecera}").Value = factura.CodigoAutorizacion;
                    detalle_cabecera.Cell($"H{filaDetalleCabecera}").Value = factura.CodigoCatalogo;
                    detalle_cabecera.Cell($"J{filaDetalleCabecera}").Value = factura.ObjetoGasto.Split(".")[0];
                    detalle_cabecera.Cell($"K{filaDetalleCabecera}").Value = factura.ObjetoGasto.Split(".")[1];
                    detalle_cabecera.Cell($"L{filaDetalleCabecera}").Value = factura.ObjetoGasto.Split(".")[2];
                    detalle_cabecera.Cell($"M{filaDetalleCabecera}").Value = factura.ObjetoGasto.Split(".")[3];
                    detalle_cabecera.Cell($"N{filaDetalleCabecera}").Value = 1;
                    detalle_cabecera.Cell($"P{filaDetalleCabecera}").Value = factura.ImporteAbonable;

                    //DETALLE FINANCIERO
                    detalle_financiero.Cell($"A{filaDetalleFinanciero}").Value = factura.TipoFactura;
                    detalle_financiero.Cell($"B{filaDetalleFinanciero}").Value = Convert.ToInt32(factura.PuntoVenta);
                    detalle_financiero.Cell($"C{filaDetalleFinanciero}").Value = Convert.ToInt32(factura.NumeroFactura);
                    detalle_financiero.Cell($"D{filaDetalleFinanciero}").Value = "CUI";
                    detalle_financiero.Cell($"E{filaDetalleFinanciero}").Value = factura.CUIT;
                    detalle_financiero.Cell($"F{filaDetalleFinanciero}").Value = factura.TipoCodigoAutorizacion;
                    detalle_financiero.Cell($"G{filaDetalleFinanciero}").Value = factura.CodigoAutorizacion;
                    detalle_financiero.Cell($"J{filaDetalleFinanciero}").Value = 41;
                    detalle_financiero.Cell($"K{filaDetalleFinanciero}").Value = 4;
                    detalle_financiero.Cell($"L{filaDetalleFinanciero}").Value = 0;


                }




                libro.SaveAs(Path.Combine(path_archivo_prueba, "PruebaSalida.xlsx"));
                MessageBox.Show("LIBRO GUARDADO correctamente");

            }
            else
            {
                MessageBox.Show("No se ha seleccionado ninguna carpeta.");

            }
        }
    }
}
