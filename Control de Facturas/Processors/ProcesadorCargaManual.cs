namespace Control_de_Facturas.Processors
{
    internal class ProcesadorCargaManual
    {
        private enum TipoServicio
        {
            ELECTRICIDAD, AGUA, GAS
        }
        private readonly Dictionary<TipoServicio, DatosCatalogo> CatalogoServicios;
        public ProcesadorCargaManual()
        {
            CatalogoServicios = new Dictionary<TipoServicio, DatosCatalogo>()
            {
                {
                    TipoServicio.ELECTRICIDAD, new DatosCatalogo
                    {
                        ObjetoGasto = "3.1.1.0", CodigoCatalogo ="3.1.1-2390-1"
                    }
                },
                {
                    TipoServicio.AGUA, new DatosCatalogo
                    {
                        ObjetoGasto = "3.1.2.0", CodigoCatalogo ="3.1.2-2391-1"
                    }
                },
                {
                    TipoServicio.GAS, new DatosCatalogo
                    {
                        ObjetoGasto = "3.1.3.0", CodigoCatalogo ="3.1.3-2392-1"
                    }
                },
            };
        }

        public Factura ProcesarFactura(DatosFactura datos)
        {
            Factura factura = new Factura();
            try
            {
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
                factura.TipoCodigoAutorizacion = datos.TipoCodAut;
                factura.CodigoAutorizacion = datos.CodigoAut;
                factura.VencimientoCodigoAutorizacion = datos.VencimientoCodAut;
                //factura.Archivo =
                factura.TipoServicio = datos.TipoServicio;
                //factura.Tarifa

                var datosCatalogo = BuscarDatosCatalogo(datos);
                factura.ObjetoGasto = datosCatalogo.ObjetoGasto;
                factura.CodigoCatalogo = datosCatalogo.CodigoCatalogo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontró el servicio en el catálogo.");
            }

            return factura;
        }

        private DatosCatalogo BuscarDatosCatalogo(DatosFactura datos)
        {
            try
            {
                string datosTipoServicio = datos.TipoServicio;
                if (datosTipoServicio.Contains("INTERIOR"))
                {
                    datosTipoServicio = datosTipoServicio.Replace(" INTERIOR", "").Trim();
                }
                if (Enum.TryParse<TipoServicio>(datosTipoServicio, out var tipoServicio))
                {
                   
                    if (CatalogoServicios.TryGetValue(tipoServicio, out var datosCatalogo))
                    {
                        return datosCatalogo;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encontró el servicio en el catálogo.");
            }
            return null;
        }


    }
}
