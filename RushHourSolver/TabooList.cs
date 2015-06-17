using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace RushHourSolver
{
    class ConcurrentTabooList<T>
    {
        ConcurrentDictionary<T, byte> list;

        public ConcurrentTabooList()
        {
            this.list = new ConcurrentDictionary<T, byte>();
        }

        public bool TryAdd(T item)
        {
            return this.list.TryAdd(item, 0);
        }
    }
}
