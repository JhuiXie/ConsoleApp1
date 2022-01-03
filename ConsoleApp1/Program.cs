using ConsoleApp1.Leetcode;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Q2 q = new Q2();
            //int[] test = new int[] { -1, 0, 1, 2, -1, -4 };
            //var result = q.ThreeSum(test);

            string a = "abcde";
            string b = "bceaf";

            var result = checkString(a, b);

        }

        private static bool checkString(string a, string b)
        {

            if ((a.Length != b.Length) || string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
            {
                return false;
            }

            List<char> aList = a.ToCharArray().ToList();

            for (int i = 0; i < aList.Count; i++)
            {
                //get string
                string tempA = "";
                foreach (var item in aList)
                {
                    tempA += item.ToString();
                }

                //check
                if (tempA == b)
                {
                    return true;
                }

                //shift
                var a0 = aList[0];
                for (int j = 0; j < aList.Count - 1; j++)
                {
                    aList[j] = aList[j + 1];
                }
                aList[aList.Count()-1] = a0;
            }


            return false;

        }
    }
}

