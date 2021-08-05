using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = File.ReadAllLines("../../eng_news_2005_100K-sentences.txt");
            Console.WriteLine(s.Length);

            var idx = new Dictionary<char, Dictionary<char, int>>();
            var lens = new Dictionary<char, int>();

            foreach(var _line in s)
            {
                var line = _line.Trim();
                var tb = line.IndexOf((char)9);
                line = line.Substring(tb + 1);
                var prev = (char)1;
                Dictionary<char, int> d;
                foreach (var c in line)
                {
                    if (!idx.ContainsKey(prev))
                    {
                        idx[prev] = new Dictionary<char, int>();
                    }
                    d = idx[prev];
                    if (d.ContainsKey(c))
                    {
                        d[c]++;
                    }
                    else
                    {
                        d[c] = 1;
                    }
                    prev = c;
                }
                if (!idx.ContainsKey(prev))
                {
                    idx[prev] = new Dictionary<char, int>();
                }
                d = idx[prev];
                if (d.ContainsKey((char)1))
                {
                    d[(char)1]++;
                }
                else
                {
                    d[(char)1] = 1;
                }
            }

            foreach (var p in idx)
            {
                var len = 0;
                foreach (var pair in p.Value)
                {
                    len += pair.Value;
                }
                lens[p.Key] = len;
            }

            var random = new Random();
            for (int i = 0; i < 10; ++i)
            {
                char prev = (char)1;
                do
                {
                    var r = random.Next(0, lens[prev]);
                    int m = 0;
                    char c = (char)1;
                    foreach (var pair in idx[prev])
                    {
                        if (m + pair.Value > r)
                        {
                            c = pair.Key;
                            break;
                        }
                        m += pair.Value;
                    }

                    if (c == (char)1)
                    {
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write(c);
                    }
                    prev = c;
                }
                while (prev != (char)1);
            }

            Console.ReadLine();
        }
    }
}
