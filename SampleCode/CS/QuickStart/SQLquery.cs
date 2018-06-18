using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Crm.Sdk.Samples;

namespace Microsoft.Crm.Sdk.Samples
{
    public class SQLquery
    {
        private SQLConnectionManager sqlconnman;

        public SQLquery()
        {
            sqlconnman = new SQLConnectionManager();
        }
        // haetaan id sopimustaulusta
        public DataTable GetThesisId()
        {
            DataTable datatable = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_SOPIMUSID_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(datatable);
            }
            conn.Close();
            return datatable;
        }
        public DataTable GetThesis(int sopimusID)
        {
            DataTable datatable = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_OPINNAYTETYO_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@sopimusID", sopimusID);
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(datatable);
            }
            conn.Close();
            return datatable;
        }

        // haetaan asiakas nimellä
        public DataTable GetClientWithName(string nimi)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_ASIAKAS_NIMELLA_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (nimi == null || nimi == "" || nimi == " " || nimi == "null")
            {
                command.Parameters.AddWithValue("@nimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@nimi", nimi);
            }
                command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan asiakas nimellä ja accountIDllä
        public DataTable GetClientWithNameAndAccountID(string nimi, Guid accountid)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_ASIAKAS_IDLLA_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if(nimi == null || nimi == "" || nimi == " " || nimi == "null") {
                command.Parameters.AddWithValue("@name", DBNull.Value);
            } else {
                command.Parameters.AddWithValue("@name", nimi);
            }
            
            command.Parameters.AddWithValue("@accountid", accountid);
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan asiakas nimellä, accountIDllä ja osoitteella
        public DataTable GetClientWithNameAndAccountIDAndAddress(string nimi, Guid accountid, string osoite)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_ASIAKAS_OSOITTEELLA_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if(nimi == null || nimi == "" || nimi == " " || nimi == "null")
            {
                command.Parameters.AddWithValue("@name", DBNull.Value);
            } else { 
            command.Parameters.AddWithValue("@name", nimi);
            }
            command.Parameters.AddWithValue("@accountid", accountid);
            if(osoite == null || osoite == "") {
                command.Parameters.AddWithValue("@osoite", DBNull.Value);
            } else {
                command.Parameters.AddWithValue("@osoite", osoite);
            }
            
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }
        

        // haetaan yhteyshenkilo etu- ja sukunimellä
        public DataTable GetContactWithName(string etunimi, string sukunimi)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_YHTEYSHENKILO_NIMELLA_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (String.IsNullOrEmpty(etunimi))
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@etunimi", etunimi);
            }
            if (String.IsNullOrEmpty(sukunimi))
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            } else
            {
                command.Parameters.AddWithValue("@sukunimi", sukunimi);
            }
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan yhteyshenkilö nimillä ja accountIDllä (ulkoinentunnus)
        public DataTable GetContactWithID(string etunimi, string sukunimi, Guid accountID)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_YHTEYSHENKILO_IDLLA_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (String.IsNullOrEmpty(etunimi))
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            } else
            {
                command.Parameters.AddWithValue("@etunimi", etunimi);
            }
            if (String.IsNullOrEmpty(sukunimi))
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@sukunimi", sukunimi);
            }
            command.Parameters.AddWithValue("@accountID", accountID);
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan yhteyshenkilo nimillä, accountIDllä ja parentIDllä (yhteisö)
        public DataTable GetContactWithParentID(string etunimi, string sukunimi, Guid accountID, Guid parentID)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_YHTEYSHENKILO_YHTEISOLLA_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (String.IsNullOrEmpty(etunimi))
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@etunimi", etunimi);
            }
            if (String.IsNullOrEmpty(sukunimi))
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@sukunimi", sukunimi);
            }
            if (String.IsNullOrEmpty(parentID.ToString()))
            {
                command.Parameters.AddWithValue("@parentID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@parentID", parentID);
            }
            if (String.IsNullOrEmpty(accountID.ToString()))
            {
                command.Parameters.AddWithValue("@accountID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@accountID", accountID);
            }
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan yhteyshenkilo nimillä, accountIDllä, parentIDllä ja emaililla
        public DataTable GetContactWithEmail(string etunimi, string sukunimi, Guid accountID, Guid parentID, string email)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_YHTEYSHENKILO_SAHKOPOSTILLA_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (String.IsNullOrEmpty(etunimi))
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@etunimi", etunimi);
            }
            if (String.IsNullOrEmpty(sukunimi))
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@sukunimi", sukunimi);
            }
            if (String.IsNullOrEmpty(parentID.ToString()))
            {
                command.Parameters.AddWithValue("@parentID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@parentID", parentID);
            }
            if (String.IsNullOrEmpty(email))
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", email);
            }
            if (String.IsNullOrEmpty(accountID.ToString()))
            {
                command.Parameters.AddWithValue("@accountID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@accountID", accountID);
            }
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan yhteyshenkilo nimillä, accountIDllä, parentIDllä, emaililla ja puh.numerolla
        public DataTable GetContactWithPhone(string etunimi, string sukunimi, Guid accountID, Guid parentID, string email, string puhelin)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_YHTEYSHENKILO_PUHELIMELLA_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (String.IsNullOrEmpty(etunimi))
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@etunimi", etunimi);
            }
            if (String.IsNullOrEmpty(sukunimi))
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@sukunimi", sukunimi);
            }
            if (String.IsNullOrEmpty(parentID.ToString()))
            {
                command.Parameters.AddWithValue("@parentID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@parentID", parentID);
            }
            if (String.IsNullOrEmpty(email))
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", email);
            }
            if (String.IsNullOrEmpty(puhelin))
            {
                command.Parameters.AddWithValue("@puhelin", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@puhelin", puhelin);
            }
            if (String.IsNullOrEmpty(accountID.ToString()))
            {
                command.Parameters.AddWithValue("@accountID", DBNull.Value);
            } else
            {
                command.Parameters.AddWithValue("@accountID", accountID);
            }
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan yhteyshenkilo nimillä, emaililla ja puh.numerolla
        public DataTable GetContactWithNameEmailPhone(string etunimi, string sukunimi, string email, string puhelin)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_YHTEYSHENKILO_NIMI_EMAIL_PUH_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (String.IsNullOrEmpty(etunimi))
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@etunimi", etunimi);
            }
            if (String.IsNullOrEmpty(sukunimi))
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@sukunimi", sukunimi);
            }
            if (String.IsNullOrEmpty(email))
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", email);
            }
            if (String.IsNullOrEmpty(puhelin))
            {
                command.Parameters.AddWithValue("@puhelin", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@puhelin", etunimi);
            }
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan yhteyshenkilo nimillä, emaililla, puh.numerolla ja parentIDllä
        public DataTable GetContactWithNameEmailPhoneParent(string etunimi, string sukunimi, string email, string puhelin, Guid parentID)
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_YHTEYSHENKILO_NIMI_EMAIL_PUH_YHTEISO_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (String.IsNullOrEmpty(etunimi))
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            } else
            {
                command.Parameters.AddWithValue("@etunimi", etunimi);
            }
            if (String.IsNullOrEmpty(sukunimi))
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@sukunimi", sukunimi);
            }
            if (String.IsNullOrEmpty(parentID.ToString()))
            {
                command.Parameters.AddWithValue("@parentID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@parentID", parentID);
            }
            if (String.IsNullOrEmpty(email))
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", email);
            }
            if (String.IsNullOrEmpty(puhelin))
            {
                command.Parameters.AddWithValue("@puhelin", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@puhelin", etunimi);
            }
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        // haetaan aiheet aihepankista
        public DataTable GetTopics()
        {
            DataTable datatable = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_AIHEPANKIN_AIHEET_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(datatable);
            }
            conn.Close();
            return datatable;
        }
        public bool setID(Guid ohos, Guid crm)
        {

            using (SqlConnection conn = sqlconnman.GetDatabaseConnection())
            {
                string commandString = "";
                commandString = "CRM_PAIVITA_ULKOINEN_TUNNUS_SP";
                using (SqlCommand command = new SqlCommand(commandString, conn))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@CRM", crm);
                    command.Parameters.AddWithValue("@OHOS", ohos);
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        command.ExecuteNonQuery();
                        conn.Close();
                        return true;
                    }

                    catch (Exception ex)
                    {
                        conn.Close();
                        return false;
                        throw new Exception("Something went wrong" + ex.Message);
                    }
                }
            }

        }

        public DataTable GetAccount()
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_ASIAKKAAT_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }

        public DataTable GetContact()
        {
            DataTable data = new DataTable();
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_HAE_KONTAKTI_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;

            using (SqlDataAdapter a = new SqlDataAdapter(command))
            {
                a.Fill(data);
            }
            conn.Close();
            return data;
        }


        // lisätään yhteyshenkilö
        public bool InsertContact(ContactModel conmodel)
        {
            bool added = false;
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_LISAA_YHTEYSHENKILO_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if(conmodel.etunimi == null || conmodel.etunimi == "" || conmodel.etunimi == " " || conmodel.etunimi == "null")
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            } else
            {
                command.Parameters.AddWithValue("@etunimi", conmodel.etunimi);
            }
            if (conmodel.sukunimi == null || conmodel.sukunimi == "" || conmodel.sukunimi == " " || conmodel.sukunimi == "null")
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@sukunimi", conmodel.sukunimi);
            }
            if (conmodel.email == null || conmodel.email == "" || conmodel.email == " " || conmodel.email == "null")
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", conmodel.email);
            }
            if (conmodel.puhelin == null || conmodel.puhelin == "" || conmodel.puhelin == " " || conmodel.puhelin == "null")
            {
                command.Parameters.AddWithValue("@puhelin", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@puhelin", conmodel.puhelin);
            }
            if (conmodel.tehtavanimike == null || conmodel.tehtavanimike == "" || conmodel.tehtavanimike == " " || conmodel.tehtavanimike == "null")
            {
                command.Parameters.AddWithValue("@tehtavanimike", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@tehtavanimike", conmodel.tehtavanimike);
            }
            if (conmodel.yhteiso == null || conmodel.yhteiso.ToString() == "" || conmodel.yhteiso.ToString() == " " || conmodel.yhteiso.ToString() == "null")
            {
                command.Parameters.AddWithValue("@yhteiso", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@yhteiso", conmodel.yhteiso);
            }
            if (conmodel.ulkoinentunniste == null || conmodel.ulkoinentunniste.ToString() == "" || conmodel.ulkoinentunniste.ToString() == " " || conmodel.ulkoinentunniste.ToString() == "null")
            {
                command.Parameters.AddWithValue("@ulkoinentunniste", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ulkoinentunniste", conmodel.ulkoinentunniste);
            }
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                int counted = command.ExecuteNonQuery();
                if (counted > 0)
                {
                    added = true;
                }
            }
            catch (Exception ex)
            {
                added = false;
                throw new Exception("something went wrong" + ex.Message);
            }
            conn.Close();
            return added;
        }

        // päivitetään yhteyshenkilö
        public bool UpdateContact(ContactModel conmodel)
        {
            bool added = false;
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_PAIVITA_YHTEYSHENKILO_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (conmodel.etunimi == null || conmodel.etunimi == "" || conmodel.etunimi == " " || conmodel.etunimi == "null")
            {
                command.Parameters.AddWithValue("@etunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@etunimi", conmodel.etunimi);
            }
            if (conmodel.sukunimi == null || conmodel.sukunimi == "" || conmodel.sukunimi == " " || conmodel.sukunimi == "null")
            {
                command.Parameters.AddWithValue("@sukunimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@sukunimi", conmodel.sukunimi);
            }
            if (conmodel.email == null || conmodel.email == "" || conmodel.email == " " || conmodel.email == "null")
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", conmodel.email);
            }
            if (conmodel.puhelin == null || conmodel.puhelin == "" || conmodel.puhelin == " " || conmodel.puhelin == "null")
            {
                command.Parameters.AddWithValue("@puhelin", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@puhelin", conmodel.puhelin);
            }
            if (conmodel.tehtavanimike == null || conmodel.tehtavanimike == "" || conmodel.tehtavanimike == " " || conmodel.tehtavanimike == "null")
            {
                command.Parameters.AddWithValue("@tehtavanimike", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@tehtavanimike", conmodel.tehtavanimike);
            }
            if (conmodel.yhteiso == null || conmodel.yhteiso.ToString() == "" || conmodel.yhteiso.ToString() == " " || conmodel.yhteiso.ToString() == "null")
            {
                command.Parameters.AddWithValue("@yhteiso", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@yhteiso", conmodel.yhteiso);
            }
            if (conmodel.ulkoinentunniste == null || conmodel.ulkoinentunniste.ToString() == "" || conmodel.ulkoinentunniste.ToString() == " " || conmodel.ulkoinentunniste.ToString() == "null")
            {
                command.Parameters.AddWithValue("@ulkoinentunniste", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ulkoinentunniste", conmodel.ulkoinentunniste);
            }
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                int counted = command.ExecuteNonQuery();
                if (counted > 0)
                {
                    added = true;
                }
            }
            catch (Exception ex)
            {
                added = false;
                throw new Exception("something went wrong" + ex.Message);
            }
            conn.Close();
            return added;
        }

        // lisätään asiakas
        public bool InsertClient(AccountModel accmodel)
        {
            bool added = false;
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_LISAA_ASIAKAS_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if(accmodel.nimi == null || accmodel.nimi == "")
            {
                command.Parameters.AddWithValue("@nimi", DBNull.Value);
            } else {
                command.Parameters.AddWithValue("@nimi", accmodel.nimi);
            }
            if (accmodel.kayntiosoite == null || accmodel.kayntiosoite == "")
            {
                command.Parameters.AddWithValue("@kayntiosoite", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@kayntiosoite", accmodel.kayntiosoite);
            }
            if (accmodel.postiosoite == null || accmodel.postiosoite == "")
            {
                command.Parameters.AddWithValue("@postiosoite", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@postiosoite", accmodel.postiosoite);
            }
            if (accmodel.email == null || accmodel.email == "")
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", accmodel.email);
            }
            if (accmodel.ulkoinenTunnus == null || accmodel.ulkoinenTunnus.ToString() == "")
            {
                command.Parameters.AddWithValue("@ulkoinentunnus", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ulkoinentunnus", accmodel.ulkoinenTunnus);
            }
            if (accmodel.ytunnus == null || accmodel.ytunnus == "")
            {
                command.Parameters.AddWithValue("@ytunnus", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ytunnus", accmodel.ytunnus);
            }
            if (accmodel.url == null || accmodel.url == "")
            {
                command.Parameters.AddWithValue("@url", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@url", accmodel.url);
            }
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                int counted = command.ExecuteNonQuery();
                if (counted > 0)
                {
                    added = true;
                }
            }
            catch (Exception ex)
            {
                added = false;
                throw new Exception("something went wrong" + ex.Message);
            }
            conn.Close();
            return added;
        }

        // päivitetään asiakas
        public bool UpdateClient(AccountModel accmodel)
        {
            bool added = false;
            SqlConnection conn = sqlconnman.GetDatabaseConnection();
            string commandstring = "CRM_PAIVITA_ASIAKAS_SP";
            SqlCommand command = new SqlCommand(commandstring, conn);
            command.Parameters.Clear();
            if (accmodel.nimi == null || accmodel.nimi == "")
            {
                command.Parameters.AddWithValue("@nimi", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@nimi", accmodel.nimi);
            }
            if (accmodel.kayntiosoite == null || accmodel.kayntiosoite == "")
            {
                command.Parameters.AddWithValue("@kayntiosoite", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@kayntiosoite", accmodel.kayntiosoite);
            }
            if (accmodel.postiosoite == null || accmodel.postiosoite == "")
            {
                command.Parameters.AddWithValue("@postiosoite", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@postiosoite", accmodel.postiosoite);
            }
            if (accmodel.email == null || accmodel.email == "")
            {
                command.Parameters.AddWithValue("@email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@email", accmodel.email);
            }
            if (accmodel.ulkoinenTunnus == null || accmodel.ulkoinenTunnus.ToString() == "")
            {
                command.Parameters.AddWithValue("@ulkoinentunnus", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ulkoinentunnus", accmodel.ulkoinenTunnus);
            }
            if (accmodel.ytunnus == null || accmodel.ytunnus == "")
            {
                command.Parameters.AddWithValue("@ytunnus", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@ytunnus", accmodel.ytunnus);
            }
            if (accmodel.url == null || accmodel.url == "")
            {
                command.Parameters.AddWithValue("@url", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@url", accmodel.url);
            }
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                int counted = command.ExecuteNonQuery();
                if (counted > 0)
                {
                    added = true;
                }
            }
            catch (Exception ex)
            {
                added = false;
                throw new Exception("something went wrong" + ex.Message);
            }
            conn.Close();
            return added;
        }


    }
}
