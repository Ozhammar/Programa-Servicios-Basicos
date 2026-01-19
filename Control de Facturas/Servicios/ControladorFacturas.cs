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
        //private readonly ProcesadorMetrogas procesadorMetrogas;
        //private readonly ProcesadorTelecom procesadorTelecom;
        public ControladorFacturas()
        {
            gestorArchivos = new GestorArchivos();
            procesadorEdesur = new ProcesadorEdesur();
            //procesadorMetrogas = new ProcesadorMetrogas();
            //procesadorTelecom = new ProcesadorTelecom();
        }

        //METODO DE ENTRADA AL PROCESAMIENTO DE DATOS
        public async Task<List<Factura>> ProcesarFacturasEnCarpeta(string carpeta, IProgress<int> progreso = null)
        {
            List<Factura> facturasProcesadas = new List<Factura>();
            IEnumerable<string> archivosPDF = gestorArchivos.ObtenerPDF(carpeta);

            //CONTEO DE PDFS Y VALOR BASE PARA CONTEO DE PROGRESO
            int total = archivosPDF.Count();
            int actual = 0;

            foreach (string archivo in archivosPDF)
            {
                try
                {
                    // Permitir que la UI se actualice
                    //await Task.Yield();
                    await Task.Run(() => {
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
                    // Manejar errores de lectura o procesamiento
                    Console.WriteLine($"Error al procesar {archivo}: {ex.Message}");
                }
                finally {
                    actual++; // Incrementar aunque falle para que el progreso continúe
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
            else if (textoPDF.Contains("Metrogas"))
            {
                return null;// procesadorMetrogas.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else if (textoPDF.Contains("Telecom"))
            {
                return null;// procesadorTelecom.ProcesarFactura(textoPDF, rutaArchivo);
            }
            else
            {
                //Console.WriteLine($"Empresa no identificada en el archivo: {rutaArchivo}");
                return null;
            }
        }
    }
}
