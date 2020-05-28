using System;

namespace U3DTools
{
    public static class Debug
    {
        private static ILog m_log;

        public static void Init(ILog log)
        {
            if(log==null)
                throw new ArgumentNullException(nameof(Log));
            
            if(m_log!=null)
                throw new Exception("");

            m_log = log;
        }

        public static void Log(object msg)   
        {
            m_log?.Log(msg);
        }

        public static void Log(string format,params object[] args)
        {
            m_log?.Log(format,args);
        }
    }
}