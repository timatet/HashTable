using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    public class ItemAlreadyExistInTableException : Exception
    {
        public ItemAlreadyExistInTableException()
            : base("Duplication of items in Hash Table is not allowed!") { }
        public ItemAlreadyExistInTableException(string message)
            : base(message) { }
    }
    public class ItemNotExistInTableException : Exception
    {
        public ItemNotExistInTableException()
            : base("Item not exist in Hash Table!") { }
        public ItemNotExistInTableException(string message)
            : base(message) { }
    }
    public class HashTableIsFullException : Exception
    {
        public HashTableIsFullException()
            : base("Hash Table is Full!") { }
        public HashTableIsFullException(string message)
            : base(message) { }
    }
}
