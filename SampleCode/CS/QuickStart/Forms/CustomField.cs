using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Crm.Sdk.Samples.Forms
{
    public class CustomField
    {
        //[Required]
        //[Display(Name = "lomakeID: ")]
        //public int lomakeID = 1;
        //[Display(Name = "{0}", AutoGenerateField = true)]
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public bool isDisabled { get; set; }
        public bool hide { get; set; }
        public int maxlength { get; set; }
        public string type { get; set; }

        public CustomField()
        {
            this.id = -1;
            this.name = string.Empty;
            this.value = string.Empty;
            this.isDisabled = false;
            this.hide = false;
            this.maxlength = 200;

        }

        public CustomField(int id, string description, string type) : this()
        {
            this.id = id;
            this.name = description;
            this.type = type;
        }
        public CustomField(int id, string description, string value, string type) : this()
        {
            this.id = id;
            this.name = description;
            this.value = value;
            this.type = type;
        }
    }
}
