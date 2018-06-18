using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private int counter;

        private void InitializeTimer()
        {
            // Run this procedure in an appropriate event.  
            counter = 0;
            timer1.Interval = 600;
            timer1.Enabled = true;
            // Hook up timer's tick event handler.  
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (counter >= 10)
            {
                // Exit loop code.  
                timer1.Enabled = false;
                counter = 0;
            }
            else
            {

                Microsoft.Crm.Sdk.Samples.CRUDOperations cRUD = new Microsoft.Crm.Sdk.Samples.CRUDOperations();

               

                counter = counter + 1;
                label1.Text = "Procedures Run: " + counter.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Stop")
            {
                button1.Text = "Start";
                timer1.Enabled = false;
            }
            else
            {
                button1.Text = "Stop";
                timer1.Enabled = true;
            }
        }
    }
}
