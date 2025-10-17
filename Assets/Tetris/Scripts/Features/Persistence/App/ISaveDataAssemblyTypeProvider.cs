using System;

namespace Features.Persistence.App
{
    public interface ISaveDataAssemblyTypeProvider
    {
        Type DataAssemblyType { get; } 
    }
}