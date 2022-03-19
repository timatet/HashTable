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
        public void AfterAddingCountMustBeIncremented()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            int oldCount = HashTable.Count;

            HashTable.Add(1, string.Empty);

            int newCount = HashTable.Count;

            Assert.Equal(newCount, oldCount + 1);
        }

        [Fact]
        public void AfterAddingExistItemThrowingException()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();
            HashTable.Add(1, string.Empty);

            Assert.Throws<ItemAlreadyExistInTableException>(() =>
            {
                HashTable.Add(1, "a");
            });
        }
    }
}
