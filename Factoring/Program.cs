using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factoring
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("          X-FACTOR          ");
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║                          ║");
                Console.WriteLine("║                          ║");
                Console.WriteLine("║                          ║");
                Console.WriteLine("║                          ║");
                Console.WriteLine("║                          ║");
                Console.WriteLine("║                          ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.WriteLine("□  X^2 ▫ □  X ▫ □");
                int left = 0;

                Console.SetCursorPosition(0, 9);
                left = Console.CursorLeft;
                string sEquCo = Console.ReadLine();
                Console.CursorLeft = left + 7;
                Console.CursorTop--;
                left = Console.CursorLeft;
                bool coAdding = Adding();
                Console.CursorLeft = left + 2;
                left = Console.CursorLeft;
                string sTarget = Console.ReadLine();
                Console.CursorLeft = left + 5;
                Console.CursorTop--;
                left = Console.CursorLeft;
                bool constAdding = Adding();
                Console.CursorLeft = left + 2;
                left = Console.CursorLeft;
                string sFactor = Console.ReadLine();


                Console.SetCursorPosition(13, 2);
                Console.Write(sFactor);
                Console.SetCursorPosition(13, 7);
                Console.Write(sTarget);

                // factor = c, target = b, equCo = a
                int factor = int.Parse(sFactor), target = int.Parse(sTarget), equCo = int.Parse(sEquCo);
                if (!constAdding)
                {
                    factor *= -1;
                }
                if (!coAdding)
                {
                    target *= -1;
                }
                int gcf = Mathmatic.GCF(equCo, factor, target);

                equCo /= gcf;
                factor /= gcf;
                target /= gcf;
                factor *= equCo;
                Dictionary<int, int> factors = Factor(factor);
                var correct = (from value in factors
                               where IsCorrect(value, target)
                               select new
                               {
                                   Value = value.Key + value.Value,
                                   X = value.Key,
                                   Y = value.Value
                               }).FirstOrDefault();
                if (correct == null)
                {
                    Console.SetCursorPosition(2, 4);
                    Console.Write("No Real Factors (prime)");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                Console.SetCursorPosition(3, 4);
                Console.Write(correct.X);
                Console.SetCursorPosition(24, 4);
                Console.Write(correct.Y);
                Console.SetCursorPosition(0, 9);
                int x1 = equCo, x2 = equCo;
                int x1Gcf = Mathmatic.GCF(x1, correct.X);
                int x2Gcf = Mathmatic.GCF(x2, correct.Y);

                x1 /= Math.Abs(x1Gcf);
                int y1 = correct.X / x1Gcf;

                x2 /= x2Gcf;
                int y2 = correct.Y / x2Gcf;

                string sX1 = x1.ToString(), sX2 = x2.ToString();
                string symbol1, symbol2;
                if (x1 == 1)
                {
                    sX1 = "";
                }
                if (x2 == 1)
                {
                    sX2 = "";
                }
                if (y1 < 0)
                {
                    symbol1 = "-";
                    y1 *= -1;
                }
                else
                {
                    symbol1 = "+";
                }
                if (y2 < 0)
                {
                    symbol2 = "-";
                    y2 *= -1;
                }
                else
                {
                    symbol2 = "+";
                }
                Console.Write("                    ");
                Console.CursorLeft = 0;
                Console.WriteLine("{6}({0}x {4} {1})({2}x {5} {3})", sX1, y1, sX2, y2, symbol1, symbol2, gcf);
                Console.ReadLine();
                Console.Clear();
            }
        }

        public static bool Adding()
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Add || key == ConsoleKey.OemPlus)
            {
                Console.Write("+ ");
                return true;
            }
            else if (key == ConsoleKey.Subtract || key == ConsoleKey.OemMinus)
            {
                Console.Write("- ");
                return false;
            }
            throw new InvalidOperationException("Only plus or minus is accepted.");
        }

        public static Dictionary<int, int> Factor(int number)
        {
            List<int> factorsAll;

            factorsAll = Factors(number).ToList();

            Dictionary<int, int> values = new Dictionary<int, int>();
            factorsAll.ForEach(i =>
            {
                factorsAll.ForEach(j =>
                {
                    if (i * j == number)
                    {
                        values.Add(i, j);
                    }
                });
            });

            return values;
        }

        public static bool IsCorrect(KeyValuePair<int, int> pair, int target)
        {
            return pair.Key + pair.Value == target;
        }
        public static bool Divides(int potentialFactor, int i)
        {
            if (potentialFactor == 0)
            {
                return false;
            }
            return i % potentialFactor == 0;
        }

        public static IEnumerable<int> Factors(int i)
        {
            if (i < 0)
            {
                List<int> items = (from potentialFactor in Enumerable.Range(i, (i * 2 + 1) * -1).Reverse()
                       where Divides(potentialFactor, i)
                       select potentialFactor).ToList();
                items.Add(i * -1);
                return items;
            }
            return from potentialFactor in Enumerable.Range(i * -1, i * 2 + 1).Reverse()
                   where Divides(potentialFactor, i)
                   select potentialFactor;
        }
    }
}
