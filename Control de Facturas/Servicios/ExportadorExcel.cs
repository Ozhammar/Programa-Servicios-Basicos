using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Servicios
{

    internal class ExportadorExcel
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private readonly string rutaPlantilla;
        private ControladorFacturas controladorFacturas;

        public ExportadorExcel()
        {
            //PLANTILLA BASE
            rutaPlantilla = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "PLANTILLA.xlsx");
            controladorFacturas = new ControladorFacturas();
        }

        public XLWorkbook abrirPlantilla()
        {
            try
            {
                XLWorkbook libro = new XLWorkbook(rutaPlantilla);
                return libro;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al abrir la plantilla de Excel: Archivo no encontrado" + ex.Message);
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

        public void generarLiquidacionGeneral(List<Factura> facturasCache)
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
                cabecera.Cell($"AJ{filaCabecera}").Value = $"SERVICIO DE {factura.TipoServicio.ToUpper()} CORRESPONDIENTE A {factura.Empresa.ToUpper()}, CLIENTE: {factura.NumeroCliente} - FACTURA N°: {factura.NumeroFactura} - PERIODO: {factura.Periodo.ToUpper()} - IMPORTE: $ {factura.ImporteAbonable}";
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

            libro.SaveAs(Path.Combine(desktopPath, $"Facturas{facturas[0].Empresa}Exportadas_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"));
            MessageBox.Show("LIBRO GUARDADO correctamente");
        }

        
    }
}