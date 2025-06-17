using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;



class Result
{

    /*
     * Complete the 'maxSubarrayLength' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY badges as parameter.
     */

    public static int maxSubarrayLength(List<int> badges)
    {
        int n = badges.Count;
        int length = 0;
        int start = 0;
        int prod = 1;
        for (int i = 0; i < n; i++)
        {
            prod *= badges[i] < 0 ? -1 : 1;
            if (badges[i] == 0)
            {
                prod = 1;
                start = i + 1;
                continue;
            }
            else if (prod < 0 && (i == n - 1 || badges[i + 1] == 0))
            {
                while (start <= i && prod < 0)
                {
                    if (badges[start] < 0)
                    {
                        prod *= -1;
                    }

                    start++;
                }
            }
            if (prod > 0)
            {
                length = Math.Max(length, i - start + 1);
            }
        }
        return length;
    }

    class Solution
    {
        public static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int badgesCount = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> badges = new List<int>();

            for (int i = 0; i < badgesCount; i++)
            {
                int badgesItem = Convert.ToInt32(Console.ReadLine().Trim());
                badges.Add(badgesItem);
            }

            int result = Result.maxSubarrayLength(badges);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
