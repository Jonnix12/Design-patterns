namespace MoonActive.SequenceSystem
{
    public interface ISequenceOperation<T>
    {
        void ExecuteTask(T data);
    }
}