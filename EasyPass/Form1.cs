using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iWallet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var designSize = this.ClientSize;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Size = designSize;
            this.Text = "EasyPass";

            this.DesktopLocation = new Point((Screen.PrimaryScreen.Bounds.Width - 130), 0);
            this.Height = Screen.PrimaryScreen.Bounds.Height;

           // tbUserName.Top = tbUserName.Top + 50;
        }

        int AllButtonHeight;
        int FormHeight = 84;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = FormHeight;
            //this.panel1.Height = 0;
            //this.panel1.Width = 0;
            panel1.Height = this.Height;
            //panel1.Width = this.Width-20;

            int ExtraScreen = 0;
            if (System.Windows.Forms.Screen.AllScreens.Length > 1)
            {
                ExtraScreen = System.Windows.Forms.Screen.AllScreens[1].WorkingArea.X;
            }

            this.Left = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - this.Width + ExtraScreen;
            this.Top = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            AllButtonHeight = 0;
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                AllButtonHeight = (panel1.Controls[i].Height + AllButtonHeight + 10);
            }
            AllButtonHeight += 40;
        }

        private void btnParola1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Parola1");
            ClickFinish();
        }

        private void btnParola2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Parola2");
            ClickFinish();
        }

        private void btnParola3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Parola3");
            ClickFinish();
        }

        private void btnParola4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Parola4");
            ClickFinish();
        }

        private void btnParola5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Parola5");
            ClickFinish();
        }

        private void btnParola6_Click(object sender, EventArgs e)
        {
             Clipboard.SetText("Parola6");
            ClickFinish();
        }

        private void btnParola7_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Parola7");
            ClickFinish();
        }

        private void btnParola8_Click(object sender, EventArgs e)
        {
            tbUserName.Text = "Parola8";
            Clipboard.SetText("UserName8");
            ClickFinish();
        }

        private void btnParola9_Click(object sender, EventArgs e)
        {
            tbUserName.Text = "UserName9";
            Clipboard.SetText("Parola9");
            ClickFinish();
        }

        private void btnParola10_Click(object sender, EventArgs e)
        {
            tbUserName.Text = "UserName10";
            Clipboard.SetText("Parola10");
            ClickFinish();
        }
        
        private void btnGoster_MouseHover(object sender, EventArgs e)
        {
            //this.Opacity = 100;
            openMenu();
        }
        private void openMenu()
        {
            for (int k = FormHeight; k < AllButtonHeight; k = k + 10)
            {
                this.Height = k;
                panel1.Height = this.Height;
                

                this.Top = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height;

            }
            btnGoster.Visible = false;
            //panel1.Width = this.Width - 30;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            closeMenu();
            //this.Opacity = 30;
        }

        private void closeMenu()
        {

            this.Height = FormHeight;
            this.Top = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            btnGoster.Visible = true;
        }

        private void ClickFinish()
        {
            closeMenu();
            //Opacity = 30;
        }


    }
}
