namespace Libs.Core
{
    public interface IPreInitializable
    {
        int Order { get; }
        void PreInitialize();
    }
}