using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Core.Lifecycle;

namespace Libs.Bootstrap
{
    internal class LifecycleControlledObjects
    {
        private readonly List<IPreInitializable> _preInitializables = new();
        private readonly List<IInitializable> _initializables = new();
        private readonly List<ITickable> _tickables = new();
        private readonly List<IDisposable> _disposables = new();
        
        internal void Add(object instance)
        {
            if(instance is IPreInitializable preInitializable)
                _preInitializables.Add(preInitializable);
            if(instance is IInitializable initializable)
                _initializables.Add(initializable);
            if(instance is ITickable tickable)
                _tickables.Add(tickable);
            if(instance is IDisposable disposable)
                _disposables.Add(disposable);
        }

        internal void Initialize()
        {
            foreach (var initializable in _initializables) 
                initializable.Initialize();
        }

        internal void PreInitialize()
        {
            foreach (var initializable in _preInitializables.OrderBy(x => x.PreInitOrder)) 
                initializable.PreInitialize();
        }

        internal void Tick(float timeDelta)
        {
            foreach (var tickable in _tickables) 
                tickable.Tick(timeDelta);
        }

        internal void Dispose()
        {
            // disposing in reverse-init order
            for (var i = _disposables.Count - 1; i >= 0; --i) 
                _disposables[i].Dispose();
        }
    }
}