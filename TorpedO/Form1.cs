using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorpedO
{
    public partial class Form1 : Form
    {

        public Button restart;

        private Jatek jatek;

        public Form1()
        {
            InitializeComponent();
        }

        private void setUp()
        {
            restart = new Button();
            restart.Size = new Size(150, 30);
            restart.Text = "Restart";
            restart.Top = 105;
            restart.Left = 60;
            restart.Click += Start;
            panelMain.Controls.Add(restart);
        }

        private void Start(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(Easy);
            panelMain.Controls.Add(Hard);
        }

        private void Easy_Click(object sender, EventArgs e)
        {
            int sor = 10;
            int oszlop = 10;

            jatek = new Jatek(sor, oszlop);
            tablaMegjelenites2();
            setUp();
        }

        private void Hard_Click(object sender, EventArgs e)
        {
            int sor = 20;
            int oszlop = 20;

            jatek = new Jatek(sor, oszlop);
            tablaMegjelenites3();
            setUp();
        }

        private void tablaMegjelenites2()
        {
            int w = 30, h = 30;
            panelMain.Controls.Clear();

            for (int i = 0; i < jatek.SorDb; i++)
            {
                for (global::System.Int32 j = 0; j < jatek.OszlopDb; j++)
                {
                    Label lb = new Label()
                    {
                        Text = "",
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Courier New", 12, FontStyle.Bold),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.LightGray,
                        Cursor = Cursors.Hand,
                        Width = w,
                        Height = h,
                        Tag = new Info(i, j),
                        Left = 470 + j * w - j,
                        Top = 105 + (i - 1) * h - i,
                        Parent = panelMain,
                    };

                    if (jatek.tabla[i, j].Jel == Mezo.Jelek.Hajo)
                    {
                        lb.Text = "H";
                    }
                    else 
                    {
                        lb.Text = "U";
                    }
                }
            }

        }

        private void tablaMegjelenites3()
        {
            int w = 20, h = 20;
            panelMain.Controls.Clear();

            for (int i = 0; i < jatek.SorDb; i++)
            {
                for (global::System.Int32 j = 0; j < jatek.OszlopDb; j++)
                {
                    Label lb = new Label()
                    {
                        Text = "",
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Courier New", 12, FontStyle.Bold),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.LightGray,
                        Cursor = Cursors.Hand,
                        Width = w,
                        Height = h,
                        Tag = new Info(i, j),
                        Left = 370 + j * w - j,
                        Top = 55 + (i - 1) * h - i,
                        Parent = panelMain,
                    };

                    if (jatek.tabla[i, j].Jel == Mezo.Jelek.Hajo)
                    {
                        lb.Text = "H";
                    }
                    else
                    {
                        lb.Text = "U";
                    }
                }
            }

        }
    }
}
