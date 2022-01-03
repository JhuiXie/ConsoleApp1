using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp1.Leetcode
{
    //https://leetcode-cn.com/problems/two-sum/
    public class Q1
    {
        public int[] TwoSum(int[] nums, int target)
        {
            List<int> numList = nums.ToList();

            for (int i = 0; i < numList.Count; i++)
            {
                var targetIndex = numList.FindIndex(i + 1, o => o == target - numList[i]);
                if (targetIndex > -1)
                {
                    return new int[] { i, targetIndex };
                }
            }

            throw new Exception("No result");
        }
    }

    //https://leetcode-cn.com/problems/3sum/
    public class Q2
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            List<int> numList = nums.ToList();

            List<IList<int>> result = new List<IList<int>>();

            for (int i = 0; i < numList.Count; i++)
            {
                var target = -numList[i];

                for (int j = i + 1; j < numList.Count; j++)
                {
                    var targetIndex = numList.FindIndex(j + 1, o => o == target - numList[j]);

                    if (targetIndex > -1)
                    {
                        var tempResult = new List<int>() { numList[i], numList[j], numList[targetIndex] }.OrderBy(o => o).ToArray();
                        if (!result.Contains(tempResult))
                        {
                            result.Add(tempResult);
                        }
                    }
                }
            }



            return result;
        }
    }
}
