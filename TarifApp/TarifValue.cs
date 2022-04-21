using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarifApp
{
    /**
     * TarifValue model
     */
    public class TarifValue
    {
        public string ID { get; set; }
        public string TarifID { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Value { get; set; }
    }
}
