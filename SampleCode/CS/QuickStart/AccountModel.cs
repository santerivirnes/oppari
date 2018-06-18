using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Crm.Sdk.Samples
{
    public class AccountModel
    {

        public string nimi { get; set; }
        public string postiosoite { get; set; }
        public string kayntiosoite { get; set; }
        public string ytunnus { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public Guid ulkoinenTunnus { get; set; }
        public DateTime paivitetty { get; set; }


    }
}
