namespace Libs.Core.Lifecycle
{
    public interface IPreInitializable
    {
        int PreInitOrder { get; }
        void PreInitialize();
    }
}