namespace U3DTools
{
    public interface ILog
    {
        void Log(object msg);
        void LogFormat(string format,params object[] args);
        void LogWarning(object msg);
        void LogWarningFormat(string format,params object[] args);
        void LogError(object msg);
        void LogErrorFormat(string format,params object[] args);
    }
}