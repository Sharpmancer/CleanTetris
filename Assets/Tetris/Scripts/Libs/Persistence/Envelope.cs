using System;

namespace Libs.Persistence
{
    [Serializable]
    internal sealed class Envelope
    {
        public string Meta;
        public string Payload;
    }
}