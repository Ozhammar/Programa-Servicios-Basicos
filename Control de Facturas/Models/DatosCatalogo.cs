namespace Control_de_Facturas.Models
{
    internal class DatosCatalogo
    {
        public DatosCatalogo(string pObjetoGasto = "", string pCodigoCatalogo = "")
        {
            ObjetoGasto = pObjetoGasto;
            CodigoCatalogo = pCodigoCatalogo;
        }

        public string ObjetoGasto { get; set; }
        public string CodigoCatalogo { get; set; }
    }
}