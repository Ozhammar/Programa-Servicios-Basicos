namespace Control_de_Facturas.Processors
{
    internal class ProcesadorCargaManual
    {
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
