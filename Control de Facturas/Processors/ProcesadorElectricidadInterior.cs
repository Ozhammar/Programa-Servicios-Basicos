namespace Control_de_Facturas.Processors
{
    internal class ProcesadorElectricidadInterior
    {

        private readonly GestorArchivos gestorArchivos;
        private readonly ConvertidorImportes convertidorImportes;
        private readonly BuscadorCUIT buscadorCUIT;
        private readonly ControladorEdesal controladorEdesal;

        public ProcesadorElectricidadInterior()
        {
            gestorArchivos = new GestorArchivos();
            convertidorImportes = new ConvertidorImportes();
            buscadorCUIT = new BuscadorCUIT();
            controladorEdesal = new ControladorEdesal();
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
            factura.ObjetoGasto = "3.1.1.0"; // Objeto de gasto fijo para ELECTRICIDAD
            factura.CodigoCatalogo = "3.1.1-2390-1"; // Código de catálogo fijo para ELECTRICIDAD
            factura.CodigoAutorizacion = ExtraerCodigoAutorizacion(textoPDF);
            factura.VencimientoCodigoAutorizacion = ExtraerVencimientoCodigoAutorizacion(textoPDF);
            factura.Archivo = gestorArchivos.RenombrarArchivo(rutaArchivo, factura.Empresa, factura.NumeroCliente, factura.PuntoVenta, factura.NumeroFactura);
            factura.TipoServicio = "ELECTRICIDAD INTERIOR";

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
                new Regex(@"(30−99902748−9)", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"(30-99902748-9)", RegexOptions.IgnoreCase),//EPEC -> DISTINTO TIPO DE CARACTER EN LOS GUIONES
                new Regex(@"(EDEA)", RegexOptions.IgnoreCase),//EDEA


            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    empresa = match.Groups[1].Value;
                    switch (empresa.ToUpperInvariant())
                    {
                        case "30−99902748−9":
                            {
                                empresa = "EPEC";
                                break;
                            }
                        case "30-99902748-9":
                            {
                                empresa = "EPEC";
                                break;
                            }
                    }
                    break;
                }

            }

            if (controladorEdesal.ControlarEdesal(textoPDF))
            {
                empresa = "EDESAL";
            }
            return empresa;
        }
        private string ExtraerNumeroCliente(string textoPDF)
        {
            // Lógica para extraer el número de cliente del texto del PDF
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"BANELCO\s*(\d{8})", RegexOptions.IgnoreCase),//EPEC -->minoritarias
                
                new Regex(@"(\d{7})\s*Liq\.\s*Serv\.", RegexOptions.IgnoreCase),//EDESAL
               new Regex(@"(\d{8})\s*\/\s*\d{2}", RegexOptions.IgnoreCase),//EPEC
               new Regex(@"BANELCO\s*(\d{9})", RegexOptions.IgnoreCase),//EDEA


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

                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?(B)""?", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"(B)\s*\(18\)", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"Liq\.\s*Serv\.\s*P[úu]b\.\s*""?(B)""?", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"FACTURA\s+([A-Z])", RegexOptions.IgnoreCase),//EDEA



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
                 new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?B""?\s*[\s\S]+(\d{5})\s*\−", RegexOptions.IgnoreCase),//EPEC
                 new Regex(@"B\s*\(18\)\s*N[º°]\s*(\d{5})-", RegexOptions.IgnoreCase),//EPEC
                 new Regex(@"Liq\.\s*Serv\.\s*P[úu]b\.\s*""?B""?\s*\(18\)\s*N[º°]\s*(\d{4})\s*\-", RegexOptions.IgnoreCase),//EDESAL


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
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?B""?\s*[\s\S]+\d{5}\s*\−\s*(\d{8})\s*P", RegexOptions.IgnoreCase),//EPEC -> no es el comprobante interno
                new Regex(@"B\s*\(18\)\s*N[º°]\s*\d{5}-(\d{8})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"Liq\.\s*Serv\.\s*P[úu]b\.\s*""?B""?\s*\(18\)\s*N[º°]\s*\d{4}\s*\-\s*(\d{8})", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"N°\s*Factura\s*(\d{8})", RegexOptions.IgnoreCase),//EDEA

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
                new Regex(@"Emisi[oó]n\s*\:?[\s\S]+(\d{2}\/\d{2}\/\d{4})\s*Imprime", RegexOptions.IgnoreCase),//EPEC
                  new Regex(@"C[óo]rdoba\s*(\d{2}\/\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EPEC
                  new Regex(@"\d{2}\/\d{4}\s*(\d{2}\/\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EDESAL
                  new Regex(@"Fecha\s*Emisión\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDEA
 
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
                new Regex(@"TOTAL\s*A\s*Pagar\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*\d{14}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"(\d{2}\/\d{2}\/\d{4})[\s\S]+\s*62405191", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"Fecha\s*Vencimiento\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDEA

            };
            DateTime fechaVencimiento = DateTime.MinValue;

            /*  if (ExtraerEmpresa(textoPDF).ToUpper() == "AGUAS SANTAFESINAS")
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
              {*/
            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    fechaVencimiento = Convert.ToDateTime(match.Groups[1].Value);
                    break;
                }
            }
            //}
            return fechaVencimiento;
        }
        private string ExtraerPeriodo(string textoPDF)
        {
            string periodo = "";
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"F\s*\d{4}\−\s*\d{8}[\s\S]+(\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"(\d{2}\/\d{4})\s*N[º°]\s*F", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"(\d{2}\/\d{4})\s*\d{2}\/\d{2}\/\d{4}", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"Periodo\s*(\d{2}/\d{2})", RegexOptions.IgnoreCase),//EDEA
 
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);

                if (match.Success)
                {
                    try
                    {
                        DateTime fecha = Convert.ToDateTime(match.Groups[1].Value);
                        periodo = fecha.ToString("MM/yy").ToUpper();
                        break;
                    }
                    catch
                    {
                        periodo = match.Groups[1].Value;
                        if (periodo.Contains("al"))
                        {
                            periodo = periodo.Replace("al", "-");
                            periodo = periodo.Replace(" ", "");
                        }
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
                new Regex(@"Total\s*a\s*Pagar\s*\$\s*([\d.,]+)", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
                new Regex(@"F\s*\d{4}\−\s*\d{8}([\d.,]+)\d{2}\/\d{4}", RegexOptions.IgnoreCase),//EPEC
                    new Regex(@"\d{2}/\d{2}/\d{4}\s*\$\s*([\d.,]+)\s*\$", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"\$\s*([\d.,]+)\s*\*", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"Importe\s*Primer\s*Vencimiento\s*([\d\.]+,\d{2})", RegexOptions.IgnoreCase),//EDEA
                
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
                 new Regex(@"(30−99902748−9)", RegexOptions.IgnoreCase),//EPEC
                 new Regex(@"C\s*\.?\s*U\s*\.?\s*I\s*\.?\s*T\.?\s*\:?\s*\s*N?[º°]?\s*(\d{2}-\d{8}-\d{1})", RegexOptions.IgnoreCase),//OBRAS SANITARIAS MDP
             
          
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    CUIT = match.Groups[1].Value;
                    if (CUIT.Contains("-") || CUIT.Contains("−"))
                    {
                        CUIT = CUIT.Replace("-", ""); //GUION
                        CUIT = CUIT.Replace("−", ""); //SIGNO MENOS
                    }
                    cuitLong = long.Parse(CUIT);
                    break;
                }
            }

            if (cuitLong == 0)
            {
                string CUIT_buscado = buscadorCUIT.BuscarCUIT(ExtraerEmpresa(textoPDF).ToUpper().Trim());
                cuitLong = long.Parse(CUIT_buscado);
            }
            return cuitLong;
        }
        private string ExtraerCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"C\.?E\.?S\.?P\.?[\s\S]+(\d{14})Vto\:?\.?", RegexOptions.IgnoreCase),
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//EPEC


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
               new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*\d{14}\s*Fecha\s*de\s*Vto\.?\s*C\.?E\.?S\.?P\.?(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EPEC
               new Regex(@"Fecha\s*de\s*Vto\.?\s*C\.?E\.?S\.?P\.?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EPEC
               new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*\d{14}\s*Fecha\s*de\s*Vto\.?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDESAL


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
    }
}