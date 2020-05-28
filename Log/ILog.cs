namespace U3DTools
{
    public interface ILog
    {
        void Log(object msg);
        void Log(string format,params object[] args);
        void LogWarning(object msg);
        void LogWarning(string format,params object[] args);
        void LogError(object msg);
        void LogError(string format,params object[] args);
    }
}