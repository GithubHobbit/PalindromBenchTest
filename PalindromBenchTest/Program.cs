using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;

namespace PalindromBenchTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = BenchmarkRunner.Run<Bench>();
        }
    }

    public class Bench
    {
        private BenchCheck _checker;
        private int num;

        public Bench()
        {
            _checker = new BenchCheck();
            Random rand = new Random();
            num = rand.Next(0, 99999);

        }

        [Benchmark]
        public bool FirstMethod() => _checker.BenchCheck1(num);

        [Benchmark]
        public bool SecondMethod() => _checker.BenchCheck2(num);
    }

    public class BenchCheck
    {
        public bool BenchCheck1(int num)
        {
            if (num >= 0 && num < 10)
                return true;
            int numLength = GetLength(num);
            int[] digits = new int[numLength];

            for (int i = numLength - 1; i >= 0; i--) {
                digits[i] = num % 10;
                num /= 10;
            }
            for (int i = 0; i < numLength / 2; i++) {
                if (digits[i] != digits[numLength - i - 1])
                    return false;
            }
            return true;
        }

        static int GetLength(int num)
        {
            int n = 0;
            while (num > 0) {
                num /= 10;
                n++;
            }
            return n;
        }
        public bool BenchCheck2(int num)
        {
            string strNum = Convert.ToString(num);
            if (strNum.Reverse().SequenceEqual(strNum)) 
                return true;
            return false;
        }
    }
}
