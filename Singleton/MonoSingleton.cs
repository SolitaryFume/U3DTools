#if UNITY
using System;
using UnityEngine;
namespace U3DTools
{
    public abstract class MonoSingleton<T> : MonoBehaviour,IDisposable
         where T : MonoSingleton<T>
    {
        private static T m_instance;

        public static T Instance
        {
            get
            {
                if (m_instance==null)
                {
                    var obj = new GameObject(typeof(T).Name,typeof(T));
                }
                return m_instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_instance != null)
                return;
            m_instance = (T)this;
            DontDestroyOnLoad(this);
        }

        public void Dispose()
        {
            GameObject.Destroy(this);
            m_instance = null;
        }
    }
}

#endif