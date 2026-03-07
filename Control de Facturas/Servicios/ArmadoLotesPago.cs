using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Servicios
{
    internal class ArmadoLotesPago
    {

        public List<LotesPago> armarLotesPago(List<Factura> facturas, string encabezado = "", int maximo_caracteres = 250)
        {
            List<LotesPago> lotesPago = new List<LotesPago>();
            StringBuilder sb = new StringBuilder();

            Factura primerfactura = null;
            decimal importeTotal = 0;
            decimal importeParcial = 0;
            string clienteActual = null;

            sb.AppendLine(encabezado);
            Factura facturaInicial = facturas[0];
            foreach (Factura factura in facturas)
            {
                string observacion_cliente = $"C{factura.NumeroCliente}";
                string observacion_factura = $"F{factura.PuntoVenta}-{factura.NumeroFactura} P{factura.Periodo} ${factura.ImporteAbonable}";

                bool entra = (sb.Length + observacion_cliente.Length + observacion_factura.Length) < maximo_caracteres;

                //COMPROBACION DE TAMAÑO PARA REALIZAR CORTE DEL LOTE
                if (!entra)
                {
                    lotesPago.Add(new LotesPago
                    {
                        Observacion = sb.ToString().TrimEnd(),
                        Importe = importeParcial,
                        PrimerFactura = facturaInicial,
                    });

                    sb.Clear();
                    sb.AppendLine("SERVICIO AGUA INTERIOR");
                    importeParcial = 0;
                    clienteActual = null;
                    primerfactura = null;
                }

                if (clienteActual != factura.NumeroCliente)
                {
                    sb.AppendLine(observacion_cliente);
                    clienteActual = factura.NumeroCliente;
                }

                if (primerfactura == null)
                {
                    primerfactura = factura;
                }

                sb.AppendLine(observacion_factura);
                importeParcial += factura.ImporteAbonable;
            }

            lotesPago.Add(new LotesPago
            {
                Observacion = sb.ToString().TrimEnd(),
                Importe = importeParcial,
                PrimerFactura = primerfactura
            });

            return lotesPago;
        }
    }
}
