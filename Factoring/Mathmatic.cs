using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factoring
{
    public static class Mathmatic
    {
        public static int GCF(params int[] numbers)
        {
            int gcf = numbers.Aggregate(GCF);
            return gcf;
        }

        private static int GCF(int a, int b)
        {
            return b == 0 ? a : GCF(b, a % b);
        }
    }
}
