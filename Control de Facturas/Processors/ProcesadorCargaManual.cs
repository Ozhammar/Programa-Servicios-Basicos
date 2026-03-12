namespace Control_de_Facturas.Processors
{
    internal class ProcesadorCargaManual
    {
        private enum TipoServicio 
        {
            ELECTRICIDAD, AGUA, GAS
        }

        Dictionary<TipoServicio, DatosCatalogo>

        /*
            factura.ObjetoGasto = "3.1.2.0"; // Objeto de gasto fijo para AGUA
            factura.CodigoCatalogo = "3.1.2-2391-1"; // Código de catálogo fijo para AGUA
            factura.ObjetoGasto = "3.1.3.0"; // Objeto de gasto fijo para GAS
            factura.CodigoCatalogo = "3.1.3-2392-1"; // Código de catálogo fijo para GAS
            factura.ObjetoGasto = "3.1.1.0"; // Objeto de gasto fijo para Edesur
            factura.CodigoCatalogo = "3.1.1-2390-1"; // Código de catálogo fijo para Edesur

         
         */
        public Factura ProcesarFactura(DatosFactura datos)
        {
            Factura factura = new Factura();

            factura.Empresa = datos.Denominacion;
            factura.NumeroCliente = datos.Cliente;
            factura.TipoFactura = datos.TipoFactura;
            factura.PuntoVenta = datos.PuntoVenta;
            factura.NumeroFactura = datos.NumeroFactura;
            factura.FechaEmision = datos.FechaEmision;
            factura.FechaVencimiento = datos.FechaVencimientoFC;
            factura.Periodo = datos.Periodo;
            factura.ImportePrimerVencimiento = datos.Importe;
            factura.CUIT = datos.CUIT;
            //factura.ObjetoGasto =
            //factura.CodigoCatalogo =
            factura.TipoCodigoAutorizacion = datos.TipoCodAut;
            factura.CodigoAutorizacion = datos.CodigoAut;
            factura.VencimientoCodigoAutorizacion = datos.VencimientoCodAut;
            //factura.Archivo =
            factura.TipoServicio = datos.TipoServicio;
            //factura.Tarifa

            return factura;
        }


    }
}
