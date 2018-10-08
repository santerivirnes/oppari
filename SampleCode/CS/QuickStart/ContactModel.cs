using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Crm.Sdk.Samples
{
    public class ContactModel
    {
        public string kokonimi { get; set; }
        //public string sukunimi { get; set; }
        public string puhelin { get; set; }
        public string email { get; set; }
        public string tehtavanimike { get; set; }
        public Guid yhteiso { get; set; }
        public string paivitetty { get; set; }
        public Guid ulkoinentunniste { get; set; }
    }
}
