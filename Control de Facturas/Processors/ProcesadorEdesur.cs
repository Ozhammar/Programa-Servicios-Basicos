

namespace Control_de_Facturas.Processors
{
    public class ProcesadorEdesur
    {
        public Factura ProcesarFactura(string textoPDF, string rutaArchivo)
        {
            Factura factura = new Factura();



            factura.Empresa = "Edesur";

            factura.NumeroCliente = ExtraerNumeroCliente(textoPDF);
            factura.NumeroFactura = ExtraerNumeroFactura(textoPDF);
            factura.FechaEmision = ExtraerFechaEmision(textoPDF);
            factura.FechaVencimiento = ExtraerFechaVencimiento(textoPDF);

            factura.ImportePrimerVencimiento = ExtraerImportePrimerVencimiento(textoPDF);

            factura.ImporteSaldoAnterior = ExtraerImporteSaldoAnterior(textoPDF);
            factura.ImporteAbonable = factura.CalcularImporteAbonable();
            factura.CUIT = 30655116512; // CUIT fijo de Edesur
            factura.ObjetoGasto = "3.1.1.0"; // Objeto de gasto fijo para Edesur
            factura.CodigoCatalogo = "3.1.1-2390-1"; // Código de catálogo fijo para Edesur
            factura.CodigoAutorizacion = ExtraerCodigoAutorizacion(textoPDF);
            factura.VencimientoCodigoAutorizacion = ExtraerVencimientoCodigoAutorizacion(textoPDF);
            factura.Archivo = rutaArchivo;
            factura.TipoServicio = "Electricidad";
            factura.Tarifa = ExtraerTarifa(textoPDF);

            return factura;
        }

        private string ExtraerTarifa(string textoPDF)
        {
            string tipoTarifa = "";

            Regex rTipoTarifa = new Regex(@"Tarifa\s*T\s*(\d{1})", RegexOptions.IgnoreCase);
            Match match = rTipoTarifa.Match(textoPDF);

            if (match.Success)
            {
                tipoTarifa = match.Groups[1].Value;
            }
            return tipoTarifa;
        }

        private decimal ExtraerImporteSaldoAnterior(string textoPDF)
        {
            decimal ImporteSaldoAnterior = 0;

            Regex rImporteSaldoAnterior = new Regex(@"Saldo\s*anterior\s*([\d.,]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match match = rImporteSaldoAnterior.Match(textoPDF);

            if (match.Success)
            {
                string valor = match.Groups[1].Value;
                valor = valor.Replace(",", "");
                valor = valor.Replace(".", ",");

                ImporteSaldoAnterior = decimal.Parse(valor, NumberStyles.Number, new CultureInfo("es-AR"));
            }
            return ImporteSaldoAnterior;

        }

        private DateTime ExtraerVencimientoCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"F\.\s*Vto\.\s*CESP:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                new Regex(@"Vto:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase)
            };
            DateTime fechaVencimientoAut = DateTime.MinValue;

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    fechaVencimientoAut = Convert.ToDateTime(match.Groups[1].Value);
                    break;
                }
            }

            return fechaVencimientoAut;
        }

        private string ExtraerCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"C.E.S.P.:\s*(\d{14})", RegexOptions.IgnoreCase),
                new Regex(@"C[óo]digo\s*CESP:\s*(\d{14})", RegexOptions.IgnoreCase)
            };
            string codigoAutorizacion = "";

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    codigoAutorizacion = match.Groups[1].Value;
                    break;
                }
            }
            return codigoAutorizacion;
        }

        private decimal ExtraerImportePrimerVencimiento(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"1\s*\D*\s*Vencimiento.*?TOTAL:\s*\$\s*([\d.,]+)\s*2\s*\D*\s*Vencimiento", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                //new Regex(@"pagar\s*hasta*?\$([\d.,]+)\s*D", RegexOptions.IgnoreCase)
                new Regex(@"\(\*\)\s*([\d.,]+)", RegexOptions.IgnoreCase)
            };
            decimal ImportePrimerVencimiento = 0;

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    string valor = match.Groups[1].Value;
                    valor = valor.Replace(",", "");
                    valor = valor.Replace(".", ",");
                    ImportePrimerVencimiento = decimal.Parse(valor);
                    //, NumberStyles.Number, new CultureInfo("es-AR")
                    break;
                }
            }

            //Regex rImportePrimerVencimiento = new Regex(@"1\s*\D*\s*Vencimiento.*?TOTAL:\s*\$\s*([\d.,]+)\s*2\s*\D*\s*Vencimiento", RegexOptions.IgnoreCase | RegexOptions.Singleline);//@"1°\s*Vencimiento:.*?TOTAL:\s*\$([\d.,]+
            //Match match = rImportePrimerVencimiento.Match(textoPDF);

            //if (match.Success)
            //{
            //    string valor = match.Groups[1].Value;
            //    valor = valor.Replace(",", "");
            //    valor = valor.Replace(".", ",");

            //    ImportePrimerVencimiento = decimal.Parse(valor);
            //    //, NumberStyles.Number, new CultureInfo("es-AR")
            //}
            return ImportePrimerVencimiento;
        }

        private DateTime ExtraerFechaVencimiento(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"1°\s*Vencimiento:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                new Regex(@"liquidación\s*vence\s*el\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase)
            };
            DateTime fechaVencimiento = DateTime.MinValue;


            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    fechaVencimiento = Convert.ToDateTime(match.Groups[1].Value);
                    break;
                }
            }
            return fechaVencimiento;
        }

        private DateTime ExtraerFechaEmision(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Capital\s*Federal\s*(\d{2}/\d{2}/\d{4})\s*C:", RegexOptions.IgnoreCase),
                new Regex(@"Capital\s*Federal\s*(\d{2}/\d{2}/\d{4})\s*T", RegexOptions.IgnoreCase)
            };

            DateTime fechaEmision = DateTime.MinValue;

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    fechaEmision = Convert.ToDateTime(match.Groups[1].Value);
                    break;
                }
            }
            return fechaEmision;
        }

        private string ExtraerNumeroFactura(string textoPDF)
        {
            string numeroFactura = "";

            Regex rNumeroFactura = new Regex(@"Servicios\s*P[úu]blicos\s*\(LSP\)\s*B\s*(\d{4}\-\d{8})", RegexOptions.IgnoreCase);
            Match match = rNumeroFactura.Match(textoPDF);

            if (match.Success)
            {
                numeroFactura = match.Groups[1].Value;
            }
            return numeroFactura;
        }

        private string ExtraerNumeroCliente(string textoPDF)
        {
            // Lógica para extraer el número de cliente del texto del PDF
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Cliente:\s*(\d{8})", RegexOptions.IgnoreCase),
                new Regex(@"N[úu]mero\s*de\s*Cliente\s*es\s*(\d{8})", RegexOptions.IgnoreCase)
            };
            string numeroCliente = "";

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    numeroCliente = match.Groups[1].Value;
                    break;
                }
            }
            return numeroCliente;
        }


    }
}
