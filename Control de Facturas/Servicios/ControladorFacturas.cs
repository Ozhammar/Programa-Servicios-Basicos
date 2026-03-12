/*Responsabilidades de ControladorFacturas:

Recibir la carpeta con los PDFs (desde Form1)
Usar GestorArchivos para obtener lista de PDFs y leer cada uno
Identificar la empresa de cada PDF (mediante análisis del texto)
Delegar al procesador correspondiente (ProcesadorEdesur, etc.)
Recolectar todas las facturas procesadas en un List<Factura>
Manejar errores y reportarlos
Retornar la lista final a Form1*/

using System.ComponentModel;

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
        private readonly ProcesadorAguaInterior procesadorAguaInterior;
        private readonly ProcesadorElectricidadInterior procesadorElectricidadInterior;

        public ControladorFacturas()
        {
            gestorArchivos = new GestorArchivos();
            procesadorEdesur = new ProcesadorEdesur();
            procesadorEdenor = new ProcesadorEdenor();
            procesadorAYSA = new ProcesadorAYSA();
            procesadorMetrogasGrandes = new ProcesadorMetrogasGrandes();
            procesadorMetrogasPequenios = new ProcesadorMetrogasPequenios();
            procesadorGasInterior = new ProcesadorGasInterior();
            procesadorAguaInterior = new ProcesadorAguaInterior();
            procesadorElectricidadInterior = new ProcesadorElectricidadInterior();
        }

        public enum TiposServicios
        {
            GAS,
            AGUA,
            LUZ
        }

        #region Procesamiento de PDFs
        /// <summary>
        /// Procesa todos los archivos PDF encontrados en la <paramref name="carpeta"/> indicada.
        /// </summary>
        /// <remarks>
        /// - Obtiene la lista de PDFs mediante <c>gestorArchivos.ObtenerPDF</c> y lee el texto de cada archivo con <c>gestorArchivos.LeerPDF</c>.<br/>
        /// - Para cada PDF, identifica la empresa y delega el procesamiento al procesador correspondiente a través de <c>IdentificarYProcesarFactura</c>.<br/>
        /// - Si el PDF requiere división en bloques (por ejemplo facturas agrupadas), se usa <c>RequiereDivisionEnBloques</c> y <c>DividirEnBloques</c>.
        ///   Para cada bloque adicional se crea una copia física del PDF original con sufijo <c>_bloque{i}.pdf</c> en el mismo directorio y se procesa cada bloque por separado.<br/>
        /// - El método ejecuta el trabajo de lectura y procesamiento dentro de <c>Task.Run</c> para no bloquear el hilo llamador y soporta reporte de progreso mediante el parámetro opcional <paramref name="progreso"/>.<br/>
        /// - En caso de error al procesar un archivo se muestra un diálogo con el mensaje de error y el procesamiento continúa con los demás archivos.
        /// </remarks>
        /// <param name="carpeta">Ruta de la carpeta que contiene los archivos PDF a procesar.</param>
        /// <param name="progreso">Objeto opcional para reportar el número de archivos procesados (incremental).</param>
        /// <returns>Lista con las <c>Factura</c> correctamente procesadas. Si no hay archivos o no se procesan facturas, la lista puede estar vacía.</returns>
        /// <exception cref="ArgumentNullException">Si <paramref name="carpeta"/> es null (no comprobado explícitamente; depende de <c>gestorArchivos.ObtenerPDF</c>).</exception>
        public async Task<List<Factura>> ProcesarFacturasEnCarpeta(string carpeta, IProgress<int> progreso = null)
        {
            List<Factura> facturasProcesadas = new List<Factura>();
            IEnumerable<string> archivosPDF = gestorArchivos.ObtenerPDF(carpeta);

            int actual = 0;

            foreach (string archivo in archivosPDF)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        string textoPDF = gestorArchivos.LeerPDF(archivo);

                        if (RequiereDivisionEnBloques(textoPDF))
                        {
                            var bloques = DividirEnBloques(textoPDF).ToList();

                            List<string> rutasPorBloque = new List<string>();
                            string dirArchivo = Path.GetDirectoryName(archivo)!;
                            string nombreSinExt = Path.GetFileNameWithoutExtension(archivo);

                            for (int i = 0; i < bloques.Count; i++)
                            {
                                if (i == 0)
                                {
                                    rutasPorBloque.Add(archivo);
                                }
                                else
                                {
                                    string rutaCopia = Path.Combine(dirArchivo, $"{nombreSinExt}_bloque{i}.pdf");
                                    File.Copy(archivo, rutaCopia, overwrite: true);
                                    rutasPorBloque.Add(rutaCopia);
                                }
                            }

                            for (int i = 0; i < bloques.Count; i++)
                            {
                                Factura factura = IdentificarYProcesarFactura(bloques[i], rutasPorBloque[i]);
                                if (factura != null && !facturasProcesadas.Any(f => f.NumeroFactura == factura.NumeroFactura))
                                {
                                    facturasProcesadas.Add(factura);
                                }
                            }
                        }
                        else
                        {
                            Factura factura = IdentificarYProcesarFactura(textoPDF, archivo);
                            if (factura != null && !facturasProcesadas.Any(f => f.NumeroFactura == factura.NumeroFactura))
                            {
                                facturasProcesadas.Add(factura);
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al procesar {archivo}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    actual++;
                    progreso?.Report(actual);
                }
            }
            return facturasProcesadas;
        }

        public bool RequiereDivisionEnBloques(string textoPDF)
        {
            bool check = false;
            if (textoPDF.Contains("30-70861788-8" /* Aguas del Tucumán*/))
            {
                check = true;
            }

            return check;
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
            else
            {
                var tipoServicioInterior = corroborarInterior(textoPDF);
                if (tipoServicioInterior == TiposServicios.GAS)
                {
                    return procesadorGasInterior.ProcesarFactura(textoPDF, rutaArchivo);
                }
                if (tipoServicioInterior == TiposServicios.AGUA)
                {
                    return procesadorAguaInterior.ProcesarFactura(textoPDF, rutaArchivo);
                }
                if (tipoServicioInterior == TiposServicios.LUZ)
                {
                    return procesadorElectricidadInterior.ProcesarFactura(textoPDF, rutaArchivo);
                }
                return null;
            }
        }
        #endregion
        #region Filtrado y Ordenamiento
        private TiposServicios? corroborarInterior(string textoPDF)
        {
            string[] palabrasClave_GAS =
            {
                #region GAS INTERIOR
                "redengas",
                "30-67775026-6",
                "naturgynoa",
                "30-65786572-5",
                "www.naturgy.com.ar",
                "30-65786633-0",
                "litoralgas",
                "gasnea",
                "30-66554905-0",
                "33-65786527-9",
                "DISTRIBUIDORA DE GAS DEL CENTRO",
                "30-67364611-1",
                "DISTRIGAS",
                #region camuzziSUR
                "94000940600148071",
                "94000940600098389",
                "94000940600168381",
                "94200940600112201",
                "92000940600020772",
                "92030940600006061",
                "83700940600049699",
                "90000940600042178",
                "91030940600057771",
                "94000940600149324",
                "83240040200274998",
                "83240040100274230",
                "83320940600029269",
                "83000940600326800",
                "83000940600081425",
                "83000220302066056",
                "83000100101297507",
                "84000940600071234",
                "85000940600125847",
                "91200940600011558",
                "84000000100284341",
                "94100180200309425",
                "90000940600043201",
                "83000211202040550",
                "83400230400207495",
                "19000940601128792",
                #endregion
                #region camuzziPampeana
                "19000940600543300",
                "7000940600260990",
                "76000990403737859",
                "76300030900324405",
                "80000940600856251",
                "73000940600135266",
                "76000940601787206",
                "63000940600187037",
                "63000031000591301",
                "63600940600122119",
                "71300061100081656",
                "66600940600000934",
                "74000990300369728",
                "76000960703428986"
                #endregion
                               #endregion
            };
            string[] palabrasClave_AGUA =
            {
                #region AGUA INTERIOR
                "USHUAIA",
                "www.osmgp.gov.ar",
                "30 - 63046762 - 0",
                "RIO NEGRO",
                "ROSARIO",
                "RECONQUISTA",
                "SANTA FE",
                "30-64516879-4",
                "AGUAS CORDOBESAS",
                "AGUAS DEL TUCUMÁN",
                "AGUAS DEL TUCUMAN",
                "30-70861788-8",
                "aguasdeltucuman",
                "30-71151356-2",
                "33-69809590-9",
                "aguasdeformosa",
                "33-71097454-9",
                "30-64263072-1"
                #endregion
            };
            string[] palabrasClave_LUZ =
            {
                #region LUZ INTERIOR
                "30−99902748−9",
                "30-99902748-9"
                #endregion
            };


            foreach (string palabra in palabrasClave_GAS)
            {
                if (textoPDF.Contains(palabra))
                {
                    return TiposServicios.GAS;
                }
            }

            foreach (string palabra in palabrasClave_AGUA)
            {
                if (textoPDF.Contains(palabra))
                {
                    foreach (string palabraLuz in palabrasClave_LUZ)
                    {
                        if (!textoPDF.Contains(palabra))
                        {
                            return TiposServicios.AGUA;
                        }
                    }
                }
            }

            foreach (string palabra in palabrasClave_LUZ)
            {
                if (textoPDF.Contains(palabra))
                {
                    return TiposServicios.LUZ;
                }
            }
            return null;
        }
        public List<Factura> FiltrarPorEmpresa(List<Factura> facturas, string empresa)
        {
            if (facturas == null || facturas.Count == 0)
                return new List<Factura>();

            var facturasFiltradas = facturas
                .Where(f => f.Empresa.Equals(empresa, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return OrdenarSegunEmpresa(facturasFiltradas, empresa);
        }

        public List<Factura> filtrarPorTipoServicio(List<Factura> facturas, string tipoServicio)
        {
            if (facturas == null || facturas.Count == 0)
                return new List<Factura>();

            var facturasFiltradas = facturas
                .Where(f => f.TipoServicio.Equals(tipoServicio, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return OrdenarSegunEmpresa(facturasFiltradas, tipoServicio);
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
        private IEnumerable<string> DividirEnBloques(string textoPDF)
        {
            textoPDF = textoPDF.Replace("\r", "");

            var bloques = Regex.Split(textoPDF, @"(?=Cliente\s*\d{8})", RegexOptions.IgnoreCase);

            return bloques.Where(b => b.Contains("Cliente"));
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

        public void ModificarMultiplesFacturas(Factura factura, string nombrePropiedad, object valorNuevo)
        {
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
