namespace U3DTools
{
    public class UnityLog : ILog
    {
        public void Log(object msg)
        {
            UnityEngine.Debug.Log(msg);
        }

        public void Log(string format, params object[] args)
        {
            UnityEngine.Debug.Log(string.Format(format,args));
        }

        public void LogWarning(object msg)
        {
            UnityEngine.Debug.LogWarning(msg);
        }

        public void LogWarning(string format, params object[] args)
        {
            UnityEngine.Debug.LogWarning(string.Format(format,args));
        }

        public void LogError(object msg)
        {
            UnityEngine.Debug.LogError(msg);
        }

        public void LogError(string format, params object[] args)
        {
            UnityEngine.Debug.LogError(string.Format(format,args));
        }

    }
}