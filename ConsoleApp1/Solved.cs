using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Johnny_Leetcode_ConsoleApp
{
    public class Solved
    {
        #region Tesla
        public class InvoiceRepository
        {
            //private IQueryable<Invoice> currentInvoices;
            private List<Invoice> currentInvoices;

            //todo:
            //public InvoiceRepository(IQueryable<Invoice> invoices)
            public InvoiceRepository(List<Invoice> invoices)
            {
                //check if invoices is null 
                if (invoices == null || invoices?.Count() == 0)
                {
                    throw new ArgumentNullException();
                }

                currentInvoices = invoices;
            }

            /// <summary>
            /// Should return a total value of an invoice with a given id. If an invoice does not exist null should be returned.
            /// </summary>
            /// <param name="invoiceId"></param>
            /// <returns></returns>
            public decimal? GetTotal(int invoiceId)
            {
                if (currentInvoices.Any(o => o.Id == invoiceId))
                {
                    decimal total = 0;
                    //incase more than 1 invoices has the same id
                    var invoiceItems = currentInvoices.Where(o => o.Id == invoiceId && o.InvoiceItems != null && o.InvoiceItems.Count > 0).ToList();

                    if (invoiceItems?.Count > 0)
                    {
                        foreach (var item in invoiceItems)
                        {
                            total += item.InvoiceItems.Sum(o => o.Price * o.Count);
                        }

                        return total;
                    }
                }
                return null;
            }

            /// <summary>
            /// Should return a total value of all unpaid invoices.
            /// </summary>
            /// <returns></returns>
            public decimal GetTotalOfUnpaid()
            {
                decimal unpaid = 0;

                var unpaidInvoices = currentInvoices.Where(o => o.AcceptanceDate == null).ToList();

                if (unpaidInvoices?.Count > 0 && unpaidInvoices.Any(o => o?.InvoiceItems?.Count > 0))
                {
                    foreach (var item in unpaidInvoices.Where(o => o?.InvoiceItems?.Count > 0))
                    {
                        unpaid += item.InvoiceItems.Sum(o => o.Price * o.Count);
                    }
                }

                return unpaid;
            }

            /// <summary>
            /// Should return a dictionary where the name of an invoice item is a key and the number of bought items is a value.
            /// The number of bought items should be summed within a given period of time (from, to). Both the from date and the end date can be null.
            /// </summary>
            /// <param name="from"></param>
            /// <param name="to"></param>
            /// <returns></returns>
            public IReadOnlyDictionary<string, long> GetItemsReport(DateTime? from, DateTime? to)
            {
                Dictionary<string, long> result = new Dictionary<string, long>();

                var filteredInvoices = currentInvoices.Where(o => (from == null || o.CreationDate >= from) && (to == null || o.CreationDate <= to)).ToList();

                if (filteredInvoices?.Count > 0 && filteredInvoices.Any(o => o?.InvoiceItems?.Count > 0))
                {
                    foreach (var invoice in filteredInvoices.Where(o => o?.InvoiceItems?.Count > 0))
                    {
                        foreach (var item in invoice.InvoiceItems)
                        {
                            if (result.ContainsKey(item.Name))
                            {
                                result[item.Name] += item.Count;
                            }
                            else
                            {
                                result.Add(item.Name, item.Count);
                            }
                        }
                    }
                }

                return result;
            }
        }

        public class Invoice
        {
            // A unique numerical identifier of an invoice (mandatory)
            public int Id { get; set; }
            // A short description of an invoice (optional).
            public string Description { get; set; }
            // A number of an invoice e.g. 134/10/2018 (mandatory).
            public string Number { get; set; }
            // An issuer of an invoice e.g. Metz-Anderson, 600  Hickman Street,Illinois (mandatory).
            public string Seller { get; set; }
            // A buyer of a service or a product e.g. John Smith, 4285  Deercove Drive, Dallas (mandatory).
            public string Buyer { get; set; }
            // A date when an invoice was issued (mandatory).
            public DateTime CreationDate { get; set; }
            // A date when an invoice was paid (optional).
            public DateTime? AcceptanceDate { get; set; }
            // A collection of invoice items for a given invoice (can be empty but is never null).
            public IList<InvoiceItem> InvoiceItems { get; }

            public Invoice()
            {
                InvoiceItems = new List<InvoiceItem>();
            }
        }

        public class InvoiceItem
        {
            // A name of an item e.g. eggs.
            public string Name { get; set; }
            // A number of bought items e.g. 10.
            public uint Count { get; set; }
            // A price of an item e.g. 20.5.
            public decimal Price { get; set; }
        }

        public class ValidateArguments
        {

            private static string nameCommand = "--name";
            private static string countCommand = "--count";
            private static string helpCommand = "--help";
            private static bool isHelp = false;

            public int Validate(string[] args)
            {
                // Console.WriteLine("Sample debug output");

                if (args == null || args?.Length == 0)
                {
                    return -1;
                }

                List<string> argsList = args.ToList();

                if (argsList.Any(o => o.Length < 2 || o.Length > 10))
                {
                    return -1;
                }

                for (int i = 0; i < argsList.Count; i++)
                {
                    var item = argsList[i].ToLowerInvariant();

                    //check name
                    if (item == nameCommand)
                    {
                        if (i + 1 >= argsList.Count)
                        {
                            return -1;
                        }
                        else if (argsList[i + 1].Length < 3 || argsList[i + 1].Length > 10)
                        {
                            return -1;
                        }

                        i += 1;
                        continue;
                    }

                    //check count
                    if (item == countCommand)
                    {
                        int count = 0;
                        if (i + 1 >= argsList.Count)
                        {
                            return -1;
                        }
                        else if (!int.TryParse(argsList[i + 1], out count))
                        {
                            return -1;
                        }
                        else if (count < 10 || count > 100)
                        {
                            return -1;
                        }

                        i += 1;
                        continue;
                    }

                    //check help
                    if (item == helpCommand)
                    {
                        isHelp = true;
                        continue;
                    }

                    return -1;
                }

                if (isHelp)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }


        }

        public bool CheckString(string a, string b)
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
                aList[aList.Count() - 1] = a0;
            }


            return false;

        }

        private string Add1AtLast(string input)
        {
            string result = "";

            var stringArray = input.ToCharArray();

            //last != 9
            if (stringArray.Length > 0)
            {
                if (stringArray[stringArray.Length] != '9')
                {
                    int lastNum = Convert.ToInt32(stringArray[stringArray.Length]) + 1;

                    for (int i = 0; i < stringArray.Length - 1; i++)
                    {
                        result += stringArray[i].ToString();
                    }
                    result += lastNum.ToString();
                }
                //last == 9
                else
                {
                    //23459999
                    if (stringArray.Any(o => o != '9'))
                    {
                        //5 
                        int lastIndex = -1;
                        for (int i = stringArray.Length - 1; i > 0; i--)
                        {
                            if (stringArray[i] != '9')
                            {
                                lastIndex = i;
                            }
                        }


                        for (int i = 0; i < stringArray.Length - 1; i++)
                        {
                            result += stringArray[i].ToString();
                        }
                        //2346
                        //0000

                    }
                    //9999
                    else
                    {
                        result = "1";
                        for (int i = 0; i < stringArray.Length - 1; i++)
                        {
                            result += "0";
                        }
                    }
                }
            }

            string temp = input;
            bool isSuccess = false;
            while (temp.Length > 0)
            {
                var lastChar = temp.LastOrDefault();
                if (lastChar != '9')
                {
                    //+1
                    //result = ...
                    isSuccess = true;
                    break;
                }
                else
                {
                    // == 9
                    temp = temp.Remove(temp.Length);
                    continue;
                }
            }

            if (!isSuccess)
            {
                //999999
                //result = ...
            }


            return result;
        }
        #endregion

        #region FNZ
        public string solution1(string S, int K)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)

            //result
            string result;

            //days of week, list
            List<string> weekDays = new List<string>
                {
                    "Mon",
                    "Tue",
                    "Wed",
                    "Thu",
                    "Fri",
                    "Sat",
                    "Sun"
                };

            //K % 7
            int R = K % 7;

            //index of input S
            int indexS = weekDays.IndexOf(S);

            //index of result
            int indexResult = (indexS + R) % 7;

            result = weekDays[indexResult];

            return result;
        }

        public int solution2(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)

            int result;

            //use list
            List<int> inputList = A.OrderBy(o => o).ToList();

            //max distance
            int masDistance = 0;

            //find max gap
            for (int i = 0; i < inputList.Count - 1; i++)
            {
                masDistance = Math.Max(masDistance, Math.Abs(inputList[i + 1] - inputList[i]));
            }

            //get result
            if (masDistance <= 2)
            {
                result = 0;
            }
            //else if (masDistance <= 4)
            //{
            //    result = 1;
            //}
            else if (masDistance % 2 == 0)
            {
                result = (masDistance / 2) - 1;
            }
            else
            {
                result = Convert.ToInt32((masDistance / 2) - 0.5);
            }

            return result;
        }

        public int solution3(int[] A)
        {
            //use list
            List<int> inputList = A.OrderBy(o => o).ToList();

            int ans = int.MinValue;
            if (inputList.Count == 2) return (inputList[1] - inputList[0]) / 2;
            for (int i = 0; i < inputList.Count - 1; i++)
            {
                //如果两点之间有空位置
                if (inputList[i + 1] - inputList[i] > 1)
                {
                    ans = Math.Max(ans, inputList[i + 1] - inputList[i]);
                }
            }

            return ans / 2;
        }

        public int FindMinMissingInt(int[] A)
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

        #endregion

        #region LeetCode

        /// <summary>
        /// https://leetcode-cn.com/problems/two-sum/
        /// </summary>
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

        #endregion
    }
}
