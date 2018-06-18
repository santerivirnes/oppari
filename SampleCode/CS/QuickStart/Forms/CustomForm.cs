using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Crm.Sdk.Samples.Forms
{
    // Yksittäinen lomake
    // Kaikki lomakkeet voisi opiskelijalla laittaa Thesis (opinnäytetyö) luokan
    // taulukkoon
    public enum FormState { NotReady, Submitted, Accepted, Signed, Ok, Archived }
    public enum FormId { AiheOhjaus = 1, Tutkimuslupa = 2, ToimeksiantoUlko = 3, opettajanArviointi = 4, Harjoittelusopimus = 5 }
    public class CustomForm
    {
        // Lomakkeen tiedot.
        // Kaikki muuttujat täyttyy vain aihe ja ohjauslomakkeella.
        // Lähinnä helpompi käsitellä kun ei tarvii kenttiä läpi looppailla
        // Tulisi kai tehdä jotennin toisin
        public int formId { get; set; }
        public int agreementId { get; set; }
        public string formname { get; set; }
        public string name { get; set; }
        public string userid { get; set; }
        public string email { get; set; }
        public string instructor { get; set; }
        public string inspector { get; set; }
        public CustomField[] fields { get; set; }
        public FormState state { get; set; }

        public CustomForm()
        {
            this.formId = 0;
            this.agreementId = 0;
            this.formname = string.Empty;
            this.fields = null;
            this.state = FormState.NotReady;
        }

        public CustomForm(int formId, string name, CustomField[] f)
        {
            this.formId = formId;
            this.formname = name;
            this.fields = f;
        }

        public CustomForm(int formid, int agreementid, string formname, string name, string userid, string email, FormState state, CustomField[] f)
        {
            this.formId = formid;
            this.agreementId = agreementid;
            this.formname = formname;
            this.name = name;
            this.userid = userid;
            this.email = email;
            this.state = state;
            this.fields = f;
        }
        //tällä on tarkoitus säätää vain-luku-ominaisuus kentälle
        public void EnableDisableField(int field, bool disable)
        {
            if (field < 0 || field >= this.fields.Length)
            {
                return;
            }
            else
            {
                this.fields[field].isDisabled = disable;
            }
        }

        //ja sama kaikille
        public void EnableDisableFields(bool disable)
        {
            foreach (CustomField cf in this.fields)
            {
                cf.isDisabled = disable;
            }
        }

        // Aseta lomakkeen kentän arvo nimellä
        // Esim "Tekijä" tai "Tekijä_puhelin" (lomakkeet_rakenne taulu ja kentta_kuvaus)
        public void SetFieldValue(string fieldname, string value)
        {
            fields.First(x => x.name == fieldname).value = value;
        }

        // Hae lomakkeen kentän arvo nimellä
        // Esim "Tekijä" tai "Tekijä_puhelin" (lomakkeet_rakenne taulu ja kentta_kuvaus)
        public string GetFieldValue(string fieldname)
        {
            var query = fields.FirstOrDefault(s => s.name == fieldname);
            return query.value.ToString();
        }
        public bool EmptyFields()
        {
            bool result;
            if (fields.Length > 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
