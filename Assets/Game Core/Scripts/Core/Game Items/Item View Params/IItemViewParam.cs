namespace GameCore.Items
{
    public interface IItemViewParam
    {
        
    }
    
    public interface IItemViewParam<T> : IItemViewParam
    {
        void SetValue(T value);
        T GetValue();
    }
}