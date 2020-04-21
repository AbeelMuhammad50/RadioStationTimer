using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using Microsoft.Win32;

namespace TimerClock
{
    public partial class Form1 : Form
    {
        // Timer to handle time in App
        System.Timers.Timer t;

        // Variables to Store Hours Minutes and Seconds
        int min=0, s=0;

        // Notification 1 time
        int notification1Time = 135;

        FontDialog fd = new FontDialog();

        public Form1()
        {
            InitializeComponent();
        }

        // When main Form is loaded
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 500;
            timer1.Enabled = true;

            // Sets Values of Progress Bar
            this.circularProgressBar1.Value = 0;
            this.circularProgressBar1.Minimum = 0;
            this.circularProgressBar1.Maximum = 3600;

            // Initializes Timer
            t = new System.Timers.Timer();
            t.Interval = 1000;

            // When 1 Second is Elapsed
            t.Elapsed += OnTimeEvent;

            //this.pictureBox1.Hide();
            this.panel1.BackColor = Color.Black;
            this.textBox1.Hide();
            this.notificationTimerlabel.Hide();
        }

        // When Form is Closed
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Stop();
            timer1.Stop();
        }

        // Blinks Label of notification
        private void timer1_Tick(object sender, EventArgs e)
        {
            // label text changes from 'Not Connected' to 'Verifying'
            if (textBox1.BackColor == Color.Red)
            {
                textBox1.BackColor = Color.FromArgb(229,172,0);
            }

            //label text changes from 'Verifying' to 'Connected'
            else if (textBox1.BackColor == Color.FromArgb(229, 172, 0))
            {
                textBox1.BackColor = Color.Red;
            }

            //initial Condition (will execute)
            else
            {
                textBox1.BackColor = Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t.Start();
        }

        // Changes Font and Size of Notification
        private void fontButton_Click(object sender, EventArgs e)
        {

            this.showNotification();
            timer1.Enabled = false;
            
            fd.Font = textBox1.Font;
            fd.AllowSimulations = true;

            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                textBox1.Font = fd.Font;
            }
        }

        // When Notification Appears Hides Picture Box
        private void showNotification()
        {
            timer1.Enabled = true;
            this.pictureBox1.Hide();
            this.notificationTimerlabel.Show();
            this.textBox1.Show();
        }

        // When 1 Second is Elapse Following Actions Take Place
        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                //Increments in Seconds
               //s += 10;


               // // If Seconds are More than 60
               // // Then Increments in Minutes
               // if (s == 60)
               // {
               //     s = 0;
               //     this.min += 1;
               // }

               // // If Minutes are More than 60
               // // Then Increments in Hours
               // if (min == 60)
               // {
               //     min = 0;
               //     s = 0;
               // }

                // Sets Seconds and minutes with System Seconds and minutes
                s = DateTime.Now.Second;
                min = DateTime.Now.Minute;

                // Shows System time
                this.timelabel.Text = DateTime.Now.ToString("HH:mm");
                this.seclabel.Text= DateTime.Now.ToString("ss");

                // Shows System Date
                this.dayLabel.Text = DateTime.Now.ToString("ddd");
                this.monthdayLabel.Text = DateTime.Now.ToString("dd");
                this.monthLabel.Text = DateTime.Now.ToString("MMM");

                
                // Increases Value of Progress Clock
                this.circularProgressBar1.Value = (min * 60) + s;
                this.circularProgressBar1.Update();
                this.circularProgressBar1.Text = (this.min).ToString().PadLeft(2, '0') + ":"
                    + (this.s).ToString().PadLeft(2, '0');

            
                // Checks for 1st Notification (0-2:15 Minutes)
                if ((this.min * 60) + s >= 0 && (this.min * 60) + s <= 135)
                {
                    showNotification();
                    this.textBox1.Text = "YOU WILL BE LIVE IN";
                    this.notificationTimerlabel.Text= (notification1Time / 60).ToString().PadLeft(2, '0') + ":" + (notification1Time % 60).ToString().PadLeft(2, '0');
                    notification1Time--;
                }

                // Checks for 2nd Notification (19-21 Minutes)
                else if (this.min >= 19 && this.min < 21)
                {
                    showNotification();
                    //Blink();
                    this.textBox1.Text = "Go TO BREAK IN";
                    this.notificationTimerlabel.Text = (20 - min).ToString().PadLeft(2, '0') + ":" + (60 - s).ToString().PadLeft(2, '0');
                }

                // Checks for 3rd Notification (39-41 Minutes)
                else if (this.min >= 39 && this.min < 41)
                {
                    showNotification();
                    //Blink();
                    this.textBox1.Text = "Go TO BREAK IN";
                    this.notificationTimerlabel.Text = (40 - min).ToString().PadLeft(2, '0') + ":" + (60 - s).ToString().PadLeft(2, '0');
                }

                // Checks for 4th Notification (54-55 Minutes)
                else if (this.min >= 54 && this.min < 55)
                {
                    showNotification();
                    this.textBox1.Text = "PREPARE TO END SHOW";
                    this.notificationTimerlabel.Text = (54 - min).ToString().PadLeft(2, '0') + ":" + (60 - s).ToString().PadLeft(2, '0');
                }

                // Checks for 5th Notification (55-56 Minutes)
                else if (this.min >= 55 && this.min < 56)
                {
                    showNotification();
                    this.textBox1.Text = "END SHOW AND CLICK AUTO-FADE";
                    this.notificationTimerlabel.Text = (55 - min).ToString().PadLeft(2, '0') + ":" + (60 - s).ToString().PadLeft(2, '0');
                }

                // If there is no need of Notification then hides Labels and Clocks
                else
                {
                    this.textBox1.BackColor = Color.FromArgb(230, 10, 10);
                    this.textBox1.Hide();
                    this.notificationTimerlabel.Hide();
                    this.pictureBox1.Show();
                }
            }));
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showNotification();
            timer1.Enabled = false;
            this.notification1Time = 135;
            fd.Font = textBox1.Font;
            fd.AllowSimulations = true;

            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                textBox1.Font = fd.Font;
            }
        }

    }
}