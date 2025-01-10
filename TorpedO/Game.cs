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
            Talalt=2

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
        public Mezo[,] tabla;

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
                tablaLetrehozasa1();
            }
        }
        public Jatek(int sorDb, int oszlopDb)
        {
            SorDb = sorDb;
            OszlopDb = oszlopDb;
        }

        private void tablaLetrehozasa1()
        {
            tabla = new Mezo[SorDb+2, OszlopDb+2];
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    tabla[i, j] = new Mezo();
                }
            }
            

            tablaFeltoltesX(tabla);
        }
        public int db = 0;
        private void tablaFeltoltesX(Mezo[,] tabla)
        {
            int[] hajokHossza = new int[] { 1, 1, 1, 1, /*2, 2, 2, 3, 3, 4 */};
            List<int> hajok = new List<int>();
            for (int i = 0; i < hajokHossza.Length; i++)
            {
                hajok.Add(hajokHossza[i]);
            }
            do
            {

                int sv = rnd.Next(0,hajok.Count()-1);
                int hajo = hajok[sv];

                int xPoz = rnd.Next(1,tabla.GetLength(0)-2);
                int yPoz = rnd.Next(1,tabla.GetLength(1)-2);

                switch (hajo) {
                    case 1:
                        if(ellenoriz(xPoz, yPoz, tabla))
                        {
                            tabla[xPoz, yPoz].Jel = Mezo.Jelek.Hajo;
                            hajok.RemoveAt(sv);
                            db++;
                        }
                        break;
                    default: break;
                }

            } while (hajok.Count()!=0);
        }

        private bool ellenoriz(int xPoz, int yPoz, Mezo[,] tabla)
        {
            if (tabla[xPoz, yPoz].Jel == Mezo.Jelek.Ures)
            {
                for (int i = xPoz - 1; i <= xPoz + 1; i++)
                {
                    for (int j = yPoz - 1; j <= yPoz + 1; j++)
                    {
                        if (tabla[i, j].Jel != Mezo.Jelek.Hajo)
                        {
                            return true;
                        }
                    }
                }
                
            }
            return false;
            
        }

    }
}
