namespace Libs.Persistence
{
    public class StaticPayloadVersionProvider : IPayloadVersionProvider
    {
        public int Version { get; }

        public StaticPayloadVersionProvider(int version) => 
            Version = version;
    }
}