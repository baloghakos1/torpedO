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

        public Button test;
        public List<Label> jatekos = new List<Label>();


        private Jatek jatek;
        public Label[,] jatekosPalya;
        public Label[,] botPalya;



        public Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void setUp()
        {
            restart = new Button();
            restart.Size = new Size(150, 30);
            restart.Text = "Restart";
            restart.Top = 85;
            restart.Left = 50;
            restart.Click += Start;
            panelMain.Controls.Add(restart);

            test = new Button();
            test.Size = new Size(150, 30);
            test.Text = "test";
            test.Top = 85;
            test.Left = 150;
            test.Click += lovesBot;
            panelMain.Controls.Add(test);
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
            jatekosPalya = new Label[sor+2,oszlop+2];
            botPalya = new Label[sor+2,oszlop+2];
            tablaMegjelenitesKonnyuBot();
            tablakMegjelenitesKonnyuJatekos();
            setUp();
        }

        private void Hard_Click(object sender, EventArgs e)
        {
            int sor = 20;
            int oszlop = 20;

            jatek = new Jatek(sor, oszlop);
            botPalya = new Label[sor + 2, oszlop + 2];
            jatekosPalya = new Label[sor + 2, oszlop+2];
            tablaMegjelenitesNehezBot();
            tablakMegjelenitesNehezJatekos();
            setUp();
        }

        private void tablakMegjelenitesKonnyuJatekos()
        {
            int w = 15, h = 15;

            for (int i = 1; i < jatek.SorDb+1; i++)
            {
                for (global::System.Int32 j = 1; j < jatek.OszlopDb+1; j++)
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
                        Left = 60 + (j-1) * w - (j-1),
                        Top = 280 + (i - 2) * h - (i-1),
                        Parent = panelMain,
                    };

                    if (jatek.tablaJatekos[i, j].Jel == Mezo.Jelek.Hajo)
                    {
                        lb.Text = "H";
                    }
                    else
                    {
                        lb.Text = "";
                    }

                    jatekosPalya[i, j] = lb;
                }
            }




        }

        private void tablakMegjelenitesNehezJatekos()
        {
            int w = 15, h = 15;

            for (int i = 1; i < jatek.SorDb+1; i++)
            {
                for (global::System.Int32 j = 1; j < jatek.OszlopDb+1; j++)
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
                        Left = 30 + (j-1) * w - (j-1),
                        Top = 150 + (i - 2) * h - (i-1),
                        Parent = panelMain,
                    };

                    if (jatek.tablaJatekos[i, j].Jel == Mezo.Jelek.Hajo)
                    {
                        lb.Text = "H";
                    }
                    else
                    {
                        lb.Text = "";
                    }

                    jatekosPalya[i, j] = lb;
                }
            }


        }

        private void tablaMegjelenitesKonnyuBot()
        {
            int w = 30, h = 30;
            panelMain.Controls.Clear();

            for (int i = 1; i < jatek.SorDb+1; i++)
            {
                for (global::System.Int32 j = 1; j < jatek.OszlopDb+1; j++)
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
                        Left = 470 + (j-1) * w - (j-1),
                        Top = 105 + (i - 2) * h - (i-1),         
                        Parent = panelMain,
                    };

                    


                    if (jatek.tablaBot[i, j].Jel == Mezo.Jelek.Hajo)
                    {
                        lb.Text = "H";
                        lb.MouseClick += felfed;
                    }
                    else 
                    {
                        lb.Text = "";
                        lb.MouseClick += felfed1;
                    }

                    lb.MouseEnter += Label_MouseEnter;
                    lb.MouseLeave += Label_MouseLeave;

                    botPalya[i,j] = lb;
                }
            }


            


        }

        private void tablaMegjelenitesNehezBot()
        {
            int w = 20, h = 20;
            panelMain.Controls.Clear();

            for (int i = 1; i < jatek.SorDb+1; i++)
            {
                for (global::System.Int32 j = 1; j < jatek.OszlopDb+1; j++)
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
                        Left = 370 + (j-1) * w - (j-1),
                        Top = 55 + (i -2) * h - (i-1),
                        Parent = panelMain,
                    };

                    lb.MouseEnter += Label_MouseEnter;
                    lb.MouseLeave += Label_MouseLeave;

                    if (jatek.tablaBot[i, j].Jel == Mezo.Jelek.Hajo)
                    {
                        lb.Text = "H";
                    }
                    else
                    {
                        lb.Text = "";
                    }

                    botPalya[i, j] = lb;
                }
            }

        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                label.BackColor = Color.LightPink; 
            }
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                label.BackColor = Color.LightGray; 
            }
        }

        private void felfed(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.Red;
            label.MouseEnter -= Label_MouseEnter;
            label.MouseLeave -= Label_MouseLeave;
            label.MouseClick -= felfed;
        }

        private void felfed1(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.Blue;
            label.MouseEnter -= Label_MouseEnter;
            label.MouseLeave -= Label_MouseLeave;
            label.MouseClick -= felfed1;
        }


        private void lovesBot(object sender, EventArgs e)
        {

            var delay = Task.Delay(500);
            delay.Wait();

            int x = rnd.Next(1, jatek.tablaJatekos.GetLength(0) - 1);
            int y = rnd.Next(1, jatek.tablaJatekos.GetLength(1) - 1);

            do
            {
                x = rnd.Next(1, jatek.tablaJatekos.GetLength(0) - 1);
                y = rnd.Next(1, jatek.tablaJatekos.GetLength(1) - 1);

            } while (jatek.tablaJatekos[x, y].Jel == Mezo.Jelek.Felfedve);

            if (jatek.tablaJatekos[x, y].Jel == Mezo.Jelek.Hajo)
            {
                jatek.tablaJatekos[x, y].Jel = Mezo.Jelek.Talalt;
            }
            else
            {
                jatek.tablaJatekos[x, y].Jel = Mezo.Jelek.Ures;
            }

            if (jatek.tablaJatekos[x, y].Jel == Mezo.Jelek.Talalt)
            {
                jatek.tablaJatekos[x, y].Jel = Mezo.Jelek.Felfedve;
                jatekosPalya[x, y].Text = "X";
            }
            else
            {
                jatek.tablaJatekos[x, y].Jel = Mezo.Jelek.Felfedve;
                jatekosPalya[x, y].Text = "*";
            }
        }





    }
}
