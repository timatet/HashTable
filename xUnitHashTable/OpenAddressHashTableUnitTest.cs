using Xunit;
using HashTable;
using System.Collections.Generic;

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
        public void IncreasedCapacityAfterAddingItemsOverFillFactor()
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

            int actualCount = HashTable.Count;

            Assert.Equal(actualCount, oldCount + 1);
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

        [Fact]
        public void WaitingOfCollisionsAddingItemsDivisibleCapacity()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            for (int i = 0; i < 5 * 7; i += 7)
            {
                HashTable.Add(i, string.Empty);
            }
        }

        [Fact]
        public void ThrowingHashTableIsFullExceptionAfterUsingHashTableWithEvenCapacity()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>(capacity: 4);

            Assert.Throws<HashTableIsFullException>(() =>
            {
                HashTable.Add(0, string.Empty);
                HashTable.Add(2, string.Empty);
                HashTable.Add(4, string.Empty);
            });
        }

        [Fact]
        public void ThrowingItemNotExistInTableExceptionAfterRemovingItemThatNotContainsInHashTable()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            Assert.Throws<ItemNotExistInTableException>(() =>
            {
                HashTable.Remove(1);
            });
        }

        [Fact]
        public void InEmptyHashTableMustBeNotContainingItems()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            Assert.False(HashTable.ContainsKey(1));
        }

        [Fact]
        public void AfterItemAddingThisItemMustBeContainsInHashTable()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();
            HashTable.Add(1, string.Empty);

            Assert.True(HashTable.ContainsKey(1));
        }

        [Fact]
        public void AfterItemRemovingThisItemMustBeNotContainsInHashTable()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();
            HashTable.Add(1, string.Empty);
            HashTable.Remove(1);

            Assert.False(HashTable.ContainsKey(1));
        }

        [Fact]
        public void AfterItemRemovingCountMustBeDecremented()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();
            HashTable.Add(1, string.Empty);

            int oldCount = HashTable.Count;
            HashTable.Remove(1);
            int actualCount = HashTable.Count;

            Assert.Equal(actualCount, oldCount - 1);
        }

        [Fact]
        public void ThrowingKeyNotFoundExceptionAfterRequestingItemThatNotContainsInHashTable()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();

            Assert.Throws<KeyNotFoundException>(() =>
            {
                var value = HashTable[0];
            });
        }

        [Fact]
        public void AfterAddingInEmptyHashTableZeroItemHeMustBeFindWithIndexatorOnTheZeroPosition()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();
            string testValue = "testValue";
            HashTable.Add(0, testValue);

            var value = HashTable[0];

            Assert.Equal(testValue, value);
        }

        [Fact]
        public void ItemsThatFindedWithForEachMustBeContainsInHashTable()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>();
            HashTable.Add(0, "a");
            HashTable.Add(1, "b");
            HashTable.Add(2, "c");
            HashTable.Add(3, "d");

            List<KeyValuePair<int, string>> AddedInHashTableItems = new List<KeyValuePair<int, string>>();
            AddedInHashTableItems.Add(new KeyValuePair<int, string>(0, "a"));
            AddedInHashTableItems.Add(new KeyValuePair<int, string>(1, "b"));
            AddedInHashTableItems.Add(new KeyValuePair<int, string>(2, "c"));
            AddedInHashTableItems.Add(new KeyValuePair<int, string>(3, "d"));


            int k = 0;
            foreach (var item in HashTable)
            {
                Assert.Equal(item, AddedInHashTableItems[k++]);
            }
        }

        [Fact]
        public void CapacityMustBeIncreasedAfterAddingSoManyItemsAfterInitHashTableWithAssignedCapacity()
        {
            OpenAddressHashTable<int, string> HashTable = new OpenAddressHashTable<int, string>(19);
            
            for (int i = 0; i < 19; i++)
            {
                HashTable.Add(i, string.Empty);
            }
        }

    }
}
