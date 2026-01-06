public interface IPoolable
{
    void OnReturnedToPool();
    void OnTakenFromPool();
}
