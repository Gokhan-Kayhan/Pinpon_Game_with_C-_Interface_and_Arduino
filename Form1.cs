using System;                                    //  Cihad Kabaklı 2015514031 
using System.Collections.Generic;                // Gökhan Kayhan 2015513031
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace PinPon_oyun_deneme_1
{
    public partial class Form1 : Form
    {
        public static string port_name, baud_rate, player1_name, player2_name;
        public static int x_speed, y_speed;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string serial_port_names in ports)
            { comboBox1.Items.Add(serial_port_names); } 
        }


        private void button1_Click(object sender, EventArgs e)
        {           
            try
            {
                port_name = comboBox1.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No COM Port was selected \n Program is closing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);          
            }
         

            for (int i = 0; i < 100; i++)
            {
                progressBar1.Visible = true;
                progressBar1.Value += 1;
            }
     

            baud_rate = textBox3.Text;

  
            if (baud_rate != "9600" & baud_rate != "19200" & baud_rate != "38400" & baud_rate != "76800" )
            {
                MessageBox.Show("Please enter correct Baud Rate Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);               
            }


            player1_name = textBox1.Text;
            player2_name = textBox2.Text;


            if (radioButton1.Checked == true)
            {
                x_speed = 20;
                y_speed = 20;
            }

            if (radioButton2.Checked == true)
            {
                x_speed = 30;
                y_speed = 30;
            }

            if (radioButton3.Checked == true)
            {
                x_speed = 40;
                y_speed = 40;
            }

            if (radioButton1.Checked == false & radioButton2.Checked == false & radioButton3.Checked == false)
            { x_speed = 15; y_speed = 15; }


            Form2 f2 = new Form2();
            f2.ShowDialog();


            Application.Exit();
        }


        private void startingGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show( " To Start Game\n 1. Choose COM Name\n 2. Write The Proper Baud Rate(9600 etc.)\n 3. Select Game Level & Write Player Names(Optional)" );
        }                 
    }
}
