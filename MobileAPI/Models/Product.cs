using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int Nummer { get; set; }
        public string Naam { get; set; }
        public string Merk { get; set; }
        public string Img_s { get; set; }
        public string Img_l { get; set; }
        public string Categorie { get; set; }
        public string PrijsPerEenheid { get; set; }
        public string Omschrijving { get; set; }
        public string Eenheid { get; set; }
    }
}
