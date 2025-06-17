using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;



class ResultImbalance
{

    /*
     * Complete the 'getTotalImbalance' function below.
     *
     * The function is expected to return a LONG_INTEGER.
     * The function accepts INTEGER_ARRAY weight as parameter.
     */

    public static long getTotalImbalance(List<int> weight)
    {
        List<int> li = new List<int>();
        int n = weight.Count;
        int toallImbalance = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                li.Clear();
                for (int k = i; k <= j; k++)
                {
                    li.Add(weight[k]);
                }
                li.Sort();
                int num1 = li.FirstOrDefault();
                int num2 = li.LastOrDefault();
                toallImbalance += num1 > num2 ? (num1 - num2) : num2 - num1;
            }
        }
        return toallImbalance;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int weightCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> weight = new List<int>();

        for (int i = 0; i < weightCount; i++)
        {
            int weightItem = Convert.ToInt32(Console.ReadLine().Trim());
            weight.Add(weightItem);
        }

        long result = ResultImbalance.getTotalImbalance(weight);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}