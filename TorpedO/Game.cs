using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TorpedO
{
    internal class Game
    {
    }

    internal class Info 
    {
        public int Sor { get; set; }
        public int Oszlop { get; set; }
        public Mezo.Jelek Jel { get; set; }
        public Info(int Sor, int Oszlop, Mezo.Jelek Jel = Mezo.Jelek.Ures)
        {
            this.Sor = Sor;
            this.Oszlop = Oszlop;
            this.Jel = Jel;
        }
    }

    internal class Mezo
    {
        public enum Jelek
        {
            Ures=0,
            Hajo = 1,
            Talalt=2,
            Felfedve=3

        }

        public Jelek Jel { get; set; }
        public char Ertek { get; set; } //' ': ures, * : talalt
        public bool Felfedve { get; set; }

        public Mezo()
        {
            Jel = Jelek.Ures;
            Ertek = ' ';
            Felfedve = false;
        }
    }
    internal class Jatek
    {
        static Random rnd = new Random();
        private int sorDb, oszlopDb;
        public Mezo[,] tablaBot;
        public Mezo[,] tablaJatekos;

        public int SorDb
        {
            get { return sorDb; }
            set { sorDb = value;
                
            }
        }

        public int OszlopDb
        {
            get { return oszlopDb; }
            set { oszlopDb = value;
                tablaLetrehozasaBot();
                tablaLetrahozasaJatekos();
            }
        }
        public Jatek(int sorDb, int oszlopDb)
        {
            SorDb = sorDb;
            OszlopDb = oszlopDb;
        }

        private void tablaLetrehozasaBot()
        {
            tablaBot = new Mezo[SorDb+2, OszlopDb+2];
            for (int i = 0; i < tablaBot.GetLength(0); i++)
            {
                for (int j = 0; j < tablaBot.GetLength(1); j++)
                {
                    tablaBot[i, j] = new Mezo();
                }
            }
            

            tablaFeltoltesX(tablaBot);
        }

        private void tablaLetrahozasaJatekos()
        {
            tablaJatekos = new Mezo[SorDb + 2, OszlopDb + 2];
            for (int i = 0; i < tablaJatekos.GetLength(0); i++)
            {
                for (int j = 0; j < tablaJatekos.GetLength(1); j++)
                {
                    tablaJatekos[i, j] = new Mezo();
                }
            }
            tablaFeltoltesX(tablaJatekos);
        }

        private void tablaFeltoltesX(Mezo[,] tabla)
        {
            int[] hajokHossza = new int[] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
            List<int> hajok = new List<int>();
            for (int i = 0; i < hajokHossza.Length; i++)
            {
                hajok.Add(hajokHossza[i]);
            }
            do
            {

                int sv = rnd.Next(0, hajok.Count() - 1);
                int hajo = hajok[sv];
                int xPoz, yPoz;

                int irany = rnd.Next(1, 3);

                switch (hajo)
                {
                    case 1:
                        xPoz = rnd.Next(1, tabla.GetLength(0) - 1);
                        yPoz = rnd.Next(1, tabla.GetLength(1) - 1);
                        if (ellenoriz(xPoz, yPoz, tabla, 0, 1))
                        {
                            tabla[xPoz, yPoz].Jel = Mezo.Jelek.Hajo;
                            hajok.RemoveAt(sv);
                        }
                        break;
                    case 2:
                        if (irany == 1)
                        {
                            xPoz = rnd.Next(1, tabla.GetLength(0) - 1);
                            yPoz = rnd.Next(1, tabla.GetLength(1) - 2);
                        }
                        else
                        {
                            xPoz = rnd.Next(1, tabla.GetLength(0) - 2);
                            yPoz = rnd.Next(1, tabla.GetLength(1) - 1);
                        }
                        if (ellenoriz(xPoz, yPoz, tabla, irany, 2))
                        {
                            if (irany == 1)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    tabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                }
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                }
                                hajok.RemoveAt(sv);
                            }
                        }
                        break;
                    case 3:
                        if (irany == 1)
                        {
                            xPoz = rnd.Next(1, tabla.GetLength(0) - 1);
                            yPoz = rnd.Next(1, tabla.GetLength(1) - 3);
                        }
                        else
                        {
                            xPoz = rnd.Next(1, tabla.GetLength(0) - 3);
                            yPoz = rnd.Next(1, tabla.GetLength(1) - 1);
                        }

                        if (ellenoriz(xPoz, yPoz, tabla, irany, 3))
                        {
                            if (irany == 1)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    tabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                }
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                }
                                hajok.RemoveAt(sv);
                            }
                        }
                        break;
                    case 4:
                        if (irany == 1)
                        {
                            xPoz = rnd.Next(1, tabla.GetLength(0) - 1);
                            yPoz = rnd.Next(1, tabla.GetLength(1) - 4);
                        }
                        else
                        {
                            xPoz = rnd.Next(1, tabla.GetLength(0) - 4);
                            yPoz = rnd.Next(1, tabla.GetLength(1) - 1);
                        }

                        if (ellenoriz(xPoz, yPoz, tabla, irany, 4))
                        {
                            if (irany == 1)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    tabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                }
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                }
                                hajok.RemoveAt(sv);
                            }
                        }
                        break;
                }

            } while (hajok.Count() != 0);
        }

        private bool ellenoriz(int xPoz, int yPoz, Mezo[,] tabla, int irany, int hossz)
        {
            if (irany == 0)
            {
                if (tabla[xPoz, yPoz].Jel == Mezo.Jelek.Ures)
                {
                    bool ok = true;
                    for (int i = xPoz - 1; i <= xPoz + 1; i++)
                    {
                        for (int j = yPoz - 1; j <= yPoz + 1; j++)
                        {
                            if (tabla[i, j].Jel == Mezo.Jelek.Hajo)
                            {
                                ok = false;
                            }
                        }
                    }
                    if (ok)
                    {
                        return true;
                    }
                }
            }
            if (irany == 1)
            {
                if (tabla[xPoz, yPoz].Jel == Mezo.Jelek.Ures)
                {
                    bool ok = true;
                    for (int i = xPoz - 1; i <= xPoz + 1; i++)
                    {
                        for (int j = yPoz - 1; j <= yPoz + hossz; j++)
                        {
                            if (tabla[i, j].Jel == Mezo.Jelek.Hajo)
                            {
                                ok = false;
                            }
                        }
                    }
                    if (ok)
                    {
                        return true;
                    }
                }
            }
            if (irany == 2)
            {
                if (tabla[xPoz, yPoz].Jel == Mezo.Jelek.Ures)
                {
                    bool ok = true;
                    for (int i = xPoz - 1; i <= xPoz + hossz; i++)
                    {
                        for (int j = yPoz - 1; j <= yPoz + 1; j++)
                        {
                            if (tabla[i, j].Jel == Mezo.Jelek.Hajo)
                            {
                                ok = false;
                            }
                        }
                    }
                    if (ok)
                    {
                        return true;
                    }
                }
            }
            return false;

        }


    }
}
