namespace U3DTools
{
    public interface IPool<T>
        where T : class, new()
    {
        T Fetch();
        void Recycle(T t);
        void Clear();
        int Count{get;}
    }
}