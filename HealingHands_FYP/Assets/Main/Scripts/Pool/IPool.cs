namespace HH.Pool
{
    public interface IPool<T>
    {
        void Prewarm(int num);

        T Request();

        void Return(T member);
    }
}
