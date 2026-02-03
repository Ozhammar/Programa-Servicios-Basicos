namespace Control_de_Facturas.Servicios
{
    internal class GestorArchivos
    {
        public IEnumerable<string> ObtenerPDF(string path)
        {
            return Directory.EnumerateFiles(path, "*.pdf", SearchOption.AllDirectories);
        }

        public string LeerPDF(string path)
        {
            StringBuilder texto = new StringBuilder();
            using (PdfDocument document = PdfDocument.Open(path))
            {
                foreach (Page page in document.GetPages())
                {
                    texto.AppendLine(page.Text);
                }
            }
            return texto.ToString();
        }

        public string RenombrarArchivo(string path, string cliente, string puntoVenta, string numeroFactura)
        {
            string PathFinal = path;
            if (File.Exists(path))
            {
                string PathNuevo = Path.Combine(Path.GetDirectoryName(path)!, $"{cliente}_{puntoVenta}-{numeroFactura}.pdf");
                if(PathFinal != PathNuevo)
                {
                    File.Copy(PathFinal, PathNuevo, true);
                    File.Delete(path);
                    PathFinal = PathNuevo;
                }
            }
            return PathFinal;
        }
    }
}
