using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourSolver
{
    class Heap<T> where T : IComparable
    {
        private List<T> items;
        private bool reversed;

        //CONSTRUCTORS
        public Heap(bool reversed)
        {
            items = new List<T>();
            this.reversed = reversed;
        }

        //METHODS
        private bool greater(int a, int b)
        {
            return (this.items[a].CompareTo(this.items[b]) > 0 ^ this.reversed);
        }

        private bool less(int a, int b)
        {
            return (this.items[a].CompareTo(this.items[b]) < 0 ^ this.reversed);
        }

        private int parent(int i)
        {
            return (i - 1)>>1;
        }

        private void swap(int a, int b)
        {
            T fst = this.items[a];

            this.items[a] = this.items[b];
            this.items[b] = fst;
        }

        private int left(int i)
        {
            return (i<<1) + 1;
        }

        private int right(int i)
        {
            return (i<<1) + 2;
        }

        private void bubbleUp()
        {
            int index = this.items.Count - 1;
            int parent = this.parent(index);

            while (index > 0 && this.greater(index, parent))
            {
                this.swap(index, parent);
                index = parent;
                parent = this.parent(index);
            }
        }

        private void bubbleDown()
        {
            int index = 0;
            int left = this.left(index);
            int right = this.right(index);
            int child;

            while (left < this.items.Count)
            {
                if (right < this.items.Count && this.greater(right, left))
                    child = right;
                else
                    child = left;

                if (this.greater(child, index))
                {
                    this.swap(child, index);
                    index = child;
                }
                else
                {
                    return;
                }

                left = this.left(index);
                right = this.right(index);
            }
        }

        public void Insert(T item)
        {
            this.items.Add(item);
            this.bubbleUp();
        }
            
        public T Get()
        {
            T ret = this.items[0];
            int last = this.items.Count - 1;

            this.items[0] = this.items[last];
            this.items.RemoveAt(last);

            this.bubbleDown();

            return ret;
        }

        public bool IsEmpty()
        {
            return this.items.Count == 0;
        }
    }
}
