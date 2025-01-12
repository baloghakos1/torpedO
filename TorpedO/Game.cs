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
            Felfedve=3,
            Bot = 4,
            Jatekos =5,
            FelfedettHajo = 6,
        }

        public enum ABC
        {
            A = 0,
            B = 1,
            C = 2,
            D = 3,
            E = 4,
            F = 5,
            G = 6,
            H = 7,
            I = 8,
            J = 9,
            ures = 10,

        }

        public Jelek Jel { get; set; }
        public char Ertek { get; set; } //' ': ures, * : talalt
        public bool Felfedve { get; set; }
        public ABC abc { get; set; }

        public Mezo()
        {
            Jel = Jelek.Ures;
            Ertek = ' ';
            Felfedve = false;
            abc = ABC.ures;
        }
    }
    internal class Jatek
    {
        static Random rnd = new Random();
        private int sorDb, oszlopDb;
        public Mezo[,] tablaBot;
        private Mezo[,] hatterTablaBot; 
        private Mezo[,] hatterTablaJatekos; 
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
            hatterTablaBot = new Mezo[SorDb+2, OszlopDb+2];
            for (int i = 0; i < tablaBot.GetLength(0); i++)
            {
                for (int j = 0; j < tablaBot.GetLength(1); j++)
                {
                    tablaBot[i, j] = new Mezo();
                    hatterTablaBot[i, j] = new Mezo();
                }
            }
            

            tablaFeltoltesX(tablaBot,hatterTablaBot);
            tablaBot[0, 0].Jel = Mezo.Jelek.Bot;
        }

        private void tablaLetrahozasaJatekos()
        {
            tablaJatekos = new Mezo[SorDb + 2, OszlopDb + 2];
            hatterTablaJatekos = new Mezo[SorDb + 2, OszlopDb + 2];
            for (int i = 0; i < tablaJatekos.GetLength(0); i++)
            {
                for (int j = 0; j < tablaJatekos.GetLength(1); j++)
                {
                    tablaJatekos[i, j] = new Mezo();
                    hatterTablaJatekos[i, j] = new Mezo();
                }
            }
            tablaFeltoltesX1(tablaJatekos,hatterTablaJatekos);

            tablaJatekos[0, 0].Jel = Mezo.Jelek.Jatekos;
        }

        public int[] hajokHossza = new int[] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
        public Mezo.ABC[] abcKezd = new Mezo.ABC[] { Mezo.ABC.A, Mezo.ABC.B, Mezo.ABC.C, Mezo.ABC.D, Mezo.ABC.E, Mezo.ABC.F, Mezo.ABC.G, Mezo.ABC.H, Mezo.ABC.I, Mezo.ABC.J };
        int db = 0;
        private void tablaFeltoltesX(Mezo[,] tabla, Mezo[,] hatterTabla)
        {
            List<int> hajok = new List<int>();
            List<Mezo.ABC> abc = new List<Mezo.ABC>();
            for (int i = 0; i < hajokHossza.Length; i++)
            {
                hajok.Add(hajokHossza[i]);
                abc.Add(abcKezd[i]);
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
                            hatterTabla[xPoz, yPoz].Jel = Mezo.Jelek.Hajo;
                            tabla[xPoz, yPoz].abc = abc[db];
                            db++;
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
                                    hatterTabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz, yPoz + i].abc = abc[db];
                                }
                                db++;
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    hatterTabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz + i, yPoz].abc = abc[db];
                                }
                                db++;
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
                                    hatterTabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz, yPoz + i].abc = abc[db];
                                }
                                db++;
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    hatterTabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz + i, yPoz].abc = abc[db];
                                }
                                db++;
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
                                    hatterTabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz, yPoz + i].abc = abc[db];
                                }
                                db++;
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    hatterTabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz + i, yPoz].abc = abc[db];
                                }
                                db++;
                                hajok.RemoveAt(sv);
                            }
                        }
                        break;
                }

            } while (hajok.Count() != 0);
        }
        int db1 = 0;
        private void tablaFeltoltesX1(Mezo[,] tabla, Mezo[,] hatterTabla)
        {
            List<int> hajok = new List<int>();
            List<Mezo.ABC> abc = new List<Mezo.ABC>();
            for (int i = 0; i < hajokHossza.Length; i++)
            {
                hajok.Add(hajokHossza[i]);
                abc.Add(abcKezd[i]);
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
                            hatterTabla[xPoz, yPoz].Jel = Mezo.Jelek.Hajo;
                            tabla[xPoz, yPoz].abc = abc[db1];
                            db1++;
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
                                    hatterTabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz, yPoz+ i].abc = abc[db1];
                                }
                                db1++;
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    hatterTabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz + i, yPoz].abc = abc[db1];
                                }
                                db1++;
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
                                    hatterTabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz, yPoz + i].abc = abc[db1];
                                }
                                db1++;
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    hatterTabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz + i, yPoz].abc = abc[db1];
                                }
                                db1++;
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
                                    hatterTabla[xPoz, yPoz + i].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz, yPoz + i].abc = abc[db1];
                                }
                                db1++;
                                hajok.RemoveAt(sv);
                            }
                            if (irany == 2)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    tabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    hatterTabla[xPoz + i, yPoz].Jel = Mezo.Jelek.Hajo;
                                    tabla[xPoz + i, yPoz].abc = abc[db1];
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

        public bool sullyedt(int x, int y, Mezo[,] tabla)
        {
            /*
            for (int i = x-1; i <= x+1; i++)
            {
                for (int j = y-1; j <= y+1; j++)
                {
                    if (i >= 1 && i <= tabla.GetLength(0) && j >= 1 && j <= tabla.GetLength(1))
                    {
                        
                        if (tabla[i, j].Jel == Mezo.Jelek.Hajo && (i != x || j != y))
                        {
                            return false;
                        }
                        /*else if (tabla[i, j].Jel == Mezo.Jelek.FelfedettHajo && (i != x || j != y))
                        {
                            return sullyedt(i, j, tabla);
                        }
                    }
                }
            }
            */
            if (tabla[x,y].abc != Mezo.ABC.ures)
            {
                for (int i = 1; i < tabla.GetLength(0) - 1; i++)
                {
                    for (int j = 1; j < tabla.GetLength(1) - 1; j++)
                    {
                        if (tabla[i, j].abc == tabla[x,y].abc)
                        {
                            if (tabla[i, j].Jel != Mezo.Jelek.FelfedettHajo)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        /*
        public (int, List<int[,]>) kilott(int x, int y, Mezo[,] tabla)
        {
            List<int[,]> koordinatak = new List<int[,]>();
            if (!sullyedt(x, y, tabla))
            {
                koordinatak.Add(new int[,] { { x, y } });
                return (1,koordinatak);
            }
            else
            {
                int mezok = 1;

                //mezok += szamlalas(x, y, tabla);

                return (mezok,koordinatak);
            }

        }
        */

        
        public int[] sullyedEll(Mezo[,] tabla, Mezo.ABC abc)
        {
            bool asd1 = true;
            bool asd2 = true;
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            if (asd1)
            {
                for (int i = 1; i < tabla.GetLength(0) - 1; i++)
                {
                    if (asd1)
                    {
                        for (int j = 1; j < tabla.GetLength(1) - 1; j++)
                        {
                            if (asd1)
                            {
                                if (tabla[i, j].abc == abc)
                                {
                                    x1 = i;
                                    y1 = j;
                                    asd1 = false;
                                }
                            }
                            
                        }
                    }
                    
                }
            }
            if (asd2)
            {
                for (int i = tabla.GetLength(0) - 2; i > 0; i--)
                {
                    if (asd2)
                    {
                        for (int j = tabla.GetLength(1) - 2; j > 0; j--)
                        {
                            if (asd2)
                            {
                                if (tabla[i, j].abc == abc)
                                {
                                    x2 = i;
                                    y2 = j;
                                    asd2= false;
                                }
                            }

                        }
                    }

                }
            }
            int[] pozok = new int[] { x1 -1, y1 - 1, x2 +1 , y2 + 1 };
            return pozok;
        }

        public bool win(Mezo[,] tabla)
        {
            int db = 0;
            for (int i = 1; i < tabla.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < tabla.GetLength(1) - 1; j++)
                {
                    if (tabla[i, j].Jel == Mezo.Jelek.Hajo)
                    {
                        db++;
                    }
                }
            }
            if (db == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool lose(Mezo[,] tabla)
        {
            int db = 0;
            for (int i = 1; i < tabla.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < tabla.GetLength(1) - 1; j++)
                {
                    if (tabla[i, j].Jel == Mezo.Jelek.Hajo)
                    {
                        db++;
                    }
                }
            }
            if (db == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    
}
