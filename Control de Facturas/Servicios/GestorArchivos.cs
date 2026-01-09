using System.Windows.Forms;

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
            using(PdfDocument document = PdfDocument.Open(path))
            {
                foreach (Page page in document.GetPages())
                {
                    texto.AppendLine(page.Text);
                }
            }
            return texto.ToString();
        }


    }
}
