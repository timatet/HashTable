using System;
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

            string[] text = sr.ReadToEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //Создание частотного словаря
            foreach (var item in text)
                if (Frequencies.ContainsKey(item))
                    Frequencies[item]++;
                else
                    Frequencies.Add(item, 1);

            Init();

            Console.WriteLine("Процесс пошел");

            var hashTable = new OpenAddressHashTable<string, int>();
            var dictionary = new Dictionary<string, int>();

            stopWatch.Start();
            foreach (var pair in Frequencies)
            {
                hashTable.Add(pair.Key, pair.Value);
            }
            stopWatch.Stop();
            Console.WriteLine("HT.ADD: " + stopWatch.ElapsedMilliseconds);

            stopWatch.Start();
            foreach (var pair in Frequencies)
            {
                dictionary.Add(pair.Key, pair.Value);
            }
            stopWatch.Stop();
            Console.WriteLine("DI.ADD: " + stopWatch.ElapsedMilliseconds);

            var WordsWithFreqMore27 = Frequencies.Where(word => word.Value > 0).Select(word => word.Key);

            stopWatch.Start();
            foreach (var word in WordsWithFreqMore27)
            {
                hashTable.Remove(word);
            }
            stopWatch.Stop();
            Console.WriteLine("HT.REM: " + stopWatch.ElapsedMilliseconds);

            stopWatch.Start();
            foreach (var word in WordsWithFreqMore27)
            {
                dictionary.Remove(word);
            }
            stopWatch.Stop();
            Console.WriteLine("DI.REM: " + stopWatch.ElapsedMilliseconds);

            Console.WriteLine(hashTable.Capacity);

            Console.ReadKey();
        }

        public static void Init()
        {
            OpenAddressHashTable<int, int> hashTable = new OpenAddressHashTable<int, int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < 100; i++)
            {
                hashTable.Add(i, i);
                dict.Add(i, i);
            }
        }
    }
}
