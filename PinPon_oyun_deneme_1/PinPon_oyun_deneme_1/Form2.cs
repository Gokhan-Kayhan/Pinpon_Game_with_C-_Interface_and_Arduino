using System;                              
using System.Collections.Generic;
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
    public partial class Form2 : Form
    {
        int y1_coordinate, y2_coordinate;  
        int ball_x; int ball_y;
        int score1 = 0; int score2 = 0;


        private void edge_control() 
        {          
            if (Ball.Top <= label1.Bottom)
            { ball_y = ball_y * -1; }

            else if (Ball.Bottom >= label2.Top) 
            { ball_y = ball_y * -1; }

            else if (Ball.Right >= player1.Left && Ball.Top >= player1.Top && Ball.Bottom <= player1.Bottom)
                ball_x = ball_x * -1;

            else if (Ball.Left <= player2.Right && Ball.Top >= player2.Top && Ball.Bottom <= player2.Bottom)
                ball_x = ball_x * -1;
        }


        private void score_event(object sender, EventArgs e)  
        {                                         
            if (score1 < 3 && score2 < 3)
            {
                if (Ball.Left >= player1.Left)
                {
                    score2++;
                    serialPort1.Write("2");
                    serialPort1.Close();
                    Form2_Load(sender, e);
                }

                if (Ball.Right <= player2.Right)
                {
                    score1++;
                    serialPort1.Write("1");
                    serialPort1.Close();
                    Form2_Load(sender, e);
                }
            }

            else if (score1 == 3)
            {
                timer2.Stop();
                timer1.Stop();
                DialogResult option = MessageBox.Show("Please Evet to Restart \n Please Hayır to Exit", label6.Text , MessageBoxButtons.YesNo);
                serialPort1.Close();

                if (option == DialogResult.Yes)
                {
                    score1 = 0; score2 = 0;
                    Form2_Load(sender, e);
                }

                if (option == DialogResult.No)
                {
                    Application.Exit();
                }
            }

            else if (score2 == 3)
            {
                timer2.Stop();
                timer1.Stop();
                DialogResult option = MessageBox.Show("Please Evet to Restart \n Please Hayır to Exit", label5.Text , MessageBoxButtons.YesNo);
                serialPort1.Close();

                if (option == DialogResult.Yes)
                {
                    score1 = 0; score2 = 0;
                    Form2_Load(sender, e);
                }

                if (option == DialogResult.No)
                {
                    Application.Exit();
                }
            }


            label3.Text = Convert.ToString(score2);
            label4.Text = Convert.ToString(score1);

        }


        private void ball_first_location()
        {
            Ball.Location = new Point(415, 200);
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer2.Enabled = true;            

            ball_first_location();      
            
            serialPort1.PortName = Form1.port_name;                                               
            serialPort1.BaudRate = Convert.ToInt32(Form1.baud_rate);

            ball_x = Form1.x_speed;
            ball_y = Form1.y_speed;
         
            label5.Text = Form1.player1_name;
            label6.Text = Form1.player2_name;

            if (label5.Text == "")
            { label5.Text = "PLAYER 1"; }

            if (label6.Text == "")
            { label6.Text = "PLAYER 2"; }
            
            timer1.Start();
            serialPort1.Open();
        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();  
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            serialPort1.Write("b");

            string result = serialPort1.ReadLine();   

            string[] pot_values = result.Split('+');  
       
            y1_coordinate = Convert.ToInt16(pot_values[0]);
            y2_coordinate = Convert.ToInt16(pot_values[1]);
           
            player1.Top = y1_coordinate;
            player2.Top = y2_coordinate;

            serialPort1.DiscardInBuffer();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {        
            Ball.Location = new Point(Ball.Location.X + ball_x, Ball.Location.Y + ball_y);
           
            edge_control();

            score_event(sender, e);
        }
    }
}