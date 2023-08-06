namespace GameCore.UI.MainMenu.GameItems
{
    public interface IComplexGameItemView<in T> : IGameItemView
        where T : class
    {
        void Setup(T t);
    }
    
    public interface IComplexGameItemView<in T1, in T2> : IComplexGameItemView<T1>
        where T1 : class
        where T2 : struct
    {
        void Setup(T1 t1, T2 t2);
    }
}