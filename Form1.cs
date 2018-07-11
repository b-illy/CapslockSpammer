using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CapslockSpammer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1) m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        bool on = false;

        private void button1_Click(object sender, EventArgs e) {
            if (on) {
                timer1.Stop();
                button1.BackgroundImage = CapslockSpammer.Properties.Resources.no;
                Form1.ActiveForm.Text = "Billy's Capslock Spammer [OFF]";
            } else {
                timer1.Start();
                button1.BackgroundImage = CapslockSpammer.Properties.Resources.yes;
                Form1.ActiveForm.Text = "Billy's Capslock Spammer [ON]";
            }

            label1.Text = Form1.ActiveForm.Text;
            on = !on;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            const int KEYEVENTF_EXTENDEDKEY = 0x1;
            const int KEYEVENTF_KEYUP = 0x2;
            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            keybd_event(0x14, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP,
            (UIntPtr)0);
        }

        private void button2_Click(object sender, EventArgs e) {
            Form1.ActiveForm.Close();
        }
        
        private void button3_Click(object sender, EventArgs e) {
            Form1.ActiveForm.WindowState = FormWindowState.Minimized;
        }
    }
}
