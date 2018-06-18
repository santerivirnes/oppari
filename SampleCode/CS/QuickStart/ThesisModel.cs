using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Crm.Sdk.Samples
{
    public class ThesisModel
    {
        public string aihe { get; set; }
        public string ta_nimi { get; set; }
        public string opiskelijaID { get; set; }
        public string koulutusohjelma { get; set; }
        public string ohjaaja { get; set; }
        public bool hanke { get; set; }
        public string thesisID { get; set; }
        public string yhteyshenkilo { get; set; }
    }
}
