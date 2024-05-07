using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomList
{
    public class CustomList<T> : IList<T>
    {
        Item<T> _list;
        int count;
        /// <summary>
        /// The property return first element of list 
        /// </summary>
        public Item<T> Head
        {
            get => _list;
        }

        /// <summary>
        /// The property return number of elements contained in the CustomList
        /// </summary>
        public int Count
        {
            get => count;
            private set => count = value;
        }

        /// <summary>
        /// Gets a value indicating whether the IList is read-only.
        /// Make it always false
        /// </summary>
        public bool IsReadOnly => false;


        /// <summary>
        /// Constructor that gets params T as parameter       
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when values is null</exception>
        /// <param name="values"></param>
        public CustomList(params T[] values)
        {
            Count = 0;
            var indexer = _list;
            if (values is null)
                throw new ArgumentNullException(nameof(values), "null");
            foreach (var item in values)
            {
                if (_list == null)
                {
                    _list = new Item<T>(item);
                    indexer = _list;
                    Count++;
                }
                else
                {
                    indexer.Next = new Item<T>(item);
                    indexer = indexer.Next;
                    Count++;
                }
            }
        }


        /// <summary>
        /// Constructor that gets Ienumerable collection as parameter       
        /// </summary>
        ///<exception cref="ArgumentNullException">Thrown when values is null</exception>
        /// <param name="values"></param>
        public CustomList(IEnumerable<T> values)
        {
            Count = 0;
            var indexer = _list;
            if (values == null) throw new ArgumentNullException(nameof(values), "null");
            foreach (var value in values)
            {
                if (_list == null)
                {
                    _list = new Item<T>(value);
                    indexer = _list;
                    Count++;
                }
                else
                {
                    indexer.Next = new Item<T>(value);
                    indexer = indexer.Next;
                    Count++;
                }
            }
        }

        /// <summary>
        /// Get or set data at the position.
        /// </summary>
        /// <param name="index">Position</param>
        /// <exception cref="IndexOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count - 1) throw new IndexOutOfRangeException();
                int idn = 0;
                if (_list != null)
                {
                    var indexer = _list;
                    while (indexer != null)
                    {
                        if (index == idn)
                        {
                            return indexer.Data;
                        }
                        indexer = indexer.Next;
                        idn++;
                    }
                }
                return _list.Data;
            }
            set
            {
                if (index < 0 || index > Count - 1) 
                    throw new IndexOutOfRangeException("out of range");
                int idn = 0;
                if (_list != null)
                {
                    var indexer = _list;
                    while (indexer != null)
                    {
                        if (index == idn)
                        {
                            indexer.Data = value;
                            break;
                        }
                        indexer = indexer.Next;
                        idn++;
                    }
                }
            }
        }


        /// <summary>
        ///  Adds an object to the end of the CustomList.
        /// </summary>
        /// <param name="item">Object that should be added in the CustomList</param>
        /// <exception cref="ArgumentNullException">Throws when you try to add null</exception>
        public void Add(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), "null");
            Count++;
            var indexer = _list;
            while (indexer.Next != null)
            {
                indexer = indexer.Next;
            }
            indexer.Next = new Item<T>(item);
        }


        /// <summary>
        /// Removes all elements from the CustomList
        /// </summary>
        public void Clear()
        {
            _list = null;
            count = 0;
        }

        /// <summary>
        /// Determines whether an element is in the CustomList
        /// </summary>
        /// <param name="item">Object we check to see if it is on the CustomLIst</param>
        /// <returns>True if the element exists in the CustomList, else false</returns>
        public bool Contains(T item)
        {
            bool isTrue = false;
            if (_list != null)
            {
                var indexer = _list;
                while (indexer != null)
                {
                    if (indexer.Data.ToString() == item.ToString())
                    {
                        isTrue = true;
                        break;
                    }
                    indexer = indexer.Next;
                }
            }
            return isTrue;
        }


        /// <summary>
        /// Removes the first occurrence of a specific object from the CustomList.
        /// </summary>
        /// <param name="item"> The object to remove from the CustomList</param>
        /// <returns>True if item is successfully removed; otherwise, false. This method also returns
        ///     false if item was not found in the CustomList.</returns>
        /// <exception cref="ArgumentNullException">Throws when you try to remove null</exception>
        public bool Remove(T item)
        {
            if (item is null)
                throw new ArgumentNullException("item");
            if (Contains(item))
            {
                RemoveAt(IndexOf(item));
                return true;
            }
            return false;
        }


        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first
        ///     occurrence within the CustomList.
        /// </summary>
        /// <param name="item">The object whose index we want to get </param>
        /// <returns>The zero-based index of the first occurrence of item within the entire CustomList,
        ///    if found; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            int index = 0; bool isTrue = false;
            if (_list != null)
            {
                var indexer = _list;
                while (indexer != null)
                {
                    if (indexer.Data.ToString() == item.ToString())
                    {
                        isTrue = true;
                        break;
                    }
                    indexer = indexer.Next;
                }
                index++;
            }
            if (isTrue)
                return index;
            else return -1;
        }


        /// <summary>
        /// Inserts an element into the CustomList at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        /// <exception cref="ArgumentNullException">Thrown when item is null</exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(new string("values"));
            if (item == null)
                throw new ArgumentNullException("item");
            if (_list != null)
            {
                var indexer = _list;
                var newItem = new Item<T>(item);
                if (index == 0)
                {
                    newItem.Next = Head;
                    _list = newItem;
                    Count++;
                    return;
                }
                if (index == Count - 1)
                {

                    newItem.Next = indexer;
                    
                    Count++;
                    return;
                }
                var Prev = Head;
                for (int i = 1; i < index; i++)
                {
                    Prev = Prev.Next;
                }
                var Next = Prev.Next;
                Prev.Next = newItem;
                newItem.Next = Next;
                Count++;
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new ArgumentOutOfRangeException(new string("Index"));
            if (_list != null)
            {
                var indexer = _list;
                if (index == 0)
                {
                    _list = _list.Next;
                    Count--;
                    return;
                }
                for (int i = 1; i < index; i++)
                {
                    indexer = indexer.Next;
                }
                var memberDel = indexer.Next;
                indexer.Next = memberDel.Next;
                Count--;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array),"null");
            if (Count > array.Length) throw new ArgumentException("error");
            while (_list != null)
            {
                array[arrayIndex] = _list.Data;
                _list = _list.Next;
                arrayIndex++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                var indexer = _list.Data;
                _list = _list.Next;
                yield return indexer;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
