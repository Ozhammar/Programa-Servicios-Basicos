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
                new Regex(@"(30-65865024-2)", RegexOptions.IgnoreCase),//EDET
                new Regex(@"(30-69383434-8)", RegexOptions.IgnoreCase),//EDEN
                new Regex(@"(30-54578816-7)", RegexOptions.IgnoreCase),//EPE
                new Regex(@"(30-65787766-9)", RegexOptions.IgnoreCase),//EDELAP
                new Regex(@"(33-67509874-9)", RegexOptions.IgnoreCase),//EDESE
                new Regex(@"(30-57190936-3)", RegexOptions.IgnoreCase),//DPE USHUAIA
                new Regex(@"(EDESA)\s*SA", RegexOptions.IgnoreCase),//EDESA
                new Regex(@"WWW\.(EDEMSA)\.COM", RegexOptions.IgnoreCase),//EDEMSA
                new Regex(@"(LA\s*ENERG[ÍI]A\s*DE\s*NUESTRA\s*GENTE)", RegexOptions.IgnoreCase),//EDERSA
                



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
                        case "30-65865024-2":
                            {
                                empresa = "EDET";
                                break;
                            }
                        case "30-69383434-8":
                            {
                                empresa = "EDEN";
                                break;
                            }
                        case "30-54578816-7":
                            {
                                empresa = "EPE";
                                break;
                            }
                        case "30-65787766-9":
                            {
                                empresa = "EDELAP";
                                break;
                            }
                        case "33-67509874-9":
                            {
                                empresa = "EDESE";
                                break;
                            }
                        case "LA ENERGÍA DE NUESTRA GENTE":
                            {
                                empresa = "EDERSA";
                                break;
                            }
                        case "30-57190936-3":
                            {
                                empresa = "DPE TIERRA DEL FUEGO";
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
               new Regex(@"B-\d{4}-\d{8}\s*\d{2}/\d{2}/\d{2}\s*[\d.,]+\s*(\d{11})", RegexOptions.IgnoreCase),//EDERSA
               new Regex(@"Usuario\s*\:?\s*(\d{3}-\d{3}-\d{3})", RegexOptions.IgnoreCase),//DPE TIERRA DEL FUEGO
               new Regex(@"BANELCO\s*(\d{8})", RegexOptions.IgnoreCase),//EPEC -->minoritarias
               new Regex(@"Exento\s*\d{12}\s*(\d{7})\d{2}", RegexOptions.IgnoreCase),//EDEN
               new Regex(@"SERVICIO\s*(\d+)\s*Vencimiento", RegexOptions.IgnoreCase),//EDET
               new Regex(@"Cod\.\s*018(\d{7})", RegexOptions.IgnoreCase),//EDEMSA
               new Regex(@"N[úu]mero\s*de\s*Cliente\s*\:?\s*(\d{9})", RegexOptions.IgnoreCase),//EPE
               new Regex(@"\,\s*\d{2}(\d{9})", RegexOptions.IgnoreCase),//EDELAP
               new Regex(@"(\d{7})\s*Liq\.\s*Serv\.", RegexOptions.IgnoreCase),//EDESAL
               new Regex(@"(\d{14})\s*OriginalBCod\.", RegexOptions.IgnoreCase),//EDESE
               new Regex(@"(\d{8})\s*\/\s*\d{2}", RegexOptions.IgnoreCase),//EPEC
               new Regex(@"Cuenta\s*\d{2}\s*-\s*(\d{7})", RegexOptions.IgnoreCase),//EDEA

            };
            string numeroCliente = "";

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    if (match.Groups[1].Value.Contains("-"))
                    {
                        numeroCliente = match.Groups[1].Value.TrimStart('0').Replace("-", "");
                    } else
                    {
                        numeroCliente = match.Groups[1].Value.TrimStart('0');
                    }
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
                new Regex(@"Liquidaci[óo]n\s*Servicios\s*P[úu]blicos\s*-?\s*?""?(B)""?", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"18\s*\((B)\)", RegexOptions.IgnoreCase),//EDESA
                new Regex(@"Original(B)Cod\.", RegexOptions.IgnoreCase),//EDESE
                 new Regex(@"(18)\s*Liq\.?\s*de\s*Serv\.?\s*P[úu]blicos", RegexOptions.IgnoreCase),//EPE
                new Regex(@"\d{4}\s*\-\d{8}\s*(B)", RegexOptions.IgnoreCase),//EDEN
                new Regex(@"(B)\s*\(18\)", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"Liq\.\s*Serv\.\s*P[úu]b\.\s*""?(B)""?", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"(B)Cod\.\s*018\d{7}", RegexOptions.IgnoreCase),//EDEMSA
                new Regex(@"Serv\.\s*P[úu]b\.\s*(B)-", RegexOptions.IgnoreCase),//EDERSA
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
                new Regex(@"Energ[íi]a\s*B\s*-\s*(\d{4})", RegexOptions.IgnoreCase),//DPE TF
                 new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?B""?\s*[\s\S]+(\d{5})\s*\−", RegexOptions.IgnoreCase),//EPEC
                 new Regex(@"Liq\s*\.?\s*de\s*Serv\s*\.?\s*P[úu]blicos\s*N[º°]\s*\:?\s*(\d{5})-", RegexOptions.IgnoreCase),//EPE
                 new Regex(@"Factura\s*N[º°]\s*\:?\s*(\d{4})-", RegexOptions.IgnoreCase),//EDEMSA
                 new Regex(@"18\s*\(B\)\s*Nro\.(\d{4})-", RegexOptions.IgnoreCase),//EDESA
                 new Regex(@"Serv\.\s*P[úu]b\.\s*B-(\d{4})", RegexOptions.IgnoreCase),//EDERSA
                 new Regex(@"OriginalBCod\.18No\.(\d{5})-", RegexOptions.IgnoreCase),//EDESE
                 new Regex(@"(\d{4})\s*\-\d{8}\s*B", RegexOptions.IgnoreCase),//EDEN
                 new Regex(@"B\s*\(18\)\s*N[º°]\s*(\d{5})-", RegexOptions.IgnoreCase),//EPEC
                 new Regex(@"Liq\.\s*Serv\.\s*P[úu]b\.\s*""?B""?\s*\(18\)\s*N[º°]\s*(\d{4})\s*\-", RegexOptions.IgnoreCase),//EDESAL
                 new Regex(@"Factura\s*Nro\s*(\d{4})-", RegexOptions.IgnoreCase),//EDET
                 



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
                new Regex(@"Energ[íi]a\s*B\s*-\s*\d{4}\s*-\s*(\d{8})", RegexOptions.IgnoreCase),//DPE TF
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*-?\s*?""?B""?\s*[\s\S]+\d{5}\s*\−\s*(\d{8})\s*P", RegexOptions.IgnoreCase),//EPEC -> no es el comprobante interno
                new Regex(@"Liq\.?\s*de\s*Serv\.?\s*P[úu]blicos\s*N[º°]\s*\:?\s*\d{5}-(\d{8})", RegexOptions.IgnoreCase),//EPE
                new Regex(@"Serv\.\s*P[úu]b\.\s*B-\d{4}-(\d{8})", RegexOptions.IgnoreCase),//EDERSA
                new Regex(@"Factura\s*N[º°]\s*\:?\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase),//EDEMSA
                new Regex(@"18\s*\(B\)\s*Nro\.\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase),//EDESA
                new Regex(@"OriginalBCod\.18No\.\d{5}-(\d{8})", RegexOptions.IgnoreCase),//EDESE
                new Regex(@"\d{4}\s*\-(\d{8})\s*B", RegexOptions.IgnoreCase),//EDEN
                new Regex(@"B\s*\(18\)\s*N[º°]\s*\d{5}-(\d{8})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"Liq\.\s*Serv\.\s*P[úu]b\.\s*""?B""?\s*\(18\)\s*N[º°]\s*\d{4}\s*\-\s*(\d{8})", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"Factura\s*(\d{8})", RegexOptions.IgnoreCase),//EDEA
                new Regex(@"Factura\s*Nro\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase),//EDET

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
                new Regex(@"Fecha\s*de\s*Emisi[óo]n\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDEA
                new Regex(@"Fecha\s*(\d{2}/\d{2}/\d{4})\s*\d{4}", RegexOptions.IgnoreCase),//DPE TF
                new Regex(@"Vencimiento\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDET
                new Regex(@"Emisi[oó]n\s*\:?[\s\S]+(\d{2}\/\d{2}\/\d{4})\s*Imprime", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"GENTE\s*(\d{2}\/\d{2}\/\d{2})", RegexOptions.IgnoreCase),//EDERSA
                new Regex(@"Vto\.\s*[\s\S]+?\s*(\d{2}/\d{2}/\d{2})\D{1}", RegexOptions.IgnoreCase),//EDESE
                new Regex(@"Emisi[oó]n\s*\:?\s*(\d{2}\/\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EPE
                new Regex(@"C[óo]rdoba\s*(\d{2}\/\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"\d{4}\s*\-?\s*\d{8}\s*\d{2}\/\d{2}\/\d{4}\s*\d{2}\/\d{2}\/\d{4}\s*(\d{2}\/\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EDEN
                new Regex(@"\d{2}\/\d{4}\s*(\d{2}\/\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EDESAL
                
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
                new Regex(@"\d{4}\-?\-?\d{8}\s*(\d{2}/\d{2}/\d{4})\s*Total\s*Factura", RegexOptions.IgnoreCase),//EDET
                new Regex(@"C\s*\d{2}/\d{2}/\d{4}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDELAP
                new Regex(@"Vence\s*El\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//DPE TF
                new Regex(@"\d{4}\s*\-?\s*\d{8}\s*\d{2}\/\d{2}\/\d{4}\s*(\d{2}\/\d{2}\/\d{4})\s*\d{2}\/\d{2}\/\d{4}", RegexOptions.IgnoreCase),//EDEN
                new Regex(@"B-\d{4}-\d{8}\s*(\d{2}/\d{2}/\d{2})", RegexOptions.IgnoreCase),//EDERSA
                new Regex(@"(\d{2}/\d{2}/\d{2})\s*Per\:", RegexOptions.IgnoreCase),//EDESE
                new Regex(@"\(A\)\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDEMSA
                new Regex(@"2\s*Fecha\s*\:?\s*(\d{2}\/\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EPE
                new Regex(@"TOTAL\s*A\s*Pagar\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*\d{14}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"Vencimiento\s*\:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDEA
                new Regex(@"(\d{2}\/\d{2}\/\d{4})[\s\S]+\s*62405191", RegexOptions.IgnoreCase),//EDESAL
                

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
                new Regex(@"CICLO\s*\:?\s*(20\d{4})", RegexOptions.IgnoreCase),//EDELAP
                new Regex(@"Per[íi]odo\s*FacturaCI[ÓO]n\s*\:?\s*(\d{2}\/\d{4})", RegexOptions.IgnoreCase), //DPE TF
                new Regex(@"facturaci[óo]n.*?(20\d{4})", RegexOptions.IgnoreCase), //EDEN
                new Regex(@"(\d{2}\/\d{4})\s*Liquidaci[óo]n", RegexOptions.IgnoreCase), //EDERSA
                new Regex(@"Vencimiento\s*\d{2}/\d{2}/\d{4}\s*(\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EDET
                new Regex(@"Per[íi]odo\s*Facturado\s*\:?\s*BIM\s*(\d{2}\/\d{4})", RegexOptions.IgnoreCase),//EDEMSA
                new Regex(@"\d{2}/\d{2}/\d{2}\s*Per\:\s*(\d{2}\/\d{2})", RegexOptions.IgnoreCase),//EDESE
                new Regex(@"Per[íi]odo\s*[\s\S]+\s*(\d{2}\/\d{2})\s*T", RegexOptions.IgnoreCase),//EPE
                new Regex(@"(\d{2}\/\d{4})\s*N[º°]\s*F", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"(\d{2}\/\d{4})\s*\d{2}\/\d{2}\/\d{4}", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"Per[íi]odo\s*(\d{2}\/\d{2})", RegexOptions.IgnoreCase),//EDEA
                
 
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

                        if (periodo.Length == 6)
                        {
                            string anio = periodo.Substring(0, 4);
                            string mes = periodo.Substring(4, 2);
                            periodo = mes + "-" + anio.Substring(2, 2);
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
                new Regex(@"Ciclo\s*\:?\s*\d{6}([\d.,]+)", RegexOptions.IgnoreCase),//EDELAP
                new Regex(@"F\s*\d{4}\−\s*\d{8}([\d.,]+)\d{2}\/\d{4}", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"\d{2}/\d{2}/\d{4}\s*\$\s*([\d.,]+)\s*\$", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"\$\s*([\d.,]+)\s*\*", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"T\s*o\s*t\s*a\s*l\s*\:?\s*([\d.,]+)", RegexOptions.IgnoreCase),//EDESA
                new Regex(@"Total\s*\$\s*([\d.,]+)", RegexOptions.IgnoreCase),//EDEMSA
                new Regex(@"Total\s*A\s*Pagar\s*:?\s*\$?\**\s*([\d\.]+,\d{2})", RegexOptions.IgnoreCase), //EDEA
                new Regex(@"Total\s*Factura\s*\**\s*([\d\.]+,\d{2})", RegexOptions.IgnoreCase), //EDESE
                new Regex(@"Servicios\s*gENERALES\s*([\d\.]+,\d{2})", RegexOptions.IgnoreCase), //EDEN
                
            };
            decimal ImportePrimerVencimiento = 0;
            if (textoPDF.Contains("30-54578816-7"))
            {
                Regex patrones_EPE = new Regex(@"Cuota\s*(\d+)[\s\S]+?Importe\s*Total\s*:\s*\$?\s*([\d.,]+)", RegexOptions.IgnoreCase);

                decimal importe_parcial = 0;

                foreach (Match match in patrones_EPE.Matches(textoPDF))
                {
                    if (decimal.TryParse(match.Groups[2].Value, out decimal valor))
                    {
                        importe_parcial += valor;
                    }
                }
                ImportePrimerVencimiento = importe_parcial;
                importe_parcial = 0;
            }
            else
            {
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

            try
            {
                if (cuitLong == 0 || cuitLong == 30624051919)
                {
                    string CUIT_buscado = buscadorCUIT.BuscarCUIT(ExtraerEmpresa(textoPDF).ToUpper().Trim());
                    cuitLong = long.Parse(CUIT_buscado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return cuitLong;
        }
        private string ExtraerCodigoAutorizacion(string textoPDF)
        {
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"C\.?E\.?S\.?P\.?[\s\S]+(\d{14})Vto\:?\.?", RegexOptions.IgnoreCase),
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*(\d{14})", RegexOptions.IgnoreCase),//EPEC
                new Regex(@"\d{4}\s*\-\d{8}\s*B18\s*(\d{14})", RegexOptions.IgnoreCase),//EDEN
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*Vto\.?\:?\s*(\d{14})", RegexOptions.IgnoreCase),//EDELAP
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*(\d{15})", RegexOptions.IgnoreCase),//EDESA
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*(\d{14})", RegexOptions.IgnoreCase),//EDERSA
                new Regex(@"(\d{14})\s*Tal[óo]n[\s\S]+CESP:", RegexOptions.IgnoreCase),//DPE TF


            };
            string codigoAutorizacion = "";

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    codigoAutorizacion = match.Groups[1].Value.TrimStart('0');
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
                new Regex(@"Vto\.?\s*C\.?E\.?S\.?P\.?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDEA
                new Regex(@"Cesp\:\s*\d{14}\s*Vto\.\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDESE
               new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*\d{14}\s*Fecha\s*de\s*Vto\.?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDESAL
                new Regex(@"C\.?E\.?S\.?P\.?:?\s*N[º°]\s*:?\s*\d{14}\s*Fecha\s*de\s*Vto\:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EPE
               new Regex(@"\d{4}\s*\-\d{8}\s*B18\s*\d{14}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDEN
               new Regex(@"C\.?E\.?S\.?P\.?:?\s*Vto\.?\:?\s*\d{14}\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDELAP
               new Regex(@"C\.?E\.?S\.?P\.?:?\s*\d{15}\s*Vto\.?\:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDESA
               new Regex(@"Fecha\s*Vto\.?\s*C\.?E\.?S\.?P\.?:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//EDEMSA
               new Regex(@"C\.?E\.?S\.?P\.?:?\s*\d{14}\s*Vto\.?\:?\s*(\d{2}/\d{2}/\d{2})", RegexOptions.IgnoreCase),//EDERSA
               new Regex(@"CESP:\s*-\s*Vto\.?\:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),//DPE TF


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