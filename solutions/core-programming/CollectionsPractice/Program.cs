// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;

Console.WriteLine("Hello, World!");

PerformLoggedServiceCountProblem();
Console.ReadLine();


static void PerformLoggedServiceCountProblem()
{
    /// <summary>
    /// You receive a stream of service log entries (just strings with service names) and need to implement a component that tracks the most frequently seen services in real-time.
    ///Requirements:
    ///Log(serviceName) records a log entry for a service.
    ///GetTopK(k) returns the top k most frequently logged services, sorted from most to least frequent.
    /// If two services have the same count, return them in alphabetical order.
    /// </summary>


    // when you have2 services with same count then you need to perform some other logic to return in alphabatical order
    DictionaryTest dictionaryTest = new DictionaryTest();

    // can achieve above problem using sortedDictionay easily as it store the keys in sorted order ,
    // just you need to perform order by desc to get tbased on most to least usage
    //SortedDictionaryTest dictionaryTest = new SortedDictionaryTest(); 
    dictionaryTest.Log("DB");
    dictionaryTest.Log("File");
    dictionaryTest.Log("Console");
    dictionaryTest.Log("Console");
    dictionaryTest.Log("Console");
    dictionaryTest.Log("File");
    dictionaryTest.Log("DB");
    dictionaryTest.Log("ABC");
    dictionaryTest.Log("ABC");

    var items = dictionaryTest.GetTopK(2);
    foreach (var item in items)
        Console.WriteLine(item);
}

class DictionaryTest
{


    Dictionary<string, int> map = new Dictionary<string, int>();
    public void Log(string msg)
    {
        if (map.ContainsKey(msg))
        {
            map[msg]++;
        }
        else
        {
            map[msg] = 1;
        }
    }

    //
    public List<string> GetTopK(int topK)
    {
        var ret = new List<string>();

        var res = map.OrderByDescending(x => x.Value).OrderBy(x => x.Key).Take(topK).ToList();

        return res.Select(x => x.Key).ToList();
    }

}

class SortedDictionaryTest
{


    SortedDictionary<string, int> map = new SortedDictionary<string, int>();
    public void Log(string msg)
    {
        if (map.ContainsKey(msg))
        {
            map[msg]++;
        }
        else
        {
            map[msg] = 1;
        }
    }

    //
    public List<string> GetTopK(int topK)
    {
        var ret = new List<string>();


        var res = map.OrderByDescending(x => x.Value).Take(topK).ToList();

        return res.Select(x => x.Key).ToList();
    }

}