namespace Libs.Bootstrap
{
    public interface IInstallableContext
    {
        T Get<T>() where T : class;
        void RegisterContract<TContract>(TContract instance);
        void RegisterRunnable(object instance);
    }
}