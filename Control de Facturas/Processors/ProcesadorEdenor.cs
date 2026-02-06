using Control_de_Facturas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Processors
{
    public class ProcesadorEdenor
    {
        private readonly GestorArchivos gestorArchivos;

        public ProcesadorEdenor()
        {
            gestorArchivos = new GestorArchivos();
        }

        public Factura ProcesarFactura(string textoPDF, string rutaArchivo)
        {
            Factura factura = new Factura();

            factura.Empresa = "EDENOR";
            factura.NumeroCliente = ExtraerNumeroCliente(textoPDF);
            factura.TipoFactura = ExtraerTipoFactura(textoPDF);
            factura.PuntoVenta = ExtraerPuntoVenta(textoPDF);
            factura.NumeroFactura = ExtraerNumeroFactura(textoPDF);
            factura.FechaEmision = ExtraerFechaEmision(textoPDF);
            factura.FechaVencimiento = ExtraerFechaVencimiento(textoPDF);
            factura.Periodo = ExtraerPeriodo(textoPDF);
            factura.ImportePrimerVencimiento = ExtraerImportePrimerVencimiento(textoPDF);
            factura.ImporteSaldoAnterior = ExtraerImporteSaldoAnterior(textoPDF);
            factura.ImporteAbonable = factura.CalcularImporteAbonable();
            factura.CUIT = 30655116202; // CUIT fijo de EDENOR
            factura.ObjetoGasto = "3.1.1.0"; // Objeto de gasto fijo para EDENOR
            factura.CodigoCatalogo = "3.1.1-2390-1"; // Código de catálogo fijo para EDENOR
            factura.CodigoAutorizacion = ExtraerCodigoAutorizacion(textoPDF);
            factura.VencimientoCodigoAutorizacion = ExtraerVencimientoCodigoAutorizacion(textoPDF);
            factura.Archivo = gestorArchivos.RenombrarArchivo(rutaArchivo, factura.Empresa, factura.NumeroCliente,factura.PuntoVenta, factura.NumeroFactura);
            factura.TipoServicio = "ELECTRICIDAD";
            factura.Tarifa = ExtraerTarifa(textoPDF);

            return factura;
        }


        private string ExtraerPeriodo(string textoPDF)
        {
            string periodo = "";
            List<Regex> patrones = new List<Regex>
            {
                 new Regex(@"Per[íi]odo\s*de\s*consumo\s*\:\d{2}/\d{2}/\d{4}\s*AL\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                 new Regex(@"Per[íi]odo\s*\:\s*(\d{2}/\d{4})", RegexOptions.IgnoreCase),
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    DateTime fecha = Convert.ToDateTime(match.Groups[1].Value);
                    periodo = fecha.ToString("MMMM").ToUpper();
                }
            }
            return periodo;
        }

        private string ExtraerTarifa(string textoPDF)
        {
            string tipoTarifa = "";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Tarifa\s*T\s*(\d{1})", RegexOptions.IgnoreCase),
                new Regex(@"Tarifa\s*\:\s*T\s*(\d{1})", RegexOptions.IgnoreCase)
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    tipoTarifa = match.Groups[1].Value;
                    break;
                }
            }
            return tipoTarifa;
        }

        private decimal ExtraerImporteSaldoAnterior(string textoPDF)
        {
            decimal ImporteSaldoAnterior = 0;

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Saldo\s*anterior\s*\$\s*([\d.,]+-)", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                new Regex(@"Saldo\s*anterior\s*\$\s*(-[\d.,]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                new Regex(@"Saldo\s*anterior\s*\$\s*([\d.,]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                new Regex(@"Saldos\s*anteriores\s*([\d.,]+-)", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                new Regex(@"Saldos\s*anteriores\s*(-[\d.,]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline),
                new Regex(@"Saldos\s*anteriores\s*([\d.,]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline)
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    string valor = match.Groups[1].Value;
                    //  valor = valor.Replace(",", "");
                    //  valor = valor.Replace(".", ",");
                    ImporteSaldoAnterior = decimal.Parse(valor, NumberStyles.Number, new CultureInfo("es-AR"));
                    break;
                }
            }
            return ImporteSaldoAnterior;

        }

        private DateTime ExtraerVencimientoCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                   new Regex(@"C\.E\.S\.P\.\s*N[º°]\s*\d{14}\s*Fecha\s*de\s*Vencimiento\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                   new Regex(@"C\.E\.S\.P\.\s*N[º°]\s*\d{14}\s*Fecha\s*de\s*Vto\s*\.\s*\:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                   new Regex(@"C\.E\.S\.P\s*N[º°]\s*\d{14}\s*Fecha\s*de\s*Vencimiento\s*\:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase)
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
                new Regex(@"C\.E\.S\.P\.\s*N[º°]\s*(\d{14})", RegexOptions.IgnoreCase),
                new Regex(@"C\.E\.S\.P\s*N[º°]\s*(\d{14})", RegexOptions.IgnoreCase)
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
                new Regex(@"Total\s*a\s*pagar\s*\$\s*([\d.,]+)", RegexOptions.IgnoreCase),
                new Regex(@"([\d.,]+)\s*Por\s*pago\s*posterior", RegexOptions.IgnoreCase)

            };
            decimal ImportePrimerVencimiento = 0;

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    string valor = match.Groups[1].Value;
                    //valor = valor.Replace(",", "");
                    // valor = valor.Replace(".", ",");
                    ImportePrimerVencimiento = decimal.Parse(valor);
                    //, NumberStyles.Number, new CultureInfo("es-AR")
                    break;
                }
            }
            return ImportePrimerVencimiento;
        }

        private DateTime ExtraerFechaVencimiento(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Hasta\s*el\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                new Regex(@"Vencimiento\s*\:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
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
                new Regex(@"Emisi[óo]n:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
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

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicio\s*P[úu]blico\s*N°\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase)
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    numeroFactura = match.Groups[1].Value;
                    break;
                }
            }
            return numeroFactura;
        }
        private string ExtraerPuntoVenta(string textoPDF)
        {
            string puntoVenta = "";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicio\s*P[úu]blico\s*N°\s*(\d{4})", RegexOptions.IgnoreCase)

            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {

                    puntoVenta = match.Groups[1].Value;

                    break;
                }
            }
            return puntoVenta;
        }

        private string ExtraerTipoFactura(string textoPDF)
        {
            string tipoFactura = "";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"(B)\s*Liquidaci[óo]n\s*de\s*Servicio\s*P[úu]blico\s*N°", RegexOptions.IgnoreCase),
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    tipoFactura = match.Groups[1].Value;
                    //BYPASS PARA FACTURA A 
                    if (tipoFactura == "A")
                    {
                        tipoFactura = "B";
                    }
                    ///////////////////////
                    break;
                }
            }

            return tipoFactura;
        }
        private string ExtraerNumeroCliente(string textoPDF)
        {
            // Lógica para extraer el número de cliente del texto del PDF
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Cuenta\s*N[º°]\s*((?:\d\s*){10})", RegexOptions.IgnoreCase),
            };
            string numeroCliente = "";

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    numeroCliente = Regex.Replace(match.Groups[1].Value,@"\s+", "");
                    break;
                }
            }
            return numeroCliente;
        }
    }


}
