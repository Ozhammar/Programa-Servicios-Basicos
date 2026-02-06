using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Processors
{
    public class ProcesadorGasInterior
    {

        private readonly GestorArchivos gestorArchivos;

        public ProcesadorGasInterior()
        {
            gestorArchivos = new GestorArchivos();
        }

        public Factura ProcesarFactura(string textoPDF, string rutaArchivo)
        {
            Factura factura = new Factura();

            factura.Empresa = ExtraerEmpresa(textoPDF).ToUpper();
            factura.NumeroCliente = ExtraerNumeroCliente(textoPDF);
            factura.TipoFactura = ExtraerTipoFactura(textoPDF);
            factura.PuntoVenta = ExtraerPuntoVenta(textoPDF, factura.Empresa);
            factura.NumeroFactura = ExtraerNumeroFactura(textoPDF);
            factura.FechaEmision = ExtraerFechaEmision(textoPDF);
            factura.FechaVencimiento = ExtraerFechaVencimiento(textoPDF);
            factura.Periodo = ExtraerPeriodo(textoPDF);
            factura.ImportePrimerVencimiento = ExtraerImportePrimerVencimiento(textoPDF);
            factura.ImporteSaldoAnterior = 0;//ExtraerImporteSaldoAnterior(textoPDF);
            factura.ImporteAbonable = factura.ImportePrimerVencimiento;//factura.CalcularImporteAbonable();
            factura.CUIT = ExtraerCUIT(textoPDF);
            factura.ObjetoGasto = "3.1.3.0"; // Objeto de gasto fijo para GAS
            factura.CodigoCatalogo = "3.1.3-2392-1"; // Código de catálogo fijo para GAS
            factura.CodigoAutorizacion = ExtraerCodigoAutorizacion(textoPDF);
            factura.VencimientoCodigoAutorizacion = ExtraerVencimientoCodigoAutorizacion(textoPDF);
            factura.Archivo = gestorArchivos.RenombrarArchivo(rutaArchivo, factura.Empresa, factura.NumeroCliente, factura.PuntoVenta, factura.NumeroFactura);
            factura.TipoServicio = "GAS";
            //factura.Tarifa = ExtraerTarifa(textoPDF);

            return factura;
        }

        private string ExtraerEmpresa(string textoPDF)
        {
            string empresa = "";
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"(REDENGAS\s*S\s*\.\s*A\s*\.)", RegexOptions.IgnoreCase),//REDENGAS S.A.
                new Regex(@"www\.(naturgynoa)\.com\.ar", RegexOptions.IgnoreCase),//naturgy noa
                
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    empresa = match.Groups[1].Value;
                    break;
                }
            }

            return empresa;
        }
        private string ExtraerNumeroCliente(string textoPDF)
        {

            // Lógica para extraer el número de cliente del texto del PDF
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"\d{2}\s*-\s*(\d{8})-\s*\d{2}", RegexOptions.IgnoreCase),//redengas
                new Regex(@"Cup[óo]n\:?\s*\d{2}\s*-\s*\d{4}\s*-\s*\d{8}(\d+)", RegexOptions.IgnoreCase),//naturgy noa

                //CuentadeServicios
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
        private string ExtraerTipoFactura(string textoPDF)
        {
            string tipoFactura = "";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Liquidaci[óo]n\s*Clase\s*(B)", RegexOptions.IgnoreCase),//redengas
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*(B)", RegexOptions.IgnoreCase),//naturgy noa
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
        private string ExtraerPuntoVenta(string textoPDF, string empresa)
        {
            string puntoVenta = "";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*B\s*N[º°]\s(\d{4})\s*-", RegexOptions.IgnoreCase)//naturgy noa

            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {

                    puntoVenta = match.Groups[1].Value;
                   /* if(puntoVenta == "")
                    {
                        puntoVenta = "0";
                    }
                    break;*/
                } else
                {
                    puntoVenta = "0";
                }
            }
            
            return puntoVenta;
        }
        private string ExtraerNumeroFactura(string textoPDF)
        {
            string numeroFactura = "";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"FCB\s*(\d{8})", RegexOptions.IgnoreCase),//redengas
                new Regex(@"N[º°]\s*\d{4}\s*-\s*(\d{8})", RegexOptions.IgnoreCase),//naturgy noa
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
        private DateTime ExtraerFechaEmision(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"FCB\s*\d{8}\s*\d{4}/\d{2}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                new Regex(@"Fecha\s*de\s*Emisi[óo]n\s*(\d{2}\s*/\s*\d{2}\s*/\s*\d{4})", RegexOptions.IgnoreCase),
            };

            DateTime fechaEmision = DateTime.MinValue;

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    string fechaStr = match.Groups[1].Value;             // Eliminar espacios
                    if(fechaStr.Contains(" "))
                    {
                        fechaStr = fechaStr.Replace(" ", "");
                        fechaEmision = Convert.ToDateTime(fechaStr);
                        break;
                    }

                    fechaEmision = Convert.ToDateTime(match.Groups[1].Value);
                    break;
                }
            }
            return fechaEmision;
        }
        private DateTime ExtraerFechaVencimiento(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Fecha\s*Vto\s*:?(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"(\d{2}/\d{2}/\d{4})\d+\.\d{2}", RegexOptions.IgnoreCase),//REDENGAS
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
        private string ExtraerPeriodo(string textoPDF)
        {
            string periodo = "";
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Per[íi]odo\s*:?\s*(\d+\s*/\s*\d{4})", RegexOptions.IgnoreCase),//naturgy noa
                 new Regex(@"FCB\s*\d{8}\s*(\d{4}/\d{2})", RegexOptions.IgnoreCase),//redengas

                 //Per[íi]odo\s*de\s*facturaci[oó]n\s*cargos\s*fijos\s*\d{2}/\d{2}/\d{4}\s*AL\s*(\d{2}/\d{2}/\d{4})
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
   
                if (match.Success)
                {
                    //POSIBLE CODIGO PARA REEMPLAZAR ESPACIOS EN PERIODO Y CONVERTIRLO A FECHA PARA EXTRAER EL MES
                    /* string periodoStr = match.Groups[1].Value;
                     if (periodoStr.Contains(" "))
                     {
                         periodoStr = periodoStr.Replace(" ", "");
                         DateTime fecha_nueva = Convert.ToDateTime(periodoStr);
                         periodo = fecha_nueva.ToString("MMMM").ToUpper();
                         break;
                     }*/

                    DateTime fecha = Convert.ToDateTime(match.Groups[1].Value);
                    periodo = fecha.ToString("MMMM").ToUpper();
                    break;
                }
            }
            return periodo;
        }
        private decimal ExtraerImportePrimerVencimiento(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"\d{2}/\d{2}/\d{4}\s*(\d+\.\d{2})\s*C", RegexOptions.IgnoreCase),             //REDENGAS
                new Regex(@"Importe\s*Total\s*:?\s*\$\s*([\d.,]+)", RegexOptions.IgnoreCase),//naturgy noa
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
            return ImportePrimerVencimiento;
        }
        private long ExtraerCUIT(string textoPDF)
        {
            string CUIT = "";
            List<Regex> patrones = new List<Regex>
            {
                 new Regex(@"C\s*\.\s*U\s*\.\s*I\s*\.\s*T\.\s*\:?\s*(\d{2}-\d{8}-\d{1})", RegexOptions.IgnoreCase),//redengas
                new Regex(@"(\d{2}-\d{8}-\d{1})\s*C\s*\.\s*U\s*\.\s*I\s*\.\s*T\.\s*\:?", RegexOptions.IgnoreCase),//naturgynoa

                 //Per[íi]odo\s*de\s*facturaci[oó]n\s*cargos\s*fijos\s*\d{2}/\d{2}/\d{4}\s*AL\s*(\d{2}/\d{2}/\d{4})
            };
            long cuitLong = 0;
            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    CUIT = match.Groups[1].Value;
                    if (CUIT.Contains("-"))
                    {
                        CUIT = CUIT.Replace("-", "");
                    }
                    cuitLong = long.Parse(CUIT);
                    break;
                }
            }
            return cuitLong;
        }
        private string ExtraerCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"C\.E\.S\.P\.:?\s*N[º°]\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//REDENGAS
                new Regex(@"C\.E\.S\.P\:?\s*(\d{14})", RegexOptions.IgnoreCase),
                new Regex(@"C\.E\.S\.P\.:?\s*N[º°]?\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//naturgy noa
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
        private DateTime ExtraerVencimientoCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {

                   new Regex(@"Vto\.?\s*C\.E\.S\.P\.:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                   new Regex(@"Fecha\s*de\s*Vto\:?\s*(\d{2}/\d+/\d{4})", RegexOptions.IgnoreCase),//naturgy noa

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
        /*private string ExtraerTarifa(string textoPDF)
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
        }*/

        /*private decimal ExtraerImporteSaldoAnterior(string textoPDF)
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

        }*/
    }
}