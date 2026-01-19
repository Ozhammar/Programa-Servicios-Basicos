using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Servicios
{

    internal class ExportadorExcel
    {
        private readonly string rutaPlantilla;

        public ExportadorExcel( )
        {
            rutaPlantilla = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "PLANTILLA EXCEL CAPITAL.xlsx");
        }

        public XLWorkbook abrirPlantilla()
        {
            try
            {
                XLWorkbook libro = new XLWorkbook(rutaPlantilla);
                return libro;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al abrir la plantilla de Excel: Archivo no encontrado" + ex.Message);
            }
        }

        


    }
}
