using System;
using System.Collections.Generic;
using System.Text;

namespace Control_de_Facturas.Servicios
{
    public class ControladorCamuzzi
    {
        public ControladorCamuzzi() { }

        public bool ControlarCamuzziSur(string textoPDF)
        {
            bool match = false;
            string[] palabrasClave =
            {
                "94000940600148071",
                "94000940600098389",
                "94000940600168381",
                "94200940600112201",
                "92000940600020772",
                "92030940600006061",
                "83700940600049699",
                "90000940600042178",
                "91030940600057771",
                "94000940600149324",
                "83240040200274998",
                "83240040100274230",
                "83320940600029269",
                "83000940600326800",
                "83000940600081425",
                "83000220302066056",
                "83000100101297507",
                "84000940600071234",
                "85000940600125847",
                "91200940600011558",
                "84000000100284341",
                "94100180200309425",
                "90000940600043201",
                "83000211202040550",
                "83400230400207495"

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


        public bool ControlarCamuzziPampeana(string textoPDF)
        {
            bool match = false;
            string[] palabrasClave =
            {
               "19000940600543300",
                "7000940600260990",
                "76000990403737859",
                "76300030900324405",
                "80000940600856251",
                "73000940600135266",
                "76000940601787206",
                "63000940600187037",
                "63000031000591301",
                "63600940600122119",
                "71300061100081656",
                "66600940600000934",
                "74000990300369728",
                "76000960703428986"

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
