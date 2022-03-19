using Xunit;
using HashTable;

namespace xUnitHashTable
{
    public class OpenAddressHashTableUnitTest
    {
        [Fact]
        public void HashTableNotEqualNullAfterCreating()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            Assert.NotNull(HashTable);
        }

        [Fact]
        public void HashTableAfterCreatedContainsZeroItems()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            Assert.Equal(0, HashTable.Count);
        }

        [Fact]
        public void IncreasedCapacity()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            HashTable.Add(1, "a");
            HashTable.Add(2, "a");
            HashTable.Add(3, "a");
            HashTable.Add(5, "a");
            HashTable.Add(6, "a");
            HashTable.Add(7, "a");

            Assert.Equal(61, HashTable.Capacity);
        }

        [Fact]
        public void Test()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            HashTable.Add(0, "a");
            HashTable.Add(7, "a");
            HashTable.Add(14, "a");

            HashTable.Remove(7);

            Assert.True(HashTable.ContainsKey(14));
            Assert.False(HashTable.ContainsKey(7));
        }
    }
}
