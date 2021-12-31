using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp1
{
    class Class2
    {
        private string test(string input)
        {
            string result = "";


            int n;

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

    }
}
