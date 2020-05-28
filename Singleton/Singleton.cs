using System;
using System.Reflection;

namespace U3DTools
{
    public abstract class Singleton<T> where T : Singleton<T>, IDisposable
    {
        private static object m_lock = new object();
        public static T m_instance;
        public static T Instance
        {
            get
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                        var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                        if (ctor == null)
                        {
                            throw new Exception($"Non-Public Constructor() no found ! in {typeof(T)}");
                        }
                        m_instance = ctor.Invoke(null) as T;
                    }
                }
                return m_instance;
            }
        }

        public virtual void Disposable()
        {
            m_instance = null;
        }
    }
}