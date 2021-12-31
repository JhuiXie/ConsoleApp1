using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class FindMinMissingInt
    {
        public int solution(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)

            //result
            int result = -1; ;

            //remove element <0
            List<int> intList = A.ToList().Where(o => o > 0).OrderBy(o => o).ToList();

            //has any element > 0
            if (intList.Count > 0)
            {
                if (intList[0] > 1)
                {
                    result = 1;
                }
                else
                {
                    for (int i = 1; i < intList.Count; i++)
                    {
                        if (intList[i] - intList[i - 1] > 1)
                        {
                            result = intList[i - 1] + 1;
                            break;
                        }
                    }

                    if (result == -1)
                    {
                        result = intList.Max() + 1;
                    }
                }
            }
            else
            {
                result = 1;
            }


            return result;
        }
    }
}
