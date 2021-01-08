using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI.Models
{
    public class BoodschapRow
    {
        public int BoodschapRowID { get; set; }
        public int BoodschapID { get; set; }
        public int Aantal { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
