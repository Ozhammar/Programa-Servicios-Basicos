using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Processors
{
    public class ProcesadorGasInterior
    {

        private readonly GestorArchivos gestorArchivos;
        private readonly ConvertidorImportes convertidorImportes;
        private readonly BuscadorCUIT buscadorCUIT;
        private readonly ControladorCamuzzi controladorCamuzzi;

        public ProcesadorGasInterior()
        {
            gestorArchivos = new GestorArchivos();
            convertidorImportes = new ConvertidorImportes();
            buscadorCUIT = new BuscadorCUIT();
            controladorCamuzzi = new ControladorCamuzzi();
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
                new Regex(@"(REDENGAS)", RegexOptions.IgnoreCase),//REDENGAS S.A.
                new Regex(@"www\.(naturgynoa)\.com\.ar", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"www.(naturgy).com.ar", RegexOptions.IgnoreCase),//naturgy ban
                new Regex(@"(LitoralGas)", RegexOptions.IgnoreCase),//litoral gas
                new Regex(@"www.(gasnea).com.ar", RegexOptions.IgnoreCase),//gasnea
                new Regex(@"www.(gasjunin).com.ar", RegexOptions.IgnoreCase),//gasJUNIN
                new Regex(@"(DISTRIBUIDORA DE GAS DEL CENTRO)", RegexOptions.IgnoreCase),//gasJUNIN
                new Regex(@"(DISTRIGAS)", RegexOptions.IgnoreCase),//distrigas
                
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    empresa = match.Groups[1].Value;
                    switch (empresa.ToUpperInvariant())
                    {
                        case "NATURGY":
                            {
                                empresa = "NATURGYBAN";
                                break;
                            }
                        case "DISTRIBUIDORA DE GAS DEL CENTRO":
                            {
                                empresa = "ECOGAS";
                                break;
                            }
                    }

                    break;
                }
            }

            if (controladorCamuzzi.ControlarCamuzziSur(textoPDF))
            {
                empresa = "CAMUZZI GAS DEL SUR";
            }

            if (controladorCamuzzi.ControlarCamuzziPampeana(textoPDF))
            {
                empresa = "CAMUZZI PAMPEANA";
            }



            return empresa;
        }
        private string ExtraerNumeroCliente(string textoPDF)
        {

            // Lógica para extraer el número de cliente del texto del PDF
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"N[º°]\s*Cliente:?\s*(\d{9})", RegexOptions.IgnoreCase),//distrigas
                new Regex(@"N[ÚU]MERO\s*DE\s*CUENTA\s*:?\s*(\d{8})", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"Exento\s*(\d{6})-", RegexOptions.IgnoreCase),//gasjunin
                new Regex(@"\d{2}\s*-\s*(\d{8})-\s*\d{2}", RegexOptions.IgnoreCase),//redengas
                new Regex(@"Cup[óo]n\:?\s*\d{2}\s*-\s*\d{4}\s*-\s*\d{8}(\d+)", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"(\d{17})\s*Capital\s*Federal", RegexOptions.IgnoreCase),//camuzzi sur
                new Regex(@"Total\s*a\s*pagar\s*(\d+)", RegexOptions.IgnoreCase),//naturgy ban
                new Regex(@"\d{2}\s*\/\s*\d{4}\s*(\d{10})", RegexOptions.IgnoreCase),//LITORAL GAS
                new Regex(@"BANELCO\s*Pagos\:?\s*\d{9}\s*(\d{8})\d+C[oó]digo", RegexOptions.IgnoreCase),//gasnea

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
                new Regex(@"COD#(\d{2})", RegexOptions.IgnoreCase),//camuzzi sur
                 new Regex(@"cod\s*\(?\d+\)?\s*:\s*([O0-9\-]+)", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"Liquidaci[óo]n\s*Clase\s*(B)", RegexOptions.IgnoreCase),//redengas
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?(B)""?", RegexOptions.IgnoreCase),//naturgy noa
                 new Regex(@"(B)\d{4}-\d{8}", RegexOptions.IgnoreCase),//gasjunin
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*Clase\s*(B)", RegexOptions.IgnoreCase),//naturgy noa
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    tipoFactura = match.Groups[1].Value;
                    //BYPASS PARA FACTURA A 
                    if (tipoFactura == "A" || tipoFactura == "18")
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
            string puntoVenta = "0";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"(\d{5})\s*-", RegexOptions.IgnoreCase),//CAMUZZISUR
                new Regex(@"Factura\s*N[º°]\s*(\d{4})-", RegexOptions.IgnoreCase),//DISTRIGAS
                new Regex(@"cod\s*\(?\d+\)?\s*:\s*([O0-9\-]+)", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"\d{2}\s*/\s*\d{2}\s*/\s*\d{2}\s*(\d{5})\s*\d+\s*\-\s*\'", RegexOptions.IgnoreCase),//naturgy ban
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*B\s*N[º°]\s*(\d{4})\s*-", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"(\d{4})\s*-\s*\d{10}", RegexOptions.IgnoreCase),//LITORAL GAS
                new Regex(@"\,\d{2}\s*(\d{4})-", RegexOptions.IgnoreCase),//gasnea

            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    puntoVenta = match.Groups[1].Value;
                    if (puntoVenta.Contains("o") || puntoVenta.Contains("O"))
                    {
                        string[] puntoVenta_partes;
                        puntoVenta = puntoVenta.ToUpper();
                        puntoVenta = puntoVenta.Replace("O", "0");

                        puntoVenta_partes = puntoVenta.Split("-");
                        puntoVenta = puntoVenta_partes[0];
                    }
                    break;
                }
            }

            return puntoVenta;
        }
        private string ExtraerNumeroFactura(string textoPDF)
        {
            string numeroFactura = "";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"\d{5}\s*-(\d{8}\/\d{1})", RegexOptions.IgnoreCase),//CAMUZZISUR
                new Regex(@"Factura\s*N[º°]\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase),//DISTRIGAS
                new Regex(@"cod\s*\(?\d+\)?\s*:\s*([O0-9\-]+)", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"Factura\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase),//gasjunin
                  new Regex(@"\,\d{2}\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase),//gasnea
                new Regex(@"FCB\s*(\d{8})", RegexOptions.IgnoreCase),//redengas
                
                new Regex(@"N[º°]\s*\d{4}\s*-\s*(\d{8})", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"\d{2}\s*/\s*\d{2}\s*/\s*\d{2}\s*\d{5}\s*(\d{8})18\s*\-\s*\'\s*Liquidaci[oó]n", RegexOptions.IgnoreCase),//naturgy ban
                new Regex(@"\d{4}\s*-\s*(\d{8})", RegexOptions.IgnoreCase),//LITORAL GAS
              
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    numeroFactura = match.Groups[1].Value;
                    if (numeroFactura.Contains("o") || numeroFactura.Contains("O"))
                    {
                        string[] numeroFactura_partes;
                        numeroFactura = numeroFactura.ToUpper();
                        numeroFactura = numeroFactura.Replace("O", "0");

                        numeroFactura_partes = numeroFactura.Split("-");
                        numeroFactura = numeroFactura_partes[1];
                    }

                    if (numeroFactura.Contains("/"))
                    {
                        numeroFactura = numeroFactura.Replace("/", "-"); 
                    }
                    break;
                }
            }
            return numeroFactura;
        }
        private DateTime ExtraerFechaEmision(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"\d{5}\s*-\d{8}\/\d{1}\s*(\d{2}\s*/\s*\d{2}\s*/\s*\d{4})", RegexOptions.IgnoreCase),//CAMUZZISUR
                new Regex(@"Fecha\s*de\s*Emisi[oó]n\s*:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                new Regex(@"−\s*(\d{2}/\d{2}/\d{4})\s*N[ÚU]MERO\s*DE\s*CUENTA", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"\d{4}\/\d{2}-\d(\d{2}/\d{2}/\d{4})\s*Ruta", RegexOptions.IgnoreCase),//naturgy noagasjunin
                new Regex(@"FCB\s*\d{8}\s*\d{4}/\d{2}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                new Regex(@"Fecha\s*de\s*Emisi[óo]n\s*(\d{2}\s*/\s*\d{2}\s*/\s*\d{4})", RegexOptions.IgnoreCase),
                new Regex(@"(\d{2}\s*/\s*\d{2}\s*/\s*\d{2})\s*\d{2}/\d{2}\-", RegexOptions.IgnoreCase),//naturgy ban
                       new Regex(@"\d{4}\s*-\s*\d{8}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//LITORAL GAS
            };

            DateTime fechaEmision = DateTime.MinValue;

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    string fechaStr = match.Groups[1].Value;// Eliminar espacios
                    if (fechaStr.Contains(" "))
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
                new Regex(@"PAGAR\?\$[\s\S]+,\d{2}(\d{2}\s*/\s*\d{2}\s*/\s*\d{4})", RegexOptions.IgnoreCase),//CAMUZZISUR
                new Regex(@"(\d{2}/\d{2}/\d{4})\s*Factura\s*N[º°]\s*\d{4}-\d{8}", RegexOptions.IgnoreCase),//DISTRIGAS
                new Regex(@"hasta\s*el\s*(\d{2}/\d{2}/\d{4})\$", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"Vto\s*\.?:?\s*(\d/\d{2}/\d{4})", RegexOptions.IgnoreCase),//gasjunin

                new Regex(@"Fecha\s*Vto\s*:?(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"Mensual\s*(\d{2}\s*/\s*\d{2}\s*/\s*\d{2})", RegexOptions.IgnoreCase),//naturgy ban
                new Regex(@"TOTAL\s*A\s*PAGAR\s*hasta\s*el\s*(\d{2}\s*/\s*\d{2}\s*/\s*\d{2})", RegexOptions.IgnoreCase),//litoral gas
                new Regex(@"\d{46}(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//gasnea
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
                 new Regex(@"(\d{2}\s*/\s*\d{2}\s*/\s*\d{4})\s*sgp", RegexOptions.IgnoreCase),//CAMUZZISUR
                new Regex(@"Ciclo:?\s*(\d{2}/\d{4})", RegexOptions.IgnoreCase),//DISTRIGAS
                    new Regex(@"Per[íi]odo\s*de\s*consumo\s*(\d{4}/\d{2})", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"Mensual\s*\d{2}\/(\d{2}/\d{4})", RegexOptions.IgnoreCase),//gasjunin
                new Regex(@"Per[íi]odo\s*:?\s*(\d+\s*/\s*\d{4})", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"FCB\s*\d{8}\s*(\d{4}/\d{2})", RegexOptions.IgnoreCase),//redengas
                new Regex(@"\d{2}\s*/\s*\d{2}\s*/\s*\d{2}\s*(\d{2}/\d{2})\-", RegexOptions.IgnoreCase),//naturgy ban
                new Regex(@"(\d{2}\s*\/\s*\d{4})\s*\d{10}", RegexOptions.IgnoreCase),//LITORAL GAS
                new Regex(@"Mensual\s*(\d{4}\s*\/\s*\d{1})", RegexOptions.IgnoreCase),//GASNEA


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
                new Regex(@"\d/\d{2}/\d{4}\s*([\d.,]+)\s*Total\s*a\s*pagar", RegexOptions.IgnoreCase),//gasjunin
                new Regex(@"\d{2}/\d{2}/\d{4}\s*(\d+\.\d{2})\s*C", RegexOptions.IgnoreCase),             //REDENGAS
                new Regex(@"Importe\s*Total\s*:?\s*\$\s*([\d.,]+)", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"Total\s*a\s*pagar[\s\S]*?\d{2}/\d{2}/\d{4}\s*\$\s*([\d.,]+)", RegexOptions.IgnoreCase),//gasnea
                new Regex(@"Total\s*a\s*pagar[\s\S]*?\$\s*([\d.,]+)", RegexOptions.IgnoreCase),//naturgy ban
                new Regex(@"\d{2}/\d{2}/\d{4}\s*([\d.,]+)<", RegexOptions.IgnoreCase),//litoral gas
            };
            decimal ImportePrimerVencimiento = 0;

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    string valor = match.Groups[1].Value;
                    ImportePrimerVencimiento = convertidorImportes.ParseImporteFlexible(valor);
                    break;
                }
            }
            return ImportePrimerVencimiento;
        }
        private long ExtraerCUIT(string textoPDF)
        {
            string CUIT = "";
            long cuitLong = 0;
            List<Regex> patrones = new List<Regex>
            {
                 new Regex(@"C\s*\.\s*U\s*\.\s*I\s*\.\s*T\.\s*\:?\s*(\d{2}-\d{8}-\d{1})", RegexOptions.IgnoreCase),//redengas
                new Regex(@"(\d{2}-\d{8}-\d{1})\s*C\s*\.\s*U\s*\.\s*I\s*\.\s*T\.\s*\:?", RegexOptions.IgnoreCase),//naturgynoa
                new Regex(@"CUIT\s*(\d{2}-\d{8}-\d{1})", RegexOptions.IgnoreCase),//ecogas

                 //Per[íi]odo\s*de\s*facturaci[oó]n\s*cargos\s*fijos\s*\d{2}/\d{2}/\d{4}\s*AL\s*(\d{2}/\d{2}/\d{4})
            };

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
                else
                {
                    string CUIT_buscado = buscadorCUIT.BuscarCUIT(ExtraerEmpresa(textoPDF).ToUpper().Trim());
                    cuitLong = long.Parse(CUIT_buscado);
                    break;
                }
            }
            return cuitLong;
        }
        private string ExtraerCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"C\.E\.S\.P\.?:?\s*N[º°]\s*:?\s*(\d{15})", RegexOptions.IgnoreCase),//gasjunin
                new Regex(@"C\.E\.S\.P\.[\s\S]+(\d{14})Vto\:", RegexOptions.IgnoreCase),//CAMUZZI SUR

                new Regex(@"C\.E\.S\.P\.?:?\s*N[º°]\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//REDENGAS
                new Regex(@"C\.E\.S\.P\.?:?\s*Nro\s*(\d{14})", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"C\.E\.S\.P\:?\s*(\d{14})", RegexOptions.IgnoreCase),
                new Regex(@"C\.E\.S\.P\.:?\s*N[º°]?\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//naturgy noa
                    new Regex(@"CESP\s*N[º°]?\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//gasnea
                new Regex(@"C\.E\.S\.P\.:?\s*Nro\.\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//LITORAL GAS
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

                   new Regex(@"C\.E\.S\.P\.?\:?\s*N[º°]:?\s*\d{15}\s*Vencimiento:?(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                        new Regex(@"C\.E\.S\.P\.[\s\S]+\d{14}Vto\:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//CAMUZZI SUR
                   new Regex(@"C\.E\.S\.P\.?\:?\s*N[º°]:?\s*\d{14}\s*Fecha\s*Vto.?:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//DISTRIGAS
                      new Regex(@"Vto\.?\s*C\.E\.S\.P\.:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                   new Regex(@"Fecha\s*de\s*Vto\.?\:?\s*(\d{2}/\d+/\d{4})", RegexOptions.IgnoreCase),//naturgy noa - naturgy ban
                   new Regex(@"Nro\.\s*:?\s*\d{14}F\.?Vto\.?\:?\s*(\d{2}/\d+/\d{4})", RegexOptions.IgnoreCase),//LITORAL GAS

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