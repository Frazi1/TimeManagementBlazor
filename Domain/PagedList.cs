using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class PagedList<T>
    {
        public int AllItemsCount { get; set; }
        public ICollection<T> Items { get; set; }

        public PagedList()
        {
        }
        
        public PagedList(int allItemsCount, List<T> items)
        {
            AllItemsCount = allItemsCount;
            Items = items;
        }

        // public IEnumerator<T> GetEnumerator()
        // {
        //     return Items.GetEnumerator();
        // }
        //
        // IEnumerator IEnumerable.GetEnumerator()
        // {
        //     return GetEnumerator();
        // }
    }
}