using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHourSolver
{
    class PriorityQueue<T, P> where P : IComparable
    {
        Heap<QItem> heap;

        private class QItem : IComparable
        {
            public P Priority { get; set; }
            public T Content { get; set; }

            //CONSTRUCTORS
            public QItem(T content, P priority)
            {
                this.Priority = priority;
                this.Content = content;
            }

            //METHODS
            public int CompareTo(Object item)
            {
                QItem q = (QItem)item;

                return this.Priority.CompareTo(q.Priority);
            }
        }

        //CONSTRUCTORS
        public PriorityQueue(bool reversed)
        {
            this.heap = new Heap<QItem>(reversed);
        }

        //METHODS
        public void Enqueue(T item, P priority)
        {
            QItem qitem = new QItem(item, priority);

            this.heap.Insert(qitem);
        }

        public T Dequeue()
        {
            return this.heap.Get().Content;
        }

        public bool IsEmpty()
        {
            return this.heap.IsEmpty();
        }
    }
}
