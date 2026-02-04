using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Processors
{
    internal class ProcesadorMetrogasGrandes
    {
        private readonly GestorArchivos gestorArchivos;

        public ProcesadorMetrogasGrandes()
        {
            gestorArchivos = new GestorArchivos();
        }

        public Factura ProcesarFactura(string textoPDF, string rutaArchivo)
        {
            Factura factura = new Factura();

            factura.Empresa = "METROGAS GRANDES CLIENTES";//
            factura.NumeroCliente = ExtraerNumeroCliente(textoPDF);//
            factura.TipoFactura = ExtraerTipoFactura(textoPDF);//
            factura.PuntoVenta = ExtraerPuntoVenta(textoPDF);//
            factura.NumeroFactura = ExtraerNumeroFactura(textoPDF);//
            factura.FechaEmision = ExtraerFechaEmision(textoPDF);//
            factura.FechaVencimiento = ExtraerFechaVencimiento(textoPDF);//
            //REFACTORIZAR EVENTUALMENTE
            factura.Periodo = ExtraerFechaVencimiento(textoPDF).ToString("MMMM").ToUpper(); //ExtraerPeriodo(textoPDF);
            factura.ImportePrimerVencimiento = ExtraerImportePrimerVencimiento(textoPDF);
            factura.ImporteSaldoAnterior = 0; // ExtraerImporteSaldoAnterior(textoPDF);
            factura.ImporteAbonable = factura.CalcularImporteAbonable();
            factura.CUIT = 30657863676; // CUIT fijo de METROGAS
            factura.ObjetoGasto = "3.1.3.0"; // Objeto de gasto fijo para METROGAS
            factura.CodigoCatalogo = "3.1.3-2392-1"; // Código de catálogo fijo para METROGAS
            factura.CodigoAutorizacion = ExtraerCodigoAutorizacion(textoPDF);
            factura.VencimientoCodigoAutorizacion = ExtraerVencimientoCodigoAutorizacion(textoPDF);
            factura.Archivo = gestorArchivos.RenombrarArchivo(rutaArchivo, factura.NumeroCliente, factura.PuntoVenta, factura.NumeroFactura);
            factura.TipoServicio = "GAS";
            factura.Tarifa = ExtraerTarifa(textoPDF);

            return factura;
        }


        /*private string ExtraerPeriodo(string textoPDF)
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
        }*/

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
        new Regex(@"Fecha\s*de\s*Vencimiento\s*CESP\:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
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
                new Regex(@"CESP\s*N[º°]\:?\s*(\d{14})", RegexOptions.IgnoreCase),
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
                new Regex(@"Vencimiento\s*\:?\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
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
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*ORIGINAL\s*\d{12}\s*\d{4}-\d{8}\s*Fecha\:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.IgnoreCase),
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
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*ORIGINAL\s*\d{12}\s*\d{4}-(\d{8})", RegexOptions.IgnoreCase)
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
                new Regex(@"Liquidaci[óo]n\s*de\s*Servicios\s*P[úu]blicos\s*ORIGINAL\s*\d{12}\s*(\d{4})-", RegexOptions.IgnoreCase)

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
                new Regex(@"(18)\s*MetroGas\s*S\.A\.", RegexOptions.IgnoreCase),
            };

            foreach (Regex regex in patrones)
            {
                Match match = regex.Match(textoPDF);
                if (match.Success)
                {
                    tipoFactura = match.Groups[1].Value;
                    //BYPASS PARA FACTURA A Y PARA TIPO 18 == B
                    #region Explicacion
                    /*
                     que significa si yo tengo una liquidacion de servicios publicos b 18
7 minutes ago

Si tienes un comprobante que indica "Liquidación de Servicios Públicos B 18", esto significa que has recibido una factura electrónica de un servicio público (luz, gas o agua) bajo un código específico del sistema tributario argentino.
Aquí te explico los detalles clave:
¿Qué es?: Es el comprobante oficial que reemplaza a la factura tradicional para las empresas prestadoras de servicios.
Código 18: Es el identificador numérico que la Agencia de Recaudación y Control Aduanero (ARCA) (ex-AFIP) asigna específicamente a las Liquidaciones Clase B.
Clase B: Significa que el comprobante fue emitido a un consumidor final, a un sujeto exento de IVA o a un monotributista.
Crédito Fiscal: Si eres un particular (consumidor final), este documento te sirve como comprobante de pago y para el control de consumos, pero no permite discriminar el IVA para ser tomado como crédito fiscal. 
Datos importantes que encontrarás en el documento:
C.E.S.P.: Código Electrónico de Servicios Públicos, que le da validez legal ante el organismo recaudador.
Segmentación Tarifaria: Información sobre tu nivel de subsidio (Nivel 1, 2 o 3).
Conceptos Facturados: El detalle del consumo del período y los impuestos aplicados. 
¿Necesitas ayuda para leer los montos de tu factura o quieres saber si puedes solicitar una tarifa social?
Las respuestas de la IA pueden incluir errores. Para obtener asesoramiento legal, consulta a un profesional. Más información




undefined
undefined
undefined
8 sitios
Factura Electrónica - Overall SRL
Los prestadores de servicios públicos deberán registrarse en el servicio “Regímenes de Facturación y Registración (REAR/RECE/RFI)”...

www.overall.com.ar

Prestadores de servicios públicos - ARCA - Consulta Frecuentes
El comprobante "Liquidación de Servicios Públicos" deberá contener -como mínimo- la información detallada en el artículo 10 de la ...

ARCA
Texto actualizado - Argentina.gob.ar
Asimismo, los aludidos responsables deberán informar las operaciones diarias realizadas con arreglo a lo previsto en el mencionado...

Argentina.gob.ar
Mostrar todo
todas las facturas con este codigo 18, son del tipo b?
7 minutes ago

Sí, dentro del sistema de facturación de la Agencia de Recaudación y Control Aduanero (ARCA), el código 18 identifica exclusivamente a la "Liquidación de Servicios Públicos Clase B". 
Para que no queden dudas, el sistema utiliza dos códigos principales para diferenciar el tipo de factura en servicios públicos:
Código 17: Corresponde a la Liquidación Clase A. Se emite cuando el cliente es otro Responsable Inscripto (permite discriminar el IVA).
                     */
                    #endregion
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
        private string ExtraerNumeroCliente(string textoPDF)
        {
            // Lógica para extraer el número de cliente del texto del PDF
            List<Regex> patrones = new List<Regex>
            {
                new Regex(@"Cuenta\s*\:\s*(\d{10})", RegexOptions.IgnoreCase),
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

