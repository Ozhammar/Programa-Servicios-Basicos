/*Responsabilidades de ControladorFacturas:

Recibir la carpeta con los PDFs (desde Form1)
Usar GestorArchivos para obtener lista de PDFs y leer cada uno
Identificar la empresa de cada PDF (mediante análisis del texto)
Delegar al procesador correspondiente (ProcesadorEdesur, etc.)
Recolectar todas las facturas procesadas en un List<Factura>
Manejar errores y reportarlos
Retornar la lista final a Form1*/

namespace Control_de_Facturas.Servicios
{
    internal class ControladorFacturas
    {
        private readonly GestorArchivos gestorArchivos;
        private readonly ProcesadorEdesur procesadorEdesur;
        private readonly ProcesadorEdenor procesadorEdenor;
        private readonly ProcesadorAYSA procesadorAYSA;
        private readonly ProcesadorMetrogasGrandes procesadorMetrogasGrandes;
        private readonly ProcesadorMetrogasPequenios procesadorMetrogasPequenios;
        private readonly ProcesadorGasInterior procesadorGasInterior;
        //private readonly ProcesadorTelecom procesadorTelecom;

        public ControladorFacturas()
        {
            gestorArchivos = new GestorArchivos();
            procesadorEdesur = new ProcesadorEdesur();
            procesadorEdenor = new ProcesadorEdenor();
            procesadorAYSA = new ProcesadorAYSA();
            procesadorMetrogasGrandes = new ProcesadorMetrogasGrandes();
            procesadorMetrogasPequenios = new ProcesadorMetrogasPequenios();
            procesadorGasInterior = new ProcesadorGasInterior();
            //procesadorTelecom = new ProcesadorTelecom();
        }

        #region Procesamiento de PDFs
        //METODO DE ENTRADA AL PROCESAMIENTO DE DATOS
        public async Task<List<Factura>> ProcesarFacturasEnCarpeta(string carpeta, IProgress<int> progreso = null)
        {
            List<Factura> facturasProcesadas = new List<Factura>();
            IEnumerable<string> archivosPDF = gestorArchivos.ObtenerPDF(carpeta);

            int total = archivosPDF.Count();
            int actual = 0;

            foreach (string archivo in archivosPDF)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        string textoPDF = gestorArchivos.LeerPDF(archivo);
                        Factura factura = IdentificarYProcesarFactura(textoPDF, archivo);
                        if (factura != null)
                        {
                            facturasProcesadas.Add(factura);
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar {archivo}: {ex.Message}");
                }
                finally
                {
                    actual++;
                    progreso?.Report(actual);
                }
            }
            return facturasProcesadas;
        }

        private Factura IdentificarYProcesarFactura(string textoPDF, string rutaArchivo)
        {
            if (textoPDF.Contains("Edesur"))
            {
                return procesadorEdesur.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else if (textoPDF.Contains("edenor") || textoPDF.Contains("30-65511620-2"))
            {
                return procesadorEdenor.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else if (textoPDF.Contains("aysa") || textoPDF.Contains("30-70956507-5"))
            {
                return procesadorAYSA.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else if (textoPDF.Contains("Metrogas Grandes Clientes") || textoPDF.Contains("grandesclientes"))
            {
                return procesadorMetrogasGrandes.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else if (textoPDF.Contains("30-65786367-6") && !textoPDF.Contains("grandesclientes"))
            {
                return procesadorMetrogasPequenios.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else if (corroborarInterior(textoPDF))
            {
                return procesadorGasInterior.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else if (textoPDF.Contains("Telecom"))
            {
                return null;// procesadorTelecom.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else
            {
                return null;
            }
        }


        #endregion

        #region Filtrado y Ordenamiento
        public List<Factura> FiltrarPorEmpresa(List<Factura> facturas, string empresa)
        {
            if (facturas == null || facturas.Count == 0)
                return new List<Factura>();

            var facturasFiltradas = facturas
                .Where(f => f.Empresa.Equals(empresa, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return OrdenarSegunEmpresa(facturasFiltradas, empresa);
        }

        private bool corroborarInterior(string textoPDF)
        {
            string[] palabrasClave = 
            {
                "redengas",
                "30-67775026-6",
                "naturgynoa",
                "30-65786572-5",
            };
            bool existe = false;
            
            foreach (string palabra in palabrasClave)
            {
                if (textoPDF.Contains(palabra))
                {
                   
                    existe = true;
                    break;
                }
            }
            return existe;
        }

        private List<Factura> OrdenarSegunEmpresa(List<Factura> facturas, string empresa)
        {
            switch (empresa.ToUpper())
            {
                case "EDESUR":
                    return facturas
                        .OrderByDescending(f => f.Tarifa)
                        .ThenBy(f => f.NumeroCliente)
                        .ToList();
                case "EDENOR":
                    return facturas
                        .OrderByDescending(f => f.Tarifa)
                        .ThenBy(f => f.NumeroCliente)
                        .ToList();

                case "METROGAS GRANDES CLIENTES":
                    return facturas
                        .OrderBy(f => f.NumeroCliente)
                        .ThenBy(f => f.FechaEmision)
                        .ToList();

                case "TELECOM":
                    return facturas
                        .OrderBy(f => f.NumeroCliente)
                        .ToList();

                default:
                    // Orden por defecto: por fecha de emisión
                    return facturas
                        .OrderBy(f => f.FechaEmision)
                        .ToList();
            }
        }

        public List<string> ObtenerEmpresas(List<Factura> facturas)
        {
            if (facturas == null || facturas.Count == 0)
                return new List<string>();

            return facturas
                .Select(f => f.Empresa)
                .Distinct()
                .OrderBy(e => e)
                .ToList();
        }
        #endregion

        #region Modificación de Facturas
        public void ModificarFactura(List<Factura> facturas, int indice, string nombrePropiedad, object valorNuevo)
        {
            if (facturas == null || indice < 0 || indice >= facturas.Count)
                throw new ArgumentException("Índice de factura inválido");

            var factura = facturas[indice];
            var propiedad = typeof(Factura).GetProperty(nombrePropiedad);

            if (propiedad == null)
                throw new ArgumentException($"La propiedad '{nombrePropiedad}' no existe en Factura");

            if (!propiedad.CanWrite)
                throw new InvalidOperationException($"La propiedad '{nombrePropiedad}' es de solo lectura");

            try
            {
                // Convertir el valor al tipo correcto
                object valorConvertido = ConvertirValor(valorNuevo, propiedad.PropertyType);
                propiedad.SetValue(factura, valorConvertido);

                // Si se modifica ImportePrimerVencimiento o ImporteSaldoAnterior, recalcular ImporteAbonable
                if (nombrePropiedad == "ImportePrimerVencimiento" || nombrePropiedad == "ImporteSaldoAnterior")
                {
                    factura.ImporteAbonable = factura.CalcularImporteAbonable();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"No se pudo convertir '{valorNuevo}' al tipo {propiedad.PropertyType.Name}: {ex.Message}");
            }
        }

        private object ConvertirValor(object valor, Type tipoDestino)
        {
            if (valor == null)
            {
                return tipoDestino.IsValueType ? Activator.CreateInstance(tipoDestino) : null;
            }

            string valorStr = valor.ToString();

            if (tipoDestino == typeof(string))
            {
                return valorStr;
            }

            if (tipoDestino == typeof(int))
            {
                return int.Parse(valorStr);
            }

            if (tipoDestino == typeof(long))
            {
                return long.Parse(valorStr);
            }

            if (tipoDestino == typeof(decimal))
            {
                decimal resultado;

                // Cultura explícita española (coma decimal)
                var culturaAR = CultureInfo.GetCultureInfo("es-AR");

                if (decimal.TryParse(valorStr, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, culturaAR, out resultado))
                {
                    return resultado;
                }

                // Soporte punto decimal (Invariant)
                if (decimal.TryParse(valorStr, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out resultado))
                {
                    return resultado;
                }

            }

            if (tipoDestino == typeof(DateTime))
            {
                return DateTime.Parse(valorStr);
            }

            if (tipoDestino == typeof(bool))
            {
                return bool.Parse(valorStr);
            }

            // Para tipos nullable
            if (Nullable.GetUnderlyingType(tipoDestino) != null)
            {
                Type tipoBase = Nullable.GetUnderlyingType(tipoDestino);
                return ConvertirValor(valor, tipoBase);
            }

            return Convert.ChangeType(valor, tipoDestino);
        }
        #endregion
    }
}
