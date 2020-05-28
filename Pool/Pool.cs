using System.Collections.Generic;

namespace U3DTools
{
    public class Pool<T> : IPool<T>
        where T : class, new()
    {
        private readonly Queue<T> m_pool = new Queue<T>();

        public int Count => m_pool.Count;

        public T Fetch()
        {
            if (m_pool.Count == 0)
            {
                return new T();
            }
            return m_pool.Dequeue();
        }

        public void Recycle(T t)
        {
            m_pool.Enqueue(t);
        }

        public void Clear()
        {
            m_pool.Clear();
        }
    }
}