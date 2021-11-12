using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Thread_lab
{
    class Program
    {
        #region Задача
        /* Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать. 
         * Эту задачу выполняют два садовника, которые не хотят встречаться друг с другом. 
         * Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо, сделав ряд, он спускается вниз.
         * Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево. 
         * Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше. Садовники должны работать параллельно. 
         * Создать многопоточное приложение, моделирующее работу садовников.
         */
        #endregion
        static int x;
        static int y;
        static char[,] garden;
        static object locker = new object();
        static void Main(string[] args)
        {
            Console.Write("Введите сторону участка: ");
            x = Convert.ToInt32(Console.ReadLine());
            y = x;
            garden = new char[x, y];
            Console.Clear();
            Thread t = new Thread(new ThreadStart(ThreadProc));
            Thread t2 = new Thread(new ThreadStart(ThreadProc2));
            t.Start();
            t2.Start();
            Console.ReadKey();
        }
        public static void ThreadProc()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    lock (locker)
                    {
                        if (garden[i, j] != '*')
                        {
                            garden[i, j] = '*';
                            Console.SetCursorPosition(j * 2, i);
                            Console.Write('*');
                        }
                        Thread.Sleep(500);
                    }
                }
            }
        }
        public static void ThreadProc2()
        {
            for (int j = x - 1; j >= 0; j--)
            {
                for (int i = y - 1; i >= 0; i--)
                {
                    lock (locker)
                    {
                        if (garden[i, j] != '*')
                        {
                            garden[i, j] = '*';
                            Console.SetCursorPosition(j * 2, i);
                            Console.Write('°');
                        }
                        Thread.Sleep(500);
                    }
                }
            }
        }
    }
}
