using System;
using System.Collections.Generic;
using Libs.Core;

namespace Libs.Bootstrap
{
    internal class RunnableContext : IInstallableContext
    {
        private readonly RunnableContext _parent;
        private readonly Dictionary<Type, object> _instances = new();
        private readonly HashSet<IInitializable> _initializables = new();
        private readonly HashSet<ITickable> _tickables = new();
        private readonly HashSet<IDisposable> _disposables = new();

        internal RunnableContext(RunnableContext parent = null) => 
            _parent = parent;

        internal void RunInstallers(IEnumerable<Installer> installers)
        {
            foreach (var installer in installers) 
                installer.Install(this);
        }

        public T Get<T>() where T : class =>  
            _instances.TryGetValue(typeof(T), out var instance) 
                ? (T)instance 
                : _parent?.Get<T>() ?? throw new KeyNotFoundException($"No binding for {typeof(T).Name} found");

        public void RegisterContract<TContract>(TContract instance) => 
            _instances.Add(typeof(TContract), instance);

        public void RegisterRunnable(object instance)
        {
            if(instance is IInitializable initializable)
                _initializables.Add(initializable);
            if(instance is ITickable tickable)
                _tickables.Add(tickable);
            if(instance is IDisposable disposable)
                _disposables.Add(disposable);
        }

        internal void RunInitializables()
        {
            foreach (var initializable in _initializables) 
                initializable.Initialize();
        }

        internal void RunTickables(float timeDelta)
        {
            foreach (var tickable in _tickables) 
                tickable.Tick(timeDelta);
        }

        internal void RunDisposables()
        {
            foreach (var disposable in _disposables) 
                disposable.Dispose();
        }
    }
}