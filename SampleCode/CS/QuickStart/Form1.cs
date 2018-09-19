using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Crm.Sdk.Samples;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;

using Microsoft.Crm.Sdk.Messages;



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Timer MyTimer = new Timer();
        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            checkBox1.Appearance = Appearance.Button;
            checkBox2.Appearance = Appearance.Button;
            checkBox3.Appearance = Appearance.Button;
            checkBox4.Appearance = Appearance.Button;
            checkBox5.Appearance = Appearance.Button;
        }

        private void MyTimer_Tick(object sender, EventArgs e, OrganizationServiceProxy serviceProxy, EntityReferenceCollection records)
        {
            CRUDOperations app = new CRUDOperations();
            app.Run(serviceProxy, records, checkBox1, checkBox2, checkBox3, checkBox4,checkBox5);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Start_Api(textBox1.Text, true);
            (sender as Button).Enabled = false;
            button2.Enabled = true;
            textBox1.Enabled = false;
        }
        
        private void Start_Api(string var, bool start)
        {
            // The connection to the Organization web service.
            OrganizationServiceProxy serviceProxy = null;
            EntityReferenceCollection records = null;
            
                // Obtain the target organization's web address and client logon credentials
                // from the user by using a helper class.
                ServerConnection serverConnect = new ServerConnection();
                ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

                // Establish an authenticated connection to the Organization web service. 
                serviceProxy = new OrganizationServiceProxy(config.OrganizationUri, config.HomeRealmUri,
                                                            config.Credentials, config.DeviceCredentials);

                CRUDOperations app = new CRUDOperations();

                // Create any records that must exist in the database. These record references are
                // stored in a collection so the records can be deleted later.
                records =
                    app.CreateRequiredEntityRecords(serviceProxy);


                // Enable early-bound entity types. This enables use of IntelliSense in Visual Studio
                // and avoids spelling errors in attribute names when using the Entity property bag.
                serviceProxy.EnableProxyTypes();

                // Here we will use the interface instead of the proxy object.
                IOrganizationService service = (IOrganizationService)serviceProxy;

                // Display information about the logged on user.
                Guid userid = ((WhoAmIResponse)service.Execute(new WhoAmIRequest())).UserId;
                SystemUser systemUser = (SystemUser)service.Retrieve("systemuser", userid,
                    new ColumnSet(new string[] { "firstname", "lastname" }));
                Console.WriteLine("Logged on user is {0} {1}.", systemUser.FirstName, systemUser.LastName);

                // Retrieve the version of Microsoft Dynamics CRM.
                RetrieveVersionRequest versionRequest = new RetrieveVersionRequest();
                RetrieveVersionResponse versionResponse =
                    (RetrieveVersionResponse)service.Execute(versionRequest);
                Console.WriteLine("Microsoft Dynamics CRM version {0}.", versionResponse.Version);
                // Perform the primary operation of this sample.

            

            // Some exceptions to consider catching.
            //<snippetCRUDOperations3>

            //</snippetCRUDOperations3>

            
            if (start)
            {
            MyTimer.Interval = (Convert.ToInt32(var) * 60 * 1000); // 1 mins
            MyTimer.Tick += (sender2, e2) => MyTimer_Tick(sender2, e2, serviceProxy, records);
            MyTimer.Start();
            } else
            {
                MyTimer.Stop();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            (sender as Button).Enabled = false;
            textBox1.Enabled = true;
            Start_Api(textBox1.Text, false);
            
               
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            


        }


    }
}
