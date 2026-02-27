using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Processors
{
    internal class ProcesadorAguaInterior
    {

        private readonly GestorArchivos gestorArchivos;
        private readonly ConvertidorImportes convertidorImportes;
        private readonly BuscadorCUIT buscadorCUIT;

        public ProcesadorAguaInterior()
        {
            gestorArchivos = new GestorArchivos();
            convertidorImportes = new ConvertidorImportes();
            buscadorCUIT = new BuscadorCUIT();
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
            factura.ObjetoGasto = "3.1.2.0"; // Objeto de gasto fijo para AGUA
            factura.CodigoCatalogo = "3.1.2-2391-1"; // Código de catálogo fijo para AGUA
            factura.CodigoAutorizacion = ExtraerCodigoAutorizacion(textoPDF);
            factura.VencimientoCodigoAutorizacion = ExtraerVencimientoCodigoAutorizacion(textoPDF);
            factura.Archivo = gestorArchivos.RenombrarArchivo(rutaArchivo, factura.Empresa, factura.NumeroCliente, factura.PuntoVenta, factura.NumeroFactura);
            factura.TipoServicio = "AGUA";
            //factura.Tarifa = ExtraerTarifa(textoPDF);

            if (factura.CodigoAutorizacion == "")
            {
                factura.TipoCodigoAutorizacion = "NA";
            }
            return factura;
        }

        private string ExtraerEmpresa(string textoPDF)
        {
            string empresa = "";
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"(USHUAIA)", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"(www.osmgp.gov.ar)", RegexOptions.IgnoreCase),//OBRAS SANITARIAS DE MAR DEL PLATA
                new Regex(@"(30 - 63046762 - 0)", RegexOptions.IgnoreCase),//OBRAS SANITARIAS DE MAR DEL PLATA
                new Regex(@"(R[íi]o\s*negro)", RegexOptions.IgnoreCase),//AGUAS RIONEGRINAS
                new Regex(@"(ROSARIO)", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                new Regex(@"(SANTA\s*FE)", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                new Regex(@"(RECONQUISTA)", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                new Regex(@"(Aguas\s*de\s*Corrientes)", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES


            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    empresa = match.Groups[1].Value;
                    switch (empresa.ToUpperInvariant())
                    {
                        case "USHUAIA":
                            {
                                empresa = "DPOSS";
                                break;
                            }
                        case "WWW.OSMGP.GOV.AR":
                            {
                                empresa = "OBRAS SANITARIAS DE MAR DEL PLATA";
                                break;
                            }
                        case "30 - 63046762 - 0":
                            {
                                empresa = "OBRAS SANITARIAS DE MAR DEL PLATA";
                                break;
                            }
                        case "RIO NEGRO":
                            {
                                empresa = "AGUAS RIONEGRINAS";
                                break;
                            }
                        case "ROSARIO":
                            {
                                empresa = "AGUAS SANTAFESINAS";
                                break;
                            }
                        case "SANTA FE":
                            {
                                empresa = "AGUAS SANTAFESINAS";
                                break;
                            }
                        case "RECONQUISTA":
                            {
                                empresa = "AGUAS SANTAFESINAS";
                                break;
                            }
                    }
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
                new Regex(@"TASA(\d{4})", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"Cuenta\s*\:?\s*(\d+)\/", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
                new Regex(@"Cuenta\s*\:?\s*(\d+)\s*\(", RegexOptions.IgnoreCase),//AGUAS RIONEGRINAS
                new Regex(@"Punto\s*Suministro\s*\:?(\d{8})", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                new Regex(@"FC\s*\-\s*\d{4}\s*\-\s*\d{8}(\d{8})", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES

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

                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?(B)""?", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"(B)-\d{1,2}", RegexOptions.IgnoreCase),//obras sanitarias mdp
                new Regex(@"""(B)""", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                 
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
                else
                {
                    //BYPASS PARA OBRAS SANITARIAS DE MAR DEL PLATA, QUE NO TIENE EL TIPO DE FACTURA EXPLICITADO, PERO SI SE LOGRA IDENTIFICAR POR EL NOMBRE DE LA EMPRESA
                    string empresa_buscada = ExtraerEmpresa(textoPDF).ToUpper();
                    bool encontrada = false;
                    string[] empresasSinTipo =
                        {
                        "OBRAS SANITARIAS DE MAR DEL PLATA",
                        "AGUAS RIONEGRINAS"
                        };
                    foreach (string empresa in empresasSinTipo)
                    {
                        if (empresa_buscada == empresa)
                        {
                            encontrada = true;
                            tipoFactura = "B";
                        }
                        ///////////////////////
                        if (encontrada)
                        {
                            break;
                        }
                    }
                }
            }

            return tipoFactura;
        }
        private string ExtraerPuntoVenta(string textoPDF, string empresa)
        {
            string puntoVenta = "0";

            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?B""?\s*(\d{4})\s*-\s*\d{8}", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"\.\.\.\s*\d{2}\s*\/\s*(\d{3})\.", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
                 new Regex(@"Factura\s*\:?\s*(\d+)\s*\-", RegexOptions.IgnoreCase),//AGUAS RIONEGRINAS
                 new Regex(@"""B""\s*(\d{4})-", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                          new Regex(@"FC\s*\-\s*(\d{4})", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES
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

                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?B""?\s*\d{4}\s*-\s*(\d{8})", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"\.\.\.\s*\d{2}\s*\/\s*\d{3}\.?([\d.]+\/\d{2})", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
                new Regex(@"Factura\s*\:?\s*\d+\s*\-[\s\S]+\-(\d+)\s*C", RegexOptions.IgnoreCase),//AGUAS RIONEGRINAS
                new Regex(@"""B""\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                         new Regex(@"FC\s*\-\s*\d{4}\s*\-\s*(\d{8})", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES

              
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

                    if (numeroFactura.Contains("/") || numeroFactura.Contains("."))
                    {
                        numeroFactura = numeroFactura.Replace("/", "");
                        numeroFactura = numeroFactura.Replace(".", "");
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
                new Regex(@"\d{4}\s*-\s*\d{8}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"Fecha\s*Emisi[óo]n\s*\:?(\d+/\d+/\d{4})", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
                new Regex(@"Fecha\s*Emisi[óo]n\s*\:?(\d+/\d+/\d{2})", RegexOptions.IgnoreCase),//AGUAS RIONEGRINAS
                new Regex(@"(\d{2}/\d{2}/\d{4})\s*Concepto", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                new Regex(@"FC\s*\-\s*\d{4}\s*\-\s*\d{16}(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES
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
                new Regex(@"POLIC[ÍI]a\s*FEDERAL\s*Argentina\s*\d{4}(\d{2}\s*/\s*\d{2}\s*/\s*\d{4})\s*\$", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"Vencimiento\s*\:?\s*(\d+\s*/\s*\d+\s*/\s*\d{4})\s*Clave", RegexOptions.IgnoreCase),//
                new Regex(@"Vencimiento\s*\:?\s*(\d+\s*/\s*\d+\s*/\s*\d{4})\s*", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MAR DEL PLATA
                 new Regex(@"(\d{2}/\d{2}/\d{4})\s*[\d.,]+\s*FC", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES
            };
            DateTime fechaVencimiento = DateTime.MinValue;

            if (ExtraerEmpresa(textoPDF).ToUpper() == "AGUAS SANTAFESINAS")
            {
                Regex patron = new Regex(@"(\d{2}/\d{2}/\d{4})\$\*+[\d.,]+PAGO", RegexOptions.IgnoreCase);//AGUAS SANTAFESINAS 

                Match match = patron.Match(textoPDF);

                while (match.Success)
                {
                    fechaVencimiento = Convert.ToDateTime(match.Groups[1].Value);

                    match = match.NextMatch();
                }
            }
            else
            {
                foreach (Regex regex in patrones)
                {
                    Match match = regex.Match(textoPDF);
                    if (match.Success)
                    {
                        fechaVencimiento = Convert.ToDateTime(match.Groups[1].Value);
                        break;
                    }
                }
            }
            return fechaVencimiento;
        }
        private string ExtraerPeriodo(string textoPDF)
        {
            string periodo = "";
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"\d{2}\s*/\s*\d{2}\s*/\s*\d{4}\s*(\d{2}\/\d{2})\s*Categor[íi]a", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"Per[íi]odo\s*\:?\s*(\d+/\d{4})", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
                new Regex(@"(\d{4}/\d{2})\s*IC", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                new Regex(@"((ENE|FEB|MAR|ABR|MAY|JUN|JUL|AGO|SEP|OCT|NOV|DIC)-\d{4})", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);

                if (match.Success)
                {
                    try
                    {
                        DateTime fecha = Convert.ToDateTime(match.Groups[1].Value);
                        periodo = fecha.ToString("MMMM").ToUpper();
                        break;
                    }
                    catch
                    {
                        periodo = match.Groups[1].Value;
                        break;
                    }
                }
            }
            return periodo;
        }
        private decimal ExtraerImportePrimerVencimiento(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"\d{2}/\d{2}/\d{4}\s*\$\s*([\d.,]+)\s*LIQUIDACI[ÓO]N", RegexOptions.IgnoreCase),//DPOSS
                new Regex(@"Total\s*a\s*Pagar\s*\$\s*([\d.,]+)", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
                new Regex(@"Anterior\s*\:?\s*([\d.]+)\s*Factura", RegexOptions.IgnoreCase),//AGUAS RIONEGRINAS
                new Regex(@"([\d.,]+)\s*[úu]ltimos", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                new Regex(@"\d{2}/\d{2}/\d{4}\s*([\d.,]+)\s*FC", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES
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
                 new Regex(@"C\s*\.?\s*U\s*\.?\s*I\s*\.?\s*T\.?\s*\:?\s*\s*N?[º°]?\s*(\d{2}-\d{8}-\d{1})", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
          
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
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*(\d{15})", RegexOptions.IgnoreCase),
                new Regex(@"C\.?E\.?S\.?P\.?[\s\S]+(\d{14})Vto\:?\.?", RegexOptions.IgnoreCase),
                new Regex(@"C\.E\.S\.P\.?:?\s*N[º°]\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//REDENGAS
                new Regex(@"C\.E\.S\.P\.?:?\s*Nro\s*(\d{14})", RegexOptions.IgnoreCase),//ecogas
                new Regex(@"C\.E\.S\.P\:?\s*(\d{14})", RegexOptions.IgnoreCase),
                new Regex(@"C\.E\.S\.P\.:?\s*N[º°]?\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//naturgy noa
                new Regex(@"CESP\s*N[º°]?\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//gasnea
                new Regex(@"C\.E\.S\.P\.:?\s*Nro\.\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//LITORAL GAS
                new Regex(@"""B""\s*\d{4}\s*-\s*\d{8}\s*(\d{14})", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                new Regex(@"FC\s*\-\s*\d{4}\s*\-\s*\d{16}\d{2}/\d{2}/\d{4}(\d{14})", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES

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
            if (codigoAutorizacion == "")
            {
            }
            return codigoAutorizacion;
        }
        private DateTime ExtraerVencimientoCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {

                 new Regex(@"C\.?E\.?S\.?P\.?[\s\S]+\d{14}Vto\.?\s*C\.?E\.?S\.?P\.?\:?\s*(\d{2}/\d{2}/\d{2})", RegexOptions.IgnoreCase),//DPOSS

                   new Regex(@"C\.E\.S\.P\.?\:?\s*N[º°]:?\s*\d{15}\s*Vencimiento:?(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                        new Regex(@"C\.?E\.?S\.?P\.?[\s\S]+\d{14}Vto\:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//CAMUZZI SUR
                   new Regex(@"C\.E\.S\.P\.?\:?\s*N[º°]:?\s*\d{14}\s*Fecha\s*Vto.?:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//DISTRIGAS
                      new Regex(@"Vto\.?\s*C\.?E\.?S\.?P\.?:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
                   new Regex(@"Fecha\s*de\s*Vto\.?\:?\s*(\d{2}/\d+/\d{4})", RegexOptions.IgnoreCase),//naturgy noa - naturgy ban
                   new Regex(@"Nro\.\s*:?\s*\d{14}F\.?Vto\.?\:?\s*(\d{2}/\d+/\d{4})", RegexOptions.IgnoreCase),//LITORAL GAS
                   new Regex(@"""B""\s*\d{4}\s*-\s*\d{8}\s*\d{14}(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//AGUAS SANTAFESINAS
                            new Regex(@"FC\s*\-\s*\d{4}\s*\-\s*\d{16}\d{2}/\d{2}/\d{4}\d{14}(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//AGUAS DE CORRIENTES

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

