namespace Control_de_Facturas.Servicios
{
    internal class ConvertidorExcel
    {
        public ConvertidorExcel() { }

        public void conversor_XLS(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb_xlsx = new XSSFWorkbook(fs, true);
                HSSFWorkbook wb = new HSSFWorkbook();

                int cantidad_hojas = wb_xlsx.NumberOfSheets;

                for (int i = 0; i < cantidad_hojas; i++)
                {
                    ISheet hojaOrigen = wb_xlsx.GetSheetAt(i);
                    if (hojaOrigen == null) continue;
                    ISheet hojaDestino = wb.CreateSheet(hojaOrigen.SheetName);

                    for (int j = 0; j <= hojaOrigen.LastRowNum; j++)
                    {
                        IRow filaOrigen = hojaOrigen.GetRow(j);
                        if (filaOrigen == null) continue;
                        IRow filaDestino = hojaDestino.CreateRow(j);


                        for (int k = 0; k < filaOrigen.LastCellNum; k++)
                        {
                            ICell celdaOrigen = filaOrigen.GetCell(k);
                            if (celdaOrigen != null)
                            {
                                ICell celdaDestino = filaDestino.CreateCell(k);

                                switch (celdaOrigen.CellType)
                                {
                                    case CellType.String:
                                        celdaDestino.SetCellValue(celdaOrigen.StringCellValue);
                                        break;

                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(celdaOrigen))
                                            celdaDestino.SetCellValue(celdaOrigen.DateCellValue.Value);
                                        else
                                            celdaDestino.SetCellValue(celdaOrigen.NumericCellValue);
                                        break;

                                    case CellType.Boolean:
                                        celdaDestino.SetCellValue(celdaOrigen.BooleanCellValue);
                                        break;

                                    case CellType.Formula:
                                        celdaDestino.SetCellFormula(celdaOrigen.CellFormula);
                                        break;

                                    case CellType.Blank:
                                        celdaDestino.SetBlank();
                                        break;

                                    default:
                                        celdaDestino.SetCellValue(celdaOrigen.ToString());
                                        break;
                                }
                            }
                        }
                    }
                }
                
                string nuevoPath = Path.ChangeExtension(path, ".xls");

                using (FileStream fsSalida = new FileStream(nuevoPath, FileMode.Create, FileAccess.Write))
                {
                    wb.Write(fsSalida);
                }
            }
        }
    }
}