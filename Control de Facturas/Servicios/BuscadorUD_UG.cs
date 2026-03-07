namespace Control_de_Facturas.Servicios
{
    internal class BuscadorUD_UG
    {
        string rutaPlantilla;
        List<Dependencias> dependencias = new List<Dependencias>();

        public BuscadorUD_UG(int pDependencia = 0, int pUGeografica = 0)
        {
            Dependencia = pDependencia;
            UGeografica = pUGeografica;
            rutaPlantilla = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "UBICACIONES GEOGRAFICAS POR DEPENDENCIA Y EMPRESA.xlsx");
        }

        public int Dependencia { get; set; }
        public int UGeografica { get; set; }

        private void cargarDependencias() 
        {
            using XLWorkbook workbook = new XLWorkbook(rutaPlantilla);
            IXLWorksheet planilla = workbook.Worksheet("UBICACIONES GEOGRAFICAS");
            dependencias.Clear();
            foreach (IXLRow row in planilla.RowsUsed().Skip(1))
            {
            
                    Dependencias dependencia = new Dependencias
                        (
                    row.Cell(1).GetString(),
                    row.Cell(2).GetValue<long>(),
                    row.Cell(3).GetValue<int>(),
                    row.Cell(4).GetValue<int>()
                        );
         
                dependencias.Add(dependencia);
            }
        }

        public BuscadorUD_UG BuscarUD_UG(long CUIT)
        {
            cargarDependencias();

            Dependencias resultado_busqueda = null;
            try { 
                resultado_busqueda = dependencias.First(d => d.CUIT == CUIT);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"No se encontró una dependencia con el CUIT {CUIT}.", ex);
            }
          
            return new BuscadorUD_UG(resultado_busqueda.Codigo, resultado_busqueda.UGeografica);
        }
    }
}
