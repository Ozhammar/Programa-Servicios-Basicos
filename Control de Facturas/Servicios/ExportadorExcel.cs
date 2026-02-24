using Control_de_Facturas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Servicios
{

    internal class ExportadorExcel
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private readonly string rutaPlantilla_SIDIF;
        private readonly string rutaPlantillaInfome_AYSA;
        private readonly string rutaPlantillaInfome_EDESUR;
        private readonly string rutaPlantillaInfome_MetroGasChicos;
        private readonly string rutaPlantillaInfome_MetroGasGrandes;
        private ControladorFacturas controladorFacturas;

        public ExportadorExcel()
        {
            //PLANTILLA BASE SIDIF
            rutaPlantilla_SIDIF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "PLANTILLA.xlsx");

            //PLANTILLAS DE INFORMES
            rutaPlantillaInfome_AYSA = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "Plantillas Pagos", "AYSA.xlsx");
            rutaPlantillaInfome_EDESUR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "Plantillas Pagos", "EDESUR.xlsx");
            rutaPlantillaInfome_MetroGasChicos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "Plantillas Pagos", "METROGAS PEQUEÑOS.xlsx");
            rutaPlantillaInfome_MetroGasGrandes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "Plantillas Pagos", "METROGAS GRANDES.xlsx");

            controladorFacturas = new ControladorFacturas();
        }

        public XLWorkbook abrirPlantilla(string Plantilla)
        {
            try
            {
                XLWorkbook libro = new XLWorkbook(Plantilla);
                return libro;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al abrir la plantilla de Excel: Archivo no encontrado" + ex.Message);
            }
        }

        private void cargarPlantillaSidif(out XLWorkbook libro, out IXLWorksheet cabecera, out IXLWorksheet detalle_cabecera, out IXLWorksheet detalle_financiero)
        {
            libro = this.abrirPlantilla(rutaPlantilla_SIDIF);
            cabecera = libro.Worksheet("Cabecera-Cpte");
            detalle_cabecera = libro.Worksheet("Detalle Cpte FacGS");
            detalle_financiero = libro.Worksheet("Detalle Presupuestario  FACGS");
        }

        private string obtenerRutaPlantillaInforme(string empresa)
        {
            switch (empresa.ToUpper())
            {
                case "AYSA":
                    return rutaPlantillaInfome_AYSA;
                case "EDESUR":
                    return rutaPlantillaInfome_EDESUR;
                case "METROGAS PEQUEÑOS CLIENTES":
                    return rutaPlantillaInfome_MetroGasChicos;
                case "METROGAS GRANDES CLIENTES":
                    return rutaPlantillaInfome_MetroGasChicos;
                default:
                    throw new Exception("No se ha encontrado una plantilla de informe para la empresa especificada.");
            }
        }



        #region PlantillaExportadores
        /*public void generarLiquidacionEdesur(List<Factura> facturasCache)
        {
            XLWorkbook libro = this.abrirPlantilla();

            IXLWorksheet cabecera = libro.Worksheet("Cabecera-Cpte");
            IXLWorksheet detalle_cabecera = libro.Worksheet("Detalle Cpte FacGS");
            IXLWorksheet detalle_financiero = libro.Worksheet("Detalle Presupuestario  FACGS");

            DatosBasicosExcel config = ConfiguracionExcel.CrearPorDefecto();
            List<Factura> facturas = facturasCache;
            int filaCabecera = 5;
            int filaDetalleCabecera = 4;
            int filaDetalleFinanciero = 4;



            foreach (Factura factura in facturas)
            {
                //CABECERA
                cabecera.Cell($"A{filaCabecera}").Value = config.SAF;
                cabecera.Cell($"B{filaCabecera}").Value = config.TipoComprobante;
                cabecera.Cell($"C{filaCabecera}").Value = config.Ejercicio;
                cabecera.Cell($"D{filaCabecera}").Value = "FSB";
                cabecera.Cell($"E{filaCabecera}").Value = factura.TipoFactura;
                cabecera.Cell($"F{filaCabecera}").Value = Convert.ToInt32(factura.PuntoVenta);
                cabecera.Cell($"G{filaCabecera}").Value = Convert.ToInt32(factura.NumeroFactura);
                cabecera.Cell($"K{filaCabecera}").Value = factura.TipoCodigoAutorizacion;
                cabecera.Cell($"L{filaCabecera}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                cabecera.Cell($"M{filaCabecera}").Value = factura.VencimientoCodigoAutorizacion.ToString("dd/MM/yyyy");
                cabecera.Cell($"O{filaCabecera}").Value = config.TipoDocumento;
                cabecera.Cell($"P{filaCabecera}").Value = factura.CUIT;
                cabecera.Cell($"X{filaCabecera}").Value = config.TipoMoneda;
                cabecera.Cell($"AA{filaCabecera}").Value = config.Cotizacion;
                cabecera.Cell($"AB{filaCabecera}").Value = config.MedioPago;
                cabecera.Cell($"AF{filaCabecera}").Value = factura.FechaEmision.ToString("dd/MM/yyyy");
                cabecera.Cell($"AG{filaCabecera}").Value = factura.FechaEmision.AddDays(3).ToString("dd/MM/yyyy");
                cabecera.Cell($"AI{filaCabecera}").Value = factura.ImporteAbonable;
                cabecera.Cell($"AJ{filaCabecera}").Value = $"SERVICIO DE {factura.TipoServicio.ToUpper()} CORRESPONDIENTE A {factura.Empresa.ToUpper()}, CLIENTE: {factura.NumeroCliente} - FACTURA N°: {factura.NumeroFactura} - PERIODO: //-// - IMPORTE: $ {factura.ImporteAbonable}";

                filaCabecera++;

                //DETALLE CABECERA
                string[] objetoGastoPartes = factura.ObjetoGasto.Split(".");

                detalle_cabecera.Cell($"A{filaDetalleCabecera}").Value = factura.TipoFactura;
                detalle_cabecera.Cell($"B{filaDetalleCabecera}").Value = Convert.ToInt32(factura.PuntoVenta);
                detalle_cabecera.Cell($"C{filaDetalleCabecera}").Value = Convert.ToInt32(factura.NumeroFactura);
                detalle_cabecera.Cell($"D{filaDetalleCabecera}").Value = config.TipoDocumento;
                detalle_cabecera.Cell($"E{filaDetalleCabecera}").Value = factura.CUIT;
                detalle_cabecera.Cell($"F{filaDetalleCabecera}").Value = factura.TipoCodigoAutorizacion;
                detalle_cabecera.Cell($"G{filaDetalleCabecera}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                detalle_cabecera.Cell($"H{filaDetalleCabecera}").Value = factura.CodigoCatalogo;
                detalle_cabecera.Cell($"J{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[0]);
                detalle_cabecera.Cell($"K{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[1]);
                detalle_cabecera.Cell($"L{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[2]);
                detalle_cabecera.Cell($"M{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[3]);
                detalle_cabecera.Cell($"N{filaDetalleCabecera}").Value = config.CantidadUnidades;
                detalle_cabecera.Cell($"P{filaDetalleCabecera}").Value = factura.ImporteAbonable;
                filaDetalleCabecera++;

                //DETALLE FINANCIERO
                detalle_financiero.Cell($"A{filaDetalleFinanciero}").Value = factura.TipoFactura;
                detalle_financiero.Cell($"B{filaDetalleFinanciero}").Value = Convert.ToInt32(factura.PuntoVenta);
                detalle_financiero.Cell($"C{filaDetalleFinanciero}").Value = Convert.ToInt32(factura.NumeroFactura);
                detalle_financiero.Cell($"D{filaDetalleFinanciero}").Value = config.TipoDocumento;
                detalle_financiero.Cell($"E{filaDetalleFinanciero}").Value = factura.CUIT;
                detalle_financiero.Cell($"F{filaDetalleFinanciero}").Value = factura.TipoCodigoAutorizacion;
                detalle_financiero.Cell($"G{filaDetalleFinanciero}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                detalle_financiero.Cell($"J{filaDetalleFinanciero}").Value = config.Jurisdiccion;
                detalle_financiero.Cell($"K{filaDetalleFinanciero}").Value = config.SubJurisdiccion;
                detalle_financiero.Cell($"L{filaDetalleFinanciero}").Value = config.Entidad;
                detalle_financiero.Cell($"M{filaDetalleFinanciero}").Value = config.SAF;


                detalle_financiero.Cell($"U{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[0]);
                detalle_financiero.Cell($"V{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[1]);
                detalle_financiero.Cell($"W{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[2]);
                detalle_financiero.Cell($"X{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[3]);
                detalle_financiero.Cell($"Y{filaDetalleCabecera}").Value = config.FuenteFinanciamiento;
                detalle_financiero.Cell($"Z{filaDetalleCabecera}").Value = config.Moneda;
                detalle_financiero.Cell($"AB{filaDetalleCabecera}").Value = config.PEX;
                detalle_financiero.Cell($"AC{filaDetalleCabecera}").Value = config.BAPIN;
                detalle_financiero.Cell($"AE{filaDetalleCabecera}").Value = config.CodigoGanancias;
                detalle_financiero.Cell($"AF{filaDetalleCabecera}").Value = config.CodigoIVA;
                detalle_financiero.Cell($"AG{filaDetalleCabecera}").Value = config.CodigoSUSS;
                detalle_financiero.Cell($"AH{filaDetalleCabecera}").Value = config.PorcentualIVA;
                detalle_financiero.Cell($"AJ{filaDetalleCabecera}").Value = factura.ImporteAbonable;
                filaDetalleFinanciero++;
            }

            libro.SaveAs(Path.Combine(desktopPath, $"FacturasEdesurExportadas_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"));
            MessageBox.Show("LIBRO GUARDADO correctamente");
        }*/
        #endregion

        public void generarLiquidacionUnica(List<Factura> facturasCache)
        {
            throw new NotImplementedException();
        }

        public void generarLiquidacionIndividual(List<Factura> facturasCache, string actividadProgramatica, int dependencia = 650, int uGeografica = 2)
        {
            XLWorkbook libro;
            IXLWorksheet cabecera, detalle_cabecera, detalle_financiero;
            cargarPlantillaSidif(out libro, out cabecera, out detalle_cabecera, out detalle_financiero);

            DatosBasicosExcel config = ConfiguracionExcel.CrearPorDefecto();
            List<Factura> facturas = facturasCache;
            int filaCabecera = 5;
            int filaDetalleCabecera = 4;
            int filaDetalleFinanciero = 4;

            foreach (Factura factura in facturas)
            {
                //CABECERA
                cabecera.Cell($"A{filaCabecera}").Value = config.SAF;
                cabecera.Cell($"B{filaCabecera}").Value = config.TipoComprobante;
                cabecera.Cell($"C{filaCabecera}").Value = config.Ejercicio;
                cabecera.Cell($"D{filaCabecera}").Value = "FSB";
                cabecera.Cell($"E{filaCabecera}").Value = factura.TipoFactura;
                if (int.TryParse(factura.PuntoVenta, out int puntoVenta))
                {
                    cabecera.Cell($"F{filaCabecera}").Value = puntoVenta;
                }
                else
                {
                    cabecera.Cell($"F{filaCabecera}").Value = 0; // o lo que quieras poner si está vacío
                }
                cabecera.Cell($"G{filaCabecera}").Value = Convert.ToInt32(factura.NumeroFactura);
                cabecera.Cell($"K{filaCabecera}").Value = factura.TipoCodigoAutorizacion;
                cabecera.Cell($"L{filaCabecera}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                cabecera.Cell($"M{filaCabecera}").Value = factura.VencimientoCodigoAutorizacion.ToString("dd/MM/yyyy");
                cabecera.Cell($"O{filaCabecera}").Value = config.TipoDocumento;
                cabecera.Cell($"P{filaCabecera}").Value = factura.CUIT;
                cabecera.Cell($"X{filaCabecera}").Value = config.TipoMoneda;
                cabecera.Cell($"AA{filaCabecera}").Value = config.Cotizacion;
                cabecera.Cell($"AB{filaCabecera}").Value = config.MedioPago;
                cabecera.Cell($"AF{filaCabecera}").Value = factura.FechaEmision.ToString("dd/MM/yyyy");
                cabecera.Cell($"AG{filaCabecera}").Value = factura.FechaEmision.AddDays(3).ToString("dd/MM/yyyy"); //SE AÑADO DE FORMA GENERICA LA FECHA DE EMISION MAS 3 DIAS, COMO PARAMETRO DE DESARROLLO BASE. LA IDEA ES QUE EN CASO DE TENER QUE SER MODIFICADA, SE LO HAGA DESDE EL ARCHIVO EXPORTADO
                cabecera.Cell($"AI{filaCabecera}").Value = factura.ImporteAbonable;
                cabecera.Cell($"AJ{filaCabecera}").Value = $"SERVICIO DE {factura.TipoServicio} CORRESPONDIENTE A {factura.Empresa}, CLIENTE: {factura.NumeroCliente} - FACTURA N°: {factura.PuntoVenta}-{factura.NumeroFactura} - PERIODO: {factura.Periodo} - IMPORTE: $ {factura.ImporteAbonable}";
                ++filaCabecera;

                //DETALLE CABECERA
                string[] objetoGastoPartes = factura.ObjetoGasto.Split(".");

                detalle_cabecera.Cell($"A{filaDetalleCabecera}").Value = factura.TipoFactura;
                detalle_cabecera.Cell($"B{filaDetalleCabecera}").Value = Convert.ToInt32(factura.PuntoVenta);
                detalle_cabecera.Cell($"C{filaDetalleCabecera}").Value = Convert.ToInt32(factura.NumeroFactura);
                detalle_cabecera.Cell($"D{filaDetalleCabecera}").Value = config.TipoDocumento;
                detalle_cabecera.Cell($"E{filaDetalleCabecera}").Value = factura.CUIT;
                detalle_cabecera.Cell($"F{filaDetalleCabecera}").Value = factura.TipoCodigoAutorizacion;
                detalle_cabecera.Cell($"G{filaDetalleCabecera}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                detalle_cabecera.Cell($"H{filaDetalleCabecera}").Value = factura.CodigoCatalogo;
                detalle_cabecera.Cell($"J{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[0]);
                detalle_cabecera.Cell($"K{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[1]);
                detalle_cabecera.Cell($"L{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[2]);
                detalle_cabecera.Cell($"M{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[3]);
                detalle_cabecera.Cell($"N{filaDetalleCabecera}").Value = config.CantidadUnidades;
                detalle_cabecera.Cell($"P{filaDetalleCabecera}").Value = factura.ImporteAbonable;
                ++filaDetalleCabecera;

                //DETALLE FINANCIERO
                string[] apertura_programatica = actividadProgramatica.Split(".");
                detalle_financiero.Cell($"A{filaDetalleFinanciero}").Value = factura.TipoFactura;
                detalle_financiero.Cell($"B{filaDetalleFinanciero}").Value = Convert.ToInt32(factura.PuntoVenta);
                detalle_financiero.Cell($"C{filaDetalleFinanciero}").Value = Convert.ToInt32(factura.NumeroFactura);
                detalle_financiero.Cell($"D{filaDetalleFinanciero}").Value = config.TipoDocumento;
                detalle_financiero.Cell($"E{filaDetalleFinanciero}").Value = factura.CUIT;
                detalle_financiero.Cell($"F{filaDetalleFinanciero}").Value = factura.TipoCodigoAutorizacion;
                detalle_financiero.Cell($"G{filaDetalleFinanciero}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                detalle_financiero.Cell($"J{filaDetalleFinanciero}").Value = config.Jurisdiccion;
                detalle_financiero.Cell($"K{filaDetalleFinanciero}").Value = config.SubJurisdiccion;
                detalle_financiero.Cell($"L{filaDetalleFinanciero}").Value = config.Entidad;
                detalle_financiero.Cell($"M{filaDetalleFinanciero}").Value = config.SAF;
                detalle_financiero.Cell($"N{filaDetalleFinanciero}").Value = dependencia;
                detalle_financiero.Cell($"O{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[0]);
                detalle_financiero.Cell($"P{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[1]);
                detalle_financiero.Cell($"Q{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[2]);
                detalle_financiero.Cell($"R{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[3]);
                detalle_financiero.Cell($"S{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[4]);
                detalle_financiero.Cell($"T{filaDetalleFinanciero}").Value = uGeografica;
                detalle_financiero.Cell($"U{filaDetalleFinanciero}").Value = int.Parse(objetoGastoPartes[0]);
                detalle_financiero.Cell($"V{filaDetalleFinanciero}").Value = int.Parse(objetoGastoPartes[1]);
                detalle_financiero.Cell($"W{filaDetalleFinanciero}").Value = int.Parse(objetoGastoPartes[2]);
                detalle_financiero.Cell($"X{filaDetalleFinanciero}").Value = int.Parse(objetoGastoPartes[3]);
                detalle_financiero.Cell($"Y{filaDetalleFinanciero}").Value = config.FuenteFinanciamiento;
                detalle_financiero.Cell($"Z{filaDetalleFinanciero}").Value = config.Moneda;
                detalle_financiero.Cell($"AB{filaDetalleFinanciero}").Value = config.PEX;
                detalle_financiero.Cell($"AC{filaDetalleFinanciero}").Value = config.BAPIN;
                detalle_financiero.Cell($"AE{filaDetalleFinanciero}").Value = config.CodigoGanancias;
                detalle_financiero.Cell($"AF{filaDetalleFinanciero}").Value = config.CodigoIVA;
                detalle_financiero.Cell($"AG{filaDetalleFinanciero}").Value = config.CodigoSUSS;
                detalle_financiero.Cell($"AH{filaDetalleFinanciero}").Value = config.PorcentualIVA;
                detalle_financiero.Cell($"AJ{filaDetalleFinanciero}").Value = factura.ImporteAbonable;
                ++filaDetalleFinanciero;
            }
            libro.SaveAs(Path.Combine(desktopPath, $"Facturas{facturas[0].Empresa}_ExportadasIndividual_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"));
            MessageBox.Show("LIBRO GUARDADO correctamente");
        }

        public void generarLiquidacionUnificada(List<Factura> facturasFiltradas, string actividadProgramatica, int dependencia = 650, int uGeografica = 2)
        {
            XLWorkbook libro;
            IXLWorksheet cabecera, detalle_cabecera, detalle_financiero;
            cargarPlantillaSidif(out libro, out cabecera, out detalle_cabecera, out detalle_financiero);

            DatosBasicosExcel config = ConfiguracionExcel.CrearPorDefecto();
            // /List<Factura> facturas = facturasEdesur;
            int filaCabecera = 5;
            int filaDetalleCabecera = 4;
            int filaDetalleFinanciero = 4;

            List<List<Factura>> facturasPorPeriodo = facturasFiltradas.GroupBy(f => f.Periodo).Select(g => g.ToList()).ToList();

            foreach (List<Factura> periodo in facturasPorPeriodo)
            {
                List<Factura> facturas = periodo;
                Factura factura = null;
                decimal importeTotalPeriodo = 0;

                foreach (Factura facturaPeriodo in facturas)
                {
                    try
                    {
                        if (factura == null)
                        {
                            factura = facturaPeriodo;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al procesar las facturas por periodo: " + ex.Message);
                    }
                    importeTotalPeriodo += facturaPeriodo.ImporteAbonable;
                }

                //CABECERA
                cabecera.Cell($"A{filaCabecera}").Value = config.SAF;
                cabecera.Cell($"B{filaCabecera}").Value = config.TipoComprobante;
                cabecera.Cell($"C{filaCabecera}").Value = config.Ejercicio;
                cabecera.Cell($"D{filaCabecera}").Value = "FSB";
                cabecera.Cell($"E{filaCabecera}").Value = factura.TipoFactura;
                cabecera.Cell($"F{filaCabecera}").Value = Convert.ToInt32(factura.PuntoVenta);
                cabecera.Cell($"G{filaCabecera}").Value = Convert.ToInt32(factura.NumeroFactura);
                cabecera.Cell($"K{filaCabecera}").Value = factura.TipoCodigoAutorizacion;
                cabecera.Cell($"L{filaCabecera}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                cabecera.Cell($"M{filaCabecera}").Value = factura.VencimientoCodigoAutorizacion.ToString("dd/MM/yyyy");
                cabecera.Cell($"O{filaCabecera}").Value = config.TipoDocumento;
                cabecera.Cell($"P{filaCabecera}").Value = factura.CUIT;
                cabecera.Cell($"X{filaCabecera}").Value = config.TipoMoneda;
                cabecera.Cell($"AA{filaCabecera}").Value = config.Cotizacion;
                cabecera.Cell($"AB{filaCabecera}").Value = config.MedioPago;
                cabecera.Cell($"AF{filaCabecera}").Value = factura.FechaEmision.ToString("dd/MM/yyyy");
                cabecera.Cell($"AG{filaCabecera}").Value = factura.FechaEmision.AddDays(3).ToString("dd/MM/yyyy");
                cabecera.Cell($"AI{filaCabecera}").Value = importeTotalPeriodo;
                cabecera.Cell($"AJ{filaCabecera}").Value = $"SERVICIO DE {factura.TipoServicio} CORRESPONDIENTE A {factura.Empresa}, PARA LOS CLIENTES INSTITUCIONALES, CORRESPONDIENTE AL PERIODO: {factura.Periodo} - IMPORTE: $ {importeTotalPeriodo}";
                filaCabecera++;

                //DETALLE CABECERA
                string[] objetoGastoPartes = factura.ObjetoGasto.Split(".");

                detalle_cabecera.Cell($"A{filaDetalleCabecera}").Value = factura.TipoFactura;
                detalle_cabecera.Cell($"B{filaDetalleCabecera}").Value = Convert.ToInt32(factura.PuntoVenta);
                detalle_cabecera.Cell($"C{filaDetalleCabecera}").Value = Convert.ToInt32(factura.NumeroFactura);
                detalle_cabecera.Cell($"D{filaDetalleCabecera}").Value = config.TipoDocumento;
                detalle_cabecera.Cell($"E{filaDetalleCabecera}").Value = factura.CUIT;
                detalle_cabecera.Cell($"F{filaDetalleCabecera}").Value = factura.TipoCodigoAutorizacion;
                detalle_cabecera.Cell($"G{filaDetalleCabecera}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                detalle_cabecera.Cell($"H{filaDetalleCabecera}").Value = factura.CodigoCatalogo;
                detalle_cabecera.Cell($"J{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[0]);
                detalle_cabecera.Cell($"K{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[1]);
                detalle_cabecera.Cell($"L{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[2]);
                detalle_cabecera.Cell($"M{filaDetalleCabecera}").Value = int.Parse(objetoGastoPartes[3]);
                detalle_cabecera.Cell($"N{filaDetalleCabecera}").Value = config.CantidadUnidades;
                detalle_cabecera.Cell($"P{filaDetalleCabecera}").Value = importeTotalPeriodo;
                filaDetalleCabecera++;

                //DETALLE FINANCIERO
                string[] apertura_programatica = actividadProgramatica.Split(".");
                detalle_financiero.Cell($"A{filaDetalleFinanciero}").Value = factura.TipoFactura;
                detalle_financiero.Cell($"B{filaDetalleFinanciero}").Value = Convert.ToInt32(factura.PuntoVenta);
                detalle_financiero.Cell($"C{filaDetalleFinanciero}").Value = Convert.ToInt32(factura.NumeroFactura);
                detalle_financiero.Cell($"D{filaDetalleFinanciero}").Value = config.TipoDocumento;
                detalle_financiero.Cell($"E{filaDetalleFinanciero}").Value = factura.CUIT;
                detalle_financiero.Cell($"F{filaDetalleFinanciero}").Value = factura.TipoCodigoAutorizacion;
                detalle_financiero.Cell($"G{filaDetalleFinanciero}").Value = Convert.ToInt64(factura.CodigoAutorizacion);
                detalle_financiero.Cell($"J{filaDetalleFinanciero}").Value = config.Jurisdiccion;
                detalle_financiero.Cell($"K{filaDetalleFinanciero}").Value = config.SubJurisdiccion;
                detalle_financiero.Cell($"L{filaDetalleFinanciero}").Value = config.Entidad;
                detalle_financiero.Cell($"M{filaDetalleFinanciero}").Value = config.SAF;
                detalle_financiero.Cell($"N{filaDetalleFinanciero}").Value = dependencia;
                detalle_financiero.Cell($"O{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[0]);
                detalle_financiero.Cell($"P{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[1]);
                detalle_financiero.Cell($"Q{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[2]);
                detalle_financiero.Cell($"R{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[3]);
                detalle_financiero.Cell($"S{filaDetalleFinanciero}").Value = int.Parse(apertura_programatica[4]);
                detalle_financiero.Cell($"T{filaDetalleFinanciero}").Value = uGeografica;
                detalle_financiero.Cell($"U{filaDetalleFinanciero}").Value = int.Parse(objetoGastoPartes[0]);
                detalle_financiero.Cell($"V{filaDetalleFinanciero}").Value = int.Parse(objetoGastoPartes[1]);
                detalle_financiero.Cell($"W{filaDetalleFinanciero}").Value = int.Parse(objetoGastoPartes[2]);
                detalle_financiero.Cell($"X{filaDetalleFinanciero}").Value = int.Parse(objetoGastoPartes[3]);
                detalle_financiero.Cell($"Y{filaDetalleFinanciero}").Value = config.FuenteFinanciamiento;
                detalle_financiero.Cell($"Z{filaDetalleFinanciero}").Value = config.Moneda;
                detalle_financiero.Cell($"AB{filaDetalleFinanciero}").Value = config.PEX;
                detalle_financiero.Cell($"AC{filaDetalleFinanciero}").Value = config.BAPIN;
                detalle_financiero.Cell($"AE{filaDetalleFinanciero}").Value = config.CodigoGanancias;
                detalle_financiero.Cell($"AF{filaDetalleFinanciero}").Value = config.CodigoIVA;
                detalle_financiero.Cell($"AG{filaDetalleFinanciero}").Value = config.CodigoSUSS;
                detalle_financiero.Cell($"AH{filaDetalleFinanciero}").Value = config.PorcentualIVA;
                detalle_financiero.Cell($"AJ{filaDetalleFinanciero}").Value = importeTotalPeriodo;
                filaDetalleFinanciero++;

                importeTotalPeriodo = 0;
                factura = null;
            }

            libro.SaveAs(Path.Combine(desktopPath, $"Facturas{facturasPorPeriodo[0][0].Empresa}_ExportadasUnidifcado_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"));
            MessageBox.Show("LIBRO GUARDADO correctamente");
        }

        public void GenerarInforme(string plantilla, List<Factura> facturas)
        {
            int fila = 2;

            //var facturasPorPeriodo = facturas.GroupBy(f => new{Año = f.FechaVencimiento.Year,f.Periodo}).Select(g => g.OrderBy(f => f.FechaVencimiento).ToList()).ToList();
            List<List<Factura>> facturasPorPeriodo = facturas.GroupBy(f => f.Periodo).Select(g => g.ToList()).ToList();

            foreach (List<Factura> periodo in facturasPorPeriodo)
            {
                XLWorkbook libro = this.abrirPlantilla(obtenerRutaPlantillaInforme(plantilla));
                IXLWorksheet informe = libro.Worksheet("Informe de Pago Realizado");

                List<Factura> facturasFiltadas = periodo;
                //  Factura factura = null;
                facturasFiltadas = facturasFiltadas.OrderBy(f => f.FechaVencimiento).ToList();
                decimal importeTotalPeriodo = 0;

                foreach (Factura facturaPeriodo in facturasFiltadas)
                {
                    long numeroCliente;
                    numeroCliente = long.Parse(facturaPeriodo.NumeroCliente);

                    importeTotalPeriodo += facturaPeriodo.ImporteAbonable;

                    informe.Cell($"A{fila}").Value = numeroCliente;
                    informe.Cell($"B{fila}").Value = $"{facturaPeriodo.PuntoVenta}B{facturaPeriodo.NumeroFactura}";
                    informe.Cell($"C{fila}").Value = facturaPeriodo.FechaVencimiento.ToString("dd/MM/yyyy");
                    informe.Cell($"D{fila}").Value = facturaPeriodo.ImportePrimerVencimiento;
                    informe.Cell($"E{fila}").Value = facturaPeriodo.ImporteSaldoAnterior;
                    informe.Cell($"F{fila}").Value = facturaPeriodo.ImporteAbonable;
                    informe.Cell($"G{fila}").Value = facturaPeriodo.CUIT;
                    informe.Cell($"H{fila}").Value = facturaPeriodo.TipoCodigoAutorizacion;
                    informe.Cell($"I{fila}").Value = facturaPeriodo.CodigoAutorizacion;
                    informe.Cell($"J{fila}").Value = facturaPeriodo.VencimientoCodigoAutorizacion.ToString("dd/MM/yyyy");
                    informe.Cell($"K{fila}").Value = facturaPeriodo.Tarifa;
                    fila++;



                }

                informe.Range($"A{fila}:G{fila}").Merge().Value = "IMPORTE TOTAL";
                informe.Range($"H{fila}:K{fila}").Merge().Value = importeTotalPeriodo;//importeTotal;
                informe.Range($"H{fila}:K{fila}").Style.NumberFormat.Format = "$ #,##0.00";

                informe.Range($"A2:K{fila - 1}").Sort(1, XLSortOrder.Ascending);


                string pathGuardado = Path.Combine(desktopPath, $"{facturasFiltadas[0].FechaVencimiento.ToString("yyyy")}-{DateTime.ParseExact(facturasFiltadas[0].Periodo, "MMMM", new CultureInfo("es-ES")).Month}-Informe de Pago - {facturasFiltadas[0].Empresa} {facturasFiltadas[0].Periodo}.xlsx");


                if (!File.Exists(pathGuardado))
                {
                    libro.SaveAs(pathGuardado);
                    MessageBox.Show($"Informe de {facturasFiltadas[0].Empresa} exportado correctamente. Se ha guardado en {pathGuardado}");
                }
                else
                {
                    string pathGuardado_Error = Path.Combine(desktopPath, $"{facturasFiltadas[0].FechaVencimiento.ToString("yyyy")}-{DateTime.ParseExact(facturasFiltadas[0].Periodo, "MMMM", new CultureInfo("es-ES")).Month}-Informe de Pago - {facturasFiltadas[0].Empresa} {facturasFiltadas[0].Periodo}_Error_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                    libro.SaveAs(pathGuardado_Error);
                    MessageBox.Show($"El informe para el periodo {facturasFiltadas[0].Periodo} ya existe en el escritorio. Se ha guardado, {pathGuardado_Error}  para evitar sobrescribir el archivo existente.");
                }



                fila = 2;
                importeTotalPeriodo = 0;
                facturasFiltadas.Clear();
            }
        }
    }
}
