using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.PriorityQueueLibrary
{
    /// <summary>
    /// A que that removes items with minimum priority first
    /// </summary>
    /// <typeparam name="TPriority">Data type of the priority, must implement IComparable</typeparam>
    /// <typeparam name="TValue">Values soreted in the Queue</typeparam>
    public class MinPriorityQueue<TPriority, TValue> where TPriority : IComparable<TPriority>
    {
        /// <summary>
        /// A leftist heap storing the elements and their priorities.
        /// </summary>
        private LeftistTree<KeyValuePair<TPriority, TValue>> _elements = null;

        /// <summary>
        /// Gets the number of elements.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Adds the given element with the given priority.
        /// </summary>
        /// <param name="p">The priority of the element.</param>
        /// <param name="x">The element to add.</param>
        public void Add(TPriority p, TValue x)
        {
            LeftistTree<KeyValuePair<TPriority, TValue>> node = new LeftistTree<KeyValuePair<TPriority, TValue>>(new KeyValuePair<TPriority, TValue>(p, x), null, null);
            _elements = Merge(_elements, node);
            Count++;
        }

        /// <summary>
        /// Merges the given leftist heaps into one leftist heap.
        /// </summary>
        /// <param name="h1">One of the leftist heaps to merge.</param>
        /// <param name="h2">The other leftist heap to merge.</param>
        /// <returns>The resulting leftist heap.</returns>
        public static LeftistTree<KeyValuePair<TPriority, TValue>> Merge(LeftistTree<KeyValuePair<TPriority, TValue>> h1, LeftistTree<KeyValuePair<TPriority, TValue>> h2)
        {
            if (h1 == null)
                return h2;
            if (h2 == null)
                return h1;
            int comp = h1.Data.Key.CompareTo(h2.Data.Key);
            if(comp < 0)
                return new LeftistTree<KeyValuePair<TPriority, TValue>>(h1.Data, h1.LeftChild, Merge(h1.RightChild, h2));
            else
                return new LeftistTree<KeyValuePair<TPriority, TValue>>(h2.Data, h2.LeftChild, Merge(h2.RightChild, h1));

        }

        /// <summary>
        /// Priority stored in the root of the leftist heap
        /// </summary>
        public TPriority MinimumPriority
        {
            get
            {
                if (_elements == null)
                    throw new InvalidOperationException();
                else
                    return _elements.Data.Key;
            }
        }

        /// <summary>
        /// Remove the lowest priority element in the queue
        /// </summary>
        /// <returns>Lowest priority element</returns>
        public TValue RemoveMinimumPriority()
        {
            if (_elements == null)
                throw new InvalidOperationException();
            TValue v = _elements.Data.Value;
            _elements = Merge(_elements.LeftChild, _elements.RightChild);
            Count--;
            return v;
        }
    }
}
