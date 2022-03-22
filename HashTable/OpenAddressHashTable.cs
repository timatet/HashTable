using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{ 
    /// <summary>
    /// Хэш-таблица с открытой адресацией
    /// </summary>
    /// <typeparam name="TKey">Ключ</typeparam>
    /// <typeparam name="TValue">Значение</typeparam>
    public class OpenAddressHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, 
        IHashTable<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private int[] SimplifyNumbers = new int[] { 7, 19, 61, 167, 359, 857, 1721, 3469,
            7103, 14177, 29063, 50833, 99991, 331777, 614657, 1336337, 4477457,
            8503057, 29986577, 45212107, 99990001, 126247697 };

        private int CurrentSimplifyIndex = 0;
        Pair<TKey, TValue>[] _table;
        public int Capacity { get; private set; }

        HashMaker<TKey> _hashMaker1, _hashMaker2;
        public int Count { get; private set; }
        private double FillFactor = 0.77;

        public OpenAddressHashTable() : this(7, 0.77)
        { }

        public OpenAddressHashTable(double fillfactor) : this(7, fillfactor)
        { }

        public OpenAddressHashTable(int capacity) : this(capacity, 0.77)
        { }

        public OpenAddressHashTable(int capacity, double fillfactor)
        {
            _table = new Pair<TKey, TValue>[capacity];
            Capacity = capacity;
            _hashMaker1 = new HashMaker<TKey>(Capacity);
            _hashMaker2 = new HashMaker<TKey>(Capacity - 1);
            Count = 0;

            FillFactor = fillfactor;
        }

        /// <summary>
        /// Вставка элемента в хэш-таблицу
        /// </summary>
        /// <param name="key">Ключ элемента</param>
        /// <param name="value">Значение элемента</param>
        public void Add(TKey key, TValue value)
        {
            var PairForInsert = new Pair<TKey, TValue>(key, value);
            
            //Первое предполагаемое место вставки
            var hash = _hashMaker1.ReturnHash(key); 

            if (!TryToPut(hash, PairForInsert)) 
            {
                //Поскольку ячейка занята, пытаемся найти удобное пустое
                //место для вставки элемента
                int iterationNumber = 1;
                while (true)
                { 
                    var place = (hash + iterationNumber * (1 + _hashMaker2.ReturnHash(key))) % Capacity;
                    if (TryToPut(place, PairForInsert))
                        break;
                    iterationNumber++;
                    if (iterationNumber >= Capacity)
                        throw new HashTableIsFullException();
                }
            }

            if ((double)Count / Capacity >= FillFactor)
            {
                IncreaseTable();
            }
        }

        private bool TryToPut(int place, Pair<TKey, TValue> pairForInsert)
        {
            //Элемент можно вставить на место, только в случае если оно не занято
            if (_table[place] == null || _table[place].IsDeleted())
            {
                _table[place] = pairForInsert;
                Count++;
                return true;
            }

            //Причем такой элемент не должен уже содержаться в таблице
            if (_table[place].Equals(pairForInsert))
            {
                throw new ItemAlreadyExistInTableException();
            }

            //Иначе ячейка занята другим элементов
            //Необходимо решить коллизию
            return false;
        }

        private Pair<TKey, TValue> Find(TKey x)
        {
            var hash = _hashMaker1.ReturnHash(x); 

            if (_table[hash] == null)
                return null;

            if (!_table[hash].IsDeleted() && _table[hash].Key.Equals(x))
            {
                return _table[hash];
            }

            int iterationNumber = 1;
            while (true)
            { 
                var place = (hash + iterationNumber * (1 + _hashMaker2.ReturnHash(x))) % Capacity;
                if (_table[place] == null)
                    return null;
                if (!_table[place].IsDeleted() && _table[place].Key.Equals(x))
                {
                    return _table[place];
                }
                iterationNumber++;
                if (iterationNumber >= Capacity)
                    return null;
            }
        }

        /// <summary>
        /// Removes an item from the hash table.
        /// </summary>
        /// <param name="x">Item for removing</param>
        public void Remove(TKey x)
        {
            var ItemForRemoving = Find(x);

            if (ItemForRemoving == null)
            {
                throw new ItemNotExistInTableException();
            }

            ItemForRemoving.DeletePair();
            Count--;
        }

        private void IncreaseTable()
        {
            if (CurrentSimplifyIndex == SimplifyNumbers.Length - 1)
            {
                throw new ArgumentException("Дальнейшее расширение невозможно");
            }

            Capacity = SimplifyNumbers.First(item => item > Capacity);

            var tmpOldTable = this._table;

            _table = new Pair<TKey, TValue>[Capacity];

            _hashMaker1 = new HashMaker<TKey>(Capacity);
            _hashMaker2 = new HashMaker<TKey>(Capacity - 1);

            Count = 0;

            foreach (var pair in tmpOldTable)
            {
                if (pair != null && !pair.IsDeleted())
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            return Find(key) != null;
        }

        public TValue this[TKey key]
        {
            get
            {
                var pair = Find(key);
                if (pair == null)
                    throw new KeyNotFoundException();
                return pair.Value;
            }

            set
            {
                var pair = Find(key);
                if (pair == null)
                    throw new KeyNotFoundException();
                pair.Value = value;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return (from pair in _table where pair != null && !pair.IsDeleted() select new KeyValuePair<TKey, TValue>(pair.Key, pair.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
