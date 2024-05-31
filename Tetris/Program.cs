using System;
using System.Collections.Generic;
using System.Threading;

namespace Tetris
{
    public abstract class PiezaBase
    {
        protected List<Bloque> bloques = new List<Bloque>();

        public abstract void CrearPieza();
        public abstract void Rotar();

        public void MoverDerecha()
        {
            if (!ColisionaConBordeDerecho())
            {
                foreach (var bloque in bloques) //Por cada bloque de la lista en "bloques" aumenta x, osea se mueve a la derecha
                {
                    bloque.X++;
                }
            }
        }

        public void MoverIzquierda()
        {
            if (!ColisionaConBordeIzquierdo())
            {
                foreach (var bloque in bloques)//Por cada bloque de la lista en "bloques" disminuye x, osea se mueve a la izquierda
                {
                    bloque.X--;
                }
            }
        }

        public void MoverAbajo()
        {
            foreach (var bloque in bloques) //Por cada bloque de la lista en "bloques" aumenta y, osea se mueve hacia abajo
            {
                bloque.Y++;
            }
        }

        public void DibujarPieza()
        {
            foreach (var bloque in bloques) //Por cada bloque de la lista en "bloques" se dibujan las piezas en la posicion predeterminada dada
            {
                Console.SetCursorPosition(bloque.X, bloque.Y);
                Console.Write("#");
            }
        }

        public bool ColisionaConSuelo()
        {
            foreach (var bloque in bloques)
            {
                if (bloque.Y >= Console.WindowHeight - 3) // Colisión con el suelo
                    return true;
            }
            return false;
        }

        public bool ColisionaConOtraPieza(List<Bloque> piezasFijas)
        {
            foreach (var bloque in bloques)
            {
                foreach (var bloqueFijo in piezasFijas)
                {
                    if (bloque.X == bloqueFijo.X && bloque.Y == bloqueFijo.Y - 1) // Colisión con otra pieza
                        return true;
                }
            }
            return false;
        }

        public bool ColisionaConBordeIzquierdo()
        {
            foreach (var bloque in bloques)
            {
                if (bloque.X <= 1) // Colisión con el borde izquierdo
                    return true;
            }
            return false;
        }

        public bool ColisionaConBordeDerecho()
        {
            foreach (var bloque in bloques)
            {
                if (bloque.X >= Console.WindowWidth - 2) // Colisión con el borde derecho
                    return true;
            }
            return false;
        }

        public void FijarPieza(List<Bloque> piezasFijas)
        {
            foreach (var bloque in bloques)
            {
                piezasFijas.Add(new Bloque(bloque.X, bloque.Y));
            }
        }
    }

    public class PiezaLinea : PiezaBase
    {
        public override void CrearPieza()
        {
            bloques.Add(new Bloque(Console.WindowWidth / 2, 0));
            bloques.Add(new Bloque(Console.WindowWidth / 2, 1));
            bloques.Add(new Bloque(Console.WindowWidth / 2, 2));
            bloques.Add(new Bloque(Console.WindowWidth / 2, 3));
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public override void Rotar()
        {
            int centroX = bloques[1].X;
            int centroY = bloques[1].Y;

            for (int i = 0; i < bloques.Count; i++)
            {
                int x = bloques[i].X - centroX;
                int y = bloques[i].Y - centroY;
                bloques[i].X = centroX - y;
                bloques[i].Y = centroY + x;
            }
        }
    }

    public class PiezaCuadrado : PiezaBase
    {
        public override void CrearPieza()
        {
            bloques.Add(new Bloque(Console.WindowWidth / 2, 0));
            bloques.Add(new Bloque(Console.WindowWidth / 2, 1));
            bloques.Add(new Bloque(Console.WindowWidth / 2 + 1, 0));
            bloques.Add(new Bloque(Console.WindowWidth / 2 + 1, 1));
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public override void Rotar()
        {
            // La pieza cuadrada no necesita rotación
        }
    }

    public class PiezaL : PiezaBase
    {
        public override void CrearPieza()
        {
            bloques.Add(new Bloque(Console.WindowWidth / 2, 0));
            bloques.Add(new Bloque(Console.WindowWidth / 2, 1));
            bloques.Add(new Bloque(Console.WindowWidth / 2, 2));
            bloques.Add(new Bloque(Console.WindowWidth / 2 + 1, 2));
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public override void Rotar()
        {
            int centroX = bloques[1].X;
            int centroY = bloques[1].Y;

            for (int i = 0; i < bloques.Count; i++)
            {
                int x = bloques[i].X - centroX;
                int y = bloques[i].Y - centroY;
                bloques[i].X = centroX - y;
                bloques[i].Y = centroY + x;
            }
        }
    }

    public class PiezaS : PiezaBase
    {
        public override void CrearPieza()
        {
            bloques.Add(new Bloque(Console.WindowWidth / 2, 0));
            bloques.Add(new Bloque(Console.WindowWidth / 2 + 1, 0));
            bloques.Add(new Bloque(Console.WindowWidth / 2 + 1, 1));
            bloques.Add(new Bloque(Console.WindowWidth / 2 + 2, 1));
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public override void Rotar()
        {
            int centroX = bloques[1].X;
            int centroY = bloques[1].Y;

            for (int i = 0; i < bloques.Count; i++)
            {
                int x = bloques[i].X - centroX;
                int y = bloques[i].Y - centroY;
                bloques[i].X = centroX - y;
                bloques[i].Y = centroY + x;
            }
        }
    }

    public class Bloque
    {
        private int x;
        private int y;

        public int X
        {
            get { return x; } set {  x = value; }
        }

        public int Y
        {
            get { return y; } set { y = value; }
        }

        public Bloque(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            // Establecer el tamaño de la consola
            Console.WindowHeight = 20;
            Console.WindowWidth = 20;

            List<Bloque> piezasFijas = new List<Bloque>();

            PiezaBase piezaActual = GenerarNuevaPieza();
            piezaActual.CrearPieza();

            while (true)
            {
                Console.Clear();
                DibujarLimites();
                DibujarPiezasFijas(piezasFijas);
                piezaActual.DibujarPieza();

                if (!piezaActual.ColisionaConSuelo() && !piezaActual.ColisionaConOtraPieza(piezasFijas))
                {
                    piezaActual.MoverAbajo();
                }
                else
                {
                    piezaActual.FijarPieza(piezasFijas);
                    EliminarLineasCompletas(piezasFijas);
                    piezaActual = GenerarNuevaPieza();
                    piezaActual.CrearPieza();
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.RightArrow)
                    {
                        piezaActual.MoverDerecha();
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        piezaActual.MoverIzquierda();
                    }
                    else if (key == ConsoleKey.Spacebar)
                    {
                        piezaActual.Rotar();
                    }
                    else if(key == ConsoleKey.UpArrow)
                    {
                        piezaActual.MoverAbajo();
                    }
                }

                Thread.Sleep(90);
            }
        }

        static PiezaBase GenerarNuevaPieza()
        {
            Random random = new Random();
            int tipo = random.Next(4);

            switch (tipo)
            {
                case 0:
                    return new PiezaLinea();
                case 1:
                    return new PiezaCuadrado();
                case 2:
                    return new PiezaL();
                case 3:
                    return new PiezaS();
                default:
                    return new PiezaLinea();
            }
        }

        static void EliminarLineasCompletas(List<Bloque> piezasFijas)
        {
            int alturaConsola = Console.WindowHeight;
            int anchuraConsola = Console.WindowWidth;

            List<int> lineasCompletas = new List<int>();

            for (int y = alturaConsola - 3; y > 0; y--)
            {
                bool filaCompleta = true;
                for (int x = 2; x < anchuraConsola - 2; x++)
                {
                    bool bloqueEncontrado = false;
                    foreach (var bloque in piezasFijas)
                    {
                        if (bloque.X == x && bloque.Y == y)
                        {
                            bloqueEncontrado = true;
                            break;
                        }
                    }
                    if (!bloqueEncontrado)
                    {
                        filaCompleta = false;
                        break;
                    }
                }
                if (filaCompleta)
                {
                    lineasCompletas.Add(y);
                }
            }

            foreach (var linea in lineasCompletas)
            {
                for (int i = piezasFijas.Count - 1; i >= 0; i--)
                {
                    if (piezasFijas[i].Y == linea)
                    {
                        piezasFijas.RemoveAt(i);
                    }
                    else if (piezasFijas[i].Y < linea)
                    {
                        piezasFijas[i].Y++;
                    }
                }
            }
        }

        static void DibujarLimites()
        {
            int alturaConsola = Console.WindowHeight;
            int anchuraConsola = Console.WindowWidth;

            for (int y = 0; y < alturaConsola - 2; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("|");
                Console.SetCursorPosition(anchuraConsola - 1, y);
                Console.Write("||");
            }

            for (int x = 0; x < anchuraConsola; x++)
            {
                Console.SetCursorPosition(x, alturaConsola - 2);
                Console.Write("¬");
            }
        }

        static void DibujarPiezasFijas(List<Bloque> piezasFijas)
        {
            foreach (var bloque in piezasFijas)
            {
                Console.SetCursorPosition(bloque.X, bloque.Y);
                Console.Write("#");
            }
        }
    }
}