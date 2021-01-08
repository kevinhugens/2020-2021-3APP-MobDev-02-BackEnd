using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI.Models
{
    public class Boodschap
    {
        public int BoodschapID { get; set; }
        public ICollection<BoodschapRow> Rows { get; set; }
        public string Status { get; set; } //uitgevoerd of gepland
        public DateTime DatumUitgevoerd { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }


    }
}
