namespace Control_de_Facturas.Servicios
{
    internal class ControladorEdesal
    {
        public ControladorEdesal() { }

        public bool ControlarEdesal(string textoPDF)
        {
            bool match = false;
            string[] palabrasClave =
            {
               "1032114",
               "1006727",
               "1098494"
            };

            foreach (string palabra in palabrasClave)
            {
                if (textoPDF.Contains(palabra))
                {
                    match = true;

                }
            }
            return match;
        }
    }
}
