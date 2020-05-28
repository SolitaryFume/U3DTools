using System.Collections.Generic;

namespace U3DTools
{
    public class PoolSingleton<T> : Pool<T>
        where T : class, new()
    {
        private static PoolSingleton<T> m_instance;
        public static PoolSingleton<T> Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new PoolSingleton<T>();
                }
                return m_instance;
            }
        }


        private PoolSingleton()
        {
        }

        private readonly Queue<T> m_pool = new Queue<T>();
    }
}