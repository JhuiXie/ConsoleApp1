using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johnny_Leetcode_ConsoleApp
{
    /// <summary>
    /// https://leetcode-cn.com/problems/3sum/
    /// </summary>
    public class Q2
    {
        public IList<IList<int>> ThreeSum_my(int[] nums)
        {
            var numList = nums.OrderBy(o => o).ToList();
            Array.Sort(nums);
            Dictionary<string, IList<int>> testResult = new Dictionary<string, IList<int>>();

            for (int i = 0; i < numList.Count; i++)
            {
                if (numList[i] > 0)
                {
                    break;
                }
                if (i > 0 && nums[i] == nums[i - 1]) continue; // 去重

                var target = -numList[i];

                for (int j = i + 1; j < numList.Count; j++)
                {
                    if (numList.Skip(j + 1).Contains((target - numList[j])))
                    {
                        testResult[string.Format("{0}{1}{2}", numList[i], numList[j], target - numList[j])] = new int[] { numList[i], numList[j], target - numList[j] };
                    }
                }
            }

            return testResult.Values.ToList();
        }

        public IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            int len = nums.Length;
            if (len < 3) return result;
            Array.Sort(nums);
            for (int i = 0; i < len - 2; i++)
            {
                if (nums[i] > 0) break;
                if (i > 0 && nums[i] == nums[i - 1]) continue; // 去重
                int left = i + 1;
                int right = len - 1;
                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];
                    if (sum == 0)
                    {
                        result.Add(new List<int>() { nums[i], nums[left], nums[right] });
                        while (left < right && nums[left] == nums[left + 1]) left++; // 去重
                        while (left < right && nums[right] == nums[right - 1]) right--; // 去重
                        left++;
                        right--;
                    }
                    else if (sum < 0) left++;
                    else if (sum > 0) right--;
                }
            }
            return result;
        }
    }

    /// <summary>
    /// https://leetcode-cn.com/problems/0H97ZC/
    /// </summary>
    public class Q3
    {
        public int[] RelativeSortArray(int[] arr1, int[] arr2)
        {
            List<int> arr1List = arr1.ToList();

            List<int> result = new List<int>();

            for (int i = 0; i < arr2.Length; i++)
            {
                result.AddRange(arr1List.Where(o => o == arr2[i]));
                arr1List.RemoveAll(o => o == arr2[i]);
            }

            result.AddRange(arr1List.OrderBy(o => o));

            return result.ToArray();
        }
    }

    /// <summary>
    /// https://leetcode-cn.com/problems/minimum-add-to-make-parentheses-valid/
    /// </summary>
    public class Q4
    {
        public int MinAddToMakeValid(string s)
        {
            while (s.Contains("()"))
            {
                s = s.Replace("()", "");
            }
            return s.Length;
        }
    }

    /// <summary>
    /// https://leetcode-cn.com/problems/he-bing-liang-ge-pai-xu-de-lian-biao-lcof/
    /// </summary>
    public class Q5
    {

        //Definition for singly-linked list.
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        public ListNode MergeTwoLists_my(ListNode l1, ListNode l2)
        {
            List<int> tempList = new List<int>();

            InsertToList(l1, tempList);
            InsertToList(l2, tempList);

            if (tempList.Count == 0)
            {
                return null;
            }

            tempList = tempList.OrderByDescending(o => o).ToList();

            ListNode lastNode = new ListNode(tempList[0]);

            for (int i = 0; i < tempList.Count; i++)
            {
                ListNode currentNode = new ListNode(tempList[i]);
                if (i > 0)
                {
                    currentNode.next = lastNode;
                    lastNode = currentNode;
                }
            }

            return lastNode;
        }

        void InsertToList(ListNode l1, List<int> tempList)
        {
            if (l1 == null)
            {
                return;
            }
            while (true)
            {
                tempList.Add(l1.val);
                if (l1.next == null)
                {
                    break;
                }
                l1 = l1.next;
            }
        }

        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode tmp = new ListNode(0);
            ListNode cur = tmp;

            while (l1 != null && l2 != null)
            {
                if (l1.val < l2.val)
                {
                    cur.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    cur.next = l2;
                    l2 = l2.next;
                }
                cur = cur.next;
            }

            if (l1 == null)
            {
                cur.next = l2;
            }
            else
            {
                cur.next = l1;
            }

            return tmp.next;
        }
    }

    /// <summary>
    /// https://leetcode-cn.com/problems/he-wei-sde-liang-ge-shu-zi-lcof/
    /// </summary>
    public class Q6
    {
        public int[] TwoSum(int[] nums, int target)
        {
            int l = 0;
            int r = nums.Length - 1;

            while (true)
            {
                if (nums[l] + nums[r] == target)
                {
                    return new int[2] { nums[l], nums[r] };
                }
                else if (nums[l] + nums[r] < target)
                {
                    l += 1;
                    continue;
                }
                else
                {
                    r -= 1;
                    continue;
                }
            }
        }
    }

    /// <summary>
    /// https://leetcode-cn.com/problems/insert-delete-getrandom-o1/
    /// </summary>
    public class Q7
    {
        public class RandomizedSet
        {
            Dictionary<int, string> intDic;
            List<int> intList;
            public RandomizedSet()
            {
                intDic = new Dictionary<int, string>();
                intList = new List<int>();
            }

            public bool Insert(int val)
            {
                bool result = false;
                if (!intDic.ContainsKey(val))
                {
                    result = true;
                    intDic.Add(val, null);
                    intList.Add(val);
                }
                return result;
            }

            public bool Remove(int val)
            {
                bool result = false;
                if (intDic.ContainsKey(val))
                {
                    result = true;
                    intDic.Remove(val);
                    intList.Remove(val);
                }
                return result;
            }

            public int GetRandom()
            {
                return intList[new Random().Next(0, intList.Count)];
            }
        }
    }
}
