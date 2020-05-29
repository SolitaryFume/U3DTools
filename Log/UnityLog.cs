#if UNITY
namespace U3DTools
{
    public class UnityLog : ILog
    {
        public void Log(object msg)
        {
            UnityEngine.Debug.Log(msg);
        }

        public void LogFormat(string format, params object[] args)
        {
            UnityEngine.Debug.Log(string.Format(format,args));
        }

        public void LogWarning(object msg)
        {
            UnityEngine.Debug.LogWarning(msg);
        }

        public void LogWarningFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogWarning(string.Format(format,args));
        }

        public void LogError(object msg)
        {
            UnityEngine.Debug.LogError(msg);
        }

        public void LogErrorFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogError(string.Format(format,args));
        }

    }
}
#endif