using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Servicios
{
    public class ConvertidorImportes
    {

     

        public decimal ParseImporteFlexible(string valor)
        {
            valor = valor.Trim();

            int lastComma = valor.LastIndexOf(',');
            int lastDot = valor.LastIndexOf('.');

            if (lastComma > lastDot)
            {
                valor = valor.Replace(".", "");
                valor = valor.Replace(",", ".");
            }
            else if (lastDot > lastComma)
            {
                valor = valor.Replace(",", "");
            }

            return decimal.Parse(valor, CultureInfo.InvariantCulture);
        }

    }
}
