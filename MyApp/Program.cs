using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HashTable;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            var sr = new StreamReader("WarAndWorld.txt");
            Dictionary<string, int> Frequencies = new Dictionary<string, int>();

            string[] words = sr.ReadToEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Init();

            Console.WriteLine("Процесс пошел");

            for (double i = 0.75; i <= 0.85; i += 0.01)
            {
                Console.WriteLine("FF: " + i);
                stopWatch.Restart();
                UseHashTable(words, i);
                stopWatch.Stop();
                Console.WriteLine("\t HASH: " + stopWatch.ElapsedMilliseconds);
            }

            stopWatch.Restart();
            UseDictionary(words);
            stopWatch.Stop();
            Console.WriteLine("DICT: " + stopWatch.ElapsedMilliseconds);

            //Console.WriteLine(hashTable.Capacity);

            Console.ReadKey();
        }

        public static void UseHashTable(string[] words, double FillFactor)
        {
            var hashTable = new OpenAddressHashTable<string, int>(FillFactor);

            foreach (var item in words)
                if (hashTable.ContainsKey(item))
                    hashTable[item]++;
                else
                    hashTable.Add(item, 1);

            var WordsWithFreqMore27 = new List<string>(hashTable.Where(word => word.Value > 27).Select(word => word.Key));

            foreach (var word in WordsWithFreqMore27)
            {
                hashTable.Remove(word);
            }
        }

        public static void UseDictionary(string[] words)
        {
            var dictionary = new Dictionary<string, int>();

            foreach (var item in words)
                if (dictionary.ContainsKey(item))
                    dictionary[item]++;
                else
                    dictionary.Add(item, 1);

            var WordsWithFreqMore27 = new List<string>(dictionary.Where(word => word.Value > 27).Select(word => word.Key));

            foreach (var word in WordsWithFreqMore27)
            {
                dictionary.Remove(word);
            }
        }

        public static void Init()
        {
            OpenAddressHashTable<int, int> hashTable = new OpenAddressHashTable<int, int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < 10000; i++)
            {
                hashTable.Add(i, i);
                dict.Add(i, i);
            }
        }
    }
}
