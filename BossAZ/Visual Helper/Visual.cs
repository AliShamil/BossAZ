using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossAZ.Visual_Helper
{
    internal static  class Visual
    {
        public static void Cursor(ConsoleKeyInfo key, int max, ref int index)
        {
            if (key.Key == ConsoleKey.UpArrow)
                index--;
            else if (key.Key == ConsoleKey.DownArrow)
                index++;

            if (index < 0) index =  max - 1;
            else if (index >= max) index = 0;
        }

        public static void ShowMenu(string[] choices, int index)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                if (index == i)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", choices[i]));
                    //Console.WriteLine($"{choices[i]}");
                    Console.ResetColor();
                    continue;
                }
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", choices[i]));
                //Console.WriteLine($"{choices[i]}");
            }
        }
    }
}
