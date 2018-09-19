// =====================================================================
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================

//<snippetCRUDOperations>
using System;
using System.ServiceModel;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security;
using System.Linq;
using System.Timers;

// These namespaces are found in the Microsoft.Xrm.Sdk.dll and 
// Microsoft.Crm.Sdk.Proxy.dll assemblies.
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.Data;
using System.Collections.Generic;
using Microsoft.Crm.Sdk.Samples.Forms;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace Microsoft.Crm.Sdk.Samples
{
    /// <summary>
    /// Demonstrates how to perform create, retrieve, update, and delete entity
    /// record operations.</summary>
    /// <remarks>
    /// At run-time, you will be given the option to delete all the
    /// entity records created by this program.</remarks>
    /// 
 
    public class CRUDOperations
    {
        SQLquery sql;


        public CRUDOperations()
        {
            sql = new SQLquery();
        }

        [STAThread]
        static public void Main(string[] args)
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }

        //<snippetCRUDOperations1>
        /// <summary>
        /// This method performs entity create, retrieve, and update operations.
        /// The delete operation is handled in the DeleteRequiredrecords() method.
        /// </summary>
        /// <param name="serviceProxy">An established connection to the Organization web service.</param>
        /// <param name="records">A collection of entity records created by this sample.</param>
        public void Run(OrganizationServiceProxy serviceProxy, EntityReferenceCollection records, CheckBox checkBox1, CheckBox checkBox2, CheckBox checkBox3, CheckBox checkBox4, CheckBox checkBox5)
        {
            // Enable early-bound entity types. This enables use of IntelliSense in Visual Studio
            // and avoids spelling errors in attribute names when using the Entity property bag.
            serviceProxy.EnableProxyTypes();

            // Here we will use the interface instead of the proxy object.
            IOrganizationService service = (IOrganizationService)serviceProxy;




            Form1 newForm = new Form1();
            

            //Console.Write("{0} {1} created, ", account.LogicalName, account.Name);

            // Retrieve the account containing several of its attributes. This results in
            // better performance compared to retrieving all attributes.
            ColumnSet colsAccount = new ColumnSet(
                new String[] { "name","emailaddress1","address1_name","accountid","modifiedon"/*,"address1_postalcode","address1_city"*/ });
            ColumnSet colsContact = new ColumnSet(
                new String[] { "fullname", "emailaddress1", "telephone1","parentcustomerid" } );
            ColumnSet colsThesis = new ColumnSet(
                new String[] { "new_name", "new_ohosid", "new_yhteyshenkilo", "new_ohjaaja" });
            QueryExpression qe = new QueryExpression();
            QueryExpression qa = new QueryExpression();
            QueryExpression qcontact = new QueryExpression();
            QueryExpression queryexpression = new QueryExpression();

            qe.EntityName = "account";
            qa.EntityName = "opportunity";
            qcontact.EntityName = "contact";
            queryexpression.EntityName = "new_opinnytety";


            qe.ColumnSet = colsAccount;
            qcontact.ColumnSet = colsContact;


            
            List<AccountModel> AccountList = new List<AccountModel>();


            List<ThesisModel> ThesisList = new List<ThesisModel>();

            List<TopicModel> TopicList = new List<TopicModel>();


            List<ContactModel> ContactList = new List<ContactModel>();

            
            EntityCollection econtact = service.RetrieveMultiple(qcontact);
            EntityCollection eaccount = service.RetrieveMultiple(qe);

            

            new_opinnytety new_Opinnytety = new new_opinnytety();






            //ALLA OLEVA FUNKTIO TOIMII, POISSA KÄYTÖSTÄ SEURAAVAN FUNKTION TEKOA VARTEN
            //checkCreateAndUpdateClients(AccountList, ec, service); Tämä oli huono funktio.
            if (checkBox1.Checked) { 
            CRMcreateOrUpdateClients(AccountList, service, colsAccount); //Tämä hyvä funktio, tämä toimii
            }
            //ALLA OLEVA FUNKTIO TOIMII
            if (checkBox2.Checked) { 
            CRMcreateOrUpdateContacts(ContactList, service, colsContact); //Tämä hyvä funktio, tämä toimii
            }
            //ALLA OLEVA FUNKTIO TOIMII
            if (checkBox3.Checked) { 
            CRMcreateOrUpdateThesis(ThesisList, service, colsThesis); //Tämä hyvä funktio, tämä toimii
            }
            if (checkBox4.Checked) { 
            OHOScreateOrUpdateClients(eaccount);
            }
            if (checkBox5.Checked) { 
            OHOScreateOrUpdateContacts(econtact);
            }
        }
        //</snippetCRUDOperations1>

        /// <summary>
        /// Create any entity records that the Run() method requires.
        /// </summary>
        public EntityReferenceCollection CreateRequiredEntityRecords(OrganizationServiceProxy service)
        {
            // For this sample, all required entity records are created in the Run() method.
            return new EntityReferenceCollection();
        }
        public void OHOScreateOrUpdateContacts(EntityCollection econtact)
        {

            foreach (var item in econtact.Entities)
            {
                //List<ContactModel> ContactList = new List<ContactModel>();
                bool nothingtoupdate = false;
                bool needstobecreated = false;
                ContactModel contact = new ContactModel();
                List<ContactModel> ContactList = new List<ContactModel>();
                if (item.Attributes.ContainsKey("emailaddress1"))
                {
                    contact.email = item.Attributes["emailaddress1"].ToString();
                }
                if (item.Attributes.ContainsKey("fullname"))
                {
                    string[] name = item.Attributes["fullname"].ToString().Split(' ');
                    contact.etunimi = name.First();
                    contact.sukunimi = name.Last();

                }
                else
                {
                    if (item.Attributes.ContainsKey("firstname"))
                    {
                        contact.etunimi = item.Attributes["firstname"].ToString();
                    }
                    if (item.Attributes.ContainsKey("lastname"))
                    {
                        contact.sukunimi = item.Attributes["lastname"].ToString();
                    }
                }
                if (item.Attributes.ContainsKey("parentcustomerid"))
                {
                    contact.yhteiso = ((EntityReference)item.Attributes["parentcustomerid"]).Id;
                }
                else
                {
                    continue;
                }
                if (item.Attributes.ContainsKey("contactid"))
                {
                    contact.ulkoinentunniste = (Guid)item.Attributes["contactid"];
                }
                if (item.Attributes.ContainsKey("telephone1"))
                {
                    contact.puhelin = item.Attributes["telephone1"].ToString();
                }
                ContactList = ContactsByName(contact);
                if (ContactList.Count() > 0)
                {
                    ContactList = ContactsByNameAndId(contact);
                    if (ContactList.Count() > 0)
                    {
                        ContactList = ContactsByNameIdParent(contact);
                        if (ContactList.Count() > 0)
                        {
                            ContactList = ContactsByNameIdParentAndEmail(contact);
                            if (ContactList.Count() > 0)
                            {
                                ContactList = ContactsByNameIdParentEmailAndTelephone(contact);
                                if (ContactList.Count() > 0)
                                {
                                    nothingtoupdate = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        needstobecreated = true;
                        ContactList = ContactsByNameEmailAndTelephone(contact);
                        if (ContactList.Count() > 0)
                        {
                            ContactList = ContactsByNameEmailTelephoneAndParent(contact);
                            if (ContactList.Count() > 0)
                            {
                                contact.ulkoinentunniste = ContactList.FirstOrDefault().ulkoinentunniste;
                            }
                            else
                            {
                                contact.ulkoinentunniste = ContactList.FirstOrDefault().ulkoinentunniste;
                                contact.yhteiso = ContactList.FirstOrDefault().yhteiso;
                            }
                        }
                    }
                    if (!nothingtoupdate && !needstobecreated)
                    {
                        sql.UpdateContact(contact);
                    }
                    else if (!nothingtoupdate && needstobecreated)
                    {
                        sql.InsertContact(contact);
                    }
                }
                else
                {
                    sql.InsertContact(contact);
                }



            }
        }
        public void OHOScreateOrUpdateClients(EntityCollection eaccount)
        {
            foreach (var item in eaccount.Entities)
            {
                List<AccountModel> AccountList = new List<AccountModel>();
                bool nothingtoupdate = false;
                AccountModel account = new AccountModel();
                if (item.Attributes.ContainsKey("emailaddress1"))
                {
                    account.email = item.Attributes["emailaddress1"].ToString();
                }
                if (item.Attributes.ContainsKey("name"))
                {
                    account.nimi = item.Attributes["name"].ToString();
                }
                if (item.Attributes.ContainsKey("accountid"))
                {
                    account.ulkoinenTunnus = (Guid)item.Attributes["accountid"];
                }
                if (item.Attributes.ContainsKey("address1_name"))
                {
                    account.kayntiosoite = item.Attributes["address1_name"].ToString();
                }
                AccountList = AccountsByName(account);
                if (AccountList.Count() > 0)
                {
                    AccountList = AccountsByNameAndId(account);
                    if (AccountList.Count() > 0)
                    {
                        AccountList = AccountsByNameIdAndAddress(account);
                        if (AccountList.Count() > 0)
                        {
                            nothingtoupdate = true;
                        }
                    }
                    else
                    {
                        AccountList = AccountsByNameIdAndAddress(account);
                        if (AccountList.Count() == 0)
                        {
                            if (item.Attributes.ContainsKey("address1_name"))
                            {
                                account.kayntiosoite = item.Attributes["address1_name"].ToString();
                            }
                            else
                            {
                                account.kayntiosoite = null;
                            }
                        }
                    }
                    if (!nothingtoupdate)
                    {
                        sql.UpdateClient(account);
                    }
                }
                else
                {
                    sql.InsertClient(account);
                }



            }
        }


        public void CRMcreateOrUpdateContacts(List<ContactModel> ContactList, IOrganizationService service, ColumnSet colsContact)
        {
            foreach (var item in ContactList)
            {
                bool nothingtoupdate = false;
                bool needstobecreated = false;
                Contact contact = new Contact
                {
                    FullName = item.etunimi + " " + item.sukunimi,
                    FirstName = item.etunimi,
                    LastName = item.sukunimi,
                    EMailAddress1 = item.email,
                    ParentCustomerId = RetrieveEntityById(service, "account", item.yhteiso).ToEntityReference(),
                    ContactId = item.ulkoinentunniste,
                    Telephone1 = item.puhelin

                };

                var query = new QueryExpression("contact");
                query.Criteria.AddCondition("firstname", ConditionOperator.Equal, item.etunimi);
                query.Criteria.AddCondition("lastname", ConditionOperator.Equal, item.sukunimi);
                query.ColumnSet = colsContact;
                EntityCollection users = service.RetrieveMultiple(query);
                if (users.Entities.Count() > 0)
                {
                    query.Criteria.AddCondition("contactid", ConditionOperator.Equal, item.ulkoinentunniste);

                    users = service.RetrieveMultiple(query);
                    if (users.Entities.Count() > 0)
                    {
                        query.Criteria.AddCondition("parentcustomerid", ConditionOperator.Equal, item.yhteiso);
                        users = service.RetrieveMultiple(query);
                        if (users.Entities.Count() > 0)
                        {
                            query.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, item.email);
                            users = service.RetrieveMultiple(query);
                            if (users.Entities.Count() > 0)
                            {
                                query.Criteria.AddCondition("telephone1", ConditionOperator.Equal, item.puhelin);
                                users = service.RetrieveMultiple(query);
                                if (users.Entities.Count() > 0)
                                {
                                    nothingtoupdate = true;
                                }

                            }
                        }

                    }
                    else
                    {
                        query.Criteria.Conditions.Remove(new ConditionExpression("contactid", ConditionOperator.Equal));
                        query.Criteria.AddCondition("emailaddress1", ConditionOperator.Equal, item.email);
                        query.Criteria.AddCondition("telephone1", ConditionOperator.Equal, item.puhelin);
                        needstobecreated = true;
                        users = service.RetrieveMultiple(query);
                        if (users.Entities.Count() > 0)
                        {
                            query.Criteria.AddCondition("parentcustomerid", ConditionOperator.Equal, item.yhteiso);
                            users = service.RetrieveMultiple(query);
                            if (users.Entities.Count() > 0)
                            {
                                contact.ContactId = (Guid)users.Entities.FirstOrDefault().Attributes["contactid"];

                            }
                            else
                            {

                                contact.ContactId = (Guid)users.Entities.FirstOrDefault().Attributes["contactid"];
                                contact.ParentCustomerId = (EntityReference)users.Entities.FirstOrDefault().Attributes["parentcustomerid"];
                            }

                        }

                    }
                    if (!nothingtoupdate && !needstobecreated)
                    {
                        service.Update(contact);
                    }
                    else if (!nothingtoupdate && needstobecreated)
                    {
                        service.Create(contact);
                    }
                }
                else
                {
                    service.Create(contact);
                }

            }
        }
        public void CRMcreateOrUpdateClients(List<AccountModel> AccountList, IOrganizationService service, ColumnSet colsAccount)
        {
            foreach (var item in AccountList)
            {
                bool nothingtoupdate = false;
                Account account = new Account
                {
                    Name = item.nimi,
                    EMailAddress1 = item.email,
                    Address1_Name = item.kayntiosoite,
                    Id = item.ulkoinenTunnus,
                    ModifiedOn = item.paivitetty

                };

                var query = new QueryExpression("account");
                query.Criteria.AddCondition("name", ConditionOperator.Equal, item.nimi);
                query.ColumnSet = colsAccount;
                EntityCollection users = service.RetrieveMultiple(query);
                if (users.Entities.Count() > 0)
                {
                    query.Criteria.AddCondition("accountid", ConditionOperator.Equal, item.ulkoinenTunnus);

                    users = service.RetrieveMultiple(query);
                    if (users.Entities.Count() > 0)
                    {
                        query.Criteria.AddCondition("address1_name", ConditionOperator.Equal, item.kayntiosoite);
                        users = service.RetrieveMultiple(query);
                        if (users.Entities.Count() > 0)
                        {
                            nothingtoupdate = true;
                        }

                    }
                    else
                    {
                        query.Criteria.AddCondition("address1_name", ConditionOperator.Equal, item.kayntiosoite);
                        users = service.RetrieveMultiple(query);
                        account.Id = (Guid)users.Entities.FirstOrDefault().Attributes["accountid"];
                        if (users.Entities.Count() == 0)
                        {
                            account.Address1_Name = item.kayntiosoite;
                        } 

                    }
                    if (!nothingtoupdate)
                    {
                        service.Update(account);
                    }
                }
                else
                {
                    service.Create(account);
                }

            }
            
        }
        public void CRMcreateOrUpdateThesis(List<ThesisModel> ThesisList, IOrganizationService service, ColumnSet colsThesis)
        {
            foreach (var item in ThesisList)
            {

                new_opinnytety thesis = new new_opinnytety
                {
                    Name = item.aihe,
                    OhosId = item.thesisID,
                    Ohjaaja = item.ohjaaja,
                    OpiskelijaID = item.opiskelijaID,
                    Koulutusohjelma = item.koulutusohjelma,
                    Toimeksiantaja = item.ta_nimi,
                    Yhteyshenkilo = item.yhteyshenkilo

                };

                var query = new QueryExpression("new_opinnytety");
                query.Criteria.AddCondition("new_name", ConditionOperator.Equal, item.aihe);
                query.Criteria.AddCondition("new_ohosid", ConditionOperator.Equal, item.thesisID);
                query.ColumnSet = colsThesis;
                EntityCollection users = service.RetrieveMultiple(query);
                if (users.Entities.Count() > 0)
                {
                    query.Criteria.AddCondition("new_yhteyshenkilo", ConditionOperator.Equal, item.yhteyshenkilo);
                    users = service.RetrieveMultiple(query);
                    thesis.OpinnytetyId = (Guid)users.Entities.FirstOrDefault().Attributes["new_opinnytetyid"];
                    if (users.Entities.Count() > 0)
                    {
                        query.Criteria.AddCondition("new_ohjaaja", ConditionOperator.Equal, item.ohjaaja);
                        users = service.RetrieveMultiple(query);
                        if (users.Entities.Count() == 0)
                        {
                            service.Update(thesis);
                        }
                    }
                    else
                    {
                        service.Update(thesis);
                    }
                }
                else
                {
                    service.Create(thesis);
                }

            }
        }
        public void checkCreateAndUpdateClients(List<AccountModel> AccountList, EntityCollection ec, IOrganizationService service)
        {
            foreach (var item in AccountList)
            {
                bool? clientfound = null;
                Account UpdateAccount = new Account();
                foreach (var acc in ec.Entities)
                {

                    if (acc.Attributes.Keys.Any(key =>
                        key.Equals("name", StringComparison.InvariantCultureIgnoreCase)))
                    {
                        if (acc.Attributes["name"].ToString() == item.nimi)
                        {
                            UpdateAccount.Attributes.Add("name", acc.Attributes["name"]);
                            if (acc.Attributes.Keys.Any(key =>
                            key.Equals("emailaddress1", StringComparison.InvariantCultureIgnoreCase)) && acc.Attributes["emailaddress1"] == item.email)
                            {
                                UpdateAccount.Attributes.Add("emailaddress1", acc.Attributes["emailaddress1"]);
                            }
                            else
                            {
                                UpdateAccount.Attributes.Add("emailaddress1", item.email);
                            }

                            if (acc.Attributes.Keys.Any(key =>
                            key.Equals("address1_name", StringComparison.InvariantCultureIgnoreCase)) && acc.Attributes["address1_name"] == item.kayntiosoite)
                            {
                                UpdateAccount.Attributes.Add("address1_name", acc.Attributes["address1_name"]);
                            }
                            else
                            {
                                UpdateAccount.Attributes.Add("address1_name", item.kayntiosoite);
                            }

                            if (acc.Attributes.Keys.Any(key =>
                            key.Equals("modifiedon", StringComparison.InvariantCultureIgnoreCase)) && acc.Attributes["modifiedon"].ToString() == item.paivitetty.ToString())
                            {
                                UpdateAccount.Attributes.Add("modifiedon", acc.Attributes["modifiedon"]);
                            }
                            else
                            {
                                UpdateAccount.Attributes.Add("modifiedon", item.paivitetty);
                            }

                            sql.setID(item.ulkoinenTunnus, (Guid)acc.Attributes["accountid"]);

                            UpdateAccount.Attributes["accountid"] = acc.Attributes["accountid"];
                            clientfound = true;
                            break;

                        }
                        else
                        {
                            clientfound = false;
                        }
                    }
                }
                if (clientfound == true)
                {
                    service.Update(UpdateAccount);
                }
                else if (clientfound == false)
                {
                    Account account = new Account
                    {
                        Name = item.nimi,
                        EMailAddress1 = item.email,
                        Address1_Name = item.kayntiosoite,
                        Id = item.ulkoinenTunnus,
                        ModifiedOn = item.paivitetty
                    };
                    service.Create(account);
                }

            }
        }

        public Entity RetrieveEntityById(IOrganizationService service, string strEntityLogicalName, Guid guidEntityId)

        {

            Entity RetrievedEntityById = service.Retrieve(strEntityLogicalName, guidEntityId, new ColumnSet(true)); //it will retrieve the all attrributes

            return RetrievedEntityById;

        }

        /// <summary>
        /// Delete all remaining entity records that were created by this sample.
        /// <param name="prompt">When true, the user is prompted whether 
        /// the records created in this sample should be deleted; otherwise, false.</param>
        /// </summary>
        public void DeleteEntityRecords(OrganizationServiceProxy service,
                                        EntityReferenceCollection records, bool prompt)
        {
            bool deleteRecords = true;

            if (prompt)
            {
                Console.WriteLine("\nDo you want these entity records deleted? (y/n) [y]: ");
                String answer = Console.ReadLine();

                deleteRecords = (answer.StartsWith("y") || answer.StartsWith("Y") || answer == String.Empty);
            }

            if (deleteRecords)
            {
                while (records.Count > 0)
                {
                    EntityReference entityRef = records[records.Count - 1];
                    Console.WriteLine("Deleting {0} '{1}' ...", entityRef.LogicalName, entityRef.Name);
                    service.Delete(entityRef.LogicalName, entityRef.Id);
                    records.Remove(entityRef);
                }

                Console.WriteLine("Entity records have been deleted.");
            }
        }

        /// Handle a thrown exception.
        /// </summary>
        /// <param name="ex">An exception.</param>
        private static void HandleException(Exception e)
        {
            // Display the details of the exception.
            Console.WriteLine("\n" + e.Message);
            Console.WriteLine(e.StackTrace);

            if (e.InnerException != null) HandleException(e.InnerException);
        }

        // Haetaan tietokannasta kaikki asiakkaat (eli yritykset, joille opinnäytetöitä tehdään)
        public List<AccountModel> Accounts()
        {
            // Luodaan lista, joka sisältää AccountModel-olion
            List<AccountModel> accountList = new List<AccountModel>();

            DataTable account = sql.GetAccount();


            foreach(DataRow row in account.Rows) { 
                AccountModel accountMod = new AccountModel();
                accountMod.nimi = row["nimi"].ToString();
                accountMod.postiosoite = row["postiosoite"].ToString();
                accountMod.kayntiosoite = row["kayntiosoite"].ToString();
                accountMod.ytunnus = row["ytunnus"].ToString();
                accountMod.url = row["url"].ToString();
                accountMod.email = row["email"].ToString();
                accountMod.ulkoinenTunnus = (Guid)row["ulkoinentunnus"];
                accountMod.paivitetty = (DateTime)row["paivitetty"];
                accountList.Add(accountMod);
                
            }

            

            return accountList;
        }

        // Haetaan tietokannasta kaikki asiakkaat (eli yritykset, joille opinnäytetöitä tehdään)
        public List<AccountModel> AccountsByName(AccountModel account)
        {
            // Luodaan lista, joka sisältää AccountModel-olion
            List<AccountModel> accountList = new List<AccountModel>();

            DataTable accountdt = sql.GetClientWithName(account.nimi);


            foreach (DataRow row in accountdt.Rows)
            {
                AccountModel accountMod = new AccountModel();
                accountMod.nimi = row["nimi"].ToString();
                accountMod.postiosoite = row["postiosoite"].ToString();
                accountMod.kayntiosoite = row["kayntiosoite"].ToString();
                accountMod.ytunnus = row["ytunnus"].ToString();
                accountMod.url = row["url"].ToString();
                accountMod.email = row["email"].ToString();
                accountMod.ulkoinenTunnus = (Guid)row["ulkoinentunnus"];
                accountMod.paivitetty = (DateTime)row["paivitetty"];
                accountList.Add(accountMod);

            }



            return accountList;
        }

        // Haetaan tietokannasta kaikki asiakkaat (eli yritykset, joille opinnäytetöitä tehdään)
        public List<AccountModel> AccountsByNameAndId(AccountModel account)
        {
            // Luodaan lista, joka sisältää AccountModel-olion
            List<AccountModel> accountList = new List<AccountModel>();

            DataTable accountdt = sql.GetClientWithNameAndAccountID(account.nimi, account.ulkoinenTunnus);


            foreach (DataRow row in accountdt.Rows)
            {
                AccountModel accountMod = new AccountModel();
                accountMod.nimi = row["nimi"].ToString();
                accountMod.postiosoite = row["postiosoite"].ToString();
                accountMod.kayntiosoite = row["kayntiosoite"].ToString();
                accountMod.ytunnus = row["ytunnus"].ToString();
                accountMod.url = row["url"].ToString();
                accountMod.email = row["email"].ToString();
                accountMod.ulkoinenTunnus = (Guid)row["ulkoinentunnus"];
                accountMod.paivitetty = (DateTime)row["paivitetty"];
                accountList.Add(accountMod);

            }



            return accountList;
        }

        

        public List<AccountModel> AccountsByNameIdAndAddress(AccountModel account)
        {
            // Luodaan lista, joka sisältää AccountModel-olion
            List<AccountModel> accountList = new List<AccountModel>();

            DataTable accountdt = sql.GetClientWithNameAndAccountIDAndAddress(account.nimi, account.ulkoinenTunnus, account.kayntiosoite);


            foreach (DataRow row in accountdt.Rows)
            {
                AccountModel accountMod = new AccountModel();
                accountMod.nimi = row["nimi"].ToString();
                accountMod.postiosoite = row["postiosoite"].ToString();
                accountMod.kayntiosoite = row["kayntiosoite"].ToString();
                accountMod.ytunnus = row["ytunnus"].ToString();
                accountMod.url = row["url"].ToString();
                accountMod.email = row["email"].ToString();
                accountMod.ulkoinenTunnus = (Guid)row["ulkoinentunnus"];
                accountMod.paivitetty = (DateTime)row["paivitetty"];
                accountList.Add(accountMod);

            }



            return accountList;
        }

        public List<ThesisModel> Thesis()
        {
            List<ThesisModel> thesisList = new List<ThesisModel>();

            DataTable id = sql.GetThesisId();
            foreach(DataRow row in id.Rows)
            {
                DataTable thesis = sql.GetThesis(Convert.ToInt32(row["sopimusID"]));
                List<string> tempList = new List<string>();
                if(thesis.Rows.Count > 0)
                {
                    ThesisModel thesisModel = new ThesisModel();
                    foreach(DataRow thesisrow in thesis.Rows) { 
                    tempList.Add(thesisrow["kentta_sisalto"].ToString());
                    }
                    thesisModel.koulutusohjelma = tempList[0];
                    thesisModel.opiskelijaID = tempList[1];
                    thesisModel.aihe = tempList[2];
                    thesisModel.ta_nimi = tempList[3];
                    thesisModel.ohjaaja = tempList[5];
                    thesisModel.yhteyshenkilo = tempList[4];
                    thesisModel.thesisID = row["sopimusID"].ToString();
                    thesisList.Add(thesisModel);

                }
            }

            return thesisList;
        }
        public Entity GetEntity(EntityReference e)
        {
            return new Entity(e.LogicalName) { Id = e.Id };
        }

        // Haetaan tietokannasta kaikki yhteyshenkilöt
        public List<ContactModel> Contacts()
        {
            List<ContactModel> contactList = new List<ContactModel>();
            DataTable contacts = sql.GetContact();

            var i = contacts.Rows.Count;

            while (i > 0)
            {
                ContactModel contactMod = new ContactModel();
                DataRow rows = contacts.Rows[i - 1];
                contactMod.sukunimi = rows["Sukunimi"].ToString();
                contactMod.etunimi = rows["Etunimi"].ToString();
                contactMod.puhelin = rows["Puhelin"].ToString();
                contactMod.email = rows["email"].ToString();
                contactMod.tehtavanimike = rows["tehtavanimike"].ToString();
                contactMod.yhteiso = (Guid)rows["yhteiso"];
                contactMod.paivitetty = rows["paivitetty"].ToString();
                contactMod.ulkoinentunniste = (Guid)rows["ulkoinentunniste"];
                contactList.Add(contactMod);
                i = i - 1;

            }

            return contactList;
        }


        public List<ContactModel> ContactsByName(ContactModel contact)
        {
            List<ContactModel> contactList = new List<ContactModel>();
            DataTable contacts = sql.GetContactWithName(contact.etunimi, contact.sukunimi);

            var i = contacts.Rows.Count;

            while (i > 0)
            {
                ContactModel contactMod = new ContactModel();
                DataRow rows = contacts.Rows[i - 1];
                contactMod.sukunimi = rows["Sukunimi"].ToString();
                contactMod.etunimi = rows["Etunimi"].ToString();
                contactMod.puhelin = rows["Puhelin"].ToString();
                contactMod.email = rows["email"].ToString();
                contactMod.tehtavanimike = rows["tehtavanimike"].ToString();
                contactMod.yhteiso = (Guid)rows["yhteiso"];
                contactMod.paivitetty = rows["paivitetty"].ToString();
                contactMod.ulkoinentunniste = (Guid)rows["ulkoinentunniste"];
                contactList.Add(contactMod);
                i = i - 1;

            }

            return contactList;
        }
        public List<ContactModel> ContactsByNameAndId(ContactModel contact)
        {
            List<ContactModel> contactList = new List<ContactModel>();
            DataTable contacts = sql.GetContactWithID(contact.etunimi, contact.sukunimi, contact.ulkoinentunniste);

            var i = contacts.Rows.Count;

            while (i > 0)
            {
                ContactModel contactMod = new ContactModel();
                DataRow rows = contacts.Rows[i - 1];
                contactMod.sukunimi = rows["Sukunimi"].ToString();
                contactMod.etunimi = rows["Etunimi"].ToString();
                contactMod.puhelin = rows["Puhelin"].ToString();
                contactMod.email = rows["email"].ToString();
                contactMod.tehtavanimike = rows["tehtavanimike"].ToString();
                contactMod.yhteiso = (Guid)rows["yhteiso"];
                contactMod.paivitetty = rows["paivitetty"].ToString();
                contactMod.ulkoinentunniste = (Guid)rows["ulkoinentunniste"];
                contactList.Add(contactMod);
                i = i - 1;

            }

            return contactList;
        }

        public List<ContactModel> ContactsByNameIdParent(ContactModel contact)
        {
            List<ContactModel> contactList = new List<ContactModel>();
            DataTable contacts = sql.GetContactWithParentID(contact.etunimi, contact.sukunimi, contact.ulkoinentunniste, contact.yhteiso);

            var i = contacts.Rows.Count;

            while (i > 0)
            {
                ContactModel contactMod = new ContactModel();
                DataRow rows = contacts.Rows[i - 1];
                contactMod.sukunimi = rows["Sukunimi"].ToString();
                contactMod.etunimi = rows["Etunimi"].ToString();
                contactMod.puhelin = rows["Puhelin"].ToString();
                contactMod.email = rows["email"].ToString();
                contactMod.tehtavanimike = rows["tehtavanimike"].ToString();
                contactMod.yhteiso = (Guid)rows["yhteiso"];
                contactMod.paivitetty = rows["paivitetty"].ToString();
                contactMod.ulkoinentunniste = (Guid)rows["ulkoinentunniste"];
                contactList.Add(contactMod);
                i = i - 1;

            }

            return contactList;
        }

        public List<ContactModel> ContactsByNameIdParentAndEmail(ContactModel contact)
        {
            List<ContactModel> contactList = new List<ContactModel>();
            DataTable contacts = sql.GetContactWithEmail(contact.etunimi, contact.sukunimi, contact.ulkoinentunniste,contact.yhteiso, contact.email);

            var i = contacts.Rows.Count;

            while (i > 0)
            {
                ContactModel contactMod = new ContactModel();
                DataRow rows = contacts.Rows[i - 1];
                contactMod.sukunimi = rows["Sukunimi"].ToString();
                contactMod.etunimi = rows["Etunimi"].ToString();
                contactMod.puhelin = rows["Puhelin"].ToString();
                contactMod.email = rows["email"].ToString();
                contactMod.tehtavanimike = rows["tehtavanimike"].ToString();
                contactMod.yhteiso = (Guid)rows["yhteiso"];
                contactMod.paivitetty = rows["paivitetty"].ToString();
                contactMod.ulkoinentunniste = (Guid)rows["ulkoinentunniste"];
                contactList.Add(contactMod);
                i = i - 1;

            }

            return contactList;
        }
        public List<ContactModel> ContactsByNameIdParentEmailAndTelephone(ContactModel contact)
        {
            List<ContactModel> contactList = new List<ContactModel>();
            DataTable contacts = sql.GetContactWithPhone(contact.etunimi, contact.sukunimi, contact.ulkoinentunniste, contact.yhteiso,contact.email,contact.puhelin);

            var i = contacts.Rows.Count;

            while (i > 0)
            {
                ContactModel contactMod = new ContactModel();
                DataRow rows = contacts.Rows[i - 1];
                contactMod.sukunimi = rows["Sukunimi"].ToString();
                contactMod.etunimi = rows["Etunimi"].ToString();
                contactMod.puhelin = rows["Puhelin"].ToString();
                contactMod.email = rows["email"].ToString();
                contactMod.tehtavanimike = rows["tehtavanimike"].ToString();
                contactMod.yhteiso = (Guid)rows["yhteiso"];
                contactMod.paivitetty = rows["paivitetty"].ToString();
                contactMod.ulkoinentunniste = (Guid)rows["ulkoinentunniste"];
                contactList.Add(contactMod);
                i = i - 1;

            }

            return contactList;
        }

        public List<ContactModel> ContactsByNameEmailAndTelephone(ContactModel contact)
        {
            List<ContactModel> contactList = new List<ContactModel>();
            DataTable contacts = sql.GetContactWithNameEmailPhone(contact.etunimi, contact.sukunimi, contact.email, contact.puhelin);

            var i = contacts.Rows.Count;

            while (i > 0)
            {
                ContactModel contactMod = new ContactModel();
                DataRow rows = contacts.Rows[i - 1];
                contactMod.sukunimi = rows["Sukunimi"].ToString();
                contactMod.etunimi = rows["Etunimi"].ToString();
                contactMod.puhelin = rows["Puhelin"].ToString();
                contactMod.email = rows["email"].ToString();
                contactMod.tehtavanimike = rows["tehtavanimike"].ToString();
                contactMod.yhteiso = (Guid)rows["yhteiso"];
                contactMod.paivitetty = rows["paivitetty"].ToString();
                contactMod.ulkoinentunniste = (Guid)rows["ulkoinentunniste"];
                contactList.Add(contactMod);
                i = i - 1;

            }

            return contactList;
        }
        public List<ContactModel> ContactsByNameEmailTelephoneAndParent(ContactModel contact)
        {
            List<ContactModel> contactList = new List<ContactModel>();
            DataTable contacts = sql.GetContactWithNameEmailPhoneParent(contact.etunimi, contact.sukunimi, contact.email, contact.puhelin, contact.yhteiso);

            var i = contacts.Rows.Count;

            while (i > 0)
            {
                ContactModel contactMod = new ContactModel();
                DataRow rows = contacts.Rows[i - 1];
                contactMod.sukunimi = rows["Sukunimi"].ToString();
                contactMod.etunimi = rows["Etunimi"].ToString();
                contactMod.puhelin = rows["Puhelin"].ToString();
                contactMod.email = rows["email"].ToString();
                contactMod.tehtavanimike = rows["tehtavanimike"].ToString();
                contactMod.yhteiso = (Guid)rows["yhteiso"];
                contactMod.paivitetty = rows["paivitetty"].ToString();
                contactMod.ulkoinentunniste = (Guid)rows["ulkoinentunniste"];
                contactList.Add(contactMod);
                i = i - 1;

            }

            return contactList;
        }
        // Haetaan tietokannasta kaikki opinnäytetyön aiheet
        public List<TopicModel> Topics()
        {
            List<TopicModel> topicList = new List<TopicModel>();
            DataTable topics = sql.GetTopics();

            var i = topics.Rows.Count;

            while (i > 0)
            {
                TopicModel topicMod = new TopicModel();
                DataRow row = topics.Rows[i - 1];

                topicMod.koulutusohjelma = row["koulutusohjelma"].ToString();
                topicMod.aihe = row["aihe"].ToString();
                topicMod.toimeksiantaja = row["toimeksiantaja"].ToString();
                topicMod.puhelin = row["puhelin"].ToString();
                topicMod.email = row["email"].ToString();
                i = i - 1;

                topicList.Add(topicMod);
            }

            return topicList;
        }
    }
}
//</snippetCRUDOperations>


// Siirsin tämän vertailu metodin tänne alas, jos tuo uusi versio ei skulaa niin on edes joku mikä toimii puoliksi

//foreach (var acc in econtact.Entities)
//{

//    if (acc.Attributes.Keys.Any(key =>
//        key.Equals("fullname", StringComparison.InvariantCultureIgnoreCase)))
//    {
//        Entity chck = GetEntity(RetrieveEntityById(service, "account", item.yhteiso).ToEntityReference());

//        if (acc.Attributes["fullname"].ToString() == item.etunimi + " " + item.sukunimi && (acc.Attributes.Keys.Any(key =>
//            key.Equals("parentcustomerid", StringComparison.InvariantCultureIgnoreCase)) && GetEntity(RetrieveEntityById(service, "contact", item.ulkoinentunniste).ToEntityReference()).Id == item.ulkoinentunniste))
//        {


//            UpdateAccount.Attributes.Add("firstname", item.etunimi);
//            UpdateAccount.Attributes.Add("lastname", item.sukunimi);
//            if (acc.Attributes.Keys.Any(key =>
//            key.Equals("emailaddress1", StringComparison.InvariantCultureIgnoreCase)) && Convert.ToString(acc.Attributes["emailaddress1"]) == item.email)
//            {
//                UpdateAccount.Attributes.Add("emailaddress1", acc.Attributes["emailaddress1"]);
//            }
//            else
//            {
//                UpdateAccount.Attributes.Add("emailaddress1", item.email);
//            }

//            if (acc.Attributes.Keys.Any(key =>
//            key.Equals("parentcustomerid", StringComparison.InvariantCultureIgnoreCase)) && GetEntity(RetrieveEntityById(service, "account", item.yhteiso).ToEntityReference()).Id == item.yhteiso)
//            {
//                UpdateAccount.Attributes.Add("parentcustomerid", acc.Attributes["parentcustomerid"]);
//            }
//            else
//            {
//                UpdateAccount.Attributes.Add("parentcustomerid", item.yhteiso);
//            }




//            UpdateAccount.Attributes["contactid"] = acc.Attributes["contactid"];
//            clientfound = true;
//            break;

//        }
//        else
//        {
//            clientfound = false;
//        }
//    }
//}
//if (clientfound == true)
//{
//    service.Update(UpdateAccount);
//}
//else if (clientfound == false)
//{
//    Contact account = new Contact
//    {
//        FullName = item.etunimi + " " + item.sukunimi,
//        FirstName = item.etunimi,
//        LastName = item.sukunimi,
//        EMailAddress1 = item.email,
//        ParentCustomerId = RetrieveEntityById(service, "account", item.yhteiso).ToEntityReference(),
//        ContactId = item.ulkoinentunniste

//    };
//    service.Create(account);
//}