
namespace Control_de_Facturas.Servicios
{
    public class BuscadorCUIT
    {
        string rutaPlantilla;
        public BuscadorCUIT()
        {
            rutaPlantilla = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Plantillas", "BENEFICIARIOS AGUA, GAS Y LUZ.xlsx");
        }
        List<Empresas> listaEmpresas = new List<Empresas>();


        public void CargarEmpresas()
        {
            using XLWorkbook workbook = new XLWorkbook(rutaPlantilla);
            IXLWorksheet planilla = workbook.Worksheet("Hoja1");
            listaEmpresas.Clear();
            foreach (IXLRow row in planilla.RowsUsed().Skip(1))
            {
                Empresas empresa = new Empresas(
                    row.Cell(1).GetValue<int>(),
                    row.Cell(2).GetString(),
                    row.Cell(3).GetString(),
                    row.Cell(4).GetString(),
                    row.Cell(5).GetString());
                listaEmpresas.Add(empresa);
            }
        }
            
        public string BuscarCUIT(string razonSocial)
        {
            CargarEmpresas();

            string cuit = string.Empty;
            foreach (Empresas empresa in listaEmpresas)
            {
                if (empresa.Denominacion.Contains(razonSocial))
                {
                    cuit = empresa.NumeroDocumento;
                }
            }
            return cuit;

        }
    }
}
