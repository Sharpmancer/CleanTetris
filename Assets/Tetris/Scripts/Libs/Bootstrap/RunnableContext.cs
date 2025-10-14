using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Core;

namespace Libs.Bootstrap
{
    internal class RunnableContext : IInstallableContext
    {
        private readonly RunnableContext _parent;
        private readonly Dictionary<Type, object> _instances = new();
        private readonly List<IPreInitializable> _preInitializables = new();
        private readonly List<IInitializable> _initializables = new();
        private readonly List<ITickable> _tickables = new();
        private readonly List<IDisposable> _disposables = new();

        internal RunnableContext(RunnableContext parent = null) => 
            _parent = parent;

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
            if(instance is IPreInitializable preInitializable)
                _preInitializables.Add(preInitializable);
        }

        internal void RunInstallers(IEnumerable<Installer> installers)
        {
            foreach (var installer in installers) 
                installer.Install(this);
        }

        internal void RunPreInitializables()
        {
            foreach (var item in _preInitializables.OrderBy(n => n.Order)) 
                item.PreInitialize();
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

        // Disposes in reverse init order
        internal void RunDisposables()
        {
            for (var i = _disposables.Count - 1; i >= 0; --i) 
                _disposables[i].Dispose();
        }
    }
}