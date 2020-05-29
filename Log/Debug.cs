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

        public static void LogFormat(string format,params object[] args)
        {
            m_log?.LogFormat(format,args);
        }

        public static void LogWarning(object msg){
            m_log?.LogWarning(msg);
        }

        public static void LogWarningFormat(string format,params object[] args)
        {
            m_log?.LogWarningFormat(format,args);
        }
        public static void LogError(object msg){
            m_log?.LogWarning(msg);
        }

        public static void LogErrorFormat(string format,params object[] args)
        {
            m_log?.LogWarningFormat(format,args);
        }
    }
}